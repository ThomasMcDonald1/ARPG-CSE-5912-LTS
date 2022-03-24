using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BattleDrakeStudios.ModularCharacters;
[CreateAssetMenu(fileName = "New Equipment", menuName = "Inventory/Equipment")]
public class Equipment : Ite
{
    public EquipmentSlot equipSlot;
    public Item equipment;

    //All equipment has the following:
    public int levelRequiredToEquip;
    public StatTypes statRequiredToEquip;
    public int statAmountRequired;

    //All equipment has the potential to have up to 5 additional random bonuses rolled when the equipment is created
    public List<Feature> features;

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
public enum EquipmentSlot { Head, Chest, Legs, MainHand, OffHand, Feet }