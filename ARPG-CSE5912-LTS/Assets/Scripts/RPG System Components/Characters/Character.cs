using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using ARPG.Combat;
using UnityEngine.InputSystem;

//The most fundamental level of defining a character. Encompasses Player character, NPCs, and enemies.
public abstract class Character : MonoBehaviour
{
    [HideInInspector] public NavMeshAgent agent;
    public List<Ability> abilitiesKnown;
    public List<Character> charactersInRange;

    public GameplayStateController gameplayStateController;
    PlayerAbilityController playerAbilityController;
    EnemyAbilityController enemyAbilityController;
    [HideInInspector] public bool abilityQueued = false;
    [HideInInspector] public Stats stats;

    [HideInInspector] public float smooth;
    [HideInInspector] public float yVelocity;
    public virtual Transform AttackTarget { get; set; }
    public virtual NPC NPCTarget { get; set; }
    [HideInInspector] public float NPCInteractionRange;

    public static event EventHandler<InfoEventArgs<AbilityCast>> AbilityIsReadyToBeCastEvent;
    public abstract Type GetCharacterType();
    public Ability basicAttackAbility;
    [HideInInspector] public float baseRunSpeed = 7f;
    private AudioManager audioManager;

    #region Built-in
    private void Awake()
    {
        NPCTarget = null;
        NPCInteractionRange = 2.0f;
        abilitiesKnown = new List<Ability>();
        charactersInRange = new List<Character>();

    }

    protected virtual void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        stats = GetComponent<Stats>();
        playerAbilityController = GetComponent<PlayerAbilityController>();
        enemyAbilityController = GetComponent<EnemyAbilityController>();
        gameplayStateController = GameObject.FindObjectOfType<GameplayStateController>();
        basicAttackAbility = gameplayStateController.GetComponentInChildren<BasicAttackDamageAbilityEffect>().GetComponentInParent<Ability>();
        smooth = 0.3f;
        yVelocity = 0.0f;
        BaseAbilityMovement.SpecialMovementCompletedEvent += OnSpecialMovementCompleted;
        CastTimerCastType.AbilityCastWasCancelledEvent += OnAbilityCastCancelled;
        audioManager = FindObjectOfType<AudioManager>();
    }

    protected virtual void Update()
    {
        //Debug.Log(abilitiesKnown);
    }

    #endregion
    #region Events

    void OnAgentMadeItWithinRangeWithoutCanceling(AbilityCast abilityCast)
    {
        Debug.Log("Agent made it to within range without cancelling. Ability should cast now.");
        abilityQueued = false;
        AbilityIsReadyToBeCastEvent?.Invoke(this, new InfoEventArgs<AbilityCast>(abilityCast));
    }

    protected void OnGroundTargetSelected(AbilityCast abilityCast)
    {
        float distFromCaster = Vector3.Distance(abilityCast.hit.point, abilityCast.caster.transform.position);
        float distToTravel = distFromCaster - abilityCast.abilityRange.range;
        //Debug.Log(abilityCast.caster + " at " + abilityCast.caster.transform.position + " clicked on " + abilityCast.hit.point);

        if (distFromCaster > abilityCast.abilityRange.range)
        {
            abilityQueued = true;
            StartCoroutine(RunWithinRange(abilityCast, distToTravel));
        }
        else
        {
            //Debug.Log("Ground target selected. Firing ability ready to be cast event");
            AbilityIsReadyToBeCastEvent?.Invoke(this, new InfoEventArgs<AbilityCast>(abilityCast));
        }
    }

    protected void OnSingleTargetSelected(AbilityCast abilityCast)
    {
        float distFromCaster = Vector3.Distance(abilityCast.hit.collider.transform.position, abilityCast.caster.transform.position);
        float distToTravel = distFromCaster - abilityCast.abilityRange.range;
        if (distFromCaster > abilityCast.abilityRange.range)
        {
            abilityQueued = true;
            StartCoroutine(RunWithinRangeCharacter(abilityCast, distToTravel, abilityCast.hit.collider.GetComponent<Character>()));
        }
        else
        {
            AbilityIsReadyToBeCastEvent?.Invoke(this, new InfoEventArgs<AbilityCast>(abilityCast));
        }
    }

    private void OnSpecialMovementCompleted(object sender, InfoEventArgs<(AbilityCast, List<Character>)> e)
    {
        if (e.info.Item1.caster == this)
        {
            Debug.Log("Special movement was completed");
            ApplyAbilityEffects(e.info.Item2, e.info.Item1);
        }
    }

    private void OnAbilityCastCancelled(object sender, InfoEventArgs<int> e)
    {
        if (agent != null)
            agent.isStopped = false;
    }

    #endregion
    #region Public Functions
    //Put any code here that should be shared functionality across every type of character
    public void QueueAbilityCast(Ability abilityToCast)
    {
        //Debug.Log("Queueing ability cast");
        //charactersInRange.Clear();
        AbilityCast abilityCast = new AbilityCast(abilityToCast);
        abilityCast.caster = this;
        abilityCast.abilityCooldown.GetReducedCooldown(abilityCast);
        abilityCast.castType.GetReducedCastTime(abilityCast);
        if (this is Player)
        {
            bool abilityCanBePerformed = abilityCast.abilityCost.CheckCharacterHasResourceCostForCastingAbility(this);
            if (abilityCanBePerformed)
            {
                if (abilityCast.abilityRequiresCursorSelection)
                {
                    Player player = (Player)this;
                    player.StopAllCoroutines();
                    gameplayStateController.aoeReticleCylinder.SetActive(false);
                    playerAbilityController.playerInSingleTargetAbilitySelectionMode = false;
                    playerAbilityController.playerInAOEAbilityTargetSelectionMode = false;
                    //Debug.Log(this + " is casting " + abilityCast.ability + "!");
                    playerAbilityController.PlayerQueueAbilityCastSelectionRequired(abilityCast);
                }
                else
                {
                    CastAbilityWithoutSelection(abilityCast);
                }
            }
            else
            {
                //TODO: Indicate to the player they're pressing a button that can't be used somehow (subtle sound? flash the ActionButton red?)
            }
        }
        else
        {
            bool abilityCanBePerformed = abilityCast.abilityCost.CheckCharacterHasResourceCostForCastingAbility(this);
            if (abilityCanBePerformed)
            {
                Enemy enemy = (Enemy)this;
                enemy.StopAllCoroutines();
                enemyAbilityController.EnemyQueueAbilityCastSelectionRequired(abilityCast);
            }
            //enemy.EnemyCastAbilitySelectionRequired(abilityToCast, requiresCharacter);

            //if it's an enemy, do AI stuff to select the target of the ability. Do all of this from within the enemy class:
            //1a) select target character if it's a character-targeting ability
            //1b) select a point on the terrain that is centered on the player character or some enemies otherwise
        }
    }

    public void QueueBasicAttack(Ability abilityToCast, Character target, Character caster)
    {
        //Debug.Log("target is " + target);
        AbilityCast abilityCast = new AbilityCast(abilityToCast);
        abilityCast.caster = caster;
        abilityCast.basicAttackTarget = target;
        //Debug.Log("Basic attack target stored in QueueBasicAttack: " + abilityCast.basicAttackTarget);
        CastAbilityWithoutSelection(abilityCast);
    }

    //Could be useful for AI to find characters in range
    public virtual bool CheckCharacterInRange(Character character)
    {
        foreach (Character chara in charactersInRange)
        {
            if (chara == character)
            {
                return true;
            }
        }
        return false;
    }

    protected void PerformSpecialAbilityMovement(AbilityCast abilityCast, List<Character> targets)
    {
        Debug.Log("Queueing special ability movement");
        abilityCast.abilityMovement.QueueSpecialMovement(abilityCast, targets);
    }

    protected void DeductCastingCost(AbilityCast abilityCast)
    {
        //Debug.Log("Deducting casting cost");
        abilityCast.abilityCost.DeductResourceFromCaster(abilityCast.caster);
    }

    protected void GetColliders(AbilityCast abilityCast)
    {
        if (this == abilityCast.caster)
        {
            Debug.Log("Getting colliders");
            List<Character> charactersCollided = abilityCast.abilityArea.PerformAOECheckToGetColliders(abilityCast);
            if (abilityCast.abilityMovement != null)
            {
                Debug.Log("There's special ability movement for this ability.");
                PerformSpecialAbilityMovement(abilityCast, charactersCollided);
            }
            else
            {
                ApplyAbilityEffects(charactersCollided, abilityCast);
            }
        }
    }

    public void ApplyAbilityEffects(List<Character> targets, AbilityCast abilityCast)
    {
        //Debug.Log("Applying ability effects");
        //TODO: Check if the ability effect should be applied to the caster or not and/or should be applied to enemies or not
        BaseAbilityEffectElement x;
        BaseAbilityEffect[] effects = abilityCast.ability.GetComponentsInChildren<BaseAbilityEffect>();
        if (targets.Count == 0)
        {
            for (int j = 0; j < effects.Length; j++)
            {
                BaseAbilityEffect effect = effects[j];
                if (effect.effectVFXObj != null)
                {
                    Debug.Log("Instantiating VFX");
                    x = effect.GetComponent<BaseAbilityEffectElement>();

                    if (x is ColdAbilityEffectElement)
                    {
                        audioManager.Play("ColdAbility");
                    }
                    if (x is FireAbilityEffectElement)
                    {
                        audioManager.Play("FireAbility");
                    }
                    if (x is LightningAbilityEffectElement)
                    {
                        audioManager.Play("LightningAbility");
                    }

                    effect.InstantiateEffectVFX(abilityCast, null);
                }
            }
        }
        else
        {
            for (int i = 0; i < targets.Count; i++)
            {
                for (int j = 0; j < effects.Length; j++)
                {
                    BaseAbilityEffect effect = effects[j];
                    if (effect.effectVFXObj != null)
                    {
                        Debug.Log("Instantiating VFX");

                        x = effect.GetComponent<BaseAbilityEffectElement>();
                        if (x is ColdAbilityEffectElement)
                        {
                            audioManager.Play("ColdAbility");
                        }
                        if (x is FireAbilityEffectElement)
                        {
                            audioManager.Play("FireAbility");
                        }
                        if (x is LightningAbilityEffectElement)
                        {
                            audioManager.Play("LightningAbility");
                        }

                        effect.InstantiateEffectVFX(abilityCast, targets[i]);
                    }

                    AbilityEffectTarget specialTargeter = effect.GetComponent<AbilityEffectTarget>();
                    if (specialTargeter.IsTarget(targets[i], abilityCast.caster))
                    {
                        Debug.Log(specialTargeter);
                        Debug.Log("Applying ability effects to " + targets[i].name);
                        effect.Apply(targets[i], abilityCast);
                        x = effect.GetComponent<BaseAbilityEffectElement>();
                        if (x == null)
                        {
                            audioManager.Play("PhysicalAbility");
                        }
                    }
                }
            }
        }
    }
    #endregion
    #region Private Functions

    private void CastAbilityWithoutSelection(AbilityCast abilityCast)
    {
        AbilityIsReadyToBeCastEvent?.Invoke(this, new InfoEventArgs<AbilityCast>(abilityCast));
    }
    #endregion
    #region Enumerators
    public IEnumerator RunWithinRange(AbilityCast abilityCast, float distToTravel)
    {
        Debug.Log("Running to within range of point.");
        Vector3 dir = (abilityCast.hit.point - abilityCast.caster.transform.position).normalized;
        Vector3 endPoint = abilityCast.caster.transform.position + (dir * (distToTravel + 0.1f));

        while (abilityQueued)
        {
            if (abilityCast.caster.agent.enabled == true)
            {
                abilityCast.caster.agent.destination = endPoint;
            }
            float distFromPlayer = Vector3.Distance(abilityCast.hit.point, abilityCast.caster.transform.position);
            if (distFromPlayer <= abilityCast.abilityRange.range)
            {
                OnAgentMadeItWithinRangeWithoutCanceling(abilityCast);
            }


            yield return null;
        }
    }

    public IEnumerator RunWithinRangeCharacter(AbilityCast abilityCast, float distToTravel, Character target)
    {
        Debug.Log("Running within range of character");
        Vector3 dir = (target.transform.position - abilityCast.caster.transform.position).normalized;
        Vector3 endPoint = abilityCast.caster.transform.position + (dir * (distToTravel + 0.1f));
        while (abilityQueued)
        {
            abilityCast.caster.agent.destination = endPoint;
            float distFromPlayer = Vector3.Distance(target.transform.position, abilityCast.caster.transform.position);
            if (distFromPlayer <= abilityCast.abilityRange.range)
            {
                OnAgentMadeItWithinRangeWithoutCanceling(abilityCast);
            }
            yield return null;
        }
    }
    #endregion

}
