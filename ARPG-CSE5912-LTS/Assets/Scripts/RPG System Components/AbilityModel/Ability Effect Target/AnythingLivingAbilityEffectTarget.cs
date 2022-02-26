using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnythingLivingAbilityEffectTarget : AbilityEffectTarget
{
    public override bool IsTarget(Character target, Character caster)
    {
        if (target == null)
            return false;
        Stats stats = target.GetComponent<Stats>();
        return stats != null && stats[StatTypes.HP] > 0;
    }
}
