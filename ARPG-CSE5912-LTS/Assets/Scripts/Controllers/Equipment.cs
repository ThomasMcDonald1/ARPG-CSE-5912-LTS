using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Equipment", menuName = "Inventory/Equipment")]
public class Equipment : Ite
{
    public int armorModifier;
    public int damageModifier;
    public EquipmentSlot equipSlot;
    // Start is called before the first frame update
    public override void Use()
    {
        base.Use();
        EquipManager.instance.Equip(this);
        RemoveFromInventory();
    }
}
public enum EquipmentSlot { Head, Chest, Legs, Weapon, Shield, Feet }