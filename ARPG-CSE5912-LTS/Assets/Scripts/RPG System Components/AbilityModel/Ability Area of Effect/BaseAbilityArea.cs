using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseAbilityArea : MonoBehaviour
{
    public int aoeRadius;

    [HideInInspector] public bool abilityAreaNeedsShown = false;
    public abstract List<Character> PerformAOECheckToGetColliders(AbilityCast abilityCast);
    public abstract void DisplayAOEArea();
    public abstract Type GetAbilityArea();
}
