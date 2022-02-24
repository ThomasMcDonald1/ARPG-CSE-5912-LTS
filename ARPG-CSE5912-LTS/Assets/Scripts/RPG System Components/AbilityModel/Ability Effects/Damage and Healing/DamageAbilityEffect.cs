using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageAbilityEffect : BaseAbilityEffect
{
    //public int minEffectDamage;
    //public int maxEffectDamage;

    public static event EventHandler<InfoEventArgs<(Character, int, bool)>> AbilityDamageReceivedEvent;

    protected override int OnApply(Character target)
    {
        Character caster = GetComponentInParent<Character>();
        BaseAbilityPower power = GetComponentInParent<BaseAbilityPower>();
        bool wasCrit = false;

        int baseDamageScaler = 3; //smaller numbers result in bigger final damage
        float casterLevel = GetStat(target, StatTypes.LVL);
        float baseDamage = power.baseDamageOrHealing;

        //Calculate the caster's total attack damage pre-mitigation
        float damage = (baseDamage * casterLevel + baseDamage / casterLevel) / (baseDamageScaler + (casterLevel * 0.01f));
        //Debug.Log("initial damage: " + damage);
        //Get armor pen values from caster
        //float casterFlatArmorPen = GetStat(caster, StatTypes.FlatArmorPen);
        float casterPercentArmorPen = GetStat(caster, StatTypes.PercentArmorPen);
        //Get magic pen values from caster
        float casterFlatMagicPen = GetStat(caster, StatTypes.FlatMagicPen);
        float casterPercentMagicPen = GetStat(caster, StatTypes.PercentMagicPen);
        float finalDamageWithPen = 0;
        //Calculate the enemy's defense pre-penetration
        float enemyDefense = 0;

        //If this ability effect is physical damage
        if (power.IsPhysicalPower())
        {
            //Debug.Log("Physical power");
            enemyDefense = GetStat(target, StatTypes.Armor);

            //Calculate the defender's total defense pre-armor pen

            //TODO: May not need this. It might be added already into Armor.
            float targetPercentArmorBonus = GetStat(target, StatTypes.PercentArmorBonus);
            enemyDefense *= (1 + targetPercentArmorBonus);

            //Apply caster physical penetration
            enemyDefense *= (1 - casterPercentArmorPen);
        }
        //If this ability effect is magical damage
        else if (power.IsMagicPower())
        {
            //Debug.Log("Magical power");
            BaseAbilityEffectElement effectElement = GetComponent<BaseAbilityEffectElement>();
            StatTypes elementResistance = effectElement.GetAbilityEffectElementResistTarget();
            enemyDefense = GetStat(target, elementResistance);
            //Debug.Log("Enemy initial defense: " + enemyDefense);
            float targetPercentAllResist = GetStat(target, StatTypes.PercentAllResistBonus);
            float targetPercentSpecificResist = GetSpecificPercentResistBonus(elementResistance, target);
            //Debug.Log("All resist: " + targetPercentAllResist + ", Specific Resist: " + targetPercentSpecificResist);
            //TODO: multiply (1 + summed %resist bonuses from equipment and other bonuses)
            enemyDefense += targetPercentAllResist + targetPercentSpecificResist;
            //Debug.Log("Enemy defense after bonuses applied: " + enemyDefense);
            //Apply caster magic penetration
            enemyDefense *= (1 - casterPercentMagicPen * 0.01f);
            //Debug.Log("Enemy defense after magic pen applied: " + enemyDefense);
        }

        //Calculate the final damage the caster would do post-penetration
        if (power.IsPhysicalPower())
        {
            finalDamageWithPen = damage * (120 / (120 + enemyDefense));
        }
        else if (power.IsMagicPower())
        {
            finalDamageWithPen = damage * (120 / (120 + enemyDefense));
        }

        //Debug.Log("damage with pen: " + finalDamageWithPen);
        //Add some randomization
        float damageRandomFloor = finalDamageWithPen * 0.95f;
        float damageRandomCeiling = finalDamageWithPen * 1.05f;
        finalDamageWithPen = UnityEngine.Random.Range(damageRandomFloor, damageRandomCeiling);
        //Debug.Log("damage with randomization: " + finalDamageWithPen);

        wasCrit = RollForCrit(caster);
        if (wasCrit)
        {
            float critDamagePercent = (200 + caster.stats[StatTypes.CritDamage]) * 0.01f;
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
