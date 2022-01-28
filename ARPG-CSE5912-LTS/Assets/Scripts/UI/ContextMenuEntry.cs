using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class ContextMenuEntry : MonoBehaviour
{
    public Ability ability;
    public ActionButton buttonToAssignAbility;
    public static event EventHandler<InfoEventArgs<bool>> AbilityAssignedToActionBarEvent;

    public void Assign()
    {
        Debug.Log("Left clicked: " + gameObject.name);
        buttonToAssignAbility.abilityAssigned = ability;
        buttonToAssignAbility.SetIcon();
        AbilityAssignedToActionBarEvent?.Invoke(this, new InfoEventArgs<bool>(true));
    }
}
