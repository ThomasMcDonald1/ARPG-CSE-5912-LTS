using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI_Sale : MonoBehaviour
{
    public Transform saleParent;
    Inventory inventory;
    public SaleSlot[] saleSlots;
    public List<Ite> saleItems = new List<Ite>();
    public Shop shop;
    
    void Start()
    {
        inventory = Inventory.instance;
        saleSlots = saleParent.GetComponentsInChildren<SaleSlot>();

    }

   
  
    public void updateUI()
    {
        switch ((int)shop.shopSaleType)
        {
            case (int)Shop.saleType.Utility:
                saleItems = inventory.utilItems;
                break;
            case (int)Shop.saleType.Armor:
                saleItems = inventory.armorItems;
                break;
            case (int)Shop.saleType.Weapon:
                saleItems = inventory.weaponItems;
                break;
        }
        //Debug.Log("Sale slots length: " + saleSlots.Length);
        for (int i = 0; i < saleSlots.Length; i++)
        {
            //Debug.Log("items.Count is " + inventory.items.Count);
            if (i < saleItems.Count)
            {
                GameObject amount = saleSlots[i].transform.GetChild(1).gameObject;
                TextMeshProUGUI text = amount.GetComponent<TextMeshProUGUI>();
                // Debug.Log(inventory.items[i].name + " index: " + i + " amount: " + inventory.amount[inventory.items[i]]);
                if (saleItems[i].stackable)
                {
                    // Debug.Log(inventory.items[i].name + " is " + inventory.items[i].stackable);


                    text.SetText(inventory.amount[saleItems[i]].ToString());

                }
                else
                {
                    text.SetText("");
                }
                saleSlots[i].AddItem(saleItems[i]);
            }
            else
            {
                saleSlots[i].ClearSlot();
            }
        }


    }
}
