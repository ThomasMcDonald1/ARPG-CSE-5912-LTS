using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Equipment", menuName = "Inventory/Equipment/Weapon")]

public class WeaponEquipment : Equipment
{
    public int attackRange;
    public int minDamage;
    public int maxDamage;
    public int attackSpeedMod;
    public int critChanceBonus;
}
