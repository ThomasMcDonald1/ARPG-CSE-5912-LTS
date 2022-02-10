using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace LootLabels {
    [RequireComponent(typeof(LabelVisibility))]
    [RequireComponent(typeof(LabelEvents))]
    public class Label : MonoBehaviour {
        public GameObject helper;    //the stacking helper containing the text
        public GameObject helperHidden;    //a box that stays stacked so we can reposition immediatly without restacking
        public GameObject textGO;   //the text object
        public GameObject backgroundImageGO;    //the text background  
        public Image IconBG;    //the icon's background
        public Image Icon;  //The icon of the object
        
        bool iconsEnabled = false;  //bool to check if the labelmanager's icons toggle has been changed

        [HideInInspector]
        public bool initialized = false;   //bool to prevent onbecamevisible showing the label before the stack script does
        
        [HideInInspector]
        public LabelSettings LabelSettings;//Struct containing all the label's settings

        [HideInInspector]
        public LabelStates LabelState = LabelStates.InCameraFrustum;   //the state of the label in respect to the camera frustum

        [HideInInspector]
        public VisibilityState VisibilityState = VisibilityState.Normal;   //is the label hidden or visible

        [HideInInspector]
        public bool Highlighted = false;    //bool when the mouse is over the label so that it's highlighted

        [HideInInspector]
        public bool ForceVisible = false;  //bool to check if showloot button is pushed 

        private void Awake() {
           // iconsEnabled = LabelManager.singleton.EnableIcons;
        }

        private void Update() {
            if (LabelState == LabelStates.InCameraFrustum) {
                if (ForceVisible != LabelManager.singleton.PushingShowLabelsButton) {
                    ForceVisible = LabelManager.singleton.PushingShowLabelsButton;

                    UpdateComponentStates();
                }
            }

            if (iconsEnabled != LabelManager.singleton.EnableIcons) {
                ToggleIcon();
                iconsEnabled = LabelManager.singleton.EnableIcons;
            }
        }

        public virtual void SetLabelState(LabelStates StateLabel) {
            LabelState = StateLabel;
            UpdateLabelState();
        }

        public virtual void UpdateLabelState() {
            UpdateComponentStates();

            switch (LabelState) {
                case LabelStates.InCameraFrustum:
                    //label needs to be active otherwise GetComponentsInChildren doesn't work
                    gameObject.SetActive(true);

                    transform.SetParent(LabelManager.singleton.LabelVisible.transform, false);
                    break;
                case LabelStates.OutCameraFrustum:
                    if (LabelSettings.ClampToScreen) {
                        transform.SetParent(LabelManager.singleton.LabelVisible.transform, false);
                    }
                    else {
                        transform.SetParent(LabelManager.singleton.LabelInvisible.transform, false);
                        gameObject.SetActive(false);
                    }
                    break;
                case LabelStates.Inactive:
                    initialized = false;
                    GetComponent<LabelEvents>().UnsubscribeEvents();
                    transform.SetParent(LabelManager.singleton.LabelPool.transform, false);
                    gameObject.SetActive(false);
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// When the label's state changes, the other components get notified
        /// </summary>
        public virtual void UpdateComponentStates() {
            GetComponent<LabelVisibility>().UpdateVisibilityState();

            helper.GetComponent<Helper>().UpdateHelperState();
            helperHidden.GetComponent<Helper>().UpdateHelperState();
        }

        /// <summary>
        /// If the enable icon setting in the manager is switched, the Icon gets toggled
        /// </summary>
        void ToggleIcon() {

            //toggle raycasts
            ToggleRaycasting();

            //set in correct parent, update visibility, Toggle physics
            UpdateLabelState();
        }

        #region Initializing
        public void Initialize(LabelSettings settings, EventHandler objectEvents) {
            gameObject.SetActive(true);

            ResetState();

            LabelSettings = settings;

            GetComponent<LabelEvents>().SubscribeEvents(objectEvents);

            SetTarget();
            SetText();
            SetIcon();
            ToggleRaycasting();

            GetComponent<LabelVisibility>().ResetState();

            StartCoroutine(SetBoxColliderSize());

            //if stacking, don't call UpdateLabelState() at initialization, first the stack script checks for overlap and then updates the state if nothing overlaps
            if (LabelManager.singleton.EnableIcons || !LabelSettings.Stack) {
                initialized = true;
                UpdateLabelState();
            }
        }
                
        /// <summary>
        /// Resets the label's state values when creating or reusing a label
        /// </summary>
        public virtual void ResetState() {
            initialized = false;
            LabelState = LabelStates.InCameraFrustum;
            VisibilityState = VisibilityState.Normal;
            Highlighted = false;
        }

        /// <summary>
        /// Sets the target to follow for the children
        /// </summary>
        /// <param name="target"></param>
        /// <param name="clampToScreen"></param>
        public virtual void SetTarget() {
            GetComponent<FollowParent>().SetTarget();

            helper.GetComponent<FollowTarget>().Initialize();
            helperHidden.GetComponent<FollowTarget>().Initialize();
        }

        /// <summary>
        /// Assigns the text to the text object
        /// </summary>
        void SetText() {
            textGO.GetComponent<Text>().text = LabelSettings.Text;

            //don't set color when stacking
            if (!LabelSettings.Stack) {
                textGO.GetComponent<Text>().color = LabelSettings.RarityColor;
            }
        }

        /// <summary>
        /// Assigns the correct icon to the icon object
        /// </summary>
        void SetIcon() {
            Icon.sprite = Resources.Load<Sprite>(LabelSettings.IconName);
        }

        /// <summary>
        /// When using icons, only enable mouse detection on the background icon, vice versa for text
        /// </summary>
        void ToggleRaycasting() {
            if (LabelManager.singleton.EnableIcons) {
                IconBG.GetComponent<Image>().raycastTarget = true;
                backgroundImageGO.GetComponent<Image>().raycastTarget = false;
            }
            else {
                IconBG.GetComponent<Image>().raycastTarget = false;
                backgroundImageGO.GetComponent<Image>().raycastTarget = true;
            }
        }

        /// <summary>
        /// After the text is set we need to wait a frame for the contentsizefitter to adjust it's size, then we copy the size to the label background and boxcolliders
        /// </summary>
        /// <returns></returns>
        public IEnumerator SetBoxColliderSize() {
            yield return new WaitForEndOfFrame();
            //yield return null;

            //take the size of the text object and add some padding at the sides
            Vector2 colliderSize = new Vector2(
                textGO.GetComponent<RectTransform>().rect.width + 10,
                textGO.GetComponent<RectTransform>().rect.height);

            //set the size of the background image and the colliders for the helpers
            backgroundImageGO.GetComponent<RectTransform>().sizeDelta = colliderSize;

            helper.GetComponent<Helper>().Initialize(colliderSize);
            helperHidden.GetComponent<Helper>().Initialize(colliderSize);
        }
        #endregion
    }
}