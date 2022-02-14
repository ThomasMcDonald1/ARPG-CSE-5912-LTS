using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

namespace LootLabels {
    /// <summary>
    /// The interactable object's raises events in this script
    /// </summary>
    public class EventHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler {

        //the events for this object, assign functions to these events
        public delegate void Events();
        public Events OnMouseDown;
        public Events OnMouseEnter;
        public Events OnMouseExit;
        public Events OnObjectVisible;
        public Events OnObjectInvisible;

        public virtual void ClearDelegates() {
            OnMouseDown = null;
            OnMouseEnter = null;
            OnMouseExit = null;
            OnObjectVisible = null;
            OnObjectInvisible = null;
        }

        public void SubscribeMouseEvents(Events mouseDownFunction, Events mouseEnterFunction, Events mouseExitFunction) {
            OnMouseDown += mouseDownFunction;
            OnMouseEnter += mouseEnterFunction;
            OnMouseExit += mouseExitFunction;
        }

        public void UnsubscribeMouseEvents(Events mouseDownFunction, Events mouseEnterFunction, Events mouseExitFunction) {
            OnMouseDown -= mouseDownFunction;
            OnMouseEnter -= mouseEnterFunction;
            OnMouseExit -= mouseExitFunction;
        }

        public void SubscribeVisibilityEvents(Events inCameraFrustumFunction, Events outCameraFrustumFunction) {
            OnObjectVisible += inCameraFrustumFunction;
            OnObjectInvisible += outCameraFrustumFunction;
        }

        public void UnsubscribeVisibilityEvents(Events inCameraFrustumFunction, Events outCameraFrustumFunction) {
            OnObjectVisible -= inCameraFrustumFunction;
            OnObjectInvisible -= outCameraFrustumFunction;
        }

        /// <summary>
        /// Onbecameinvisible doesn't get called when the game starts and the mesh is already out of view of the camera
        /// To be sure the label is completely initialized we wait a second and then turn it off if needed
        /// </summary>
        /// <returns></returns>
        public IEnumerator VisibilityCoroutine() {
            yield return new WaitForSeconds(1);
            CheckIfVisible();
        }

        /// <summary>
        /// Checks if the object is visible after initializing the label.
        /// If it's not visible, disable colliders etc.
        /// </summary>
        void CheckIfVisible() {
            if (GetComponent<Renderer>()) {
                if (!GetComponent<Renderer>().isVisible) {
                    InvisibleEvent();
                }
            }
            else {
                if (!GetComponentInChildren<VisibilityEventsChild>()) {
                    Debug.Log("Attach the VisibilityEventsChild to one of the children with a renderer");
                }

                if (GetComponentInChildren<Renderer>()) {
                    if (!GetComponentInChildren<Renderer>().isVisible) {
                        InvisibleEvent();
                    }
                }
                else {
                    Debug.Log("No renderer found");
                }
            }
        }

        #region Events
        //Called by the pointer interfaces or by label script
        public void MouseDownEvent() {
            //if (NavManager.singleton.EnableRangeChecker) {
            //    //if in range call code else queue this, walk to it and call code
            //    NavManager.singleton.QueueInteraction(OnMouseDown, gameObject.transform);
            //}
            //else {
            //    if (OnMouseDown != null) {
            //        OnMouseDown();
            //    }
            //}
        }

        public void MouseEnterEvent() {
            if (OnMouseEnter != null) {
                OnMouseEnter();
            }
        }

        public void MouseExitEvent() {
            if (OnMouseExit != null) {
                OnMouseExit();
            }
        }

        //called on a renderer or a child with a renderer who has the VisibilityEventsChild script attached
        public void VisibleEvent() {
            if (OnObjectVisible != null) {
                OnObjectVisible();
            }
        }

        //used in dropped loot when checking visibility manually or in children with a renderer when the parent game object does not have a renderer
        public void InvisibleEvent() {
            if (OnObjectInvisible != null) {
                OnObjectInvisible();
            }
        }
        #endregion

        #region event raisers

        public virtual void OnPointerEnter(PointerEventData eventData) {
            MouseEnterEvent();
        }

        public virtual void OnPointerExit(PointerEventData eventData) {
            MouseExitEvent();
        }

        public virtual void OnPointerDown(PointerEventData eventData) {
            MouseDownEvent();
        }

        //only works on objects with a renderer component
        public virtual void OnBecameVisible() {
            //Debug.Log("OnBecameVisible eventhandler "+gameObject.name);
            VisibleEvent();
        }

        //only works on objects with a renderer component
        public virtual void OnBecameInvisible() {
            //Debug.Log("OnBecameInvisible");
            InvisibleEvent();
        }

        #endregion
    }
}