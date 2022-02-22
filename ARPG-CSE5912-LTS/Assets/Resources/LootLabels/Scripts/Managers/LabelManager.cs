using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LootLabels {
    public class LabelManager : MonoBehaviour {
        public KeyCode ShowAllLootBtn = KeyCode.LeftAlt;    //button to push to show all labels
        public bool EnableIcons = false;    //Changes text to icons

        public GameObject LabelPrefab;    //Prefab reference for a label
        
        public GameObject LabelVisible;//all the labels that are in the camera frustum go in this object
        public GameObject LabelPool;    //Inactive labels go here so we can reuse them later instead of instantiating another one
        public GameObject LabelInvisible;  //Temporary place to put invisible labels so there updates don't get called etc

        private bool pushingShowLabelsButton = false; //bool to know when player is pushing the view labels button to prevent unwanted dissappearing

        List<Drop> HelperResetList = new List<Drop>();  //list containing stack helper scripts to reset
        List<Drop> HelperHiddenResetList = new List<Drop>();  //list containing hidden stack helper scripts to reset

        bool HelperResetIsRunning = false;
        bool HelperHiddenResetIsRunning = false;

        public static LabelManager singleton = null;

        public bool PushingShowLabelsButton
        {
            get
            {
                return pushingShowLabelsButton;
            }

            set
            {
                pushingShowLabelsButton = value;
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
        }

        // Update is called once per frame
        void Update() {
            if (Input.GetKeyDown(ShowAllLootBtn)) {
                PushingShowLabelsButton = true;
            }
            if (Input.GetKeyUp(ShowAllLootBtn)) {
                PushingShowLabelsButton = false;
            }

            if (Input.GetKeyDown(KeyCode.I)) {
                EnableIcons = !EnableIcons;
                Debug.Log("Icons toggled, press I to toggle back");
            }
        }

        public void InstantiateLabel(LabelSettings labelSettings, EventHandler eventHandlerRef) {
            GameObject labelGO = null;

            //take a label from the pool if any
            if (LabelPool.transform.childCount > 0) {
                Transform inactiveLabel = LabelPool.transform.GetChild(0);
                labelGO = inactiveLabel.gameObject;
            }
            else {
                labelGO = Instantiate(LabelPrefab) as GameObject;
                //labelGO = Instantiate(LabelPrefab, Camera.main.WorldToScreenPoint(labelSettings.ObjectToFollow.transform.position), Quaternion.identity) as GameObject;
            }

            Label labelScript = labelGO.GetComponent<Label>();
            labelScript.Initialize(labelSettings, eventHandlerRef);
            
            labelGO.transform.SetParent(LabelVisible.transform, false);
        }

        #region helper resetting
        public void AddHelperToResetQueue(Drop dropScript) {
            HelperResetList.Add(dropScript);

            //if the coroutine is off, turn it back on
            if (!HelperResetIsRunning) {
                HelperResetIsRunning = true;
                StartCoroutine(ResetHelperInQueue());
            }
        }

        /// <summary>
        /// Go over the queue and only check the next item if the previous item has been reset or can't reset to the base position
        /// </summary>
        /// <returns></returns>
        IEnumerator ResetHelperInQueue() {

            for (int i = 0; i < HelperResetList.Count; i++) {
                if (HelperResetList[i].GetLabelState() == LabelStates.InCameraFrustum) {
                    if (HelperResetList[i].HelperCanReset()) {
                        yield return StartCoroutine(HelperResetList[i].HelperReset());
                    }
                }

                //reset the bool so the helper can be readded to the list
                HelperResetList[i].AddedToResetQueue = false;

            }

            HelperResetList.Clear();
            HelperResetIsRunning = false;
        }

        public void AddHiddenHelperToResetQueue(Drop dropScript) {
            HelperHiddenResetList.Add(dropScript);

            //if the coroutine is off, turn it back on
            if (!HelperHiddenResetIsRunning) {
                HelperHiddenResetIsRunning = true;
                StartCoroutine(ResetHiddenHelpersInQueue());
            }
        }

        IEnumerator ResetHiddenHelpersInQueue() {
            //go over the queue and only check the next item if the previous item has been reset or skipped
            for (int i = 0; i < HelperHiddenResetList.Count;) {
                if (HelperHiddenResetList[i].GetLabelState() == LabelStates.InCameraFrustum) {
                    if (HelperHiddenResetList[i].HelperCanReset()) {
                        yield return StartCoroutine(HelperHiddenResetList[i].HelperReset());
                    }
                }

                //reset the bool so the helper can be readded to the list
                HelperHiddenResetList[i].AddedToResetQueue = false;

                i++;
            }

            HelperHiddenResetList.Clear();
            HelperHiddenResetIsRunning = false;
        }
        #endregion
    }
}