using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbilityEffectTarget : MonoBehaviour
{
    public abstract bool IsTarget(Character target, Character caster);
}
