using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColdAbilityEffectElement : BaseAbilityEffectElement
{
    public override StatTypes GetAbilityEffectElementResistTarget()
    {
        return StatTypes.ColdRes;
    }
    public override StatTypes GetAbilityEffectElementBonusMultType()
    {
        return StatTypes.ColdDmgBonus;
    }
}
