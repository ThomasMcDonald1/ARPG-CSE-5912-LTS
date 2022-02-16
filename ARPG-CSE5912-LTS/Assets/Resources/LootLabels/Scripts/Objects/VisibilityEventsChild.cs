using UnityEngine;

namespace LootLabels {
    /// <summary>
    /// Assign this script to one of the children of a gameobject with a renderer when the parent object does not have a renderer
    /// We need this script to know when an object is no longer visible to the camera
    /// </summary>
    public class VisibilityEventsChild : MonoBehaviour {

        //assign the parent's eventhandler script here
        public EventHandler eventHandlerRef;

        private void OnBecameInvisible() {
            //Debug.Log("OnBecameInvisible child");
            if (eventHandlerRef != null) {
                eventHandlerRef.InvisibleEvent();
            }
            else {
                Debug.Log("Assign parent event handler script to visibilityEventsChild script");
            }
        }

        private void OnBecameVisible() {
            //Debug.Log("OnBecameVisible child");
            if (eventHandlerRef != null) {
                eventHandlerRef.VisibleEvent();
            }
            else {
                Debug.Log("Assign parent event handler script to visibilityEventsChild script");
            }
        }
    }
}