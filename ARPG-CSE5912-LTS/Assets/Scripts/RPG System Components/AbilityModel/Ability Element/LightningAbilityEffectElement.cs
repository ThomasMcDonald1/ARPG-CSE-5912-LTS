using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningAbilityEffectElement : BaseAbilityEffectElement
{
    public override StatTypes GetAbilityEffectElementResistTarget()
    {
        return StatTypes.LightningRes;
    }
}
