using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryUI : MonoBehaviour
{
    public Transform itemsParent;
    Inventory inventory;
    // Start is called before the first frame update
    InventorySlot[] slots;
    void Start()
    {
        inventory = Inventory.instance;
        //inventory.onItemChangedCallback += UpdateUI;
        slots = itemsParent.GetComponentsInChildren<InventorySlot>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateUI();

    }
    void UpdateUI()
    {
        //for(int i = 0; i < inventory.items.Count; i++)
        //{

        //}
        for (int i = 0; i < slots.Length; i++)
        {
            //Debug.Log("items.Count is " + inventory.items.Count);
            if (i < inventory.weaponItems.Count)
            {
                GameObject amount = slots[i].transform.GetChild(1).gameObject;
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
                slots[i].AddItem(inventory.weaponItems[i]);
            }
            else
            {
                slots[i].ClearSlot();
            }
        }
    }
}
