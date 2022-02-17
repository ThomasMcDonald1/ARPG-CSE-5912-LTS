using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using ARPG.Movement;
using System;

public class PlayerAbilityController : Player
{
    public static event EventHandler<InfoEventArgs<(RaycastHit, Ability)>> AgentMadeItWithinRangeToPerformAbilityWithoutCancelingEvent;
    public static event EventHandler<InfoEventArgs<(RaycastHit, Ability)>> PlayerSelectedGroundTargetLocationEvent;
    public static event EventHandler<InfoEventArgs<(RaycastHit, Ability)>> PlayerSelectedSingleTargetEvent;
    public static event EventHandler<InfoEventArgs<(Ability, RaycastHit, Character)>> AbilityIsReadyToBeCastEvent;

    Coroutine aoeAbilitySelectionMode;
    Coroutine singleTargetSelectionMode;
    [HideInInspector] public bool playerInAOEAbilityTargetSelectionMode;
    [HideInInspector] public bool playerInSingleTargetAbilitySelectionMode;
    [HideInInspector] public bool playerNeedsToReleaseMouseButton;

    [SerializeField] Ability basicAttack;
    [SerializeField] Ability fireballTest;
    [SerializeField] Ability bowAbilityTest;

    Player player;

    int groundLayerMask = 1 << 6;


    private void Awake()
    {
        player = GetComponent<Player>();
        abilitiesKnown.Add(basicAttack);
        abilitiesKnown.Add(fireballTest);
        abilitiesKnown.Add(bowAbilityTest);
    }

    private void OnEnable()
    {
        InputController.CancelPressedEvent += OnCancelPressed;
        AgentMadeItWithinRangeToPerformAbilityWithoutCancelingEvent += OnAgentMadeItWithinRangeWithoutCanceling;
        PlayerSelectedGroundTargetLocationEvent += OnPlayerSelectedGroundTargetLocation;
        PlayerSelectedSingleTargetEvent += OnPlayerSelectedSingleTarget;
        CastTimerCastType.AbilityCastTimeWasCompletedEvent += OnCompletedCastTimerCast;
    }

    private void OnCancelPressed(object sender, InfoEventArgs<int> e)
    {
        playerInAOEAbilityTargetSelectionMode = false;
        playerInSingleTargetAbilitySelectionMode = false;
        if (singleTargetSelectionMode != null)
            StopCoroutine(singleTargetSelectionMode);
        if (aoeAbilitySelectionMode != null)
            StopCoroutine(aoeAbilitySelectionMode);
        cursorChanger.ChangeCursorToDefaultGraphic();
        gameplayStateController.aoeReticleCylinder.SetActive(false);
    }

    void OnAgentMadeItWithinRangeWithoutCanceling(object sender, InfoEventArgs<(RaycastHit, Ability)> e)
    {
        BaseAbilityArea abilityArea = e.info.Item2.GetComponent<BaseAbilityArea>();
        abilityQueued = false;
        AbilityIsReadyToBeCastEvent?.Invoke(this, new InfoEventArgs<(Ability, RaycastHit, Character)>((e.info.Item2, e.info.Item1, player)));
    }

    void OnPlayerSelectedGroundTargetLocation(object sender, InfoEventArgs<(RaycastHit, Ability)> e)
    {
        BaseAbilityArea abilityArea = e.info.Item2.GetComponent<BaseAbilityArea>();
        BaseAbilityRange abilityRange = e.info.Item2.GetComponent<BaseAbilityRange>();
        //TODO: make player run to max range of the ability
        float distFromCharacter = Vector3.Distance(e.info.Item1.point, transform.position);
        float distToTravel = distFromCharacter - abilityRange.range;
        if (GetComponent<Player>() != null && distFromCharacter > abilityRange.range)
        {
            Debug.Log("Too far away");
            abilityQueued = true;
            StartCoroutine(RunWithinRange(e.info.Item1, abilityRange.range, distToTravel, e.info.Item2));
        }
        else if (GetComponent<Player>() != null)
        {
            AbilityIsReadyToBeCastEvent?.Invoke(this, new InfoEventArgs<(Ability, RaycastHit, Character)>((e.info.Item2, e.info.Item1, player)));
        }
    }

    void OnPlayerSelectedSingleTarget(object sender, InfoEventArgs<(RaycastHit, Ability)> e)
    {
        //abilityQueued = true;
        //do a coroutine for running within range of enemy
    }

    void OnCompletedCastTimerCast(object sender, InfoEventArgs<(Ability, RaycastHit, Character)> e)
    {
        Debug.Log("Cast timer cast completed");
        DeductCastingCost(e.info.Item1);
        GetColliders(e.info.Item2, e.info.Item1, player);
    }

    public void PlayerQueueAbilityCastSelectionRequired(Ability ability, bool requiresCharacter)
    {
        cursorChanger.ChangeCursorToSelectionGraphic();

        if (requiresCharacter)
        {
            playerInAOEAbilityTargetSelectionMode = true;
            singleTargetSelectionMode = StartCoroutine(WaitForPlayerClick(ability));
        }
        else
        {
            playerInAOEAbilityTargetSelectionMode = true;
            aoeAbilitySelectionMode = StartCoroutine(WaitForPlayerClickAOE(ability));
        }
    }

    void GetColliders(RaycastHit hit, Ability ability, Character caster)
    {
        BaseAbilityArea abilityArea = ability.GetComponent<BaseAbilityArea>();
        List<Character> charactersCollided = abilityArea.PerformAOECheckToGetColliders(hit, caster);
        ApplyAbilityEffects(charactersCollided, ability);
    }

    void DeductCastingCost(Ability ability)
    {
        BaseAbilityCost cost = ability.GetComponent<BaseAbilityCost>();
        cost.DeductResourceFromCaster(player);
    }

    void ApplyAbilityEffects(List<Character> targets, Ability ability)
    {
        BaseAbilityEffect[] effects = ability.GetComponentsInChildren<BaseAbilityEffect>();
        for (int i = 0; i < targets.Count; i++)
        {
            for (int j = 0; j < effects.Length; j++)
            {
                BaseAbilityEffect effect = effects[j];
                AbilityEffectTarget specialTargeter = effect.GetComponent<AbilityEffectTarget>();
                if (specialTargeter.IsTarget(targets[i]))
                {
                    Debug.Log("Applying ability effects to " + targets[i].name);
                    effect.Apply(targets[i]);
                }
            }
        }
    }

    private IEnumerator RunWithinRange(RaycastHit hit, float range, float distToTravel, Ability ability)
    {
        Debug.Log("Running to within range of point.");
        BaseAbilityArea abilityArea = ability.GetComponent<BaseAbilityArea>();
        Vector3 dir = hit.point - transform.position;
        Vector3 normalizedDir = dir.normalized;
        Vector3 endPoint = transform.position + (normalizedDir * (distToTravel + 0.1f));

        while (abilityQueued)
        {
            agent.destination = endPoint;
            float distFromPlayer = Vector3.Distance(hit.point, transform.position);
            if (distFromPlayer <= range)
            {
                AgentMadeItWithinRangeToPerformAbilityWithoutCancelingEvent?.Invoke(this, new InfoEventArgs<(RaycastHit, Ability)>((hit, ability)));
            }

            yield return null;
        }
    }

    private IEnumerator WaitForPlayerClick(Ability ability)
    {
        playerInSingleTargetAbilitySelectionMode = true;
        while (playerInSingleTargetAbilitySelectionMode)
        {
            Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                GameObject go = hit.collider.gameObject;
                Character target = go.GetComponent<Character>();
                //TODO: If ability requires enemy or ally to be clicked, excluding the other, check that first
                if (target != null && Mouse.current.leftButton.wasReleasedThisFrame)
                {
                    playerInSingleTargetAbilitySelectionMode = false;
                    cursorChanger.ChangeCursorToDefaultGraphic();
                    Debug.Log("Clicked on: " + target.name + " as ability selection target.");
                    bool targetInRange = CheckCharacterInRange(target);
                    if (!targetInRange)
                    {
                        PlayerSelectedSingleTargetEvent?.Invoke(this, new InfoEventArgs<(RaycastHit, Ability)>((hit, ability)));
                    }
                }
            }

            yield return null;
        }
    }

    private IEnumerator WaitForPlayerClickAOE(Ability ability)
    {
        BaseAbilityArea abilityArea = ability.GetComponent<BaseAbilityArea>();
        BaseAbilityRange abilityRange = ability.GetComponent<BaseAbilityRange>();
        bool playerHasNotClicked = true;

        while (playerHasNotClicked)
        {
            if (abilityArea != null)
            {
                abilityArea.DisplayAOEArea();
            }
            if (!playerNeedsToReleaseMouseButton && Mouse.current.leftButton.wasReleasedThisFrame)
            {
                playerHasNotClicked = false;
                cursorChanger.ChangeCursorToDefaultGraphic();
                if (abilityArea != null)
                {
                    Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
                    RaycastHit hit;

                    if (Physics.Raycast(ray, out hit, groundLayerMask))
                    {
                        PlayerSelectedGroundTargetLocationEvent?.Invoke(this, new InfoEventArgs<(RaycastHit, Ability)>((hit, ability)));
                    }
                    abilityArea.abilityAreaNeedsShown = false;
                    gameplayStateController.aoeReticleCylinder.SetActive(false);
                }
            }
            //TODO: Check for other input that would stop this current ability cast, like queueing up a different ability instead, or pressing escape
            yield return null;
        }
    }
}
