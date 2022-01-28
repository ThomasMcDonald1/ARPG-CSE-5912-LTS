using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueActivator : MonoBehaviour, IInteractable
{
    [SerializeField] private DialogueObject dialogueObject;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && other.TryGetComponent(out Player player))
        {
            player.Interactable = this;

            Debug.Log("triggered");


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
                cap.GetComponent<Renderer>().material.color = new Color(51f/255f, 192f/255f, 14f/255f);
            }
        }
    }
    public void Interact(Player player)
    {
        player.transform.LookAt(this.transform.position);
        player.DialogueUI.ShowDialogue(dialogueObject);
    }
}
