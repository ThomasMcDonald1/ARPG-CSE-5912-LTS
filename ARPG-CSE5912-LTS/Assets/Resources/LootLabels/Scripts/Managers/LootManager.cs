using System;
using System.Collections;
using UnityEngine;

namespace LootLabels
{
    /// <summary>
    /// Manager takes care of creating loot
    /// </summary>
    public class LootManager : MonoBehaviour
    {

        public static LootManager singleton = null;

        public Transform LootParent;    //all dropped loot goes in this parent

        LootGenerator lootGenerator;
        RarityColors rarityColors;
        [SerializeField] GameObject featureTablesGeneratorGO;
        FeatureTablesGenerator featureTablesGenerator;

        public RarityColors RarityColors
        {
            get
            {
                return rarityColors;
            }

            set
            {
                rarityColors = value;
            }
        }
        public LootGenerator LootGenerator
        {
            get
            {
                return lootGenerator;
            }

            set
            {
                lootGenerator = value;
            }
        }

        void Awake()
        {
            //Check if instance already exists
            if (singleton == null)
            {
                //if not, set instance to this
                singleton = this;
            }

            //If instance already exists and it's not this:
            else if (singleton != this)
            {
                //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
                Destroy(gameObject);
            }

            LootGenerator = GetComponent<LootGenerator>();
            RarityColors = GetComponent<RarityColors>();
            featureTablesGenerator = GetComponentInParent<GameplayStateController>().GetComponentInChildren<FeatureTablesGenerator>();
        }

        //Depending on the loot source calculate the amounts of loot, instantiate at the position of the loot source 
        public void DropLoot(LootSource lootSource, Transform objectTransform, LootType type)
        {
            int itemAmount = LootGenerator.CalculateLootAmount(lootSource);

            //if you want to use animations on loot, you need to put it in a empty parent object, which is located at the spawn location otherwise the position of the animation is completely wrong
            //this might not be needed by setting root motion or something like that, need to look into it
            GameObject lootOrigin = null;

            //if (LootParent.childCount > 0) {
            //    for (int i = 0; i < LootParent.childCount; i++) {
            //        if (LootParent.GetChild(i).childCount == 0) {
            //            //use this instead of instantiating a new origin
            //            lootOrigin = LootParent.GetChild(i).gameObject;
            //            lootOrigin.transform.SetPositionAndRotation(objectTransform.position, objectTransform.rotation);
            //            break;
            //        }

            //        if (i == LootParent.childCount - 1) {
            //            lootOrigin = Instantiate(Resources.Load("LootLabels/3D models/LootOrigin", typeof(GameObject)), objectTransform.position, objectTransform.rotation) as GameObject;
            //            Debug.Log("lootOrigin is " + lootOrigin);
            //            lootOrigin.transform.SetParent(LootParent, true);
            //        }
            //    }
            //}
            //else {
            lootOrigin = Instantiate(Resources.Load("LootLabels/3D models/LootOrigin", typeof(GameObject)), objectTransform.position, objectTransform.rotation) as GameObject;
            lootOrigin.transform.SetParent(LootParent, true);
            //}

            StartCoroutine(DropLootCoroutine(lootOrigin.transform, itemAmount, type));
        }

        //if you want a delay on each item that spawns, use this
        IEnumerator DropLootCoroutine(Transform lootOrigin, int amount, LootType type)
        {
            int i = amount;

            while (i != 0)
            {
                i--;
                yield return new WaitForSeconds(.2f);
                GenerateLoot(lootOrigin, type);
            }
            int random = UnityEngine.Random.Range(0, 3);
            for (int j = 0; j < random; j++)
            {
                Debug.Log("generating health potions");
                yield return new WaitForSeconds(.2f);
                GenerateHealthPotions(lootOrigin, type);
            }
            GameObject lootdrop = GameObject.Find("LootDrops");
        }

        //Choses which type of loot will drop.
        //currency, items, spellbooks, ...
        void GenerateLoot(Transform lootOrigin, LootType type)
        {
            LootTypes lootType = LootGenerator.SelectRandomLootType();

            switch (lootType)
            {
                case LootTypes.Currency:
                    ChooseCurrency(lootOrigin, type);
                    break;
                case LootTypes.Items:
                    ChooseItemType(lootOrigin, type);
                    break;
                default:
                    Debug.Log("loottype doesn't exist");
                    break;
            }
        }

        /// <summary>
        /// Choses a currency to drop and creates it
        /// </summary>
        /// <param name="lootOrigin"></param>
        void ChooseCurrency(Transform lootOrigin, LootType type)
        {
            BaseCurrency currency = LootGenerator.CreateCurrency(type);

            GameObject droppedItem = Instantiate(Resources.Load(currency.ModelName, typeof(GameObject)), transform.position, Quaternion.Euler(0, 0, 0), lootOrigin) as GameObject;
            droppedItem.GetComponent<DroppedCurrency>().currency = currency;
        }
        void GenerateHealthPotions(Transform lootOrigin, LootType type)
        {
            int randomPotion = UnityEngine.Random.Range(0, 5);
            GameObject droppedItem;
            Rarity itemRarity;
            GearTypes gearType;
            Debug.Log("randPotion is " + randomPotion);
            switch (randomPotion)
            {
               //case 1:
               //     droppedItem = Instantiate(Resources.Load("LootLabels/3D models/DefensePotion", typeof(GameObject)), transform.position, Quaternion.Euler(0, 0, 0), lootOrigin) as GameObject;
               //     itemRarity = LootGenerator.SelectRandomRarity(type);
               //     gearType = GearTypes.DefensePotion;
               //     break;
               // case 2:
               //     droppedItem = Instantiate(Resources.Load("LootLabels/3D models/ManaPotion", typeof(GameObject)), transform.position, Quaternion.Euler(0, 0, 0), lootOrigin) as GameObject;
               //     itemRarity = LootGenerator.SelectRandomRarity(type);
               //     gearType = GearTypes.ManaPotion;
               //     break;
               //case 3:
               //     droppedItem = Instantiate(Resources.Load("LootLabels/3D models/SpeedPotion", typeof(GameObject)), transform.position, Quaternion.Euler(0, 0, 0), lootOrigin) as GameObject;
               //     itemRarity = LootGenerator.SelectRandomRarity(type);
               //     gearType = GearTypes.SpeedPotion;
               //     break;
                default:
                    droppedItem = Instantiate(Resources.Load("LootLabels/3D models/DefensePotion", typeof(GameObject)), transform.position, Quaternion.Euler(0, 0, 0), lootOrigin) as GameObject;
                    itemRarity = LootGenerator.SelectRandomRarity(type);
                    gearType = GearTypes.HealthPotion;
                    break;

            }

            string modelName = ResourceManager.singleton.GetModelName(gearType, itemRarity);
            string iconName = ResourceManager.singleton.GetIconName(gearType);

            BaseGear gear = new BaseGear(itemRarity, gearType, modelName, iconName);
            Debug.Log("creategear is making " + itemRarity + iconName);
            if (droppedItem.GetComponent<ItemPickup>() != null)
            {
                Ite item = Instantiate(droppedItem.GetComponent<ItemPickup>().item);
                RollStatsForItems(gear.ItemRarity, item, gear.GearType);
                gear.ItemName = gear.ItemRarity + " " + gear.ItemName;
                item.name = gear.ItemName;
                item.itemNameColor = singleton.RarityColors.ReturnRarityColor(gear.ItemRarity);
            }
            droppedItem.GetComponent<DroppedGear>().gear = gear;
        }
        /// <summary>
        /// Choses a item type to drop and creates it
        /// </summary>
        /// <param name="lootOrigin"></param>
        void ChooseItemType(Transform lootOrigin, LootType type)
        {

            ItemTypes itemType = LootGenerator.SelectRandomItemType();
            Debug.Log("itemType is " + itemType);
            switch (itemType)
            {
                case ItemTypes.Gear:
                    BaseGear gear = LootGenerator.CreateGear(type);
                    Debug.Log("gear.ModelName is " + gear.ModelName);

                    GameObject droppedItem = Instantiate(Resources.Load(gear.ModelName, typeof(GameObject)), transform.position, Quaternion.Euler(0, 0, 0), lootOrigin) as GameObject;
                    droppedItem.GetComponent<DroppedGear>().gear = gear;

                    if (droppedItem.GetComponent<ItemPickup>() != null)
                    {
                        Ite item = Instantiate(droppedItem.GetComponent<ItemPickup>().item);
                        Debug.Log("item is " + item);
                        // Potion potion = (Potion)droppedItem.GetComponent<ItemPickup>().item;
                        //if (potion != null)
                        //    Debug.Log("dropped item health amount after rollstatsforitems is now" + potion.health);
                        /*droppedItem.GetComponent<ItemPickup>().item =*/
                        RollStatsForItems(gear.ItemRarity, item, gear.GearType);
                        // Equipment equipment = (Equipment)item;

                        //if (item.type == Ite.ItemType.utility){
                        //    Potion potion = (Potion)item;
                        //   // Debug.Log("dropped item health amount after rollstatsforitems is now" + potion.health);
                        //}                        
                        //else
                        if (droppedItem.GetComponent<ItemPickup>().item.type != Ite.ItemType.utility)
                        { 
                            Equipment equipment = (Equipment)item;
                            PrefixSuffix prefix = featureTablesGenerator.prefixTables.GetRandomPrefixForRarityAndGearType(gear.ItemRarity, gear.GearType);
                            equipment.prefix = prefix;
                            PrefixSuffix suffix = featureTablesGenerator.suffixTables.GetRandomSuffixForRarityAndGearType(gear.ItemRarity, gear.GearType);
                            equipment.suffix = suffix;
                            
                            foreach (GameObject featureGO in prefix.FeaturesGOs) {
                                Feature feature = featureGO.GetComponent<Feature>();
                                Type typeFeature = feature.GetType();
                                if (typeFeature == typeof(FlatStatModifierFeature))
                                {
                                    FlatStatModifierFeature flatStat = (FlatStatModifierFeature)feature;

                                    flatStat.flatAmount = RollStatsForFeatures(gear.ItemRarity, flatStat.type);
                                }
                                else
                                {
                                    PercentStatModifierFeature percentStat = (PercentStatModifierFeature)feature;
                                    percentStat.percentAmount = RollStatsForPercent(gear.ItemRarity);
                                }
                            }
                            foreach (GameObject featureGO in suffix.FeaturesGOs)
                            {
                                Feature feature = featureGO.GetComponent<Feature>();
                                Type typeFeature = feature.GetType();
                                if (typeFeature == typeof(FlatStatModifierFeature))
                                {
                                    FlatStatModifierFeature flatStat = (FlatStatModifierFeature)feature;
                                    flatStat.flatAmount = RollStatsForFeatures(gear.ItemRarity, flatStat.type);
                                }
                                else
                                {
                                    PercentStatModifierFeature percentStat = (PercentStatModifierFeature)feature;
                                    percentStat.percentAmount = RollStatsForPercent(gear.ItemRarity);
                                }

                            }
                            gear.ItemName = prefix.Name + gear.ItemName + suffix.Name;
                            droppedItem.GetComponent<ItemPickup>().item = equipment;
                        }
                        else
                        {
                            gear.ItemName = gear.ItemRarity + " " + gear.ItemName;
                        }
                        item.name = gear.ItemName;
                        item.itemNameColor = singleton.RarityColors.ReturnRarityColor(gear.ItemRarity);
                        //Debug.Log("the name of the item is now" + droppedItem.GetComponent<ItemPickup>().item.name);
                    }
                    break;
                default:
                    Debug.Log("Itemtype not yet implemented");
                    break;
            }
        }

        private int RollStatsForFeatures(Rarity ItemRarity, StatTypes type)
        {
            int stat = 1;
            if (type == StatTypes.PercentArmorBonus || type == StatTypes.CritChance || type == StatTypes.CritDamage || type == StatTypes.PercentArmorPen
                || type == StatTypes.PercentMagicPen || type == StatTypes.RunSpeed || type == StatTypes.ExpGainMod || type == StatTypes.PhysDmgBonus
                || type == StatTypes.MagDmgBonus || type == StatTypes.FireDmgBonus || type == StatTypes.ColdDmgBonus || type == StatTypes.LightningDmgBonus
                || type == StatTypes.PoisonDmgBonus || type == StatTypes.Lifesteal || type == StatTypes.CastSpeed || type == StatTypes.CooldownReduction
                || type == StatTypes.CostReduction || type == StatTypes.BlockChance || type == StatTypes.Evasion || type == StatTypes.PercentAllResistBonus
                || type == StatTypes.PercentFireResistBonus || type == StatTypes.PercentColdResistBonus || type == StatTypes.PercentLightningResistBonus
                || type == StatTypes.PercentPoisonResistBonus)
            {
                switch (ItemRarity)
                {
                    case Rarity.Poor:
                        stat = 0;
                        break;
                    case Rarity.Normal:
                        stat = UnityEngine.Random.Range(0, 3);
                        break;
                    case Rarity.Rare:
                        stat = UnityEngine.Random.Range(3, 5);
                        break;
                    case Rarity.Epic:
                        stat = UnityEngine.Random.Range(5, 8);
                        break;
                    case Rarity.Legendary:
                        stat = UnityEngine.Random.Range(8, 11);
                        break;

                }
            }
            else
            {
                switch (ItemRarity)
                {
                    case Rarity.Poor:
                        stat = 0;
                        break;
                    case Rarity.Normal:
                        stat = UnityEngine.Random.Range(1, 5);
                        break;
                    case Rarity.Rare:
                        stat = UnityEngine.Random.Range(6, 11);
                        break;
                    case Rarity.Epic:
                        stat = UnityEngine.Random.Range(13, 18);
                        break;
                    case Rarity.Legendary:
                        stat = UnityEngine.Random.Range(21, 26);
                        break;

                }
            }

            return stat;

        }

        private int RollStatsForPercent(Rarity ItemRarity)
        {
            int stat = 1;
            switch (ItemRarity)
            {
                case Rarity.Poor:
                    stat = UnityEngine.Random.Range(1, 5);
                    break;
                case Rarity.Normal:
                    stat = UnityEngine.Random.Range(6, 11);
                    break;
                case Rarity.Rare:
                    stat = UnityEngine.Random.Range(13, 18);
                    break;
                case Rarity.Epic:
                    stat = UnityEngine.Random.Range(21, 26);
                    break;
                case Rarity.Legendary:
                    stat = UnityEngine.Random.Range(28, 33);
                    break;

            }
            return stat;
        }

        private void RollStatsForItems(Rarity itemRarity, Ite item, GearTypes gearType)
        {
            //Ite ite = Instantiate(item);
            double multiplier = LootGenerator.RollAmountOfStats(itemRarity, item, gearType);
            if (string.Equals("Health Potion", item.name))
            {
                Potion potion = (Potion)item;
                Debug.Log("potion health before change is" + potion.health);
                potion.health = (int)(potion.health + (250 * multiplier));
                string name = itemRarity.ToString() + " " + potion.name;
                potion.name = name;
                Debug.Log("health potion health amount is " + potion.health);
                Debug.Log("potion.name is " + potion.name);
                item = potion;
            }
            else if(string.Equals("Mana Potion", item.name))
            {
                Potion potion = (Potion)item;
                Debug.Log("potion health before change is" + potion.health);
                potion.mana = (int)(potion.mana + (250 * multiplier));
                string name = itemRarity.ToString() + " " + potion.name;
                potion.name = name;
                item = potion;
            }
            else if (string.Equals("Potion of Steel", item.name))
            {
                Potion potion = (Potion)item;
                Debug.Log("potion health before change is" + potion.health);
                potion.defense = (int)(potion.defense + (2 * multiplier));
                string name = itemRarity.ToString() + " " + potion.name;
                potion.name = name;
                item = potion;
            }
            else if (string.Equals("Potion of Speed", item.name))
            {
                Potion potion = (Potion)item;
                Debug.Log("potion health before change is" + potion.health);
                potion.speed = (int)(potion.speed + (2 * multiplier));
                string name = itemRarity.ToString() + " " + potion.name;
                potion.name = name;
                item = potion;
            }
            else if (item.type == Ite.ItemType.weapon)
            {
                Equipment equip = (Equipment)item;
                if (equip.equipSlot == EquipmentSlot.OffHand)
                {
                    ShieldEquipment shield = (ShieldEquipment)item;
                    shield.armor = (int)(shield.armor + multiplier);
                    shield.blockChance = (int)(shield.blockChance + multiplier);
                    item = shield;
                }
                else
                {
                    WeaponEquipment weapon = (WeaponEquipment)item;
                    weapon.attackRange = (int)(weapon.attackRange + multiplier);
                    weapon.minimumDamage = (int)(weapon.minimumDamage + multiplier);
                    weapon.maximumDamage = (int)(weapon.maximumDamage + multiplier);
                    weapon.attackSpeed = (int)(weapon.attackSpeed + multiplier);
                    weapon.critChance = (int)(weapon.critChance + multiplier);
                    item = weapon;
                }
            }
            else
            {
                ArmorEquipment armor = (ArmorEquipment)item;
                if(itemRarity != Rarity.Legendary)
                {
                    armor.Evasion = (int)(armor.Evasion * multiplier);
                    armor.Armor = (int)(armor.Armor * multiplier);
                }
                item = armor;
            }           
        }
    }
}