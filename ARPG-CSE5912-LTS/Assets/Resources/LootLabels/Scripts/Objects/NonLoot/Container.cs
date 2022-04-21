using System.Collections;
using UnityEngine;

namespace LootLabels {
    /// <summary>
    /// Example class for container objects
    /// </summary>
    [RequireComponent(typeof(CreateLabel))]
    [RequireComponent(typeof(ObjectHighlight))]
    public class Container : InteractableObject {
        public string objectName;    //the name of the object, this name is shown in the labels
        public LootSource lootSource;   //the rarity of the source to determine loot drops
        public LootType type;               //the rarity of the dropped loot
        bool chestOpened = false;   //Toggle to check if the chest has been opened yet
        bool enemyDead = false;
        
        // Use this for initialization
        void Start() {
            SpawnLabel();
        }

        /// <summary>
        /// Creates the label!!
        /// Called in animator or in start when there is no animator
        /// </summary>
        public override void SpawnLabel() {
            GetComponent<EventHandler>().ClearDelegates();
            //GetComponent<EventHandler>().SubscribeMouseEvents(MouseDownFunction, MouseEnterFunction, MouseExitFunction);

            GetComponent<CreateLabel>().SpawnLabelByColor(objectName, "LootLabels/Icons/UI_Icon_Bag1");

            StartCoroutine(GetComponent<EventHandler>().VisibilityCoroutine());
        }
        private void Update()
        {
            Collider[] hitColliders = Physics.OverlapSphere(this.transform.position, 3);
            foreach (var hitCollider in hitColliders)
            {
                if (hitCollider.CompareTag("Player") && this.GetComponent<Stats>() == null)
                {
                    OpenChest();
                }
                else if(this.GetComponent<Stats>() != null)
                {
                    if(this.GetComponent<Stats>()[StatTypes.HP] <= 0 && !enemyDead)
                    {
                        enemyDead = true;
                        if (GetComponent<AudioSource>())
                        {
                            GetComponent<AudioSource>().Play();
                        }
                        StartCoroutine(DropLootCoroutine());
                    }
                    
                }
            }
        }
        void OpenChest() {
            if (!chestOpened) {
                chestOpened = true;
                GetComponent<ObjectHighlight>().StopHighlightObject();

                if (GetComponent<AudioSource>()) {
                    GetComponent<AudioSource>().Play();
                }

                if (GetComponent<Animation>()) {
                    GetComponent<Animation>().Play("open");
                }

                StartCoroutine(DropLootCoroutine());
            }
        }

        //Wait half a second for the chest open animation to finish and then start dropping legendaries
        IEnumerator DropLootCoroutine() {
            yield return new WaitForSeconds(.5f);
            LootManager.singleton.DropLoot(lootSource, transform, type);
        }

        #region mouse functions
        public override void MouseDownFunction() {
            if (chestOpened) {
                return;
            }

            OpenChest();
        }

        public override void MouseEnterFunction() {
            if (chestOpened) {
                return;
            }

            GetComponent<ObjectHighlight>().HighlightObject();
        }

        public override void MouseExitFunction() {
            if (chestOpened) {
                return;
            }

            GetComponent<ObjectHighlight>().StopHighlightObject();
        }
        #endregion
    }
}
