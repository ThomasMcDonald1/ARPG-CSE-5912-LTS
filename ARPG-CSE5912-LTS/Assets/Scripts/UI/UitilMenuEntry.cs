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
    public string amount;
    public static event EventHandler<InfoEventArgs<bool>> UtilityAssignedToActionBarEvent;

    public void Assign()
    {
        //Debug.Log("Item is?: " + item.name);
        UtilityAssignedToActionBarEvent?.Invoke(this, new InfoEventArgs<bool>(true));

        // Following codes should be tested after adding more utilities 
        if(buttonToAssignUtil.item != null)
        {
           
            UtilityMenuPanel.instance.itemNamesExited.Remove(buttonToAssignUtil.item);
            UtilityMenuPanel.instance.itemNamesExited.Add(item);

        }
        else
        {
            UtilityMenuPanel.instance.itemNamesExited.Add(item);

        }
        buttonToAssignUtil.AddItem(item, amount);


    }
}
