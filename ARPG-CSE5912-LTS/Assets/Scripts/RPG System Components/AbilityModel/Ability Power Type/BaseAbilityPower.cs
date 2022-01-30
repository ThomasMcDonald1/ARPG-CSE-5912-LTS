using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseAbilityPower : MonoBehaviour
{
    public float multiplier = 1;

    protected abstract int GetBaseAttack();
    protected abstract int GetBaseDefense(Character target);
    protected abstract float GetMultiplier();

    //TODO: Add anything else that should be base functionality for all types of ability power here
}
