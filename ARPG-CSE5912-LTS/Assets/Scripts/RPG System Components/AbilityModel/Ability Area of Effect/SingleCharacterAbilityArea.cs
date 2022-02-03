using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleCharacterAbilityArea : BaseAbilityArea
{
    public override void PerformAOE(RaycastHit hit)
    {
        Character character = hit.collider.gameObject.GetComponent<Character>();
        if (character != null)
        {
            //Do ability effects on character
        }
    }

    public override void DisplayAOEArea()
    {
        //Doing nothing is probably best for this area type
    }
}
