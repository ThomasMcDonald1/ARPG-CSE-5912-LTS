using System.Collections;
using UnityEngine;

namespace LootLabels {
    /// <summary>
    /// Class containing functionality for dropping or resetting the labels
    /// </summary>
    public class Drop : MonoBehaviour {
        RaycastHit2D objectHit; //the boxcast hit result
        int layerBitMask; //the boxcast filter layer for dropping the labels 
        RectTransform thisRectTransform;   //the cached transform of the ui element
        Rigidbody2D targetRigidbody;    //cached Rigidbody2D of the target ui

        bool addedToResetQueue = false;    //bool to prevent the helper script from being added to the reset queue multiple times
        FollowTarget followScript;
        Helper helperScript;

        public bool AddedToResetQueue
        {
            get
            {
                return addedToResetQueue;
            }

            set
            {
                addedToResetQueue = value;
            }
        }

        public Rigidbody2D TargetRigidbody
        {
            get
            {
                return targetRigidbody;
            }

            set
            {
                targetRigidbody = value;
            }
        }

        //called in labelmanager when resetting labels
        public LabelStates GetLabelState() {
            return helperScript.LabelScript.LabelState;
        }

        private void Awake() {
            helperScript = GetComponent<Helper>();
            thisRectTransform = GetComponent<RectTransform>();
            followScript = GetComponent<FollowTarget>();

            layerBitMask = LayerMask.GetMask(helperScript.LayerMaskFilter);
        }

        /// <summary>
        /// Follows the target label, checks if it can drop down or reset back to the base position
        /// </summary>
        public virtual void CheckForDrop() {
            if (followScript.FollowingUI) {
                //don't reduce amount of times this function gets called, when moving the camera it causes the labels to shift up when badly timed
                if (!TargetViable()) {
                    //Debug.Log("not viable");
                    DropDown();
                }

                //reduce the amount of frames this function gets called for performance
                if (Time.frameCount % 30 == 0) {
                    if (!AddedToResetQueue) {
                        AddedToResetQueue = true;
                        StartCoroutine(CheckForReset());
                    }
                }
            }
        }

        /// <summary>
        /// Checks if the label can reset to it's base position.
        /// We use 2 lists for checking reset on both helpers so they don't slow each other down
        /// </summary>
        /// <returns></returns>
        public virtual IEnumerator CheckForReset() {
            yield return new WaitForEndOfFrame();

            if (BasePositionIsEmpty()) {
                if (helperScript.RunInBackground) {
                    LabelManager.singleton.AddHiddenHelperToResetQueue(this);
                }
                else {
                    LabelManager.singleton.AddHelperToResetQueue(this);
                }
            }
            else {
                AddedToResetQueue = false;
            }
        }

        /// <summary>
        /// Checks if the baseposition is empty, called by labelmanager
        /// </summary>
        /// <returns></returns>
        public virtual bool HelperCanReset() {
            //if showing loot, the hiddenhelper takes care of positioning
            if (!helperScript.RunInBackground) {
                if (LabelManager.singleton.PushingShowLabelsButton) {
                    return false;
                }
            }

            //we need to check the base position again because a previous label might have been reset already
            if (!BasePositionIsEmpty()) {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Resets the helper to it's baseposition
        /// </summary>
        /// <returns></returns>
        public virtual IEnumerator HelperReset() {
            //disable Rigidbody for triggerexit event
            GetComponent<Rigidbody2D>().simulated = false;

            //yield return new WaitForEndOfFrame();
            yield return null;

            followScript.ClearTargetUI();

            GetComponent<Rigidbody2D>().simulated = true;
        }

        /// <summary>
        /// Casts a box at the base position of the label to see if it can be reset
        /// </summary>
        /// <returns></returns>
        public virtual bool BasePositionIsEmpty() {
            objectHit = Physics2D.BoxCast(followScript.GetParentBasePosition(), thisRectTransform.sizeDelta, 0, Vector2.down, 0, layerBitMask);

            if (objectHit) {
                //Debug.Log("cant reset");
                return false;
            }
            else {
                //Debug.Log("can reset");
                return true;
            }
        }

        /// <summary>
        /// Casts a box downward untill it hits a potential target or resets the label
        /// </summary>
        public virtual void DropDown() {
            objectHit = Physics2D.BoxCast(followScript.GetPositionUnderLabel(), thisRectTransform.sizeDelta, 0, Vector2.down, Screen.height, layerBitMask);

            if (objectHit) {
                //Debug.Log("objecthit");
                followScript.SetTargetIfViable(objectHit.collider.gameObject);
            }
            else {
                //Debug.Log("ClearTargetUI");
                followScript.ClearTargetUI();
            }
        }

        /// <summary>
        /// Checks if the follow target is simulated or if it overlaps with a custom made box under the label
        /// Replaces the need for additional boxcollider and rigidbody which cost more performance
        /// </summary>
        /// <returns></returns>
        public virtual bool TargetViable() {
            if (!TargetRigidbody.simulated) {
                //Debug.Log("target not simulated");
                return false;
            }

            if (!rectOverlaps(followScript.TargetUiTransform)) {
                //Debug.Log("target doesn't overlap");
                return false;
            }

            return true;
        }

        Rect rect1 = new Rect();
        Rect rect2 = new Rect();
        /// <summary>
        /// Checks if a rect positioned under this label overlaps with the target rect it's following
        /// </summary>
        /// <param name="targetRect"></param>
        /// <returns></returns>
        public bool rectOverlaps(RectTransform targetRect) {

            //the anchors get put on the left side when using new rect(x,y,width,height), so we need to position them by setting the center
            //also if we first set the center and then the size, something is fucky for some reason
            rect1.size = thisRectTransform.sizeDelta;
            rect1.center = followScript.GetAnchoredPositionUnderLabel();

            rect2.size = targetRect.rect.size;
            rect2.center = targetRect.anchoredPosition;

            //Debug.Log(rect1.center + " " + rect1.size);
            //Debug.Log(rect2.center + " " + rect2.size);
            return rect1.Overlaps(rect2);
        }
    }
}