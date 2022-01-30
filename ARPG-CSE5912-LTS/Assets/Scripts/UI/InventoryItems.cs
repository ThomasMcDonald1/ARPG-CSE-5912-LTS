using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItems
{
   public enum ItemType
    {
        Sword,
        HealthPotion,
        ManaPotion,
        Coin
    }
    public ItemType itemType;
    public int amount;
}
