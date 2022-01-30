using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfAbilityRange : BaseAbilityRange
{
    List<Character> characters;
    Character self;

    private void Awake()
    {
        characters = new List<Character>();
        self = GetComponentInParent<Character>();
    }

    public override List<Character> GetCharactersInRange()
    {
        characters.Add(self);
        return characters;
    }
}
