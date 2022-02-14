using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class InventorySlot : MonoBehaviour
{
    public Image icon;
    public TextMeshProUGUI amount;
    public Button removeButton;
    public bool isEmpty = true;
    Ite item;
    public void AddItem(Ite newItem)
    {
        item = newItem;
        icon.sprite = item.icon;
        icon.enabled = true;
        removeButton.interactable = true;
        isEmpty = false;
    }

    public void ClearSlot()
    {
            item = null;
            icon.sprite = null;
            icon.enabled = false;
            removeButton.interactable = false;
            amount.SetText("");
            isEmpty = true;
    }

    public void OnRemoveButton()
    {
        Inventory.instance.Remove(item);
    }

    public void UseItem()
    {
        if(item != null)
        {
            item.Use();
        }
    }
}
