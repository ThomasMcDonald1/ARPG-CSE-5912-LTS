using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueActivator : MonoBehaviour, IInteractable
{
    [SerializeField] private DialogueObject dialogueObject;
    //bool started = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && other.TryGetComponent(out Player player))
        {
            player.Interactable = this;

            Debug.Log("triggered");



            ////fix bug where this gets triggered at start of game, only for game scene
            //if (!started)
            //{
            //    player.Interactable = null;
            //    started = true;
            //}


            GameObject cap;
            cap = this.transform.Find("Capsule").gameObject;
            cap.GetComponent<Renderer>().material.color = new Color(0, 204, 102);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && other.TryGetComponent(out Player player))
        {
            if (player.Interactable is DialogueActivator dialogueActivator && dialogueActivator == this)
            {
                player.Interactable = null;

                GameObject cap;
                cap = this.transform.Find("Capsule").gameObject;
                cap.GetComponent<Renderer>().material.color = new Color(0, 0, 0);
            }
        }
    }
    public void Interact(Player player)
    {
        player.DialogueUI.ShowDialogue(dialogueObject);
    }
}
