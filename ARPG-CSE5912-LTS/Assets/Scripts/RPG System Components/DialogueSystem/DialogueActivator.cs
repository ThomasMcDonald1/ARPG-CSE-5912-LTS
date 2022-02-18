using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class DialogueActivator : MonoBehaviour, IInteractable
{
    [SerializeField] private DialogueObject dialogueObject;
    NavMeshAgent playerAgent;
    NavMeshAgent agent;
    Player thePlayer;
    private void Start()
    {
        agent = this.GetComponent<NavMeshAgent>();
        agent.stoppingDistance = 2f;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && other.TryGetComponent(out Player player))
        {
            thePlayer = player;
            //playerAgent = player.GetComponent<NavMeshAgent>();
            //playerAgent.isStopped = true;
            player.GetComponent<NavMeshAgent>().enabled = false;
            player.Interactable = this;
            player.Interactable.Interact(player);
            Debug.Log("triggered");


            ////GameObject cap;
            ////cap = this.transform.Find("Capsule").gameObject;
            ////cap.GetComponent<Renderer>().material.color = new Color(0, 204, 102);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && other.TryGetComponent(out Player player))
        {
            if (player.Interactable is DialogueActivator dialogueActivator && dialogueActivator == this)
            {
                player.Interactable = null;

                //GameObject cap;
                //cap = this.transform.Find("Capsule").gameObject;
                //cap.GetComponent<Renderer>().material.color = new Color(51f/255f, 192f/255f, 14f/255f);
            }
        }
    }
    public void Interact(Player player)
    {
        player.DialogueUI.ShowDialogue(dialogueObject);
    }
    private void Update()
    {
        if (thePlayer != null)
        {
            if (thePlayer.DialogueUI.IsOpen)
            {
                agent.destination = thePlayer.transform.position;
                //thePlayer.GetComponent<NavMeshAgent>().isStopped = false;
                //thePlayer.GetComponent<NavMeshAgent>().enabled = true;
            }
            else
            {
                thePlayer.GetComponent<NavMeshAgent>().enabled = true;

            }
        }
    }
}
