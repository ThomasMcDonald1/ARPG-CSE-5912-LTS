using UnityEngine;

namespace LootLabels {
    /// <summary>
    /// Base class for interactable objects that use mouse and visibility events
    /// </summary>
    [RequireComponent(typeof(EventHandler))]
    public class InteractableObject : MonoBehaviour {
        
        /// <summary>
        /// Spawn a label for the object
        /// </summary>
        public virtual void SpawnLabel() {
        }
                
        /// <summary>
        /// Object specific mouse down logic
        /// </summary>
        public virtual void MouseDownFunction() {

        }

        /// <summary>
        /// Object specific mouse enter logic
        /// </summary>
        public virtual void MouseEnterFunction() {

        }

        /// <summary>
        /// Object specific mouse exit logic
        /// </summary>
        public virtual void MouseExitFunction() {

        }

        /// <summary>
        /// Object specific code when object isn't visible to camera
        /// </summary>
        public virtual void OutOfCameraFrustum() {

        }

        /// <summary>
        /// Object specific code when object is visible to camera
        /// </summary>
        public virtual void InCameraFrustum() {

        }
    }
}