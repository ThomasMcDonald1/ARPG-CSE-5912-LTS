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
    public List<Ite> items = new List<Ite>();
    public int space = 24;

    // Add a new item if enough room
    public void Add(Ite item)
    {
        bool iteInInventory = false;
        //Debug.Log("Type: " + item.type);
        if (item.showInInventory)
        {
            if (items.Count >= space)
            {
                Debug.Log("Not enough room.");
                return;
            }
            if (item.stackable)
            {
                // Debug.Log("get in stackable if statement");
                foreach (Ite inventoryItem in items)
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
                items.Add(item);
                Debug.Log("the unstackable Item is " + item.name);

                if (onItemChangedCallback != null)
                    onItemChangedCallback.Invoke();

            }
            else if (item.stackable && !amount.ContainsKey(item))
            {
                // item.amount += 1;
                //int num = (int)amount[item] + 1;
                amount.Add(item, 1);
                items.Add(item);

                if (onItemChangedCallback != null)
                    onItemChangedCallback.Invoke();
            }
        }
    }

    /*
      public void Add(Ite item)
      {
          //Debug.Log("add was called");
          //Debug.Log("Room:  " + items.Count);
          if (item.showInInventory)
          {
              if (items.Count >= space)
              {
                  Debug.Log("Not enough room:  " + items.Count);
                  return;

              }
              else
              {
                  if (item.stackable)
                  {
                      Debug.Log("get in stackable if statement");
                      foreach (Ite inventoryItem in items)
                      {
                          bool iteInInventory = false;
                          Debug.Log(item.type + "has: " + ((int)inventoryItem.type == (int)item.type));
                          *//* if ((int)inventoryItem.type==(int)item.type)
                           {
                               Debug.Log("Room:  " + items.Count);

                               inventoryItem.amount += item.amount;
                               iteInInventory = true;

                           }*//*
                          if (!iteInInventory)
                          {
                              items.Add(item);
                          }
                      }
                  }
                  //items.Add(item);

              }
              if (onItemChangedCallback != null)
              {
                  onItemChangedCallback.Invoke();
              }

              //items.Add(item);



          }

      }*/
    private void Update()
    {
        foreach (Ite item in items)
        {
           // Debug.Log(item.name + " amount: " + item.amount);
        }
    }
    // Remove an item
    public void Remove(Ite item)
    {
        if (!item.stackable)
        {
            items.Remove(item);
        }
        else if ((int)amount[item] > 1)
        {
            int num = (int)amount[item] - 1;
            amount[item] = num;
        }
        else if ((int)amount[item] == 1)
        {
            amount[item] = "";
            items.Remove(item);
            amount.Remove(item);
        }

        if (onItemChangedCallback != null)
            onItemChangedCallback.Invoke();
    }
}
