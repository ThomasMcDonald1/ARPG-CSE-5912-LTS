using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleCharacterAbilityArea : BaseAbilityArea
{
    public override List<Character> PerformAOECheckToGetColliders(AbilityCast abilityCast)
    {
        List<Character> charList = new List<Character>();
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
            charList.Add(character);
        }
        return charList;
    }

    public override void DisplayAOEArea()
    {
        //Doing nothing is probably best for this area type
    }
}
