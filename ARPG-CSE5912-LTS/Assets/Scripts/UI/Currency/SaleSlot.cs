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
    public Button removeButton;

    Ite item;
    public void AddItem(Ite newItem)
    {
        item = newItem;
        icon.sprite = item.icon;
        icon.enabled = true;
        removeButton.interactable = true;
        if (newItem.stackable)
        {
           
            amount.text = Inventory.instance.amount[newItem.name].ToString();

        }
        //UI_Sale.instance.updateUI();


    }

    public void ClearSlot()
    {
        item = null;
        icon.sprite = null;
        icon.enabled = false;
        removeButton.interactable = false;

        amount.text = "";
    }

 
  
    public void saleItem()
    {
        Inventory.instance.Sell(item);

        playerMoney.addMoney(item.cost);
        UI_Sale.instance.updateUI();

    }
}
