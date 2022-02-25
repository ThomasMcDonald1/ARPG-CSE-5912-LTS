using System.Collections;
using UnityEngine;

namespace LootLabels {
    /// <summary>
    /// Manager takes care of creating loot
    /// </summary>
    public class LootManager : MonoBehaviour {

        public static LootManager singleton = null;

        public Transform LootParent;    //all dropped loot goes in this parent

        LootGenerator lootGenerator;
        RarityColors rarityColors;

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

        void Awake() {
            //Check if instance already exists
            if (singleton == null) {
                //if not, set instance to this
                singleton = this;
            }

            //If instance already exists and it's not this:
            else if (singleton != this) {
                //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
                Destroy(gameObject);
            }

            LootGenerator = GetComponent<LootGenerator>();
            RarityColors = GetComponent<RarityColors>();
        }

        //Depending on the loot source calculate the amounts of loot, instantiate at the position of the loot source 
        public void DropLoot(LootSource lootSource, Transform objectTransform, Type type) {
            int itemAmount = LootGenerator.CalculateLootAmount(lootSource);

            //if you want to use animations on loot, you need to put it in a empty parent object, which is located at the spawn location otherwise the position of the animation is completely wrong
            //this might not be needed by setting root motion or something like that, need to look into it
            GameObject lootOrigin = null;

            if (LootParent.childCount > 0) {
                for (int i = 0; i < LootParent.childCount; i++) {
                    if (LootParent.GetChild(i).childCount == 0) {
                        //use this instead of instantiating a new origin
                        lootOrigin = LootParent.GetChild(i).gameObject;
                        lootOrigin.transform.SetPositionAndRotation(objectTransform.position, objectTransform.rotation);
                        break;
                    }

                    if (i == LootParent.childCount - 1) {
                        lootOrigin = Instantiate(Resources.Load("LootLabels/3D models/LootOrigin", typeof(GameObject)), objectTransform.position, objectTransform.rotation) as GameObject;
                        Debug.Log("lootOrigin is " + lootOrigin);
                        lootOrigin.transform.SetParent(LootParent, true);
                    }
                }
            }
            else {
                lootOrigin = Instantiate(Resources.Load("LootLabels/3D models/LootOrigin", typeof(GameObject)), objectTransform.position, objectTransform.rotation) as GameObject;
                lootOrigin.transform.SetParent(LootParent, true);
            }

            StartCoroutine(DropLootCoroutine(lootOrigin.transform, itemAmount, type));
        }

        //if you want a delay on each item that spawns, use this
        IEnumerator DropLootCoroutine(Transform lootOrigin, int amount, Type type) {
            int i = amount;

            while (i != 0) {
                i--;
                yield return new WaitForSeconds(.2f);
                GenerateLoot(lootOrigin, type);
            }
        }

        //Choses which type of loot will drop.
        //currency, items, spellbooks, ...
        void GenerateLoot(Transform lootOrigin, Type type) {
            LootTypes lootType = LootGenerator.SelectRandomLootType();

            switch (lootType) {
                case LootTypes.Currency:
                    ChooseCurrency(lootOrigin);
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
        void ChooseCurrency(Transform lootOrigin) {
            BaseCurrency currency = LootGenerator.CreateCurrency();

            GameObject droppedItem = Instantiate(Resources.Load(currency.ModelName, typeof(GameObject)), transform.position, Quaternion.Euler(0, 0, 0), lootOrigin) as GameObject;
            droppedItem.GetComponent<DroppedCurrency>().currency = currency;
        }

        /// <summary>
        /// Choses a item type to drop and creates it
        /// </summary>
        /// <param name="lootOrigin"></param>
        void ChooseItemType(Transform lootOrigin, Type type) {

            ItemTypes itemType = LootGenerator.SelectRandomItemType();
            Debug.Log("itemType is " + itemType);
            switch (itemType) {
                case ItemTypes.Gear:
                    BaseGear gear = LootGenerator.CreateGear(type);
                    Debug.Log("gear.ModelName is " + gear.ModelName);

                    GameObject droppedItem = Instantiate(Resources.Load(gear.ModelName, typeof(GameObject)), transform.position, Quaternion.Euler(0, 0, 0), lootOrigin) as GameObject;
                    droppedItem.GetComponent<DroppedGear>().gear = gear;
                    break;
                default:
                    Debug.Log("Itemtype not yet implemented");
                    break;
            }
        }
    }
}