using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BattleDrakeStudios.ModularCharacters;
[CreateAssetMenu(fileName = "New Equipment", menuName = "Inventory/Equipment")]
public class Equipment : Ite
{
    public EquipmentSlot equipSlot;
    public Item equipment;
    //public Stats playerStat;

    //All equipment has the following:
    public int levelRequiredToEquip;
    // public StatTypes statRequiredToEquip;
    // public int statAmountRequired;
    // public int attackRange;
    // public int damage;
    // public int attackSpeedMod;
    // public int critChanceBonus;
    // public int defendRate;
    // public int defense;
    // public int evasion;
    [SerializeField] public PrefixSuffix prefix;
    public PrefixSuffix suffix;

    // Start is called before the first frame update
    public override void Use()
    {
        //Debug.Log("equipment is " + equipment);
        base.Use();
        EquipManager.instance.Equip(this);
        if (this.equipment != null)
        {
            EquipmentManager.instance.EquipItem(this.equipment);
        }
        RemoveFromInventory();
    }
}
public enum EquipmentSlot { MainHand, OffHand, Head, Chest, Feet, Jewelry }