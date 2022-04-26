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
    public int saleItemChanged;

    public static UI_Sale instance;
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        inventory = Inventory.instance;
        saleSlots = saleParent.GetComponentsInChildren<SaleSlot>();
        //saleItemChanged = saleItems.Count;
       
    }

   
  
    public void updateUI()
    {
        
        if (shop != null)
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
        }
      

        for (int i = 0; i < saleSlots.Length; i++)
            {
                //Debug.Log("items.Count is " + inventory.items.Count);
                if (i < saleItems.Count)
                {
                    
                    saleSlots[i].AddItem(saleItems[i]);
                }
                else
                {
                    saleSlots[i].ClearSlot();
                }
            }
       
    }
}
