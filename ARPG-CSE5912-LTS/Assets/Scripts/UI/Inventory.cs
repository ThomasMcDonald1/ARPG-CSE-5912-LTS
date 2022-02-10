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
                    if ((int)inventoryItem.type == (int)item.type)
                    {
                        //Debug.Log("Room:  " + items.Count);


                        inventoryItem.amount += 1;
                        //Debug.Log(item.type + "has: " + inventoryItem.amount);

                        iteInInventory = true;
                       

                    }

                    
                }
            }
            if (!item.stackable)
            {
                items.Add(item);

                if (onItemChangedCallback != null)
                    onItemChangedCallback.Invoke();
            }else if(!iteInInventory && item.stackable)
            {
                item.amount += 1;
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
            Debug.Log(item.type + " amount: " + item.amount);
        }
    }
    // Remove an item
    public void Remove(Ite item)
    {
        items.Remove(item);

        if (onItemChangedCallback != null)
            onItemChangedCallback.Invoke();
    }
}
