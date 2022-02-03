using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory 
{
    private List<InventoryItems> itemList;
    
    public Inventory()
    {
        itemList = new List<InventoryItems>();
        AddItem(new InventoryItems { itemType = InventoryItems.ItemType.Coin, amount = 1 });
        AddItem(new InventoryItems { itemType = InventoryItems.ItemType.HealthPotion, amount = 1 });
        AddItem(new InventoryItems { itemType = InventoryItems.ItemType.ManaPotion, amount = 1 });


       // Debug.Log(itemList.Count);
    }
    public void AddItem(InventoryItems item)
    {
        itemList.Add(item);
    }
    public void addItemDraft()
    {

    }
    public List<InventoryItems> GetItemList()
    {
        return itemList;
    }
}
