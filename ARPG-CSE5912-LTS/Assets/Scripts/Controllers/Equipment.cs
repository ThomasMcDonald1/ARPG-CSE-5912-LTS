using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BattleDrakeStudios.ModularCharacters;
[CreateAssetMenu(fileName = "New Equipment", menuName = "Inventory/Equipment")]
public class Equipment : Ite
{
    public int armorModifier;
    public int damageModifier;
    public EquipmentSlot equipSlot;
    public Item equipment;
    // Start is called before the first frame update
    public override void Use()
    {
        Debug.Log("equipment is " + equipment);
        base.Use();
        EquipManager.instance.Equip(this);
        if (this.equipment != null)
        {
            EquipmentManager.instance.EquipItem(this.equipment);
        }
        RemoveFromInventory();
    }
}
public enum EquipmentSlot { Head, Chest, Legs, Weapon, Shield, Feet }