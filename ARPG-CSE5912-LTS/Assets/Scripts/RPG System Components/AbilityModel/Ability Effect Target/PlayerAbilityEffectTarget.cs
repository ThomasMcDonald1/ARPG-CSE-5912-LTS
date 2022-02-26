using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilityEffectTarget : AbilityEffectTarget
{
    public override bool IsTarget(Character character)
    {
        if (character == null)
            return false;
        Stats stats = character.GetComponent<Stats>();

        return character.GetComponent<Player>() != null && stats != null && stats[StatTypes.HP] > 0; 
    }
}
