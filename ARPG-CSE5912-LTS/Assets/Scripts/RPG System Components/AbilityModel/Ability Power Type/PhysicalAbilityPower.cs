using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicalAbilityPower : BaseAbilityPower
{
    protected override int GetBaseAttack()
    {
        return GetComponentInParent<Stats>()[StatTypes.STR];
    }
    protected override int GetBaseDefense(Character target)
    {
        return target.GetComponent<Stats>()[StatTypes.Armor];
    }

    protected override float GetMultiplier()
    {
        return baseDamageOrHealing;
    }
}
