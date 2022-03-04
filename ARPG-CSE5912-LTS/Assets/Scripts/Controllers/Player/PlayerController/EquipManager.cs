using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipManager : MonoBehaviour
{
    #region Singleton
    public static EquipManager instance;
    public CustomCharacter character;

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
            switch (oldItem.type)
            {
                case Ite.ItemType.armor:
                    Inventory.instance.Add(oldItem, Inventory.instance.armorItems);
                    break;
                case Ite.ItemType.weapon:
                    Inventory.instance.Add(oldItem, Inventory.instance.weaponItems);
                    break;

            }
        }
        currentEquipment[slotIndex] = newItem;

        // Equipment has been removed so we trigger the callback
        if (onEquipmentChanged != null)
            onEquipmentChanged.Invoke(null, oldItem);
    }

    public void Unequip(int slotIndex)
    {
        if (currentEquipment[slotIndex] != null)
        {
            Equipment oldItem = currentEquipment[slotIndex];
            switch (oldItem.type)
            {
                case Ite.ItemType.armor:
                    Inventory.instance.Add(oldItem, Inventory.instance.armorItems);
                    break;
                case Ite.ItemType.weapon:
                    Inventory.instance.Add(oldItem, Inventory.instance.weaponItems);
                    break;

            }

            currentEquipment[slotIndex] = null;
            EquipmentManager.instance.UnequipItem(oldItem.equipment, character);

            // Equipment has been removed so we trigger the callback
            if (onEquipmentChanged != null)
                onEquipmentChanged.Invoke(null, oldItem);

        }


    }
}