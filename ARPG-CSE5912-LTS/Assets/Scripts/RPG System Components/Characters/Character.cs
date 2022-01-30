using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

//The most fundamental level of defining a character. Encompasses Player character, NPCs, and enemies.
public abstract class Character : MonoBehaviour
{
    public List<Ability> abilitiesKnown;
    public List<Character> charactersInRange;    

    private void Awake()
    {
        abilitiesKnown = new List<Ability>();
        charactersInRange = new List<Character>();
    }

    //Put any code here that should be shared functionality across every type of character
    public void CastAbility(Ability abilityToCast)
    {
        charactersInRange.Clear();

        bool selectionRequired = CheckForSelectionRequirement(abilityToCast);
        BaseAbilityRange range = abilityToCast.GetComponent<BaseAbilityRange>();
        charactersInRange = range.GetCharactersInRange();

        if (selectionRequired)
        {
            if (this is Player)
            {
                Player player = (Player)this;
                player.PlayerCastAbility(abilityToCast);
            }
            else 
            {
                //if it's an enemy, do AI stuff to select the target of the ability
            }

        }
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
