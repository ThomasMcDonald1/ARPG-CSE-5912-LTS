using UnityEngine;

namespace LootLabels {

    public class FollowTarget : MonoBehaviour {

        public RectTransform parentRect;    //reference to the parent rect to retreive the hierarchy index        
        public RectTransform backgroundImageRect;   //reference to the text object for transforming position
        public RectTransform iconBackgroundRect;   //reference to the icon object for transforming position
        public Label labelScript;   //reference to the label script containing all the variables
        public FollowParent followParentScript;  //reference to followparent script that calculates screen position for all helpers

        float textOffset = 0;   //add some amount to the Y axis to push the text up a bit
        float iconOffset = 40;  //icons are bigger then text so offset them some more
        float spaceBetweenLabels = 2f;    //the space between stacked labels

        Vector2 NPCScreenPosition;      //the screen space position of the object        
        Vector2 tempPosition;     //used for returning the calculated position
        Vector2 previousPosition;    //if the position hasn't changed, no need to assign new position

        RectTransform thisRectTransform;   //the cached transform of the ui element        
        RectTransform targetUiTransform;    //cached recttransform of the target ui

        bool followUI = false;  //trigger to follow a UI object instead of the 3d target   
        bool addedToResetQueue = false;    //bool to prevent the helper script from being added to the reset queue multiple times
        bool clampToScreen = false;  //when enabled the label stays on the canvas
        bool manualUpdate = false;  //if enabled, the visibleStack object in the canvas updates by looping through a list

        Drop dropScript;
        Helper helperScript;
        RaycastHit2D objectHit; //the boxcast hit result

        #region Get/set
        public bool FollowingUI
        {
            get
            {
                return followUI;
            }

            set
            {
                followUI = value;
            }
        }
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
        public RectTransform TargetUiTransform
        {
            get
            {
                return targetUiTransform;
            }

            set
            {
                targetUiTransform = value;
            }
        }
        public bool ManualUpdate
        {
            get
            {
                return manualUpdate;
            }

            set
            {
                manualUpdate = value;
            }
        }
        #endregion

        #region Initialize
        private void Awake() {
            SetComponents();
        }

        public void SetComponents() {
            thisRectTransform = GetComponent<RectTransform>();
            helperScript = GetComponent<Helper>();
            dropScript = GetComponent<Drop>();
        }

        //called when instantiating a label in the labelmanager
        public void Initialize() {
            ClearTargetUI();

            textOffset = labelScript.LabelSettings.LabelHeight;
            clampToScreen = labelScript.LabelSettings.ClampToScreen;

            UpdateState();
        }
        #endregion

        //if it's a normal label, just update position
        private void LateUpdate() {
            if (!ManualUpdate) {                
                switch (labelScript.LabelState) {
                    case LabelStates.InCameraFrustum:
                        //if normal label or icon, follow normally
                        if (LabelManager.singleton.EnableIcons || !labelScript.LabelSettings.Stack) {
                            FollowParentTargetScreenPoint();
                        }
                        break;
                    case LabelStates.OutCameraFrustum:
                        if (labelScript.LabelSettings.ClampToScreen) {
                            FollowParentTargetScreenPoint();
                        }
                        break;
                    case LabelStates.Inactive:
                        break;
                    default:
                        break;
                }
            }
        }
        
        /// <summary>
        /// Called by the script on the visible object in the canvas, loops through the list and calls manualLateUpdate
        /// </summary>
        public void ManualLateUpdate() {
            if (ManualUpdate) {
                if (helperScript.RunInBackground) {
                    FollowHiddenStateMachine();
                }
                else {
                    FollowStateMachine();
                }
            }
        }

        #region State machines
        /// <summary>
        /// The state machine for the hidden helper when stacking
        /// </summary>
        void FollowHiddenStateMachine() {
            //when clamped the follow scripts keep working but we only need the hiddenhelper to follow in the visible state
            switch (labelScript.LabelState) {
                case LabelStates.InCameraFrustum:
                    if (FollowingUI) {
                        FollowUiParent();
                        dropScript.CheckForDrop();
                    }
                    else {
                        FollowParentTargetScreenPoint();
                    }

                    //when pushing showloot, force the label to follow the hidden helper instead
                    if (LabelManager.singleton.PushingShowLabelsButton) {
                        backgroundImageRect.position = thisRectTransform.position;
                    }
                    break;
                case LabelStates.OutCameraFrustum:
                    break;
                case LabelStates.Inactive:
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// The state machine for the helper when stacking
        /// </summary>
        void FollowStateMachine() {
            //don't update unless highlighted/standard or clamped
            switch (labelScript.LabelState) {
                case LabelStates.InCameraFrustum:
                    switch (labelScript.VisibilityState) {
                        case VisibilityState.Normal:
                            if (!LabelManager.singleton.PushingShowLabelsButton) {
                                if (FollowingUI) {
                                    FollowUiParent();
                                    dropScript.CheckForDrop();
                                }
                                else {                                    
                                    FollowParentTargetScreenPoint();
                                }
                            }
                            break;
                        case VisibilityState.Hidden:
                            //don't update positions when forcing labels visible
                            if (!LabelManager.singleton.PushingShowLabelsButton) {
                                if (FollowingUI) {
                                    FollowUiParent();
                                    dropScript.CheckForDrop();
                                }
                                else {
                                    FollowParentTargetScreenPoint();
                                }
                            }
                            break;
                        default:
                            break;
                    }
                    break;
                case LabelStates.OutCameraFrustum:
                    Debug.Log("shouldn't be called");
                    break;
                case LabelStates.Inactive:
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// when the parent script changes states, the helpers get notified and states of the components get updated
        /// </summary>
        public void UpdateState() {
            if (helperScript.RunInBackground) {
                UpdateStateHiddenHelper();
            }
            else {
                UpdateStateHelper();
            }
        }

        void UpdateStateHiddenHelper() {
            switch (labelScript.LabelState) {
                case LabelStates.InCameraFrustum:
                    //turn on manual update if stack label and no icons
                    if (!LabelManager.singleton.EnableIcons && labelScript.LabelSettings.Stack) {
                        ManualUpdate = true;
                    }
                    else {
                        ManualUpdate = false;
                    }
                    break;
                case LabelStates.OutCameraFrustum:
                    //turn off manual update
                    ManualUpdate = false;

                    //when the labels go invisible they get placed in a different parent, this also means the order index of the labels can change
                    //if we don't clear the target we got situations where labels follow each other causing them to keep pushing up
                    ClearTargetUI();
                    break;
                case LabelStates.Inactive:
                    break;
                default:
                    break;
            }
        }

        void UpdateStateHelper() {
            switch (labelScript.LabelState) {
                case LabelStates.InCameraFrustum:
                    if (!LabelManager.singleton.PushingShowLabelsButton || LabelManager.singleton.EnableIcons) {
                        backgroundImageRect.localPosition = Vector3.zero;
                    }

                    //turn on manual update if stack label and no icons
                    if (!LabelManager.singleton.EnableIcons && labelScript.LabelSettings.Stack) {
                        ManualUpdate = true;
                    }
                    else {
                        ManualUpdate = false;
                    }

                    if (labelScript.VisibilityState == VisibilityState.Hidden) {
                        FollowingUI = false;
                    }
                    break;
                case LabelStates.OutCameraFrustum:
                    ManualUpdate = false;

                    if (clampToScreen) {
                        FollowingUI = false;
                        //when pushing show loot button, the text follows the hidden helper, but we need to reset it back to the labelhelper center when not pushing show loot or when clamped
                        backgroundImageRect.localPosition = Vector3.zero;
                    }
                    else {
                        //when the labels go invisible they get placed in a different parent, this also means the hierarchy of the labels can change
                        //if we don't clear the target we got situations where labels follow each other causing them to keep pushing up
                        ClearTargetUI();
                    }
                    break;
                case LabelStates.Inactive:
                    break;
                default:
                    break;
            }
        }

        #endregion

        #region Calculate positions
        /// <summary>
        /// Converts 3d position to screen point, Not used
        /// </summary>
        //public void UpdateTargetScreenPosition() {
        //    if (targetTransform == null) {
        //        Debug.Log("No target assigned to follow ");
        //        return;
        //    }

        //    if (!cameraSet) {
        //        SetCamera();
        //    }

        //    NPCScreenPosition = cam.WorldToScreenPoint(targetTransform.position);
        //}

        /// <summary>
        /// The Vector2 position of the label while it's just hovering above an object, not used
        /// </summary>
        //public Vector2 GetBasePosition() {
        //    tempPosition = NPCScreenPosition;
        //    tempPosition.y += yOffset;

        //    return tempPosition;
        //}

        /// <summary>
        /// The base position taken from the parent's followParent script
        /// </summary>
        public Vector2 GetParentBasePosition() {
            tempPosition = followParentScript.TargetScreenPosition;

            if (LabelManager.singleton.EnableIcons) {
                tempPosition.y += iconOffset;
            }
            else {
                tempPosition.y += textOffset;
            }

            return tempPosition;
        }

        /// <summary>
        /// Returns the stacked position plus all offsets, not used
        /// </summary>
        //public Vector2 GetStackedPosition() {
        //    tempPosition = NPCScreenPosition;
        //    tempPosition.y = targetUiTransform.position.y + targetUiTransform.sizeDelta.y + spaceBetweenLabels;
        //    return tempPosition;
        //}

        /// <summary>
        /// The stacked position based on the parent's screen position
        /// </summary>
        public Vector2 GetParentStackedPosition() {
            tempPosition = followParentScript.TargetScreenPosition;
            tempPosition.y = TargetUiTransform.position.y + TargetUiTransform.sizeDelta.y + spaceBetweenLabels;
            return tempPosition;
        }

        /// <summary>
        /// Returns the position under the label where another label can be
        /// </summary>
        public Vector2 GetPositionUnderLabel() {
            tempPosition = thisRectTransform.position;
            tempPosition.y -= (thisRectTransform.sizeDelta.y + spaceBetweenLabels);
            return tempPosition;
        }

        public Vector2 GetAnchoredPositionUnderLabel() {
            tempPosition = thisRectTransform.anchoredPosition;
            tempPosition.y -= (thisRectTransform.sizeDelta.y + spaceBetweenLabels);
            return tempPosition;
        }
        #endregion

        #region Follow functions
        /// <summary>
        /// Sets the 2D position for the label to the screen position of the target, not used
        /// </summary>
        //public void FollowTargetScreenPoint() {

        //    if (GetBasePosition() != previousPosition) {
        //        if (clampToScreen) {
        //            thisRectTransform.position = ClampToScreen(GetBasePosition());
        //        }
        //        else {
        //            thisRectTransform.position = GetBasePosition();
        //        }

        //        previousPosition = thisRectTransform.position;
        //    }
        //}

        /// <summary>
        /// Follows the UI element, while still following the x axis of the 3d object, not used
        /// </summary>
        //public void FollowUI() {
        //    if (targetUiTransform == null) {
        //        Debug.Log("No UI element assigned to follow ");
        //        return;
        //    }

        //    if (clampToScreen) {
        //        //stack untill it gets clamped, then turn off colliders and follow screenpoint
        //    }
        //    else {
        //        //don't drop the label beneath it's base position
        //        if (GetStackedPosition().y > GetBasePosition().y) {
        //            GetComponent<RectTransform>().position = GetStackedPosition();
        //            //GetComponent<RectTransform>().position = new Vector2(horizontalPos, verticalPos);
        //        }
        //        else {
        //            ClearTargetUI();
        //        }
        //    }
        //}

        /// <summary>
        /// Sets the 2D position for the label to the screen position of the target taken from the parent
        /// Used by all the helpers
        /// </summary>
        public void FollowParentTargetScreenPoint() {
            if (GetParentBasePosition() != previousPosition) {
                if (clampToScreen) {
                    thisRectTransform.position = ClampToScreen(GetParentBasePosition());
                }
                else {
                    thisRectTransform.position = GetParentBasePosition();
                }

                previousPosition = thisRectTransform.position;
            }
        }

        /// <summary>
        /// Follows the UI element, while still following the x axis of the 3d object, takes worldtoscreenpoint from parent
        /// </summary>
        public void FollowUiParent() {
            if (TargetUiTransform == null) {
                Debug.Log("No UI element assigned to follow ");
                return;
            }

            //don't drop the label beneath it's base position
            if (GetParentStackedPosition().y > GetParentBasePosition().y) {
                //only update when the position has changed
                if (GetParentStackedPosition() != previousPosition) {
                    thisRectTransform.position = GetParentStackedPosition();
                    previousPosition = thisRectTransform.position;
                }
            }
            else {
                //Debug.Log("postion under base, clear target");
                ClearTargetUI();
            }
        }
        #endregion

        #region assign target       
        /// <summary>
        /// Criteria for following a target
        /// </summary>
        /// <param name="targetLabel"></param>
        /// <returns></returns>
        public void SetTargetIfViable(GameObject targetLabel) {

            //if somehow the object targets itself, reset
            if (ReferenceEquals(gameObject, targetLabel)) {
                ClearTargetUI();
                return;
            }

            //- position above the target
            float verticalPos = targetLabel.GetComponent<RectTransform>().position.y + GetComponent<BoxCollider2D>().size.y + spaceBetweenLabels;

            //check if target is beneath base position
            if (verticalPos <= GetParentBasePosition().y) {
                //Debug.Log("base position");
                ClearTargetUI();
                return;
            }

            //- target must be visible
            //if (targetLabel.GetComponent<LabelStates>()) {
            //    if (!targetLabel.GetComponent<LabelStates>().LabelVisible) {
            //        Debug.Log("don't set target, it's invisible");
            //        //ClearTargetUI();
            //        StartCoroutine(helperScript.HelperReset());
            //        return;
            //    }
            //}

            //targets index must be lower
            int targetIndex = targetLabel.GetComponent<FollowTarget>().parentRect.GetSiblingIndex();
            int thisIndex = parentRect.GetSiblingIndex();

            if (targetIndex > thisIndex) {
                //Debug.Log("index");
                ClearTargetUI();
                return;
            }

            SetTargetUI(targetLabel);
        }
        
        /// <summary>
        /// Assigns the target the label should follow
        /// </summary>
        /// <param name="targetLabel"></param>
        public void SetTargetUI(GameObject targetLabel) {
            TargetUiTransform = targetLabel.GetComponent<RectTransform>();
            dropScript.TargetRigidbody = targetLabel.GetComponent<Rigidbody2D>();

            //set position instantly to prevent seeing it at the instantiate position
            thisRectTransform.position = GetParentStackedPosition();
            FollowingUI = true;
        }
        #endregion

        /// <summary>
        /// Clears the targets it was following and turns off following
        /// </summary>
        public void ClearTargetUI() {
            //Debug.Log("ClearTargetUI");
            FollowingUI = false;
            TargetUiTransform = null;
            dropScript.TargetRigidbody = null;
        }

        Vector2 clampedPosition;
        /// <summary>
        /// Clamps the position of the label within camera view.
        /// Adds half the label to keep it completely on screen.
        /// </summary>
        /// <param name="coordinates"></param>
        /// <returns></returns>
        public Vector2 ClampToScreen(Vector2 coordinates) {

            if (LabelManager.singleton.EnableIcons) {
                clampedPosition = new Vector2(
            Mathf.Clamp(coordinates.x, iconBackgroundRect.sizeDelta.x / 2, Screen.width - (iconBackgroundRect.sizeDelta.x / 2)),
            Mathf.Clamp(coordinates.y, iconBackgroundRect.sizeDelta.y / 2, Screen.height - (iconBackgroundRect.sizeDelta.y / 2)));
            }
            else {
                clampedPosition = new Vector2(
            Mathf.Clamp(coordinates.x, backgroundImageRect.sizeDelta.x / 2, Screen.width - (backgroundImageRect.sizeDelta.x / 2)),
            Mathf.Clamp(coordinates.y, backgroundImageRect.sizeDelta.y / 2, Screen.height - (backgroundImageRect.sizeDelta.y / 2)));
            }

            return clampedPosition;
        }
    }
}