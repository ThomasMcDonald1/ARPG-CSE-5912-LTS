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
    [SerializeField] private GameObject tradeMenu;
    [SerializeField] public TextMeshProUGUI playerMoneyText;

    [SerializeField] public GameObject shopSlotUI;
    public Transform ShopSlots;
    ShopSlot[] shopSlots;

    [SerializeField] PlayerMoney playerMoney;
    [SerializeField] public UI_Sale SaleUI;


    [SerializeField] Player player;

    [SerializeField] private float typingSpeed = 0.04f;

    private Story currentStory;

    private static InteractionManager instance;

    private Coroutine displayLineCoroutine;
  

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
        shopSlots = ShopSlots.GetComponentsInChildren<ShopSlot>();
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
    }

    public void ContinueStory()
    {
        if (displayLineCoroutine != null)
        {
            StopCoroutine(displayLineCoroutine);
        }
        displayLineCoroutine = StartCoroutine(DisplayLine(currentStory.Continue()));
        if (currentStory.canContinue)
        {
            continueDialogueButton.SetActive(true);
        }
        else
        {
            continueDialogueButton.SetActive(false);
        }
    }

    private IEnumerator DisplayLine(string line)
    {
        dialogueText.text = "";
        foreach (char letter in line.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    public void EnterOptionsMenu()
    {
        worldNames.SetActive(false);
        dialogueBox.SetActive(false);
        tradeMenu.SetActive(false);
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
        //player.NPCTarget.GetComponent<NPC>().StartTrading();
        tradeMenu.SetActive(true);
        optionsMenu.SetActive(false);
        worldNames.SetActive(false);
        dialogueBox.SetActive(false);
        playerMoneyText.SetText("Player: "+playerMoney.money.ToString()+"$");
        //StopCoroutine(player.NPCTarget.GetComponent<NPC>().BeginInteraction());
        foreach (ShopSlot slot in shopSlots)
        {
            //slot.purchaseButton.enabled = true;
            if (slot.icon.IsActive())
            {
                if (playerMoney.money - slot.item.cost < 0)
                {
                    slot.purchaseButton.interactable = false;
                }
                else
                {
                    slot.purchaseButton.interactable = true;
                }
            }
        }
        SaleUI.updateUI();
    }

    public void BeginDialogue()
    {
        currentStory = new Story(player.NPCTarget.GetComponent<NPC>().GetCurrentDialogue().text);
        EnterDialogueMode(player.NPCTarget.GetComponent<NPC>().GetCurrentDialogue());
        if (currentStory.canContinue)
        {
            ContinueStory();
            if (currentStory.canContinue)
            {
                continueDialogueButton.SetActive(true);
            }
        }
    }

    public void StopInteraction()
    {
        player.NPCTarget = null;
        worldNames.SetActive(true);
        continueDialogueButton.SetActive(false);
        optionsMenu.SetActive(false);
        dialogueBox.SetActive(false);
        tradeMenu.SetActive(false);
    }

}

