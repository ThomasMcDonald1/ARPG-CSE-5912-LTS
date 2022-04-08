using UnityEngine;

namespace LootLabels {
    /// <summary>
    /// Base class for all loot that drops on the ground
    /// </summary>
    [RequireComponent(typeof(CreateLabel))]
    [RequireComponent(typeof(ObjectHighlight))]
    public class DroppedLoot : InteractableObject {

        /// <summary>
        /// Loots the item when the player and the object collide
        /// </summary>
        public bool LootOnCollision = false;

        /// <summary>
        /// Enable premade when you just want to manually drop an item in the scene, otherwise the item doesn't get created
        /// </summary>
        public bool Premade = false;

        /// <summary>
        /// If there is no animator or the animator is off, spawn a label manually
        /// otherwise the animator calls the spawnlabel function at the end of the animation
        /// </summary>
        protected void CreateLabel() {
            if (GetComponent<Animator>()) {
                if (!GetComponent<Animator>().enabled) {
                    SpawnLabel();
                }
            }
            else {
                SpawnLabel();
            }
        }

        /// <summary>
        /// Play audio file at the end of the drop animation
        /// </summary>
        public void PlayDropSound() {
            if (GetComponent<AudioSource>()) {
                AudioSource audio = GetComponent<AudioSource>();
                audio.Play();
            }
        }

        protected virtual void DestroyMesh() {
            Destroy(gameObject);
        }

        /// <summary>
        /// Loot objects by walking over it
        /// </summary>
        /// <param name="other"></param>
        //void OnTriggerEnter(Collider other) {
        //    if (LootOnCollision) {
        //        //change tag to the name of your player tag
        //        if (other.tag == "Player") {
        //            if (GetComponent<EventHandler>().OnMouseDown != null) {
        //                GetComponent<EventHandler>().OnMouseDown();
        //            }
        //        }
        //    }
        //}
    }
}