using ARPG.Combat;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RivalAbilityEffectTarget : AbilityEffectTarget
{
    public override bool IsTarget(Character target, Character caster)
    {
        if (target == null)
            return false;
        Stats stats = target.GetComponent<Stats>();

        return (caster.GetCharacterType() != target.GetCharacterType()) && target.GetComponent<Enemy>() != null && stats != null && stats[StatTypes.HP] > 0;
    }
}
