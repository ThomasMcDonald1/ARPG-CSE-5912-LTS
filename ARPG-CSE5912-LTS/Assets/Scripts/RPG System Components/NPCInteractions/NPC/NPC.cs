using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using Ink.Runtime;

public abstract class NPC : MonoBehaviour
{
    

    [SerializeField] protected Player player;

    
    public float smooth;
    public float yVelocity;


    protected bool isTalking;
    protected bool isTrading;

    protected bool hasNewInfo;
    //private bool hasAvailableQuest;

    private void Start()
    {
        hasNewInfo = false;
        isTalking = false;
        isTrading = false;

        smooth = 0.3f;
        yVelocity = 0.0f;
    }

    protected bool Interactable()
    {
        if (player.NPCTarget == this && player.InInteractNPCRange())
        {
            return true;
        }
        else
        {
            isTalking = false;
            isTrading = false;
        }
        return false;
    }

    public void Interact()
    {
        if (Interactable())
        {
            if (!hasNewInfo) { InteractionManager.GetInstance().EnterOptionsMenu(); }
            else { SetDialogue(); }
            StartCoroutine(BeginInteraction());
        }
    }

    protected abstract IEnumerator BeginInteraction();


    protected void SetMenu()
    {
        if (isTalking)
        {
            InteractionManager.GetInstance().EnterDialogueMode(player.NPCTarget.GetComponent<NPC>().GetCurrentDialogue());
        }
        else if (isTrading)
        {
            // enable trade menu here
            InteractionManager.GetInstance().EnterTradeMenu();
        }
        else
        {
            InteractionManager.GetInstance().EnterOptionsMenu();
        }
    }

    public void StopDialogue()
    {
        isTalking = false;
    }
    
    public void SetDialogue()
    {
        isTalking = true;
    }

    public void StopTrading()
    {
        isTrading = false;
    }

    public void StartTrading()
    {
        isTrading = true;
    }

    public string GetName()
    {
        return this.tag;
    }

    public abstract TextAsset GetCurrentDialogue();
    public abstract void NextStory();
}
