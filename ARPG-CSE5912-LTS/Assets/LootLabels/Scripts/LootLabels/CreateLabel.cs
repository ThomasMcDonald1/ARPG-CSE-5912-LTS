using UnityEngine;

namespace LootLabels {
    /// <summary>
    /// Struct containing all the settings a label needs to initialize
    /// </summary>
    public struct LabelSettings {
        public float LabelHeight;
        public GameObject ObjectToFollow;
        public string Text, IconName;
        public Color RarityColor;
        public bool Stack, AutoHide, ClampToScreen, DisableOnClick;

        public LabelSettings(bool stack, float labelHeight, GameObject objectToFollow, string text, Color rarityColor, bool autoHide, bool clampToScreen, bool disableOnClick, string iconName) {
            Stack = stack;
            LabelHeight = labelHeight;
            ObjectToFollow = objectToFollow;
            Text = text;
            RarityColor = rarityColor;
            AutoHide = autoHide;
            ClampToScreen = clampToScreen;
            DisableOnClick = disableOnClick;
            IconName = iconName;
        }
    }

    /// <summary>
    /// Script used on the desired object for label creation
    /// </summary>
    public class CreateLabel : MonoBehaviour {
        public float labelHeight = 25;      //the amount of height the label hovers above the object      
        public GameObject objectToFollow;   //assign the object you want the label to follow here
        public Color rarityColor = new Color32(255, 255, 255, 255);     //color of the label's text or icon's background circle, only used if label is spawned by color
        public bool stack = false;          //if you want the label to stack or not
        public bool autoHide = false;       //if enabled to label starts to fade away after X seconds and can only be shown by mouse over or pushing the show loot button
        public bool clampToScreen = false;  //if enabled the label stays in the viewport
        public bool disableOnClick = false; //if you want the label to be destroyed after clicking on the object, f.e. when opening a chest, the label doesn't need to stay alive

        LabelSettings labelSettings;

        /// <summary>
        /// Instantiates a label with a color based on the given rarity
        /// </summary>
        /// <param name="text"></param>
        /// <param name="iconName"></param>
        /// <param name="rarity"></param>
        public void SpawnLabelByRarity(string text, string iconName, Rarity rarity) {
            if (objectToFollow == null) {
                Debug.Log("Assign object to follow in createLabel script");
                return;
            }

            Color itemRarityColor = LootManager.singleton.RarityColors.ReturnRarityColor(rarity);
            labelSettings = new LabelSettings(stack, labelHeight, objectToFollow, text, itemRarityColor, autoHide, clampToScreen, disableOnClick, iconName);

            LabelManager.singleton.InstantiateLabel(labelSettings, GetComponent<EventHandler>());
        }

        /// <summary>
        /// Instantiates a label with a manually given color
        /// </summary>
        /// <param name="text"></param>
        /// <param name="iconName"></param>
        public void SpawnLabelByColor(string text, string iconName) {
            if (objectToFollow == null) {
                Debug.Log("Assign object to follow in createLabel script for "+gameObject.name);
                return;
            }

            labelSettings = new LabelSettings(stack, labelHeight, objectToFollow, text, rarityColor, autoHide, clampToScreen, disableOnClick, iconName);

            LabelManager.singleton.InstantiateLabel(labelSettings, GetComponent<EventHandler>());
        }
    }
}