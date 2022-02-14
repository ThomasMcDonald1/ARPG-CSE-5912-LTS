using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryUI : MonoBehaviour
{
    public Transform weaponsParent;
    public Transform armorsParent;
    public Transform utilsParent;
    Inventory inventory;
    // Start is called before the first frame update
    InventorySlot[] weaponSlots;
    InventorySlot[] armorSlots;
    InventorySlot[] utilSlots;
    void Start()
    {
        inventory = Inventory.instance;
        //inventory.onItemChangedCallback += UpdateUI;
        weaponSlots = weaponsParent.GetComponentsInChildren<InventorySlot>();
        armorSlots = armorsParent.GetComponentsInChildren<InventorySlot>();
        utilSlots = utilsParent.GetComponentsInChildren<InventorySlot>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateUI();

    }
    void UpdateUI()
    {
        for (int i = 0; i < weaponSlots.Length; i++)
        {
            //Debug.Log("items.Count is " + inventory.items.Count);
            if (i < inventory.weaponItems.Count)
            {
                GameObject amount = weaponSlots[i].transform.GetChild(1).gameObject;
                TextMeshProUGUI text = amount.GetComponent<TextMeshProUGUI>();
                // Debug.Log(inventory.items[i].name + " index: " + i + " amount: " + inventory.amount[inventory.items[i]]);
                if (inventory.weaponItems[i].stackable)
                {
                    // Debug.Log(inventory.items[i].name + " is " + inventory.items[i].stackable);


                    text.SetText(inventory.amount[inventory.weaponItems[i]].ToString());

                }
                else
                {
                    text.SetText("");
                }
                weaponSlots[i].AddItem(inventory.weaponItems[i]);
            }
            else
            {
                weaponSlots[i].ClearSlot();
            }
        }

        for (int i = 0; i < armorSlots.Length; i++)
        {
            //Debug.Log("items.Count is " + inventory.items.Count);
            if (i < inventory.armorItems.Count)
            {
                GameObject amount = armorSlots[i].transform.GetChild(1).gameObject;
                TextMeshProUGUI text = amount.GetComponent<TextMeshProUGUI>();
                // Debug.Log(inventory.items[i].name + " index: " + i + " amount: " + inventory.amount[inventory.items[i]]);
                if (inventory.armorItems[i].stackable)
                {
                    // Debug.Log(inventory.items[i].name + " is " + inventory.items[i].stackable);
                    text.SetText(inventory.amount[inventory.armorItems[i]].ToString());

                }
                else
                {
                    text.SetText("");
                }
                armorSlots[i].AddItem(inventory.armorItems[i]);
            }
            else
            {
                armorSlots[i].ClearSlot();
            }
        }

        for (int i = 0; i < utilSlots.Length; i++)
        {
            //Debug.Log("items.Count is " + inventory.items.Count);
            if (i < inventory.utilItems.Count)
            {
                GameObject amount = utilSlots[i].transform.GetChild(1).gameObject;
                TextMeshProUGUI text = amount.GetComponent<TextMeshProUGUI>();
                // Debug.Log(inventory.items[i].name + " index: " + i + " amount: " + inventory.amount[inventory.items[i]]);
                if (inventory.utilItems[i].stackable)
                {
                    // Debug.Log(inventory.items[i].name + " is " + inventory.items[i].stackable);


                    text.SetText(inventory.amount[inventory.utilItems[i]].ToString());

                }
                else
                {
                    text.SetText("");
                }
                utilSlots[i].AddItem(inventory.utilItems[i]);
            }
            else
            {
                utilSlots[i].ClearSlot();
            }
        }
    }
}
