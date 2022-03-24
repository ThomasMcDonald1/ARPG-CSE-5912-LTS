namespace LootLabels {
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    /// <summary>
    /// Base class for gear objects
    /// </summary>
    [System.Serializable]
    public class BaseGear : BaseItem {
        public GearTypes GearType;
        //List of stats, ...

        //public BaseGear() {
        //    ItemType = ItemTypes.Gear;
        //    GearType = GearTypes.Gloves;
        //    ItemRarity = Rarity.Poor;
        //    ItemName = ItemRarity + " " + GearType;
        //    ModelName = "LootLabels/3D models/Gloves1";
        //    IconName = "LootLabels/Icons/UI_Icon_InvGloves";
        //}

        public BaseGear(Rarity rarity, GearTypes gearType, string modelName, string iconName) {
            ItemType = ItemTypes.Gear;
            GearType = gearType;
            ItemRarity = rarity;
            ItemName = ItemRarity + " " + GearType;
            ModelName = modelName;
            IconName = iconName;
            Debug.Log("ItemName is " + ItemName);
        }
    }
}