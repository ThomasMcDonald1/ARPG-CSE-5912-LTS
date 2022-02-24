using System.Collections;
using UnityEngine;

namespace LootLabels {
    public struct OverlapStruct {
        public int overlapAmount;
        public GameObject firstContact;
    }

    /// <summary>
    /// Class takes care of checking for overlap and assigning target to follow
    /// </summary>
    [RequireComponent(typeof(BoxCollider2D))]
    public class Stack : MonoBehaviour {
        public RectTransform parentRect;     //used for retreiving the hierarchy index
        public LabelVisibility visibilityScript;    //hides the text when stacking labels

        int overlapCount = 0;   //amount of objects in the overlap list

        Helper helperScript;    //reference to the helper script containing most variables

        BoxCollider2D thisBoxCollider;    //cached boxcollider

        OverlapStruct hit = new OverlapStruct();
        ContactFilter2D overlapFilter;
        Collider2D[] contactList = new Collider2D[2];

        bool checkCollision = false; //stop checking for collisions once it's been established there are no collisions

        private void Awake() {
            GetComponents();

            InitializeContactfilter();
        }

        void GetComponents() {
            thisBoxCollider = GetComponent<BoxCollider2D>();
            helperScript = GetComponent<Helper>();
        }

        private void Update() {
            if (checkCollision) {
                PushLabelOnTop();
            }
        }

        //instead of using the ontrigger callback object that enters the collider, we just check overlap manually
        //ontriggerenter is not consistent in the callbacks it retrieves
        //probably because we don't position by rigidbody 
        //also the objects go to sleep while they are being pushed up causing callbacks to stop
        private void OnTriggerEnter2D(Collider2D collision) {
            //Debug.Log("ontriggerenter");
            checkCollision = true;
        }

        //Set the values for contactFilter2D
        protected void InitializeContactfilter() {
            overlapFilter.useLayerMask = true;
            overlapFilter.useTriggers = true;
            overlapFilter.layerMask = LayerMask.GetMask(helperScript.LayerMaskFilter);
        }

        //After fixing to boxcollider's size, check if it has overlaps, show the label if not
        public void InitializeStacking() {
            StartCoroutine(CheckIfEmpty());
        }

        /// <summary>
        /// Wait untill the end of the frame so that positioning is done, and then check if the label has overlap
        /// if not show the label
        /// </summary>
        /// <returns></returns>
        IEnumerator CheckIfEmpty() {
            //yield return null;
            yield return new WaitForEndOfFrame();
            //yield return new WaitForSeconds(1);
            //Debug.Log(transform.localPosition);
            OverlapStruct overlapStruct = GetOverlapContact();

            if (overlapStruct.overlapAmount == 0) {
                OverlapFixed();
            }

            helperScript.LabelScript.initialized = true;
        }

        //called when there is no overlap
        //Reset the bools, set the label visible,...
        public virtual void OverlapFixed() {
            checkCollision = false;

            if (!helperScript.RunInBackground) {
                //Debug.Log("OverlapFixed");
                helperScript.LabelScript.UpdateLabelState();
            }
        }

        //The position in the hierarchy needs to be respected
        //if labels follow each other in a chain according to the hierarchy 1>2>3>4>...  There's a visible delay
        //If we do 4>3>2>1, it's fine initially but enabling/disabling, changing parents breaks this functionality
        //To fix this we collect all follow scripts when a label gets added to the visible parent, and just loop through the list and update the positions
        public virtual void PushLabelOnTop() {
            OverlapStruct overlapStruct = GetOverlapContact();

            if (overlapStruct.overlapAmount != 0) {
                int overlapPosition = overlapStruct.firstContact.GetComponent<Stack>().parentRect.GetSiblingIndex();
                int thisPosition = parentRect.GetSiblingIndex();

                if (overlapPosition < thisPosition) {

                    if (!helperScript.RunInBackground) {
                        //while stacking set the text transparent
                        if (visibilityScript) {
                            //Debug.Log("SetTextTransparent");
                            visibilityScript.SetTextTransparent();
                        }
                    }

                    GetComponent<FollowTarget>().SetTargetUI(overlapStruct.firstContact);
                }
            }
            else {
                OverlapFixed();
            }
        }
        
        /// <summary>
        /// Gathers a list of all colliders that overlap this collider with the given settings
        /// </summary>
        /// <returns></returns>
        protected OverlapStruct GetOverlapContact() {
            overlapCount = thisBoxCollider.OverlapCollider(overlapFilter, contactList);

            if (overlapCount == 0) {
                hit.overlapAmount = 0;
                hit.firstContact = null;
            }
            else {
                hit.overlapAmount = overlapCount;
                hit.firstContact = contactList[0].gameObject;
            }

            return hit;
        }
    }
}