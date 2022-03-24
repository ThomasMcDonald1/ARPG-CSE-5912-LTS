using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicalAbilityPower : BaseAbilityPower
{
    public override int GetBaseAttack()
    {
        return GetComponentInParent<Stats>()[StatTypes.STR];
    }
    public override float GetBaseDefense(Character target, BaseAbilityEffectElement effectElement)
    {
        return target.GetComponent<Stats>()[StatTypes.Armor];
    }

    public override float GetPercentDefense(Character target, BaseAbilityEffectElement effectElement)
    {
        return target.GetComponent<Stats>()[StatTypes.PercentArmorBonus] * 0.01f;
    }

    public override float AdjustDefenseForFlatPenetration(Character caster)
    {
        return GetStat(caster, StatTypes.FlatArmorPen);
    }

    public override float AdjustDefenseForPercentPenetration(Character caster)
    {
        return 1 - GetStat(caster, StatTypes.PercentArmorPen) * 0.01f;
    }

    public override float GetDamageBonusMultiplier(Character caster, BaseAbilityEffectElement element)
    {
        return caster.GetComponent<Stats>()[StatTypes.PhysDmgBonus];
    }
}
