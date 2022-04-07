using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Ink.Runtime;
using UnityEngine.UI;
using System;


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

    [SerializeField] private GameObject travelMenu;

    [SerializeField] private GameObject tradeMenu;
    [SerializeField] private GameObject TradeButton;
    [SerializeField] private GameObject PorterButton;

    [SerializeField] public UI_shop shopUI;
    [SerializeField] public TextMeshProUGUI playerMoneyText;

    public Transform ShopSlots;
    ShopSlot[] shopSlots;

    [SerializeField] private PlayerMoney playerMoney;
    [SerializeField] public UI_Sale SaleUI;

    public static event EventHandler EndOfStoryEvent;

    [SerializeField] Player player;

    [SerializeField] private float typingSpeed = 0;

    private Story currentStory;

    private static InteractionManager instance;

    private Coroutine displayLineCoroutine;


    private void Update()
    {

        playerMoneyText.SetText("Player: " + '\n' + playerMoney.money.ToString() + "$");
        SaleUI.updateUI();


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
                case "StartPorter":
                    SetNames("Perry");
                    break;
                default:
                    SetNames("");
                    break;
            }
        }

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
    }

    public void EnablePorterButton()
    {
        PorterButton.SetActive(true);
    }

    public void DisablePorterButton()
    {
        PorterButton.SetActive(false);
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

    private void EnterDialogueMode(TextAsset inkJSON)
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
            EndOfStoryEvent?.Invoke(this, EventArgs.Empty);
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
        travelMenu.SetActive(false);
        worldNames.SetActive(false);
        dialogueBox.SetActive(false);
        tradeMenu.SetActive(false);
        optionsMenu.SetActive(true);
    }

    public void DisableInteractionView()
    {
        travelMenu.SetActive(false);
        dialogueBox.SetActive(false);
        optionsMenu.SetActive(false);
        worldNames.SetActive(true);
    }

    public void EnterTradeMenu()
    {
        tradeMenu.SetActive(true);
        travelMenu.SetActive(false);
        optionsMenu.SetActive(false);
        worldNames.SetActive(false);
        dialogueBox.SetActive(false);
    }

    public void EnterTravelMenu()
    {
        travelMenu.SetActive(true);
        tradeMenu.SetActive(false);
        optionsMenu.SetActive(false);
        worldNames.SetActive(false);
        dialogueBox.SetActive(false);
    }

    // For NPCs who will not trade
    public void DisableTradeButton()
    {
        TradeButton.SetActive(false);
    }

    // For NPCs who will trade
    public void EnableTradeButton()
    {
        TradeButton.SetActive(true);
    }

    public void BeginDialogue()
    {
        worldNames.SetActive(false);
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
        TradeButton.SetActive(true);
        player.NPCTarget = null;
        worldNames.SetActive(true);
        continueDialogueButton.SetActive(false);
        optionsMenu.SetActive(false);
        dialogueBox.SetActive(false);
        tradeMenu.SetActive(false);
        travelMenu.SetActive(false);
        shopUI.resetShop();
    }

    public void EnterRuinsOfYeager()
    {
        LoadingStateController.Instance.LoadScene("Dungeon1");
    }

}

