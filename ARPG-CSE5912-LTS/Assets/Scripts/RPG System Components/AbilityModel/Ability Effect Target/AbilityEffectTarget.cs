using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbilityEffectTarget : MonoBehaviour
{
    //To be overwritten by specific effect target types
    public abstract bool IsTarget(Character target, Character caster);
}
