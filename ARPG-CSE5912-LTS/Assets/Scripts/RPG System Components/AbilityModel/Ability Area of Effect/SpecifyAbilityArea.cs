using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecifyAbilityArea : BaseAbilityArea
{
    List<Character> characters;
    public int aoeRadius;

    private void Awake()
    {
        characters = new List<Character>();
    }

    public override List<Character> GetCharactersInAOE(RaycastHit hit)
    {
        Collider[] hitColliders = Physics.OverlapSphere(hit.point, aoeRadius);
        //TODO: Visual representation of overlap sphere
        foreach (Collider hitCollider in hitColliders)
        {
            Character character = hitCollider.gameObject.GetComponent<Character>();
            characters.Add(character);
        }

        return characters;
    }
}
