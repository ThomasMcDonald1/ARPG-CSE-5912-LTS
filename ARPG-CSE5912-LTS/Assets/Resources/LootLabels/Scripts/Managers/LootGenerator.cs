using UnityEngine;

namespace LootLabels {
    public class LootGenerator : MonoBehaviour {

        /// <summary>
        /// Return a random value for dropped currency
        /// </summary>
        /// <returns></returns>
        public int CalculateCurrencyAmount() {
            int seed = Random.Range(1, 5);
            int randomValue;
            switch (seed) {
                case 1:
                    randomValue = Random.Range(1, 99);
                    break;
                case 2:
                    randomValue = Random.Range(100, 999);
                    break;
                case 3:
                    randomValue = Random.Range(1000, 9999);
                    break;
                case 4:
                    randomValue = Random.Range(10000, 99999);
                    break;
                default:
                    randomValue = 0;
                    break;
            }

            return randomValue;
        }

        /// <summary>
        /// Checks the source of the loot and returns the amount of items it can drop
        /// </summary>
        /// <param name="lootSource"></param>
        /// <returns></returns>
        public int CalculateLootAmount(LootSource lootSource) {
            switch (lootSource) {
                case LootSource.Normal:
                    return Random.Range(2, 3);
                case LootSource.Elite:
                    return Random.Range(4, 6);
                case LootSource.Boss:
                    return Random.Range(7, 10);
                default:
                    Debug.Log("no lootsource");
                    return 0;
            }
        }

        /// <summary>
        /// Check amount of loot types and pick a random one
        /// </summary>
        /// <returns></returns>
        public LootTypes SelectRandomLootType() {
            int lootTypesCount = System.Enum.GetNames(typeof(LootTypes)).Length;
            int randomIndex = Random.Range(0, lootTypesCount);
            return (LootTypes)randomIndex;
        }

        /// <summary>
        /// Check amount of item types and pick a random one
        /// </summary>
        /// <returns></returns>
        public ItemTypes SelectRandomItemType() {
            int itemTypesCount = System.Enum.GetNames(typeof(ItemTypes)).Length;
            int randomIndex = Random.Range(0, itemTypesCount);
            return (ItemTypes)randomIndex;
        }

        /// <summary>
        /// Check amount of gear types and pick a random one
        /// </summary>
        /// <returns></returns>
        public GearTypes SelectRandomGearType() {
            int gearTypeCount = System.Enum.GetNames(typeof(GearTypes)).Length;
            Debug.Log("gearTypeCount is " + gearTypeCount);
            int randomIndex = Random.Range(0, gearTypeCount);

            return (GearTypes)randomIndex;
        }

        /// <summary>
        /// Check amount of rarities pick a random one
        /// </summary>
        /// <returns></returns>
        // <=20 poor Item
        // <= 60 Normal Item
        // <= 80 Rare Item
        // <= 95 Epic Item
        // <= 100 Alpha Item
        public virtual Rarity SelectRandomRarity(Type type) {
           // int rarityCount = System.Enum.GetNames(typeof(Rarity)).Length;
            
            Rarity rare = Rarity.Poor;
            switch (type)
            {
                case Type.Poor:
                    if (Random.value < 0.1)
                    {
                        rare = Rarity.Normal;
                    };
                    break;
                case Type.Normal:
                    if (Random.value < 0.80)
                    {
                        rare = Rarity.Normal;
                    }
                    else if (Random.value > 0.90)
                    {
                        rare = Rarity.Rare;
                    }
                    break;
                case Type.Rare:
                    if (Random.value < 0.85)
                    {
                        rare = Rarity.Rare;
                    }
                    else if (Random.value > 0.95)
                    {
                        rare = Rarity.Epic;
                    }
                    else
                    {
                        rare = Rarity.Normal;
                    }
                    break;
                case Type.Epic:
                    if (Random.value < 0.98)
                    {
                        rare = Rarity.Epic;
                    }
                    else
                    {
                        rare = Rarity.Legendary;
                    }
                    break;
                case Type.Legendary:
                    rare = Rarity.Legendary;
                    break;
                //case Type.SuperUltraHyperExPlusAlpha:
                //    rare = Rarity.SuperUltraHyperExPlusAlpha;
                //    break;
                default:
                    rare = Rarity.Poor;
                    break;
            }
            return rare;
        }

        /// <summary>
        /// Check amount of currency types and pick a random one
        /// </summary>
        /// <returns></returns>
        public CurrencyTypes SelectRandomCurrency() {
            int currencyTypesCount = System.Enum.GetNames(typeof(CurrencyTypes)).Length;
            int randomIndex = Random.Range(0, currencyTypesCount);

            return (CurrencyTypes)randomIndex;
        }

        /// <summary>
        /// Example of a stat roll based on the rarity of an item
        /// </summary>
        /// <param name="itemRarity"></param>
        /// <returns></returns>
        public double RollAmountOfStats(Rarity itemRarity, Ite item, GearTypes gearType) {
            double statAmount = 1;

            switch (itemRarity)
            {
                case Rarity.Poor:
                    statAmount = 0.7;
                    break;
                case Rarity.Normal:
                    statAmount = 1;
                    break;
                case Rarity.Rare:
                    statAmount = 2;
                    break;
                case Rarity.Epic:
                    statAmount = 4;
                    break;
                case Rarity.Legendary:
                    statAmount = 1;
                    break;
                //case Rarity.Set:
                //    statAmount = 6;
                    //break;
                //case Rarity.SuperUltraHyperExPlusAlpha:
                //    statAmount = 7;
                //    break;
                default:
                    break;
            }

            return statAmount;
        }

        /// <summary>
        /// Create a randomized gear item
        /// </summary>
        /// <returns></returns>
        public BaseGear CreateGear(Type type) {
            Debug.Log("Create Gear with GetModelName has been run");
            Rarity itemRarity = SelectRandomRarity(type);
            GearTypes gearType = SelectRandomGearType();
            string modelName = ResourceManager.singleton.GetModelName(gearType, itemRarity);
            string iconName = ResourceManager.singleton.GetIconName(gearType);

            BaseGear gear = new BaseGear(itemRarity, gearType, modelName, iconName);
            Debug.Log("creategear is making " + itemRarity + " " + modelName);
            return gear;
        }

        /// <summary>
        /// Create a randomized currency item
        /// </summary>
        /// <returns></returns>
        public BaseCurrency CreateCurrency() {
            CurrencyTypes currencyType = SelectRandomCurrency();
            int amount = CalculateCurrencyAmount();
            string modelName = ResourceManager.singleton.GetModelName(currencyType);
            string iconName = ResourceManager.singleton.GetIconName(currencyType);

            BaseCurrency currency = new BaseCurrency(currencyType, amount, modelName, iconName);
            return currency;
        }
    }
}