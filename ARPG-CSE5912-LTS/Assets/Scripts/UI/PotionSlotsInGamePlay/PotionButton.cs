using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.EventSystems;

public class PotionButton : MonoBehaviour
{
    public Image icon;
    public TextMeshProUGUI amountText;
    //private float timeToWait = 0.05f;
    public Ite item;
    public void AddItem(Ite newItem, string amount)
    {
        item = newItem;
        icon.sprite = item.icon;
        icon.enabled = true;
        amountText.SetText(amount);

    }
    public void UseItem()
    {
        if (item != null)
        {
            item.Use();

            if (item.stackable)
            {
                Debug.Log("Amount is: " + amountText.text.ToString());
                int amt = int.Parse(amountText.text.ToString());
                amt = amt - 1;
                if(amt > 0)
                {
                    amountText.SetText(amt.ToString());

                }
                else
                {
                    clearButton();
                }

            }
            else
            {
                clearButton();

            }


        }
    }
    public void clearButton()
    {
        UtilityMenuPanel.instance.itemNamesExited.Remove(item);

        item = null;
        icon.sprite = null;
        icon.enabled = false;
        amountText.SetText("");
    }
}
