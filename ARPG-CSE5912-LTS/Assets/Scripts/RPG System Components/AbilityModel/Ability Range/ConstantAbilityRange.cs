using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstantAbilityRange : BaseAbilityRange
{
    List<Character> characters;

    public void Awake()
    {
        characters = new List<Character>();    
    }

    public override List<Character> GetCharactersInRange()
    {
        Collider[] hitColliders = Physics.OverlapSphere(caster.transform.position, range);
        //TODO: Visual representation of overlap sphere
        foreach (Collider hitCollider in hitColliders)
        {
            Character character = hitCollider.gameObject.GetComponent<Character>();
            characters.Add(character);
        }

        return characters;
    }
}
