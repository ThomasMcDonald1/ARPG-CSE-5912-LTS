using System;
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

    public override Type GetAbilityRange()
    {
        return typeof(SelfAbilityRange);
    }

    public override List<Character> GetCharactersInRange()
    {
        characters.Add(self);
        return characters;
    }
}
