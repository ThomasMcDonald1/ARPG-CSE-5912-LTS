using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingAbilityEffect : BaseAbilityEffect
{
    protected override int OnApply(Character target)
    {
        Character caster = GetComponentInParent<Character>();
        BaseAbilityPower power = GetComponent<BaseAbilityPower>();
        MagicalAbilityPower magPow = (MagicalAbilityPower)power;

        float healing;
        float multiplier = power.multiplier;
        float healStatValue = 1;
        if (magPow != null)
        {
            //Get whatever stat determines the power of a heal
            healStatValue = GetStat(caster, StatTypes.MAGPWR);
        }

        //TODO: pull in modifiers from equipment and such
        //TODO: do an actual calculation
        healing = healStatValue * multiplier + 2;

        int finalHealing = Mathf.RoundToInt(healing);
        finalHealing = Mathf.Clamp(finalHealing, minDamage, maxDamage);

        return finalHealing;
    }

}
