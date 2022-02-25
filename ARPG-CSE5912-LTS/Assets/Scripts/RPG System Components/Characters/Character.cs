using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;


//The most fundamental level of defining a character. Encompasses Player character, NPCs, and enemies.
public abstract class Character : MonoBehaviour
{
    [HideInInspector] public NavMeshAgent agent;

    public List<Ability> abilitiesKnown;
    public List<Character> charactersInRange;

    public GameplayStateController gameplayStateController;
    PlayerAbilityController playerAbilityController;
    [HideInInspector] public bool abilityQueued = false;

    [HideInInspector] public Stats stats;

    [HideInInspector] public float smooth;
    [HideInInspector] public float yVelocity;
    public virtual Transform AttackTarget { get; set; }
    public virtual NPC NPCTarget { get; set; }
    [HideInInspector] public float NPCInteractionRange;

    public static event EventHandler<InfoEventArgs<AbilityCast>> AgentMadeItWithinRangeToPerformAbilityWithoutCancelingEvent;
    public static event EventHandler<InfoEventArgs<AbilityCast>> AbilityIsReadyToBeCastEvent;

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
        smooth = 0.3f;
        yVelocity = 0.0f;
        AttackTarget = null;
    }

    protected virtual void Update()
    {

    }

    private void OnEnable()
    {
        AgentMadeItWithinRangeToPerformAbilityWithoutCancelingEvent += OnAgentMadeItWithinRangeWithoutCanceling;
        PlayerAbilityController.PlayerSelectedGroundTargetLocationEvent += OnGroundTargetSelected;
        PlayerAbilityController.PlayerSelectedSingleTargetEvent += OnSingleTargetSelected;
        //EnemyAbilityControlller.EnemySelectedGroundTargetLocationEvent += OnGroundTargetSelected;
        //EnemyAbilityController.EnemySelectedSingleTargetEvent += OnSingleTargetSelected;
    }
    #endregion
    #region Events
    void OnAgentMadeItWithinRangeWithoutCanceling(object sender, InfoEventArgs<AbilityCast> e)
    {
        abilityQueued = false;
        AbilityIsReadyToBeCastEvent?.Invoke(this, new InfoEventArgs<AbilityCast>(e.info));
    }

    void OnGroundTargetSelected(object sender, InfoEventArgs<AbilityCast> e)
    {
        float distFromCaster = Vector3.Distance(e.info.hit.point, e.info.caster.transform.position);
        float distToTravel = distFromCaster - e.info.abilityRange.range;
        if (distFromCaster > e.info.abilityRange.range)
        {
            Debug.Log("Too far away");
            abilityQueued = true;
            StartCoroutine(RunWithinRange(e.info, distToTravel));
        }
        else
        {
            AbilityIsReadyToBeCastEvent?.Invoke(this, new InfoEventArgs<AbilityCast>(e.info));
        }
    }

    void OnSingleTargetSelected(object sender, InfoEventArgs<AbilityCast> e)
    {
        //abilityQueued = true;
        //do a coroutine for running within range of enemy
    }

    #endregion
    #region Public Functions
    //Put any code here that should be shared functionality across every type of character
    public void QueueAbilityCast(Ability abilityToCast)
    {
        //charactersInRange.Clear();
        AbilityCast abilityCast = new AbilityCast(abilityToCast);
        abilityCast.caster = this;
        bool abilityCanBePerformed = abilityCast.abilityCost.CheckCharacterHasResourceCostForCastingAbility(this);

        if (abilityCanBePerformed)
        {
            //charactersInRange = range.GetCharactersInRange();

            if (abilityCast.abilityRequiresCursorSelection)
            {
                if (this is Player)
                {
                    Player player = (Player)this;
                    player.StopAllCoroutines();
                    gameplayStateController.aoeReticleCylinder.SetActive(false);
                    playerAbilityController.playerInSingleTargetAbilitySelectionMode = false;
                    playerAbilityController.playerInAOEAbilityTargetSelectionMode = false;
                    playerAbilityController.PlayerQueueAbilityCastSelectionRequired(abilityCast);
                }
                else
                {
                    //Enemy enemy = (Enemy)this;
                    //enemy.EnemyCastAbilitySelectionRequired(abilityToCast, requiresCharacter);

                    //if it's an enemy, do AI stuff to select the target of the ability. Do all of this from within the enemy class:
                    //1a) select target player character if it's a character-targeting ability
                    //1b) select a point on the terrain that is centered on the player character otherwise
                    //2) call abilityArea.PerformAOE from from within the Enemy class
                }
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

    private void CastAbilityWithoutSelection(AbilityCast abilityCast)
    {
        AbilityIsReadyToBeCastEvent?.Invoke(this, new InfoEventArgs<AbilityCast>(abilityCast));
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
    #endregion
    #region Private Functions
    public void DeductCastingCost(AbilityCast abilityCast)
    {
        abilityCast.abilityCost.DeductResourceFromCaster(abilityCast.caster);
    }

    public void GetColliders(AbilityCast abilityCast)
    {
        BaseAbilityArea abilityArea = abilityCast.ability.GetComponent<BaseAbilityArea>();
        List<Character> charactersCollided = abilityArea.PerformAOECheckToGetColliders(abilityCast);
        if (abilityCast.abilityDoesNotTargetCaster)
            charactersCollided.Remove(abilityCast.caster);
        ApplyAbilityEffects(charactersCollided, abilityCast.ability);
    }

    void ApplyAbilityEffects(List<Character> targets, Ability ability)
    {
        //TODO: Check if the ability effect should be applied to the caster or not and/or should be applied to enemies or not
        BaseAbilityEffect[] effects = ability.GetComponentsInChildren<BaseAbilityEffect>();
        for (int i = 0; i < targets.Count; i++)
        {
            for (int j = 0; j < effects.Length; j++)
            {
                BaseAbilityEffect effect = effects[j];
                AbilityEffectTarget specialTargeter = effect.GetComponent<AbilityEffectTarget>();
                if (specialTargeter.IsTarget(targets[i]))
                {
                    //Debug.Log("Applying ability effects to " + targets[i].name);
                    effect.Apply(targets[i]);
                }
            }
        }
    }
    #endregion
    #region Enumerators
    public IEnumerator RunWithinRange(AbilityCast abilityCast, float distToTravel)
    {
        //Debug.Log("Running to within range of point.");
        Vector3 dir = abilityCast.hit.point - abilityCast.caster.transform.position;
        Vector3 normalizedDir = dir.normalized;
        Vector3 endPoint = abilityCast.caster.transform.position + (normalizedDir * (distToTravel + 0.1f));

        while (abilityQueued)
        {
            agent.destination = endPoint;
            float distFromPlayer = Vector3.Distance(abilityCast.hit.point, abilityCast.caster.transform.position);
            if (distFromPlayer <= abilityCast.abilityRange.range)
            {
                AgentMadeItWithinRangeToPerformAbilityWithoutCancelingEvent?.Invoke(this, new InfoEventArgs<AbilityCast>(abilityCast));
            }

            yield return null;
        }
    }
    #endregion

}
