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

    public Stats stats;

    public float smooth;
    public float yVelocity;
    public virtual Transform AttackTarget { get; set; }
    public virtual NPC NPCTarget { get; set; }
    public float NPCInteractionRange;

    private void Awake()
    {
        NPCTarget = null;
        NPCInteractionRange = 2.0f;
        abilitiesKnown = new List<Ability>();
        charactersInRange = new List<Character>();
    }

    protected virtual void Start()
    {
        stats = GetComponent<Stats>();
        playerAbilityController = GetComponent<PlayerAbilityController>();
        smooth = 0.3f;
        yVelocity = 0.0f;
        AttackTarget = null;
    }

    protected virtual void Update()
    {

    }

    //Put any code here that should be shared functionality across every type of character
    public void QueueAbilityCast(Ability abilityToCast)
    {
        //charactersInRange.Clear();
        BaseAbilityCost abilityCost = abilityToCast.GetComponent<BaseAbilityCost>();
        bool abilityCanBePerformed = abilityCost.CheckCharacterHasResourceCostForCastingAbility(this);

        if (abilityCanBePerformed)
        {
            bool requiresCharacter = CheckForCharacterRequiredUnderCursor(abilityToCast);
            bool selectionRequired = CheckForSelectionRequirement(abilityToCast);
            //BaseAbilityRange range = abilityToCast.GetComponent<BaseAbilityRange>();
            //BaseAbilityArea abilityArea = abilityToCast.GetComponent<BaseAbilityArea>();
            //charactersInRange = range.GetCharactersInRange();

            if (selectionRequired)
            {
                if (this is Player)
                {
                    Player player = (Player)this;
                    player.StopAllCoroutines();
                    gameplayStateController.aoeReticleCylinder.SetActive(false);
                    playerAbilityController.playerInSingleTargetAbilitySelectionMode = false;
                    playerAbilityController.playerInAOEAbilityTargetSelectionMode = false;
                    playerAbilityController.PlayerQueueAbilityCastSelectionRequired(abilityToCast, requiresCharacter);
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
        else
        {
            //TODO: Indicate to the player they're pressing a button that can't be used somehow (subtle sound? flash the ActionButton red?)
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
}
