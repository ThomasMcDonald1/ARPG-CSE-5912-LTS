using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageAbilityEffect : BaseAbilityEffect
{
    //public int minEffectDamage;
    //public int maxEffectDamage;

    protected override int OnApply(Character target)
    {
        Character caster = GetComponentInParent<Character>();
        BaseAbilityPower power = GetComponent<BaseAbilityPower>();
        MagicalAbilityPower magPow = (MagicalAbilityPower)power;
        PhysicalAbilityPower physPow = (PhysicalAbilityPower)power;
        
        int baseDamageScaler = 5;
        float casterLevel = GetStat(target, StatTypes.LVL);
        float baseDamage = power.baseDamageOrHealing;

        //To store the damage calculated
        //Calculate the caster's total attack damage pre-mitigation
        float damage = (baseDamage * casterLevel + baseDamage / casterLevel) / (baseDamageScaler + (casterLevel * 0.01f));
        float enemyDefense;

        //If this ability effect is physical damage
        if (physPow != null)
        {
            //Get some stats for attacker and defender
            enemyDefense = GetStat(target, StatTypes.Armor);
            float casterFlatArmorPen = GetStat(caster, StatTypes.FlatArmorPen);
            float casterPercentArmorPen = GetStat(caster, StatTypes.PercentArmorPen);
            float targetPercentArmorBonus = GetStat(target, StatTypes.PercentArmorBonus);

            //Calculate the defender's total defense 
            enemyDefense *= (1 + targetPercentArmorBonus);
            enemyDefense = (enemyDefense - casterFlatArmorPen) * (100 - casterPercentArmorPen);
        }

        //If this ability effect is magical damage
        if (magPow != null)
        {
            //Get stats like above
            BaseAbilityEffectElement effectElement = GetComponent<BaseAbilityEffectElement>();
            StatTypes elementResistance = effectElement.GetAbilityEffectElementResistTarget();
            enemyDefense = GetStat(target, elementResistance);
            float casterFlatMagicPen = GetStat(caster, StatTypes.FlatMagicPen);
            float casterPercentMagicPen = GetStat(caster, StatTypes.PercentMagicPen);
            float targetPercentAllResist = GetStat(target, StatTypes.PercentAllResistBonus);
            float targetPercentSpecificResist = GetSpecificPercentResistBonus(elementResistance, target);
            //TODO: multiply (1 + summed %resist bonuses from equipment and other bonuses)
            enemyDefense *= (1 + targetPercentAllResist + targetPercentSpecificResist);
            enemyDefense = (enemyDefense - casterFlatMagicPen) * (100 - casterPercentMagicPen);
        }

        

        //round damage to an integer to return
        int finalCalculatedDamage = Mathf.RoundToInt(damage);

        //If we want an upper limit on damage, we can clamp it like this
        finalCalculatedDamage = Mathf.Clamp(finalCalculatedDamage, minDamage, maxDamage);

        //TODO: You could also modify ALL damage done in the game by multiplying this by the variable 'globalDamageBalanceAdjustment' which can be
        //changed in the BaseAbilityEffect script

        return finalCalculatedDamage;
    }

    private float GetSpecificPercentResistBonus(StatTypes resist, Character target)
    {
        float result = 0;

        switch (resist)
        {
            case StatTypes.FireRes:
                result = GetStat(target, StatTypes.PercentFireResistBonus);
                break;
            case StatTypes.ColdRes:
                result = GetStat(target, StatTypes.PercentColdResistBonus);
                break;
            case StatTypes.LightningRes:
                result = GetStat(target, StatTypes.PercentLightningResistBonus);
                break;
            case StatTypes.PoisonRes:
                result = GetStat(target, StatTypes.PercentPoisonResistBonus);
                break;
            default:
                break;
        }
        return result;
    }
}
