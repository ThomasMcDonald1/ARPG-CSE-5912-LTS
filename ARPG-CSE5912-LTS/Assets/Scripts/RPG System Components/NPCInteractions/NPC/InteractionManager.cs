using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Ink.Runtime;
using UnityEngine.UI;
using System;
using ARPG.Movement;
using UnityEngine.AI;

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

    [SerializeField] private GameObject TombOfMortemierButton;
    [SerializeField] private GameObject ForsakenCathedralButton;


    [SerializeField] private GameObject tradeMenu;
    [SerializeField] private GameObject TradeButton;
    [SerializeField] private GameObject PorterButton;

    [SerializeField] private GameObject waypointMenu;

    [SerializeField] public UI_shop shopUI;
    [SerializeField] public TextMeshProUGUI playerMoneyText;

    [SerializeField] Porter porter;

    // We still need lorekeeper to be active in the scene to update quests... But we need to disable collider, otherwise player may accidentally click, which would be weird
    [SerializeField] Lorekeeper lorekeeper;
    // We also don't want the lorekeeper's model to be active when switching to dungeon scenes...
    [SerializeField] GameObject lorekeeperModel;
    // Turn his quest icon back on when re-entering town
    [SerializeField] GameObject lorekeeperQuestIcon;

    [SerializeField] GeneralStore generalStore;
    [SerializeField] Blacksmith blacksmith;


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


        if (player.NPCTarget == null) return;
        else
        {
           //SaleUI.updateUI();

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

    public void EnableTombOfMortemier()
    {
        TombOfMortemierButton.SetActive(true);
    }

    public void EnableForsakenCathedralButton()
    {
        ForsakenCathedralButton.SetActive(true);
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
        UI_Sale.instance.updateUI();
        waypointMenu.SetActive(false);
        travelMenu.SetActive(false);
        worldNames.SetActive(false);
        dialogueBox.SetActive(false);
        tradeMenu.SetActive(false);
        optionsMenu.SetActive(true);
    }

    public void DisableInteractionView()
    {
        waypointMenu.SetActive(false);
        travelMenu.SetActive(false);
        dialogueBox.SetActive(false);
        optionsMenu.SetActive(false);
        worldNames.SetActive(true);
    }

    public void EnterTradeMenu()
    {
        waypointMenu.SetActive(false);
        travelMenu.SetActive(false);
        optionsMenu.SetActive(false);
        worldNames.SetActive(false);
        dialogueBox.SetActive(false);
        tradeMenu.SetActive(true);
    }

    public void EnterTravelMenu()
    {
        waypointMenu.SetActive(false);
        tradeMenu.SetActive(false);
        optionsMenu.SetActive(false);
        worldNames.SetActive(false);
        dialogueBox.SetActive(false);
        travelMenu.SetActive(true);
    }

    public void EnterWaypointMenu()
    {
        waypointMenu.SetActive(true);
        travelMenu.SetActive(false);
        tradeMenu.SetActive(false);
        optionsMenu.SetActive(false);
        worldNames.SetActive(false);
        dialogueBox.SetActive(false);
    }

    public void EnterTown()
    {
        player.GetComponent<PlayerController>().DungeonNum = 0;
        //optionsMenu.SetActive(true);

        LoadingStateController.Instance.LoadScene("NoControllerDuplicate");

        generalStore.gameObject.SetActive(true);
        blacksmith.gameObject.SetActive(true);
        porter.gameObject.SetActive(true);

        //lorekeeper.GetComponent<NavMeshAgent>().enabled = false;
        lorekeeper.GetComponent<Collider>().enabled = true;
        lorekeeperModel.SetActive(true);
        lorekeeperQuestIcon.SetActive(true);
        //lorekeeper.GetComponent<NavMeshAgent>().enabled = true;


        player.agent.enabled = false;
        player.transform.position = GameObject.Find("TownSpawnLocation").transform.position;
        player.agent.enabled = true;

        StopInteraction();
    }

    public void ReactivateNPCS()
    {
        if (player.GetComponent<PlayerController>().DungeonNum == 0)
        {

            player.GetComponent<PlayerController>().DungeonNum = 0;
            generalStore.gameObject.SetActive(true);
            blacksmith.gameObject.SetActive(true);
            porter.gameObject.SetActive(true);

            //lorekeeper.GetComponent<NavMeshAgent>().enabled = false;
            lorekeeper.GetComponent<Collider>().enabled = true;
            lorekeeperModel.SetActive(true);
            lorekeeperQuestIcon.SetActive(true);
            //lorekeeper.GetComponent<NavMeshAgent>().enabled = true;

            StopInteraction();
        }
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
        player.NPCTarget = null;
        TradeButton.SetActive(true);
        if (player.GetComponent<PlayerController>().DungeonNum == 0) 
        { worldNames.SetActive(true); } 
        else { worldNames.SetActive(false); }
        continueDialogueButton.SetActive(false);
        optionsMenu.SetActive(false);
        dialogueBox.SetActive(false);
        tradeMenu.SetActive(false);
        travelMenu.SetActive(false);
        waypointMenu.SetActive(false);
        shopUI.resetShop();
    }
}

