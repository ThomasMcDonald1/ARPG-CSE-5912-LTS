using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingAbilityEffect : BaseAbilityEffect
{
    public static event EventHandler<InfoEventArgs<(Character, int, bool)>> AbilityHealingReceivedEvent;

    protected override int OnApply(Character target)
    {
        Character caster = GetComponentInParent<Character>();
        BaseAbilityPower power = GetComponentInParent<BaseAbilityPower>();
        bool wasCrit = false;

        int baseHealingScaler = 5;
        float casterLevel = GetStat(target, StatTypes.LVL);
        float baseHealing = power.baseDamageOrHealing;

        //Calculate the caster's total healing 
        float healing = (baseHealing * casterLevel + baseHealing / casterLevel) / (baseHealingScaler + (casterLevel * 0.01f));

        float healingRandomFloor = healing * 0.95f;
        float healingRandomCeiling = healing * 1.05f;
        healing = UnityEngine.Random.Range(healingRandomFloor, healingRandomCeiling);

        wasCrit = RollForCrit(caster);
        if (wasCrit)
        {
            float critHealingPercent = (200 + caster.stats[StatTypes.CritDamage]) * 0.01f;
            healing *= critHealingPercent;
        }

        int finalHealing = Mathf.RoundToInt(healing);
        finalHealing = Mathf.Clamp(finalHealing, minDamage, maxDamage);

        target.stats[StatTypes.HP] += finalHealing;
        target.stats[StatTypes.HP] = Mathf.Clamp(target.stats[StatTypes.HP], 0, target.stats[StatTypes.MaxHP]);
        AbilityHealingReceivedEvent?.Invoke(this, new InfoEventArgs<(Character, int, bool)>((target, finalHealing, wasCrit)));
        Debug.Log("Healing for " + finalHealing + " to " + target.name);
        return finalHealing;
    }

}
