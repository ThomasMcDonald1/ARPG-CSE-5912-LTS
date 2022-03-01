using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class SaleSlot : MonoBehaviour
{
    public Image icon;
    public TextMeshProUGUI amount;
    [SerializeField] PlayerMoney playerMoney;
    Ite item;
    public void AddItem(Ite newItem)
    {
        item = newItem;
        icon.sprite = item.icon;
        icon.enabled = true;
       

    }

    public void ClearSlot()
    {
        item = null;
        icon.sprite = null;
        icon.enabled = false;
        amount.SetText("");

    }

 
  
    public void saleItem()
    {
        Inventory.instance.Remove(item);
        playerMoney.addMoney(item.cost);
    }
}
