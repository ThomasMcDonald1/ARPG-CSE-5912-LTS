using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Equipment", menuName = "Inventory/Equipment/Shield")]
public class ShieldEquipment : Equipment
{
    public int armor;
    public int blockChance;
}
