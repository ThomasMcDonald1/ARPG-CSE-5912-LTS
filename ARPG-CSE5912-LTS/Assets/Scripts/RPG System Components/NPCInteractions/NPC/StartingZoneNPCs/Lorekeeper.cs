using System;
using Ink.Runtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lorekeeper : NPC
{
    [Header("Ink JSON")]
    [SerializeField] public Shop shop;
    [SerializeField] public UI_shop shopUI;
    [SerializeField] public UI_Sale saleUI;
    [SerializeField] Porter porter;


    public List<TextAsset> DialogueJSON;

    private int currentStory;
    public int CurrentStory { get { return currentStory; } }

    GameObject child;

    private void OnEnable()
    {       
        QuestGiver.QuestCompleteEvent += OnQuestComplete;
        InteractionManager.EndOfStoryEvent += NextStory;
    }
    private void OnDisable()
    {
        QuestGiver.QuestCompleteEvent -= OnQuestComplete;
        InteractionManager.EndOfStoryEvent -= NextStory;
    }
    private void OnQuestComplete(object sender, EventArgs e)
    {
        Debug.Log("Quest completed! From Lorekeeper");
        GetComponent<QuestGiver>().UpdateQuestIcon();
        currentStory++;
    }

    private void Start()
    {
        Player.InteractNPC += Interact;
        child = transform.GetChild(0).gameObject;
        currentStory = 0;

        //questGiver = this.GetComponent<QuestGiver>(); 
    }

    public override TextAsset GetCurrentDialogue()
    {
        return DialogueJSON[currentStory];
    }

    public override IEnumerator LookAtPlayer()
    {

        float time = 0.0f;
        float speed = 1.0f;

        Quaternion rotate = Quaternion.LookRotation(player.transform.position - child.transform.position);

        while (time < 1.0f)
        {
            child.transform.rotation = Quaternion.RotateTowards(child.transform.rotation, rotate, 50f * Time.deltaTime);
            child.transform.eulerAngles = new Vector3(0, child.transform.eulerAngles.y, 0);

            time += Time.deltaTime * speed;

            yield return null;
        }
    }

    private void NextStory(object sender, EventArgs e)
    {
        if (player.NPCTarget != this) return;
        switch (currentStory)
        {
            case 0:
                // Quest to kill regualr knight
                GetComponent<QuestGiver>().AddQuestToLogIfNew();
                if (porter.currentStory == 1) { porter.currentStory = 2; }
                currentStory++;
                GetComponent<QuestGiver>().UpdateQuestIcon();
                break;
            case 1:
                // Dialogue that says you can use portal now with Perry
                break;
            case 2:
                // Kill Stout quest
                GetComponent<QuestGiver>().AddQuestToLogIfNew();
                currentStory++;
                GetComponent<QuestGiver>().UpdateQuestIcon();
                break;
            case 3:
                break;
            case 4:
                currentStory++;
                GetComponent<QuestGiver>().UpdateQuestIcon();
                break;
            case 5:
                InteractionManager.GetInstance().EnableTombOfMortemier();
                GetComponent<QuestGiver>().AddQuestToLogIfNew();
                currentStory++;
                GetComponent<QuestGiver>().UpdateQuestIcon();
                break;
            case 6:
                break;
            case 7:
                GetComponent<QuestGiver>().AddQuestToLogIfNew();
                currentStory++;
                GetComponent<QuestGiver>().UpdateQuestIcon();
                break;
            case 8:
                break;
            case 9:
                InteractionManager.GetInstance().EnableForsakenCathedralButton();
                currentStory++;
                GetComponent<QuestGiver>().UpdateQuestIcon();
                break;
            case 10:
                break;
            default:
                break;
        }
    }

    protected override void Interact(object sender, EventArgs e)
    {
        if (Interactable())
        {
            InteractionManager.GetInstance().DisableTradeButton();
            InteractionManager.GetInstance().DisablePorterButton();
            if (currentStory == 0)
            {
                InteractionManager.GetInstance().BeginDialogue();
            }
            else
            {
                InteractionManager.GetInstance().EnterOptionsMenu();
            }

            shopUI.initializeShop(shop);
            StartCoroutine(LookAtPlayer());
            saleUI.shop = shop;
        }
    }

    public void Update()
    {
        //Debug.Log(currentStory);
    }
}
