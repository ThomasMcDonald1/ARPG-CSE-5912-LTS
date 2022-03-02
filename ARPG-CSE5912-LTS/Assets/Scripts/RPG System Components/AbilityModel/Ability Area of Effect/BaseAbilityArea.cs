using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseAbilityArea : MonoBehaviour
{
    public bool abilityAreaNeedsShown = false;
    public abstract List<Character> PerformAOECheckToGetColliders(AbilityCast abilityCast);
    public abstract void DisplayAOEArea();
}
