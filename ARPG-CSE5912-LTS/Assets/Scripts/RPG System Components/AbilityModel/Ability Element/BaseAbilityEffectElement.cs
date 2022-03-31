using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseAbilityEffectElement : MonoBehaviour
{
    public abstract StatTypes GetAbilityEffectElementResistTarget();
    public abstract StatTypes GetAbilityEffectElementBonusMultType();
}
