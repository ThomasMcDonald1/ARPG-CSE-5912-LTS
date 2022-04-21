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
    public List<Ite> utilItems = new List<Ite>();
    public Ite healthPotion;
    public PotionButton[] potionButtons;

    private GameObject player;
    private GameObject potionSlots;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        potionSlots = GameObject.FindGameObjectWithTag("PotionSlot");
        potionButtons = potionSlots.GetComponentsInChildren<PotionButton>();
        utilItems.Add(healthPotion);
        amount.Add(healthPotion.name, 3);
    }


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
            else
            {
                if (item.stackable)
                {
                    // Debug.Log("get in stackable if statement");
                    foreach (Ite inventoryItem in list)
                    {

                        //Debug.Log(item.type + "has: " + items.Count);
                        if (inventoryItem.name.Equals(item.name) && amount.ContainsKey(item.name))
                        {
                            //Debug.Log("Room:  " + items.Count);


                            int num = (int)amount[item.name] + 1;
                            amount[item.name] = num;
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
                else if (item.stackable && !amount.ContainsKey(item.name))
                {
                    // item.amount += 1;
                    //int num = (int)amount[item] + 1;
                    amount.Add(item.name, 1);
                    list.Add(item);

                    if (onItemChangedCallback != null)
                        onItemChangedCallback.Invoke();
                }
            }
        }
        foreach (PotionButton pButton in potionButtons)
        {
            if (pButton.item != null && pButton.item.Equals(item))
            {
                pButton.AddItem(item, amount[item.name].ToString());
            }
        }
    }

    
    
    // Remove an item
    public void Remove(Ite item)
    {
        List<Ite> list = null;
        switch (item.type)
        {
            case Ite.ItemType.armor:
                list = armorItems;
                break;
            case Ite.ItemType.weapon:
                list = weaponItems;
                break;
            case Ite.ItemType.utility:
                list = utilItems;
                break;

        }

        if (!item.stackable)
        {
            list.Remove(item);
          //  Destroy(item);
        }
        else if ((int)amount[item.name] > 1)
        {

            int num = (int)amount[item.name] - 1;
            amount[item.name] = num;
        }
        else if ((int)amount[item.name] == 1)
        {
            amount[item.name] = "0";
            list.Remove(item);
            amount.Remove(item.name);
           // Destroy(item);
        }
        if (onItemChangedCallback != null)
            onItemChangedCallback.Invoke();
    }
    //sell an item

    public void Sell(Ite item)
    {
        List<Ite> list = null;
        switch (item.type)
        {
            case Ite.ItemType.armor:
                list = armorItems;
                break;
            case Ite.ItemType.weapon:
                list = weaponItems;
                break;
            case Ite.ItemType.utility:
                list = utilItems;
                break;

        }

        if (!item.stackable)
        {
            list.Remove(item);
        }
        else if ((int)amount[item.name] > 1)
        {
            int num = (int)amount[item.name] - 1;
            amount[item.name] = num;
        }
        else if ((int)amount[item.name] == 1)
        {
            amount[item.name] = "0";
            list.Remove(item);
            amount.Remove(item.name);
        }
        //Debug.Log("item prefab:" + item.prefab);

        if (onItemChangedCallback != null)
            onItemChangedCallback.Invoke();
    }
    // Remove an item
    public void RemoveEquip(Ite item)
    {
        List<Ite> list = null;
        switch (item.type)
        {
            case Ite.ItemType.armor:
                list = armorItems;
                break;
            case Ite.ItemType.weapon:
                list = weaponItems;
                break;
            case Ite.ItemType.utility:
                list = utilItems;
                break;

        }

        if (!item.stackable)
        {
            list.Remove(item);
        }
        else if ((int)amount[item.name] > 1)
        {
            int num = (int)amount[item.name] - 1;
            amount[item.name] = num;
        }
        else if ((int)amount[item.name] == 1)
        {
            amount[item.name] = "0";
            list.Remove(item);
            amount.Remove(item.name);
        }
        //Debug.Log("item prefab:" + item.prefab);
        if (onItemChangedCallback != null)
            onItemChangedCallback.Invoke();
    }
}
