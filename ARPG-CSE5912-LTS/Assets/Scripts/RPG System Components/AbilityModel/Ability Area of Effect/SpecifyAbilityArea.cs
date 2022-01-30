using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecifyAbilityArea : BaseAbilityArea
{
    public int aoeRadius;

    public override void PerformAOE(RaycastHit hit)
    {
        Collider[] hitColliders = Physics.OverlapSphere(hit.point, aoeRadius);
        //TODO: Visual representation of overlap sphere
        foreach (Collider hitCollider in hitColliders)
        {
            Character character = hitCollider.gameObject.GetComponent<Character>();
            if (character != null)
            {
                //Do ability effects on character
            }
        }
    }

    public override void DisplayAOEArea()
    {
        
    }
}
