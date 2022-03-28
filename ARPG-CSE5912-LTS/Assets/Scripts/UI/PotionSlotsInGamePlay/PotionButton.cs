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
    public TextMeshProUGUI amount;
    //private float timeToWait = 0.05f;
    Ite item;
    public void AddItem(Ite newItem)
    {
        item = newItem;
        icon.sprite = item.icon;
        icon.enabled = true;

    }
    public void UseItem()
    {
        if (item != null)
        {
            item.Use();
            if (item.type == Ite.ItemType.utility)
            {
                Inventory.instance.Remove(item);

            }

        }
    }
}
