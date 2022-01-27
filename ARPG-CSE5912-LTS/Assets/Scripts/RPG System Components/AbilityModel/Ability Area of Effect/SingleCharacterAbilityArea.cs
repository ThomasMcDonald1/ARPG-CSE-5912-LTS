using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleCharacterAbilityArea : BaseAbilityArea
{
    List<Character> characters;

    private void Awake()
    {
        characters = new List<Character>();
    }

    public override List<Character> GetCharactersInAOE(RaycastHit hit)
    {
        characters.Add(hit.collider.gameObject.GetComponent<Character>());
        return characters;
    }
}
