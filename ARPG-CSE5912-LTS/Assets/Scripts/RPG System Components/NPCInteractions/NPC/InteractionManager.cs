using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Ink.Runtime;
using UnityEngine.UI;

public class InteractionManager : MonoBehaviour
{
    [Header("Dialogue UI")]
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private TextMeshProUGUI DisplayDialogueName;
    [SerializeField] private TextMeshProUGUI DisplayOptionsName;

    [SerializeField] private GameObject continueDialogueButton;
    [SerializeField] private GameObject worldNames;
    [SerializeField] private GameObject optionsMenu;
    [SerializeField] private GameObject dialogueBox;

    [SerializeField] Player player;

    private Story currentStory;

    private static InteractionManager instance;

    private void Update()
    {
        if (player.NPCTarget == null) return;
        else
        {      
            switch (player.NPCTarget.transform.tag)
            {
                case "StartBlacksmith":
                    SetNames("Berry");
                    break;
                case "StartGeneralStore":
                    SetNames("Mary");
                    break;
                case "StartLorekeeper":
                    SetNames("Larry");
                    break;
                default:
                    SetNames("");
                    break;
            }
        }
    }

    private void SetNames(string name)
    {
        DisplayDialogueName.text = name;
        DisplayOptionsName.text = name;
    }

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than 1 instance of the dialogue manager found...");
        }
        instance = this;
    }

    public static InteractionManager GetInstance()
    {
        return instance;
    }

    public void EnterDialogueMode(TextAsset inkJSON)
    {
        dialogueBox.SetActive(true);
        optionsMenu.SetActive(false);
        if (currentStory.canContinue)
        {
            continueDialogueButton.SetActive(true);
        }
        else
        {
            continueDialogueButton.SetActive(false);
        }
    }

    public void ContinueStory()
    {
        dialogueText.text = currentStory.Continue();
    }

    // We will use these for now... later we will incoroporate ink & update text this way

    public void EnterOptionsMenu()
    {
        worldNames.SetActive(false);
        dialogueBox.SetActive(false);
        optionsMenu.SetActive(true);
    }

    public void DisableInteractionView()
    {
        dialogueBox.SetActive(false);
        optionsMenu.SetActive(false);
        worldNames.SetActive(true);
    }

    public void EnterTradeMenu()
    {
        optionsMenu.SetActive(false);
    }

    public void BeginDialogue()
    {
        currentStory = new Story(player.NPCTarget.GetComponent<NPC>().GetCurrentDialogue().text);
        player.NPCTarget.GetComponent<NPC>().SetDialogue();
        if (currentStory.canContinue)
        {
            ContinueStory();
            if (currentStory.canContinue)
            {
                continueDialogueButton.SetActive(true);
            }
        }

    }

    public void StopDialogue()
    {
        dialogueText.text = "";
        player.NPCTarget.GetComponent<NPC>().StopDialogue();
    }

    public void ExitDialogueMenu()
    {
        player.NPCTarget.GetComponent<NPC>().StopDialogue();
    }

    public void StopInteraction()
    {
        player.NPCTarget = null;
    }


}

