using UnityEngine;
using UnityEngine.EventSystems;

namespace LootLabels {
    /// <summary>
    /// The event raisers for the label object
    /// </summary>
    public class LabelEvents : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler {

        Label labelScript;
        EventHandler ObjectEventHandler;  //reference to linked object's eventhandler to call it's functions

        private void Awake() {
            labelScript = GetComponent<Label>();
        }

        void OnDestroy() {
            //Debug.Log("label destroy");
            UnsubscribeEvents();
        }

        /// <summary>
        /// Subscribes the mouse and visibility events in the eventhandler component and adds a reference of the eventhandler to the other object
        /// </summary>
        public virtual void SubscribeEvents(EventHandler objectEventHandler) {
            ObjectEventHandler = objectEventHandler;
            ObjectEventHandler.SubscribeMouseEvents(MouseDownFunction, MouseEnterFunction, MouseExitFunction);
            ObjectEventHandler.SubscribeVisibilityEvents(InCameraFrustum, OutOfCameraFrustum);
        }

        /// <summary>
        /// Unsubscribe the delegates in the eventhandlers, otherwise we get nullreference calls on exiting etc..
        /// </summary>
        public virtual void UnsubscribeEvents() {
            ObjectEventHandler.UnsubscribeMouseEvents(MouseDownFunction, MouseEnterFunction, MouseExitFunction);
            ObjectEventHandler.UnsubscribeVisibilityEvents(InCameraFrustum, OutOfCameraFrustum);
        }

        /// <summary>
        /// Object specific mouse down logic
        /// </summary>
        public virtual void MouseDownFunction() {
            if (labelScript.LabelState == LabelStates.Inactive) {
                return;
            }

            if (labelScript.LabelSettings.DisableOnClick) {
                labelScript.SetLabelState(LabelStates.Inactive);
            }
        }

        /// <summary>
        /// Object specific mouse enter logic
        /// </summary>
        public virtual void MouseEnterFunction() {
            if (labelScript.LabelState == LabelStates.Inactive) {
                return;
            }

            labelScript.Highlighted = true;
            labelScript.UpdateComponentStates();
        }

        /// <summary>
        /// Object specific mouse exit logic
        /// </summary>
        public virtual void MouseExitFunction() {
            if (labelScript.LabelState == LabelStates.Inactive) {
                return;
            }

            labelScript.Highlighted = false;
            labelScript.UpdateComponentStates();
        }

        /// <summary>
        /// Object specific invisible logic
        /// </summary>
        public void OutOfCameraFrustum() {
            //Debug.Log("OutOfCameraFrustum");
            if (labelScript.LabelState == LabelStates.Inactive) {
                return;
            }

            labelScript.SetLabelState(LabelStates.OutCameraFrustum);
        }

        /// <summary>
        /// Object specific visible logic
        /// </summary>
        public void InCameraFrustum() {
            //When the renderer becomes visible, this gets called, but on stack labels the state machine update needs to wait even when the object is visible
            if (!labelScript.initialized) {
                return;
            }

            if (labelScript.LabelState == LabelStates.Inactive) {
                return;
            }

            labelScript.SetLabelState(LabelStates.InCameraFrustum);
        }

        public void OnPointerEnter(PointerEventData eventData) {
            ObjectEventHandler.MouseEnterEvent();
        }

        public void OnPointerExit(PointerEventData eventData) {
            ObjectEventHandler.MouseExitEvent();
        }

        public void OnPointerDown(PointerEventData eventData) {
            ObjectEventHandler.MouseDownEvent();
        }
    }
}