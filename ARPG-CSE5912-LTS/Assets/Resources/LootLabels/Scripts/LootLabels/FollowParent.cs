using UnityEngine;

namespace LootLabels {
    /// <summary>
    /// Calculates the screenposition of the 3d object, so it can be used by both helpers
    /// </summary>
    public class FollowParent : MonoBehaviour {
        Label labelScript;

        FollowTarget helperLabelFollowScript;
        FollowTarget helperHiddenFollowScript;

        Transform targetTransform;  //the 3d target's cached transform 
        Vector2 targetScreenPosition;      //the screen space position of the object, used by all the helper objects for positioning

        protected Camera cam; //cached cam
        protected bool cameraSet = false;

        public Vector2 TargetScreenPosition
        {
            get
            {
                return targetScreenPosition;
            }

            set
            {
                targetScreenPosition = value;
            }
        }

        private void Awake() {
            labelScript = GetComponent<Label>();
            helperLabelFollowScript = labelScript.helper.GetComponent<FollowTarget>();
            helperHiddenFollowScript = labelScript.helperHidden.GetComponent<FollowTarget>();

            SetCamera();
        }

        public void SetCamera() {
            cam = Camera.main;
            cameraSet = true;
        }

        // Update is called once per frame
        void Update() {
            switch (labelScript.LabelState) {
                case LabelStates.InCameraFrustum:
                    UpdateTargetScreenPosition();
                    break;
                case LabelStates.OutCameraFrustum:
                    //keep updating position when the label is clamped
                    if (labelScript.LabelSettings.ClampToScreen) {
                        UpdateTargetScreenPosition();
                    }
                    break;
                case LabelStates.Inactive:
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Called when the label initializes so screen position can begin calculating
        /// </summary>
        public void SetTarget() {
            targetTransform = labelScript.LabelSettings.ObjectToFollow.GetComponent<Transform>();

            //UpdateTargetScreenPosition();
            //Debug.Log(TargetScreenPosition);
        }

        /// <summary>
        /// Manually update the position of the helpers cause this needs to happen in hierarchy order
        /// </summary>
        public void ManualUpdateHelpers() {
            helperLabelFollowScript.ManualLateUpdate();
            helperHiddenFollowScript.ManualLateUpdate();
        }

        /// <summary>
        /// Converts 3d position to screen point. 
        /// Used by all helpers so they don't need to convert the position separately
        /// </summary>
        public virtual void UpdateTargetScreenPosition() {
            if (targetTransform == null) {
                //Debug.Log("No target assigned to follow ");
                return;
            }

            if (!cameraSet) {
                SetCamera();
            }

            TargetScreenPosition = cam.WorldToScreenPoint(targetTransform.position);
        }
    }
}