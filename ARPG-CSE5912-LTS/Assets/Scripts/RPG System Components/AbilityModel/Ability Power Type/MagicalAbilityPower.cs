using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicalAbilityPower : BaseAbilityPower
{
    protected override int GetBaseAttack()
    {
        return GetComponentInParent<Stats>()[StatTypes.INT];
    }
    protected override int GetBaseDefense(Character target)
    {
        //TODO: Determine the element of the ability being cast, and then get the appropriate resistance from the defender
        //TODO: instead of only returning fire resistance
        return target.GetComponent<Stats>()[StatTypes.FireRes];
    }
    public override bool IsPhysicalPower()
    {
        return false;
    }

    public override bool IsMagicPower()
    {
        return true;
    }
}
