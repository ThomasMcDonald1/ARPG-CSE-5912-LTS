using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Inventory 
{
    private List<InventoryItems> itemList;
    public event EventHandler OnItemListChanged;
    public Inventory()
    {
        itemList = new List<InventoryItems>();
        
        // Debug.Log(itemList.Count);
    }
    public void AddItem(InventoryItems item)
    {
        itemList.Add(item);
        OnItemListChanged?.Invoke(this, EventArgs.Empty);
    }
    
    public List<InventoryItems> GetItemList()
    {
        return itemList;
    }
}
