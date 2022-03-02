using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageAbilityEffect : BaseAbilityEffect
{
    //public int minEffectDamage;
    //public int maxEffectDamage;

    public static event EventHandler<InfoEventArgs<(Character, int, bool)>> AbilityDamageReceivedEvent;

    protected override int OnApply(Character target, AbilityCast abilityCast)
    {
        if (effectVFXObj != null)
            InstantiateEffectVFX(abilityCast, target);

        int dmg = DealDamage(target, abilityCast);
        return dmg;
    }

    public int DealDamage(Character target, AbilityCast abilityCast)
    {
        bool wasCrit = false;

        int baseDamageScaler = 3; //smaller numbers result in bigger final damage
        float casterLevel = GetStat(target, StatTypes.LVL);
        float baseDamage = abilityCast.abilityPower.baseDamageOrHealing;

        //Calculate the caster's total attack damage pre-mitigation
        float damage = (baseDamage * casterLevel + baseDamage / casterLevel) / (baseDamageScaler + (casterLevel * 0.01f));
        //Debug.Log("initial damage: " + damage);
        //Get armor pen values from caster
        //float casterFlatArmorPen = GetStat(caster, StatTypes.FlatArmorPen);
        float casterPercentArmorPen = GetStat(abilityCast.caster, StatTypes.PercentArmorPen);
        //Get magic pen values from caster
        float casterFlatMagicPen = GetStat(abilityCast.caster, StatTypes.FlatMagicPen);
        float casterPercentMagicPen = GetStat(abilityCast.caster, StatTypes.PercentMagicPen);
        float finalDamageWithPen;
        //Get the element of the effect, if any
        BaseAbilityEffectElement effectElement = GetComponent<BaseAbilityEffectElement>();
        //Calculate the enemy's defense pre-penetration
        float enemyDefense = abilityCast.abilityPower.GetBaseDefense(target, effectElement);
        enemyDefense *= (1 + abilityCast.abilityPower.GetPercentDefense(target, effectElement));
        //Calculate enemy defense post-penetration
        enemyDefense *= abilityCast.abilityPower.AdjustDefenseForPenetration(abilityCast.caster);

        //Calculate the final damage the caster would do post-penetration
        finalDamageWithPen = damage * (120 / (120 + enemyDefense));

        //Debug.Log("damage with pen: " + finalDamageWithPen);
        //Add some randomization
        float damageRandomFloor = finalDamageWithPen * 0.95f;
        float damageRandomCeiling = finalDamageWithPen * 1.05f;
        finalDamageWithPen = UnityEngine.Random.Range(damageRandomFloor, damageRandomCeiling);
        //Debug.Log("damage with randomization: " + finalDamageWithPen);

        wasCrit = RollForCrit(abilityCast.caster);
        if (wasCrit)
        {
            float critDamagePercent = (200 + abilityCast.caster.stats[StatTypes.CritDamage]) * 0.01f;
            finalDamageWithPen *= critDamagePercent;
        }

        //round damage to an integer to return
        int finalCalculatedDamage = Mathf.RoundToInt(finalDamageWithPen);
        //Debug.Log("rounded damage to int: " + finalCalculatedDamage);
        //If we want an upper limit on damage, we can clamp it like this
        finalCalculatedDamage = Mathf.Clamp(finalCalculatedDamage, minDamage, maxDamage);

        //TODO: You could also modify ALL damage done in the game by multiplying this by the variable 'globalDamageBalanceAdjustment' which can be
        //changed in the BaseAbilityEffect script
        target.stats[StatTypes.HP] -= finalCalculatedDamage;
        target.stats[StatTypes.HP] = Mathf.Clamp(target.stats[StatTypes.HP], 0, target.stats[StatTypes.MaxHP]);
        AbilityDamageReceivedEvent?.Invoke(this, new InfoEventArgs<(Character, int, bool)>((target, finalCalculatedDamage, wasCrit)));
        Debug.Log("Doing " + finalCalculatedDamage + " damage to " + target.name);
        return finalCalculatedDamage;
    }
}
