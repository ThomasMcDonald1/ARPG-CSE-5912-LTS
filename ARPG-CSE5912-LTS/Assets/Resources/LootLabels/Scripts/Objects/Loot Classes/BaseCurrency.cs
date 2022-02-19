namespace LootLabels {
    /// <summary>
    /// Base class for currency objects
    /// </summary>
    [System.Serializable]
    public class BaseCurrency : BaseObject {
        
        public CurrencyTypes CurrencyType;
        public int CurrencyValue;

        public BaseCurrency() {
            CurrencyType = CurrencyTypes.Gold;
            CurrencyValue = 1;
            ItemName = CurrencyValue + " " + CurrencyType;
            ModelName = "LootLabels/3D models/Gold";
            IconName = "LootLabels/Icons/UI_Icon_Coin";
        }

        public BaseCurrency(CurrencyTypes currencyType, int amount, string modelName, string iconName) {
            CurrencyType = currencyType;
            CurrencyValue = amount;
            ItemName = CurrencyValue + " " + CurrencyType;
            ModelName = modelName;
            IconName = iconName;
        }
    }
}