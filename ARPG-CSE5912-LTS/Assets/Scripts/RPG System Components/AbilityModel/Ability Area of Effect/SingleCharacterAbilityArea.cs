using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleCharacterAbilityArea : BaseAbilityArea
{
    public override List<Character> PerformAOECheckToGetColliders(AbilityCast abilityCast)
    {
        List<Character> characters = new List<Character>();
        Character character = null;
        if (abilityCast.abilityRequiresCursorSelection)
        {
            character = abilityCast.hit.collider.gameObject.GetComponent<Character>();
        }
        else if (abilityCast.abilityRange.GetAbilityRange() == typeof(SelfAbilityRange))
        {
            character = abilityCast.caster;
        }

        if (character != null)
        {
            characters.Add(character);
        }
        abilityCast.charactersAffected = characters;

        return characters;
    }

    public override void DisplayAOEArea()
    {
        //Doing nothing is probably best for this area type
    }

    public override Type GetAbilityArea()
    {
        return typeof(SingleCharacterAbilityArea);
    }
}
