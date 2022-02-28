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
        base.Use();
        EquipManager.instance.Equip(this);
        RemoveFromInventory();
        EquipmentManager.instance.EquipItem(this.equipment);
    }
}
public enum EquipmentSlot { Head, Chest, Legs, Weapon, Shield, Feet }