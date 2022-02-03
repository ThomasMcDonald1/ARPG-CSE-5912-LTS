using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

//The most fundamental level of defining a character. Encompasses Player character, NPCs, and enemies.
public abstract class Character : MonoBehaviour
{
    public NavMeshAgent agent;

    public List<Ability> abilitiesKnown;
    public List<Character> charactersInRange;
    public GameplayStateController gameplayStateController;
    public bool abilityQueued = false;

    public static event EventHandler<InfoEventArgs<(RaycastHit, Ability)>> AgentMadeItWithinRangeToPerformAbilityWithoutCancelingEvent;
    public static event EventHandler<InfoEventArgs<Ability>> AbilityIsReadyToBeCastEvent;

    private void Awake()
    {
        abilitiesKnown = new List<Ability>();
        charactersInRange = new List<Character>();
    }

    private void OnEnable()
    {
        Player.PlayerSelectedGroundTargetLocationEvent += OnPlayerSelectedGroundTargetLocation;
        Player.PlayerSelectedSingleTargetEvent += OnPlayerSelectedSingleTarget;
        AgentMadeItWithinRangeToPerformAbilityWithoutCancelingEvent += OnAgentMadeItWithinRangeWithoutCanceling;
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
            AbilityIsReadyToBeCastEvent?.Invoke(this, new InfoEventArgs<Ability>(e.info.Item2));
            abilityArea.PerformAOE(e.info.Item1);
        }
    }

    void OnPlayerSelectedSingleTarget(object sender, InfoEventArgs<(RaycastHit, Ability)> e)
    {
        //abilityQueued = true;
        //do a coroutine for running within range of enemy
    }

    void OnAgentMadeItWithinRangeWithoutCanceling(object sender, InfoEventArgs<(RaycastHit, Ability)> e)
    {
        BaseAbilityArea abilityArea = e.info.Item2.GetComponent<BaseAbilityArea>();
        abilityQueued = false;
        AbilityIsReadyToBeCastEvent?.Invoke(this, new InfoEventArgs<Ability>(e.info.Item2));
        abilityArea.PerformAOE(e.info.Item1);
    }

    //Put any code here that should be shared functionality across every type of character
    public void QueueAbilityCast(Ability abilityToCast)
    {
        charactersInRange.Clear();

        bool requiresCharacter = CheckForCharacterRequiredUnderCursor(abilityToCast);
        bool selectionRequired = CheckForSelectionRequirement(abilityToCast);
        BaseAbilityRange range = abilityToCast.GetComponent<BaseAbilityRange>();
        BaseAbilityArea abilityArea = abilityToCast.GetComponent<BaseAbilityArea>();
        charactersInRange = range.GetCharactersInRange();

        if (selectionRequired)
        {
            if (this is Player)
            {
                Player player = (Player)this;
                player.StopAllCoroutines();
                player.playerInSingleTargetAbilitySelectionMode = false;
                player.playerInAOEAbilityTargetSelectionMode = false;
                player.PlayerQueueAbilityCastSelectionRequired(abilityToCast, requiresCharacter);
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
    }

    /// <summary>
    /// Returns true if the ability requires that a specific character be selected (targeted). Returns false if it's ground-targeted, self-targeted, etc.
    /// </summary>
    bool CheckForCharacterRequiredUnderCursor(Ability abilityToCast)
    {
        BaseAbilityConditional[] conditionals = abilityToCast.GetComponentsInChildren<BaseAbilityConditional>();
        foreach (BaseAbilityConditional conditional in conditionals)
        {
            if (conditional is AbilityRequiresCharacterUnderCursor)
                return true;
        }
        return false;
    }

    /// <summary>
    /// Returns true if ability requires cursor selection. Returns false if ability bypasses cursor selection.
    /// </summary>
    bool CheckForSelectionRequirement(Ability abilityToCast)
    {
        BaseAbilityConditional[] conditionals = abilityToCast.GetComponentsInChildren<BaseAbilityConditional>();
        foreach (BaseAbilityConditional conditional in conditionals)
        {
            if (conditional is AbilityRequiresCursorSelection)
            {
                return true;
            }
        }
        return false;
    }

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
    
}
