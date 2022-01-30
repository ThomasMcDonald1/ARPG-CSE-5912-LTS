using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseAbilityArea : MonoBehaviour
{
    public abstract void PerformAOE(RaycastHit hit);
    public abstract void DisplayAOEArea();
}
