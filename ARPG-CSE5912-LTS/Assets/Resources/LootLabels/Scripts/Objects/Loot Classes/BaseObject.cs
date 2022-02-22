namespace LootLabels {
    /// <summary>
    /// Base object class, all loot inherits from this
    /// </summary>
    [System.Serializable]
    public class BaseObject {

        public string ItemName;     //the full name you want displayed
        public string ModelName;    //the name of the 3d model prefab in the resources folder
        public string IconName;     //the name if the icon in the resources folder

    }
}