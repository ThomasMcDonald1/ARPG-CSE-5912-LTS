using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using Ink.Runtime;

public class NPC : MonoBehaviour
{
    

    [SerializeField] Player player;


    [Header("Ink JSON")]
    [SerializeField] private TextAsset inkJSON;

    
    GameObject child;
    public float smooth;
    public float yVelocity;

    private bool isTalking;
    private bool isTrading;

    private bool hasNewInfo;
    //private bool hasAvailableQuest;

    private void Start()
    {
        hasNewInfo = false;
        isTalking = false;
        isTrading = false;

        child = transform.GetChild(0).gameObject;
        smooth = 0.3f;
        yVelocity = 0.0f;
    }

    private bool Interactable()
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

    IEnumerator BeginInteraction()
    {
        Quaternion rotate = Quaternion.LookRotation(player.transform.position - child.transform.position);

        while (Interactable())
        {
            child.transform.rotation = Quaternion.RotateTowards(child.transform.rotation, rotate, 50f * Time.deltaTime);
            child.transform.eulerAngles = new Vector3(0, child.transform.eulerAngles.y, 0);
            SetMenu();
            yield return null;
        }
        InteractionManager.GetInstance().StopInteraction();
        InteractionManager.GetInstance().DisableInteractionView();
    }

    private void SetMenu()
    {
        if (isTalking)
        {
            InteractionManager.GetInstance().EnterDialogueMode(inkJSON);
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

    public string GetName()
    {
        return this.tag;
    }

    public TextAsset GetCurrentDialogue()
    {
        return inkJSON;
    }
}
