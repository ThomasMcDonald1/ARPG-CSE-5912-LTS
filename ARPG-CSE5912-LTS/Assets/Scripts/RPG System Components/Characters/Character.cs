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

    private void Awake()
    {
        abilitiesKnown = new List<Ability>();
        charactersInRange = new List<Character>();
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
                gameplayStateController.aoeReticleCylinder.SetActive(false);
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
}
