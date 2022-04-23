using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseAbilityMovement : MonoBehaviour
{
    public static event EventHandler<InfoEventArgs<(AbilityCast, List<Character>)>> SpecialMovementCompletedEvent;
    public abstract void QueueSpecialMovement(AbilityCast abilityCast, List<Character> targets);

    protected void CompleteSpecialMovement(AbilityCast abilityCast, List<Character> targets)
    {
        Debug.Log("Firing special movement completed event");
        SpecialMovementCompletedEvent?.Invoke(this, new InfoEventArgs<(AbilityCast, List<Character>)>((abilityCast, targets)));
    }
}
