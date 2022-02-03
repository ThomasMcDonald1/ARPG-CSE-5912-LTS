using UnityEngine;

namespace LootLabels {
    /// <summary>
    /// Example class for NPC
    /// </summary>
    [RequireComponent(typeof(CreateLabel))]
    [RequireComponent(typeof(ObjectHighlight))]
    public class NPC : InteractableObject {

        public string objectName;    //the name of the object, this name is shown in the labels

        void Start() {
            SpawnLabel();
        }

        /// <summary>
        /// Creates the label!!
        /// Called in animator or in start when there is no animator
        /// </summary>
        public override void SpawnLabel() {
            GetComponent<EventHandler>().ClearDelegates();
            GetComponent<EventHandler>().SubscribeMouseEvents(MouseDownFunction, MouseEnterFunction, MouseExitFunction);

            GetComponent<CreateLabel>().SpawnLabelByColor(objectName, "LootLabels/Icons/UI_Icon_Chat");

            StartCoroutine(GetComponent<EventHandler>().VisibilityCoroutine());
        }

        //Add your mouse logic here
        public override void MouseDownFunction() {
            Debug.Log("Buy legendary weapon? Price: Bout three fiddy");
        }

        public override void MouseEnterFunction() {
            GetComponent<ObjectHighlight>().HighlightObject();
        }

        public override void MouseExitFunction() {
            GetComponent<ObjectHighlight>().StopHighlightObject();
        }
    }
}