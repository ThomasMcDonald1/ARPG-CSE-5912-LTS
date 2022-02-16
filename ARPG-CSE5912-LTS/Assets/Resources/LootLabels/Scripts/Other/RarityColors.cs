using UnityEngine;

namespace LootLabels {
    /// <summary>
    /// Script returns the color for the given rarity
    /// </summary>
    public class RarityColors : MonoBehaviour {
        public Color Poor;
        public Color Normal;
        public Color Rare;
        public Color Epic;
        public Color Legendary;
        public Color Set;
        public Color Ultra;

        public Color ReturnRarityColor(Rarity itemRarity) {
            Color rarityColor = default(Color);

            switch (itemRarity) {
                case Rarity.Poor:
                    rarityColor = Poor;
                    break;
                case Rarity.Normal:
                    rarityColor = Normal;
                    break;
                case Rarity.Rare:
                    rarityColor = Rare;
                    break;
                case Rarity.Epic:
                    rarityColor = Epic;
                    break;
                case Rarity.Legendary:
                    rarityColor = Legendary;
                    break;
                case Rarity.Set:
                    rarityColor = Set;
                    break;
                case Rarity.SuperUltraHyperExPlusAlpha:
                    rarityColor = Ultra;
                    break;
                default:
                    rarityColor = Color.white;
                    break;
            }

            return rarityColor;
        }
    }
}