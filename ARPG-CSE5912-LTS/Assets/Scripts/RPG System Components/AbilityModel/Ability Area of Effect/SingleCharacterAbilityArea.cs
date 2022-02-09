using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleCharacterAbilityArea : BaseAbilityArea
{
    public override List<Character> PerformAOECheckToGetColliders(RaycastHit hit, Character caster)
    {
        List<Character> charList = new List<Character>();
        Character character = hit.collider.gameObject.GetComponent<Character>();
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
