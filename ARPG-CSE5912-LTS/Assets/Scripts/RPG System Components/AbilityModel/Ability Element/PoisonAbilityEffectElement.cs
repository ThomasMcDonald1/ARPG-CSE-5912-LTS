using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonAbilityEffectElement : BaseAbilityEffectElement
{
    public override StatTypes GetAbilityEffectElementResistTarget()
    {
        return StatTypes.PoisonRes;
    }
}
