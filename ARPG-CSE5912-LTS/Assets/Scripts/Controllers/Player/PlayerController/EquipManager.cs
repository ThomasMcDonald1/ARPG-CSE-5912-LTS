using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipManager : MonoBehaviour
{
    #region Singleton
    public static EquipManager instance;

    public delegate void OnEquipmentChanged(Equipment newItem, Equipment oldItem);
    public event OnEquipmentChanged onEquipmentChanged;
    private void Awake()
    {
        instance = this;
    }
    #endregion
    public Equipment[] currentEquipment;
    Inventory inventory;
    private void Start()
    {
        inventory = Inventory.instance;
        int numSlots = System.Enum.GetNames(typeof(EquipmentSlot)).Length;
        Debug.Log("numSlot is " + numSlots);
        currentEquipment = new Equipment[numSlots];
    }

    public void Equip(Equipment newItem)
    {
        int slotIndex = (int)newItem.equipSlot;
        Debug.Log("slotnIndex is " + slotIndex);
        Equipment oldItem = null;
        if (currentEquipment[slotIndex] != null)
        {
            oldItem = currentEquipment[slotIndex];
            Debug.Log("oldItem is " + oldItem.name);
            inventory.Add(oldItem);
        }
        currentEquipment[slotIndex] = newItem;
    }

    public void Unequip(int slotIndex)
    {
        if (currentEquipment[slotIndex] != null)
        {
            Equipment oldItem = currentEquipment[slotIndex];
            inventory.Add(oldItem);

            currentEquipment[slotIndex] = null;


            // Equipment has been removed so we trigger the callback
            if (onEquipmentChanged != null)
                onEquipmentChanged.Invoke(null, oldItem);

        }


    }
}
