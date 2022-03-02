using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicalAbilityPower : BaseAbilityPower
{
    public override int GetBaseAttack()
    {
        return GetComponentInParent<Stats>()[StatTypes.INT];
    }
    public override float GetBaseDefense(Character target, BaseAbilityEffectElement effectElement)
    {
        StatTypes elementResistance = effectElement.GetAbilityEffectElementResistTarget();
        return GetStat(target, elementResistance);
    }

    public override float GetPercentDefense(Character target, BaseAbilityEffectElement effectElement)
    {  
        float allResist = GetStat(target, StatTypes.PercentAllResistBonus);
        StatTypes elementResistance = effectElement.GetAbilityEffectElementResistTarget();
        float targetPercentSpecificResist = GetSpecificPercentResistBonus(elementResistance, target);
        float defense = allResist + targetPercentSpecificResist;
        return defense;
    }

    public override float AdjustDefenseForPenetration(Character caster)
    {
        return 1 - GetStat(caster, StatTypes.PercentMagicPen) * 0.01f;
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
