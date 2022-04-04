namespace LootLabels {

    //When expanding the system you'll probably have to distinguish alot more objects types, for now we only auto generate loot
    public enum ObjectTypes {
        Loot,
        //Npc,..
    }

    //types of loot
    //items is everything that can go in an inventory array
    //gold is a currency, so not the same as items
    public enum LootTypes {
        Currency,
        Items,
        //Crafting, ...
    }

    //the different groups of items that go in the inventory
    public enum ItemTypes {
        Gear,
      //  Potions,
        //Weapons,
        //weapons, potions
    }

    //types of gear
    //each item is a separate gear slot, if you need more types that can go in a single slot add another enum
    public enum GearTypes {
        Dagger,
        Sword,
        TwoHandedSword,
        Shield,
        Helm,
        Chest,
        Boots,
        Jewelry,
        HealthPotion,
    }

    //types of currency
    //this is just a number somewhere in your characters UI
    public enum CurrencyTypes {
        Gold,
        //Other currencies
    }

    //assign to objects that drop loot to determine loot amounts
    public enum LootSource {
        Normal,
        Elite,
        Boss,
    }
    public enum Type
    {
        Poor,
        Normal,
        Rare,
        Epic,
        Legendary,
      //  Set,
       // SuperUltraHyperExPlusAlpha,
    }
    //The rarity of your objects, also used to determine label color
    public enum Rarity {
        Poor,
        Normal,
        Rare,
        Epic,
        Legendary,
       // Set,
       // SuperUltraHyperExPlusAlpha,
    }

    /// <summary>
    /// The state of the label game object
    /// Visible to the camera, not visible to camera or disabled
    /// </summary>
    public enum LabelStates {
        InCameraFrustum,
        OutCameraFrustum,
        Inactive
    }

    /// <summary>
    /// The state of the text and the background
    /// Normal is opaque, hidden is transparent
    /// </summary>
    public enum VisibilityState {
        Normal,
        Hidden
    }
}