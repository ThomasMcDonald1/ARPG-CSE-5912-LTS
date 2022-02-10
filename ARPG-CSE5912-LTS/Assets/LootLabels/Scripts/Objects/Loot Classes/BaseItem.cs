namespace LootLabels {
    /// <summary>
    /// main item class, all items that inherit from this can go in a inventory array
    /// </summary>
    [System.Serializable]
    public class BaseItem : BaseObject {
        public ItemTypes ItemType;
        public Rarity ItemRarity;
        //other data like itemlevel, stats, etc ..
    }
}