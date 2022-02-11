using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipManager : MonoBehaviour
{
    #region Singleton
    public static EquipManager instance;
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
}
