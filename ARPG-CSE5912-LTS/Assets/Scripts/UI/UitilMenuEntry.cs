using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
public class UitilMenuEntry : MonoBehaviour
{
    public Ite item;
    public PotionButton buttonToAssignUtil;
    public static event EventHandler<InfoEventArgs<bool>> UtilityAssignedToActionBarEvent;

    public void Assign()
    {
        Debug.Log("Item is?: " + item.name);
        buttonToAssignUtil.AddItem(item);
        UtilityAssignedToActionBarEvent?.Invoke(this, new InfoEventArgs<bool>(true));
    }
}
