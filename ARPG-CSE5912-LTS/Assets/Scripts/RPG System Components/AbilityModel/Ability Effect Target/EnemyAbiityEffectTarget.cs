using ARPG.Combat;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAbiityEffectTarget : AbilityEffectTarget
{
    public override bool IsTarget(Character character)
    {
        if (character == null)
            return false;
        Stats stats = character.GetComponent<Stats>();

        return character.GetComponent<Enemy>() != null && stats != null && stats[StatTypes.HP] > 0;
    }
}
