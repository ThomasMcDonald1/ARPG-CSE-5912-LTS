using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentUI : MonoBehaviour
{
    EquipSlot[] slots;
    EquipManager manager;
    void Start()
    {
        manager = EquipManager.instance;
        //inventory.onItemChangedCallback += UpdateUI;
        slots = this.gameObject.GetComponentsInChildren<EquipSlot>();
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
            if (manager.currentEquipment[i] != null)
            {
                Debug.Log("i is " + i);
                slots[i].AddItem(manager.currentEquipment[i]);

            }
            else
            {
                slots[i].ClearSlot();
            }
                ////Debug.Log("items.Count is " + inventory.items.Count);
                //if (i < inventory.items.Count)
                //{
                //    GameObject amount = slots[i].transform.GetChild(1).gameObject;
                //    TextMeshProUGUI text = amount.GetComponent<TextMeshProUGUI>();
                //    // Debug.Log(inventory.items[i].name + " index: " + i + " amount: " + inventory.amount[inventory.items[i]]);
                //    if (inventory.items[i].stackable)
                //    {
                //        // Debug.Log(inventory.items[i].name + " is " + inventory.items[i].stackable);


                //        text.SetText(inventory.amount[inventory.items[i]].ToString());

                //    }
                //    else
                //    {
                //        text.SetText("");
                //    }
                //    slots[i].AddItem(inventory.items[i]);
                //}
                //else
                //{
                //    slots[i].ClearSlot();
                //}
            }
    }
}
