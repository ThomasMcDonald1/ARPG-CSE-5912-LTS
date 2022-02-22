using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace LootLabels {
    /// <summary>
    /// Class takes care of showing and hiding the label's text/images according to the label states
    /// </summary>
    public class LabelVisibility : MonoBehaviour {

        public Color mouseoverColor;    //The mouse over color of the background image
        public Color standardColor;     //The standard color of the background image        
        public float timeTillTransparent = 4;       //the time untill the object starts to become transparent
        public float timeTransparentTransition = 1.5f; //the time it takes to go from opaque to transparent

        Text labelText; //cached label text
        Image labelBG;  //cached text background
        Image IconBG;   //cached icon background
        Image Icon;     //cached label icon

        bool autoHideLabel = false;          //when turned on the label dissappears after "timeTillTransparent" seconds
        bool clampToScreen = false;     //when turned on the label stays in view of the camera
        Color rarityColor; //the text color/icon background color, depends on the rarity of the object

        Label labelScript;

        private void Awake() {
            labelScript = GetComponent<Label>();

            labelText = labelScript.textGO.GetComponent<Text>();
            labelBG = labelScript.backgroundImageGO.GetComponent<Image>();
            IconBG = labelScript.IconBG;
            Icon = labelScript.Icon;
        }

        //State machine
        public void UpdateVisibilityState() {
            switch (labelScript.LabelState) {
                case LabelStates.InCameraFrustum:
                    switch (labelScript.VisibilityState) {
                        case VisibilityState.Normal:
                            if (labelScript.Highlighted) {
                                HighlightLabel();
                            }
                            else {
                                ShowLabel();
                            }
                            break;
                        case VisibilityState.Hidden:
                            if (labelScript.ForceVisible) {
                                if (labelScript.Highlighted) {
                                    HighlightLabel();
                                }
                                else {
                                    ShowLabel();
                                }
                            }
                            else {
                                if (labelScript.Highlighted) {
                                    HighlightLabel();
                                }
                                else {
                                    HideLabel();
                                }
                            }
                            break;
                        default:
                            break;
                    }
                    break;
                case LabelStates.OutCameraFrustum:
                    if (autoHideLabel) {
                        //we need to set this now to remember when the label comes in view again
                        labelScript.VisibilityState = VisibilityState.Hidden;
                    }

                    if (clampToScreen) {
                        if (labelScript.Highlighted) {
                            HighlightLabel();
                        }
                        else {
                            ShowLabel();
                        }
                    }
                    else {
                        HideLabel();
                    }
                    break;
                case LabelStates.Inactive:
                    HideLabel();
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// When a label gets reused from the pool, reset the values and start fade if checked
        /// </summary>
        public void ResetState() {
            rarityColor = labelScript.LabelSettings.RarityColor;
            autoHideLabel = labelScript.LabelSettings.AutoHide;
            clampToScreen = labelScript.LabelSettings.ClampToScreen;

            if (autoHideLabel) {
                StartCoroutine(FadeToTransparent());
            }
        }

        /// <summary>
        /// After X seconds the labels starts to fade if not clicked
        /// </summary>
        /// <returns></returns>
        public IEnumerator FadeToTransparent() {
            yield return new WaitForSeconds(timeTillTransparent);

            //cancels the fade if it's looted while dissappearing
            if (labelScript.LabelState == LabelStates.Inactive) {
                //UpdateVisibilityState();
                yield break;
            }

            float alphaValue;   //the current alpha value for the text/icon
            float alphaBgValue;  //the current alpha value for the background

            Color tempColor;
            Color tempBgColor;

            if (LabelManager.singleton.EnableIcons) {
                alphaValue = GetColor(Icon).a;
                alphaBgValue = GetColor(IconBG).a;

                tempColor = GetColor(Icon);
                tempBgColor = GetColor(IconBG);
            }
            else {
                alphaValue = GetColor(labelText).a;
                alphaBgValue = GetColor(labelBG).a;

                tempColor = GetColor(labelText);
                tempBgColor = GetColor(labelBG);
            }

            for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / timeTransparentTransition) {

                if (LabelManager.singleton.EnableIcons) {
                    SetColor(Icon, LerpTransparent(tempColor, alphaValue, t));
                    SetColor(IconBG, LerpTransparent(tempBgColor, alphaBgValue, t));
                }
                else {
                    SetColor(labelText, LerpTransparent(tempColor, alphaValue, t));
                    SetColor(labelBG, LerpTransparent(tempBgColor, alphaBgValue, t));
                }


                //cancels the fade if mousing over while fading
                if (labelScript.ForceVisible || labelScript.Highlighted) {
                    labelScript.VisibilityState = VisibilityState.Hidden;
                    UpdateVisibilityState();
                    yield break;
                }

                yield return null;
            }

            labelScript.VisibilityState = VisibilityState.Hidden;
            labelScript.UpdateComponentStates();
        }

        #region Label functions
        //shows the label highlighted
        void HighlightLabel() {
            if (LabelManager.singleton.EnableIcons) {
                SetIconTransparent();
                SetTextOpaque();
            }
            else {
                HighlightText();
                SetIconTransparent();
            }
        }

        //shows the label normally
        void ShowLabel() {
            if (LabelManager.singleton.EnableIcons) {
                SetIconOpaque();
                SetTextTransparent();
            }
            else {
                //Debug.Log("ShowLabel");
                SetTextOpaque();
                SetIconTransparent();
            }
        }

        //hides the label and turns off the physics so it doesn't get called in trigger events unless highlighted
        void HideLabel() {
            if (LabelManager.singleton.EnableIcons) {
                SetIconTransparent();
                SetTextTransparent();
            }
            else {
                SetTextTransparent();
            }
        }
        #endregion

        #region Text Label
        //make the text highlighted for mouse over
        void HighlightText() {
            SetTextOpaque();
            SetColor(labelBG, mouseoverColor);
        }

        //make the text visible 
        void SetTextOpaque() {
            //Debug.Log("shouldnt call");
            SetColor(labelText, SetOpaque(rarityColor));
            SetColor(labelBG, standardColor);
        }

        //make the text transparent
        public void SetTextTransparent() {
            SetColor(labelText, SetTransparent(GetColor(labelText)));
            SetColor(labelBG, SetTransparent(GetColor(labelBG)));
        }
        #endregion

        #region Icon Label
        void HighlightIcon() {
            //hide icon and show the text label
            SetIconTransparent();
            SetTextOpaque();
        }

        //make the icon visible 
        void SetIconOpaque() {
            SetColor(Icon, SetOpaque(GetColor(Icon)));
            SetColor(IconBG, SetOpaque(rarityColor));
        }

        //make the icon transparent
        void SetIconTransparent() {
            SetColor(Icon, SetTransparent(GetColor(Icon)));
            SetColor(IconBG, SetTransparent(GetColor(IconBG)));
        }
        #endregion

        #region color functions
        /// <summary>
        /// Sets the given color's alpha value to 1
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        Color SetOpaque(Color color) {
            Color tempColor = color;
            tempColor.a = 1;
            return tempColor;
        }

        /// <summary>
        /// Sets the given color's alpha value to 0
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        Color SetTransparent(Color color) {
            Color tempColor = color;
            tempColor.a = 0;
            return tempColor;
        }

        /// <summary>
        /// Lerps the alpha value from a given color from it's initial alpha value to 0 over lerpValue time
        /// </summary>
        /// <param name="tempColor"></param>
        /// <param name="alphaValue"></param>
        /// <param name="lerpValue"></param>
        /// <returns></returns>
        Color LerpTransparent(Color tempColor, float alphaValue, float lerpValue) {
            tempColor.a = Mathf.Lerp(alphaValue, 0, lerpValue);
            return tempColor;
        }

        #endregion

        #region get/set colors      
        /// <summary>
        /// Set the color for the given Image
        /// </summary>
        /// <param name="image"></param>
        /// <param name="color"></param>
        void SetColor(Image image, Color color) {
            image.color = color;
        }

        /// <summary>
        /// Set the color for the given text
        /// </summary>
        /// <param name="text"></param>
        /// <param name="color"></param>
        void SetColor(Text text, Color color) {
            text.color = color;
        }

        /// <summary>
        /// Return the color from the given Image
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        Color GetColor(Image image) {
            return image.color;
        }

        /// <summary>
        /// Return the color from the given Text
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        Color GetColor(Text text) {
            return text.color;
        }
        #endregion
    }
}