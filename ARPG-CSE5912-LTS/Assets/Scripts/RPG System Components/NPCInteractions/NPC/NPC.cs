using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class NPC : NPCManager
{
    [SerializeField] Player player;
    [SerializeField] GameObject worldNames;
    [SerializeField] GameObject optionsMenu;
    [SerializeField] GameObject dialogueBox;
    [SerializeField] Button proceedToQuestButton;

 
    GameObject child;
    public float smooth;
    public float yVelocity;

    private bool isTalking;
    private bool isTrading;
    //private bool hasAvailableQuest;

    private void Start()
    {
        isTalking = false;
        isTrading = false;

        child = transform.GetChild(0).gameObject;
        smooth = 0.3f;
        yVelocity = 0.0f;
    }

    protected override bool Interactable()
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

    public override void Interact()
    {
        if (Interactable())
        {
            OpenMainMenu();
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
        player.NPCTarget = null;
        DisableInteractionView();
    }

    private void SetMenu()
    {
        if (isTalking)
        {
            dialogueBox.SetActive(true);
            optionsMenu.SetActive(false);
        }
        else if (isTrading)
        {
            // enable trade menu here
            optionsMenu.SetActive(false);
        }
        else
        {
            dialogueBox.SetActive(false);
            optionsMenu.SetActive(true);
        }
    }

    public void StopDialogue()
    {
        isTalking = false;
    }
    
    public void OpenDialogue()
    {
        isTalking = true;
    }

    public void OpenMainMenu()
    {
        worldNames.SetActive(false);
        dialogueBox.SetActive(false);
        optionsMenu.SetActive(true);
    }

    public void StopInteraction()
    {
        player.NPCTarget = null;
    }

    public void DisableInteractionView()
    {
        dialogueBox.SetActive(false);
        optionsMenu.SetActive(false);
        worldNames.SetActive(true);
    }
}
