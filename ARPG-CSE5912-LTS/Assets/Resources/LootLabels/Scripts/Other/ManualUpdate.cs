using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace LootLabels {
    public class ManualUpdate : MonoBehaviour {

        /// <summary>
        /// There doesn't seem to be a reliable update order from unity itself so we have to make it ourselves
        /// we update the position of the labels manually in order from top to bottom in the hierarchy
        /// </summary>
        FollowParent[] labelFollowList = new FollowParent[0];
        public GameObject lootdrops;
        void OnEnable()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        void OnDisable()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            foreach (Transform child in lootdrops.transform)
            {
                Destroy(child.gameObject);
            }
            foreach(Transform visible in transform)
            {
                Destroy(visible.gameObject);
            }
        }
        private void LateUpdate() {
            manualUpdate();
        }

        void manualUpdate() {
            if (labelFollowList.Length != 0) {
                for (int i = 0; i < labelFollowList.Length; i++) {
                    labelFollowList[i].ManualUpdateHelpers();
                }
            }
        }
        
        //every time a child is added or removed, the script gets notified to update it's child list
        //game object needs to be active before being added to the parent otherwise components don't get collected
        private void OnTransformChildrenChanged() {
            GetFollowScripts();
        }

        void GetFollowScripts() {
            labelFollowList = GetComponentsInChildren<FollowParent>();
            //Debug.Log(labelStackList.Length);
        }
    }
}