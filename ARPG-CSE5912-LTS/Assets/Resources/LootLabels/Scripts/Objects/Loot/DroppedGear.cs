using UnityEngine;

namespace LootLabels {
    /// <summary>
    /// Inherited class from dropped loot containing logic for dropped gear
    /// </summary>
    public class DroppedGear : DroppedLoot {

        public GearTypes gearType;  //chose geartype when spawning manually
        public Rarity gearRarity;   //chose Rarity when spawning manually

        [HideInInspector]
        public BaseGear gear;      //created manually or through the lootgenerator

        private void Start() {
            //if (Premade) {
            //    gear = new BaseGear(gearRarity, gearType, ResourceManager.singleton.GetModelName(gearType), ResourceManager.singleton.GetIconName(gearType));
            //}
            
            CreateLabel();
        }

        /// <summary>
        /// Creates the label!!
        /// Called in animator or in start when there is no animator
        /// </summary>
        public override void SpawnLabel() {
            //  GetComponent<EventHandler>().ClearDelegates();
            // GetComponent<EventHandler>().SubscribeMouseEvents(MouseDownFunction, MouseEnterFunction, MouseExitFunction);
            Debug.Log("gear name is " + gear.ItemName);
            Debug.Log("gear rarity is " + gear.ItemRarity);
            if (gear != null) {
                GetComponent<CreateLabel>().SpawnLabelByRarity(gear.ItemName, gear.IconName, gear.ItemRarity);
            }
            else {
                Debug.Log("Turn on testing or assign gear on instantiate");
            }

            StartCoroutine(GetComponent<EventHandler>().VisibilityCoroutine());
        }

        //What you want to happen when an item is looted
        void AddItemToInventory() {
            Debug.Log("add " + gear.GearType + " to inventory");
        }

        #region add mouse events here
        public override void MouseDownFunction() {
            AddItemToInventory();
            DestroyMesh();
        }

        public override void MouseEnterFunction() {
            GetComponent<ObjectHighlight>().HighlightObject();
        }

        public override void MouseExitFunction() {
            GetComponent<ObjectHighlight>().StopHighlightObject();
        }
        #endregion
    }
}