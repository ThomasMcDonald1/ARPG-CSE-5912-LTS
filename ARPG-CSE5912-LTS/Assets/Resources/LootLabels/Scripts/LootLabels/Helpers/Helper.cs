using UnityEngine;

namespace LootLabels {
    /// <summary>
    /// Main script for the helpers to update their state and components
    /// </summary>
    public class Helper : MonoBehaviour {

        public Label LabelScript;   //reference to parent label script
        public string LayerMaskFilter;      //put the layer name of the object you want to scan here
        public bool RunInBackground = false;    //Enabled for hiddenHelper so that it stays stacked in the background so we can reposition immediatly without restacking when showing loot, also needs less functionality

        public void Initialize(Vector2 boxSize) {
            GetComponent<BoxCollider2D>().size = boxSize;
            GetComponent<RectTransform>().sizeDelta = boxSize;

            EnableStacking();
        }

        public void EnableStacking() {
            CheckPhysics();

            //if stack label, start stacking
            if (!LabelManager.singleton.EnableIcons && LabelScript.LabelSettings.Stack) {
                GetComponent<Stack>().InitializeStacking();
            }
        }

        public virtual void UpdateHelperState() {
            GetComponent<FollowTarget>().UpdateState();
            
            if (RunInBackground) {
                UpdateHiddenHelperPhysics();
            }
            else {
                UpdateHelperPhysics();
            }
        }

        void UpdateHelperPhysics() {
            switch (LabelScript.LabelState) {
                case LabelStates.InCameraFrustum:
                    switch (LabelScript.VisibilityState) {
                        case VisibilityState.Normal:
                            CheckPhysics();
                            break;
                        case VisibilityState.Hidden:
                            if (LabelScript.ForceVisible) {
                                EnablePhysics(false);
                            }
                            else {
                                if (LabelScript.Highlighted) {
                                    CheckPhysics();
                                }
                                else {
                                    EnablePhysics(false);
                                }
                            }
                            break;
                        default:
                            break;
                    }
                    break;
                case LabelStates.OutCameraFrustum:
                    EnablePhysics(false);
                    break;
                case LabelStates.Inactive:
                    EnablePhysics(false);
                    break;
                default:
                    break;
            }
        }

        void UpdateHiddenHelperPhysics() {
            switch (LabelScript.LabelState) {
                case LabelStates.InCameraFrustum:
                    CheckPhysics();
                    break;
                case LabelStates.OutCameraFrustum:
                    EnablePhysics(false);
                    gameObject.SetActive(false);
                    break;
                case LabelStates.Inactive:
                    EnablePhysics(false);
                    break;
                default:
                    break;
            }
        }

        void CheckPhysics() {
            if (!LabelManager.singleton.EnableIcons && LabelScript.LabelSettings.Stack) {
                if (RunInBackground) {
                    gameObject.SetActive(true);
                }

                EnablePhysics(true);
            }
            else {
                EnablePhysics(false);

                if (RunInBackground) {
                    gameObject.SetActive(false);
                }
            }
        }

        public virtual void EnablePhysics(bool enabled) {
            if (GetComponent<Rigidbody2D>()) {
                if (enabled) {
                    GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
                    GetComponent<Rigidbody2D>().simulated = true;
                }
                else {
                    GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
                    GetComponent<Rigidbody2D>().simulated = false;
                }
            }

            if (GetComponent<BoxCollider2D>()) {
                GetComponent<BoxCollider2D>().enabled = enabled;
            }
        }
    }
}