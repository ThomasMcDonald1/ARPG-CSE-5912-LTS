using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilityEffectTarget : AbilityEffectTarget
{
    public override bool IsTarget(Character character)
    {
        if (character == null)
            return false;
        return character.GetComponent<Player>() != null;
    }
}
