using UnityEngine;

namespace LootLabels {
    /// <summary>
    /// Manager returns the correct path string to the resources folder
    /// </summary>
    public class ResourceManager : MonoBehaviour {

        public static ResourceManager singleton = null;

        void Awake() {
            //Check if instance already exists
            if (singleton == null) {
                //if not, set instance to this
                singleton = this;
            }

            //If instance already exists and it's not this:
            else if (singleton != this) {
                //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
                Destroy(gameObject);
            }
        }

        /// <summary>
        /// Returns the path to the model in the resources folder for the given geartype
        /// </summary>
        /// <param name="gearType"></param>
        /// <returns></returns>
        public string GetModelName(GearTypes gearType) {
            string modelPath = "LootLabels/3D models/";

            switch (gearType) {
                case GearTypes.Gloves:
                    return modelPath + "Gloves1";
                case GearTypes.Shoulders:
                    return modelPath + "Shoulders";
                case GearTypes.Belt:
                    return modelPath + "Belt";
                case GearTypes.Shoes:
                    return modelPath + "Shoes";
                case GearTypes.Lance:
                    return modelPath + "Lance";
                case GearTypes.Potion:
                    return modelPath + "Potion";
                case GearTypes.Shield:
                    return modelPath + "Shield";
                default:
                    Debug.Log("Case not implemented");
                    return modelPath + "Potion";
            }
        }

        /// <summary>
        /// Returns the path to the model in the resources folder for the given currencyType
        /// </summary>
        /// <param name="currencyType"></param>
        /// <returns></returns>
        public string GetModelName(CurrencyTypes currencyType) {
            string modelPath = "LootLabels/3D models/";

            switch (currencyType) {
                case CurrencyTypes.Gold:
                    return modelPath + "Gold";
                default:
                    Debug.Log("Case not implemented");
                    return modelPath + "Gold";
            }
        }

        /// <summary>
        /// Returns the path to the icon in the resources folder for the given geartype
        /// </summary>
        /// <param name="gearType"></param>
        /// <returns></returns>
        public string GetIconName(GearTypes gearType) {
            string iconPath = "LootLabels/Icons/UI_Icon_";

            switch (gearType) {
                case GearTypes.Gloves:
                    return iconPath + "InvGloves";
                case GearTypes.Shoulders:
                    return iconPath + "InvShoulders";
                case GearTypes.Belt:
                    return iconPath + "InvBelt";
                case GearTypes.Shoes:
                    return iconPath + "InvBoots";
                default:
                   // Debug.Log("Case not implemented");
                    return iconPath + "QuestionMark";
            }
        }

        /// <summary>
        /// Returns the path to the icon in the resources folder for the given currencyType
        /// </summary>
        /// <param name="currencyType"></param>
        /// <returns></returns>
        public string GetIconName(CurrencyTypes currencyType) {
            string iconPath = "LootLabels/Icons/UI_Icon_";

            switch (currencyType) {
                case CurrencyTypes.Gold:
                    return iconPath + "Coin";
                default:
                    Debug.Log("Case not implemented");
                    return iconPath + "QuestionMark";
            }
        }
    }
}