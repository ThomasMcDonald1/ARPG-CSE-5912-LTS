using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireAbilityEffectElement : BaseAbilityEffectElement
{
    public override StatTypes GetAbilityEffectElementResistTarget()
    {
        return StatTypes.FireRes;
    }
}
