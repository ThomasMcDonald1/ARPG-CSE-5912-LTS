using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseAbilityPower : MonoBehaviour
{
    public float baseDamageOrHealing;

    protected abstract int GetBaseAttack();
    protected abstract int GetBaseDefense(Character target);
    public abstract bool IsPhysicalPower();
    public abstract bool IsMagicPower();

    //TODO: Add anything else that should be base functionality for all types of ability power here
}
