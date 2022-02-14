using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    #region Singleton

    public static Inventory instance;

    void Awake()
    {
        instance = this;
    }

    #endregion
    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;
    // Our current list of items in the inventory
    public Hashtable amount = new Hashtable();
    public List<Ite> weaponItems = new List<Ite>(); 
    public List<Ite> armorItems = new List<Ite>();
    public List<Ite> untilityItems = new List<Ite>();


    public int space = 20;

    // Add a new item if enough room
    public void Add(Ite item, List<Ite> list)
    {
        
        if (item.showInInventory)
        {
            if (list.Count >= space)
            {
                Debug.Log("Not enough room.");
                return;
            }
            if (item.stackable)
            {
                // Debug.Log("get in stackable if statement");
                foreach (Ite inventoryItem in list)
                {
                   
                    //Debug.Log(item.type + "has: " + items.Count);
                    if (inventoryItem.name.Equals(item.name) && amount.ContainsKey(item))
                    {
                        //Debug.Log("Room:  " + items.Count);


                        int num = (int)amount[item] + 1;
                        amount[item] = num;
                        //Debug.Log(item.type + "has: " + inventoryItem.amount);

                       // iteInInventory = true;
                       

                    }

                    
                }
            }
            if (!item.stackable)
            {
                list.Add(item);
               // Debug.Log("the unstackable Item is " + item.name);

                if (onItemChangedCallback != null)
                    onItemChangedCallback.Invoke();

            }
            else if (item.stackable && !amount.ContainsKey(item))
            {
                // item.amount += 1;
                //int num = (int)amount[item] + 1;
                amount.Add(item, 1);
                list.Add(item);

                if (onItemChangedCallback != null)
                    onItemChangedCallback.Invoke();
            }
        }
    }

    
    
    // Remove an item
    public void Remove(Ite item)
    {
        if (!item.stackable)
        {
            weaponItems.Remove(item);
        }
        else if ((int)amount[item] > 1)
        {
            int num = (int)amount[item] - 1;
            amount[item] = num;
        }
        else if ((int)amount[item] == 1)
        {
            amount[item] = "";
            weaponItems.Remove(item);
            amount.Remove(item);
        }

        if (onItemChangedCallback != null)
            onItemChangedCallback.Invoke();
    }
}
