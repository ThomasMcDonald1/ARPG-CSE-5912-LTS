using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    public Ite Sword;
    public PotionButton[] potionButtons;

    public bool loaded = true;
    

    private GameObject player;
    private GameObject potionSlots;
    private SaveSlot saveSlot;
   // [SerializeField] InventorySlot starterSwordInventorySlot;
    bool starterSwordEquipped = false;
    private bool tutorialNotSeen = true;

    private void Start()
    {
        amount = new Hashtable();
        player = GameObject.FindGameObjectWithTag("Player");
        potionSlots = GameObject.FindGameObjectWithTag("PotionSlot");
        potionButtons = potionSlots.GetComponentsInChildren<PotionButton>();

        var gameplayController = player.GetComponentInParent<GameplayStateController>();
        var slotNum = gameplayController.customCharacter.slotNum;
        Debug.Log("Inventory is trying to access save slot number " + slotNum);
        saveSlot = gameplayController.saveSlots[slotNum - 1];

        if (saveSlot.newGame)
        {
            Add(Sword, weaponItems);
            //WeaponEquipment wep = (WeaponEquipment)Sword;
            //string jwep = JsonUtility.ToJson(wep);
            //saveSlot.currentEquipment[0] = jwep;

            utilItems.Add(healthPotion);
            amount.Add(healthPotion.name, 3);
            Potion health = (Potion)healthPotion;
            string json = JsonUtility.ToJson(health);
            saveSlot.utilItems.Add(json);
            saveSlot.amount.Add(healthPotion.name, 3);

            saveSlot.newGame = false;
        }
    }

    public void LoadSaveData(SaveSlot slot)
    {
        saveSlot = slot;

        int wepCount = saveSlot.weaponItems.Count;
            for (int i = 0; i < wepCount; i++)
            {
                if (saveSlot.weaponItems[i].Contains("Shield") && loaded){
                    ShieldEquipment shield = ScriptableObject.CreateInstance<ShieldEquipment>();
                    JsonUtility.FromJsonOverwrite(saveSlot.weaponItems[i], shield);
                    Add(shield, weaponItems);
                }
                else if (loaded)
                {
                    WeaponEquipment wep = ScriptableObject.CreateInstance<WeaponEquipment>();
                    JsonUtility.FromJsonOverwrite(saveSlot.weaponItems[i], wep);
                    Add(wep, weaponItems);
                }

            }
        int armCount = saveSlot.armorItems.Count;
            for (int i = 0; i < armCount; i++)
            {
                if (saveSlot.armorItems[i].Contains("Jewelry") && loaded)
                {
                    JewelryEquipment jewelry = ScriptableObject.CreateInstance<JewelryEquipment>();
                    JsonUtility.FromJsonOverwrite(saveSlot.armorItems[i], jewelry);
                    Add(jewelry, armorItems);
                }
                else if(loaded)
                {
                    ArmorEquipment arm = ScriptableObject.CreateInstance<ArmorEquipment>();
                    JsonUtility.FromJsonOverwrite(saveSlot.armorItems[i], arm);
                    Add(arm, armorItems);
                }

            }
        int utCount = saveSlot.utilItems.Count;
        for (int i = 0; i < utCount; i++)
            {
            if (loaded)
            {
                Potion potion = ScriptableObject.CreateInstance<Potion>();
                JsonUtility.FromJsonOverwrite(saveSlot.utilItems[i], potion);
                Add(potion, utilItems);
            }


            }
        amount = new Hashtable(saveSlot.amount);
        loaded = false;

        //   // weaponItems = new List<Ite>(saveSlot.weaponItems);

        //if (saveSlot.armorItems != null && saveSlot.armorItems.Count > 0)
        //{

        //}
        //   // armorItems = new List<Ite>(saveSlot.armorItems);

        //if (saveSlot.utilItems != null && saveSlot.utilItems.Count > 0) { 
        //}
        //   // utilItems = new List<Ite>(saveSlot.utilItems);
    }

    public int space = 20;

    private void Update()
    {
        if (starterSwordInventorySlot.item == Sword && !starterSwordEquipped)
        {
            WeaponEquipment weapon = (WeaponEquipment)starterSwordInventorySlot.item;
            weapon.equipSlot = EquipmentSlot.MainHand;
            starterSwordInventorySlot.item.Use();
            starterSwordEquipped = true;
            saveSlot.usedSword = false;
        }
    }

    void UpdateSaveData(Ite item)
    {
        foreach(WeaponEquipment weapon in weaponItems.OfType<WeaponEquipment>())
        {

            string json = JsonUtility.ToJson(weapon);
            Debug.Log("json string is " + json);
            if (!saveSlot.weaponItems.Contains(json))
            {
                saveSlot.weaponItems.Add(json);
            }
        }

        foreach (ShieldEquipment shield in weaponItems.OfType<ShieldEquipment>())
        {

            string json = JsonUtility.ToJson(shield);
            Debug.Log("json string is " + json);
            if (!saveSlot.weaponItems.Contains(json))
            {
                saveSlot.weaponItems.Add(json);
            }

        }

        foreach (JewelryEquipment jewel in armorItems.OfType<JewelryEquipment>())
        {
            string json = JsonUtility.ToJson(jewel);
            Debug.Log("json string is " + json);
            if (!saveSlot.armorItems.Contains(json))
            {
                saveSlot.armorItems.Add(json);
            }

        }

        foreach (ArmorEquipment armor in armorItems.OfType<ArmorEquipment>())
        {
            string json = JsonUtility.ToJson(armor);
            if (!saveSlot.armorItems.Contains(json))
            {
                saveSlot.armorItems.Add(json);
            }
        }

        foreach(Potion util in utilItems)
        {
            string json = JsonUtility.ToJson(util);
            Debug.Log("json string is " + json);
            if (!saveSlot.utilItems.Contains(json))
            {
                saveSlot.utilItems.Add(json);
            }
        }
        saveSlot.amount = new Hashtable(amount);
        //saveSlot.weaponItems = new List<Ite>(weaponItems);
        //saveSlot.armorItems = new List<Ite>(armorItems);
        //saveSlot.utilItems = new List<Ite>(utilItems);
    }

    void UpdateSaveDataRemove(Ite item)
    {
        if (item.name.Contains("Jewelry"))
        {
            JewelryEquipment jewel = (JewelryEquipment)item;
            string json = JsonUtility.ToJson(jewel);
            saveSlot.armorItems.Remove(json);
        }
        else if (item.name.Contains("Shield"))
        {
            ShieldEquipment shield = (ShieldEquipment)item;
            string json = JsonUtility.ToJson(shield);
            saveSlot.weaponItems.Remove(json);
        }
        else if(item.type == Ite.ItemType.armor)
        {
            ArmorEquipment armor = (ArmorEquipment)item;
            string json = JsonUtility.ToJson(armor);
            saveSlot.armorItems.Remove(json);
        }
        else if(item.type == Ite.ItemType.weapon)
        {
            WeaponEquipment wep = (WeaponEquipment)item;
            string json = JsonUtility.ToJson(wep);
            saveSlot.weaponItems.Remove(json);
        }
        else
        {
            if (amount.ContainsKey(item.name))
            {

                saveSlot.amount[item.name] = amount[item.name];
            }
            else
            {
                saveSlot.amount.Remove(item.name);
                Potion pot = (Potion)item;
                string json = JsonUtility.ToJson(pot);
                saveSlot.utilItems.Remove(json);
            }
        }
        //foreach (WeaponEquipment weapon in weaponItems.OfType<WeaponEquipment>())
        //{

        //    string json = JsonUtility.ToJson(weapon);
        //    Debug.Log("json string is " + json);
        //    if (!saveSlot.weaponItems.Contains(json))
        //    {
        //        saveSlot.weaponItems.Add(json);
        //    }
        //    else
        //    {
        //        Debug.Log("calling remove");
        //        saveSlot.weaponItems.Remove(json);
        //    }
        //}

        //foreach (ShieldEquipment shield in weaponItems.OfType<ShieldEquipment>())
        //{

        //    string json = JsonUtility.ToJson(shield);
        //    Debug.Log("json string is " + json);
        //    if (!saveSlot.weaponItems.Contains(json))
        //    {
        //        saveSlot.weaponItems.Add(json);
        //    }
        //    else
        //    {
        //        Debug.Log("calling remove");
        //        saveSlot.weaponItems.Remove(json);
        //    }

        //}

        //foreach (JewelryEquipment jewel in armorItems.OfType<JewelryEquipment>())
        //{
        //    string json = JsonUtility.ToJson(jewel);
        //    Debug.Log("json string is " + json);
        //    if (!saveSlot.armorItems.Contains(json))
        //    {
        //        saveSlot.armorItems.Add(json);
        //    }
        //    else
        //    {
        //        Debug.Log("calling remove");
        //        saveSlot.armorItems.Remove(json);
        //    }

        //}

        //foreach (ArmorEquipment armor in armorItems.OfType<ArmorEquipment>())
        //{
        //    string json = JsonUtility.ToJson(armor);
        //    if (!saveSlot.armorItems.Contains(json))
        //    {
        //        saveSlot.armorItems.Add(json);
        //    }
        //    else
        //    {
        //        Debug.Log("calling remove");
        //        saveSlot.armorItems.Remove(json);
        //    }
        //}

        //foreach (Potion util in utilItems)
        //{
        //    string json = JsonUtility.ToJson(util);
        //    Debug.Log("json string is " + json);
        //    if (!saveSlot.utilItems.Contains(json))
        //    {
        //        saveSlot.utilItems.Add(json);
        //    }
        //    else
        //    {
        //        saveSlot.utilItems.Remove(json);
        //    }
        //}
        //saveSlot.amount = new Hashtable(amount);
        //saveSlot.weaponItems = new List<Ite>(weaponItems);
        //saveSlot.armorItems = new List<Ite>(armorItems);
        //saveSlot.utilItems = new List<Ite>(utilItems);
    }
    // Add a new item if enough room
    public void Add(Ite item, List<Ite> list)
    {
        if (tutorialNotSeen && (list == weaponItems || list == armorItems))
        {
            TutorialWindow.Instance.text.text = TutorialWindow.Instance.openPanelsTutorial;
            TutorialWindow.Instance.ShowCanvas();
            tutorialNotSeen = false;
        }
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

        UpdateSaveData(item);
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

        UpdateSaveDataRemove(item);
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

        UpdateSaveDataRemove(item);
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

        UpdateSaveDataRemove(item);
    }
}
