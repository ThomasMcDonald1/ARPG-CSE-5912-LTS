using UnityEngine;
using System;
using System.Collections;

public class QuestGiver : MonoBehaviour
{

    [SerializeField] private int questIndex;// used to know what quest to give
    [SerializeField] private Quest[] quests;
    [SerializeField] private Texture2D questionIcon, questionIconGray, exclamationIcon;
    [SerializeField] private Sprite questionIconSprite, questionIconGraySprite, exclamationIconSprite;
    [SerializeField] private SpriteRenderer iconRenderer;
    [SerializeField] private QuestLog testLog;//for testing only
    public static event EventHandler QuestCompleteEvent;
    private void OnEnable()
    {
        InteractionManager.EndOfStoryEvent += OnEndOfStory;


    }
    private void OnDisable()
    {
        InteractionManager.EndOfStoryEvent -= OnEndOfStory;
    }
    private void OnEndOfStory(object sender, EventArgs e)
    {
        AddQuestToLogIfNew();
    }
    public int QuestIndex
    {
        get
        {
            return questIndex;
        }
    }
    public Quest[] Quests
    {
        get
        {
            return quests;
        }
    }
    void Awake()
    {
        questIndex = 0;

        //create sprites from textures
        questionIconSprite = Sprite.Create(questionIcon, new Rect(0.0f, 0.0f, questionIcon.width, questionIcon.height), new Vector2(0.5f, 0.5f), 100.0f);
        questionIconGraySprite = Sprite.Create(questionIconGray, new Rect(0.0f, 0.0f, questionIconGray.width, questionIconGray.height), new Vector2(0.5f, 0.5f), 100.0f);
        exclamationIconSprite = Sprite.Create(exclamationIcon, new Rect(0.0f, 0.0f, exclamationIcon.width, exclamationIcon.height), new Vector2(0.5f, 0.5f), 100.0f);

        iconRenderer.sprite = exclamationIconSprite;//default icon is exclamation
        foreach (Quest quest in quests)
        {
            quest.QuestGiverReference = this;
        }
    }
    //test
    private void Update()
    {

        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    if (!QuestLog.QuestLogInstance.HasQuest(quests[questIndex]))
        //    {
        //        QuestLog.QuestLogInstance.AddQuest(quests[questIndex]);
        //    }
        //}
        UpdateQuestIcon();

    }
    public void AddQuestToLogIfNew()
    {
        if (!QuestLog.QuestLogInstance.HasQuest(quests[questIndex]))
        {
            QuestLog.QuestLogInstance.AddQuest(quests[questIndex]);
        }
    }
    public void UpdateQuestIcon()
    {
        if (quests[questIndex].IsComplete && QuestLog.QuestLogInstance.HasQuest(quests[questIndex]))
        {
            QuestCompleteEvent?.Invoke(this, EventArgs.Empty);
            iconRenderer.sprite = questionIconSprite;
            if (questIndex < quests.Length-1)
            {
                questIndex++;
            }
            else//if have no more quests
            {
                iconRenderer.sprite = null;
            }
        }
        else if (!QuestLog.QuestLogInstance.HasQuest(quests[questIndex]))
        {
            iconRenderer.sprite = exclamationIconSprite;

        }
        else if (!quests[questIndex].IsComplete && QuestLog.QuestLogInstance.HasQuest(quests[questIndex]))
        {
            iconRenderer.sprite = questionIconGraySprite;
        }
        //foreach(Quest quest in quests)
        //{
        //    if(quest != null)
        //    {
        //        if(quest.IsComplete && QuestLog.QuestLogInstance.HasQuest(quest))
        //        {
        //            iconRenderer.sprite = questionIconSprite;
        //            break;//turn into question when one quest is complete, might need to change later since we might have just one quest active at a time
        //        }
        //        else if(!QuestLog.QuestLogInstance.HasQuest(quest)){
        //            iconRenderer.sprite = exclamationIconSprite;
        //        }
        //        else if(!quest.IsComplete && QuestLog.QuestLogInstance.HasQuest(quest))
        //        {
        //            iconRenderer.sprite = questionIconGraySprite;
        //        }
        //    }
        //}
    }

    public void TestAddQuest()
    {
        //testing
        Quest testQuest = new Quest();
        testQuest.Title = "Test Quest";
        testLog.AddQuest(testQuest);
        Debug.Log("quest added!");
    }
    public void TestKill()
    {
        //testing
        // testLog.selectedQuest.KillingGoals[0].CurrentAmount++;
    }


}
















//public Quest quest;
//public Player player; //disable for test

//public GameObject questWindow;
//public TextMeshProUGUI titleText;
//public TextMeshProUGUI descpritionText;
//public TextMeshProUGUI expRewardText;
//public TextMeshProUGUI goldRewardText;
//private void Start()
//{
//    quest.title = "spider mission";
//    quest.description = "kill five spiders";
//    quest.expReward = 90;
//    quest.goldReward = 50;
//}

//public void OpenQuestWindow()
//{
//    questWindow.SetActive(true);
//    titleText.SetText(quest.title);
//    descpritionText.SetText(quest.description);
//    expRewardText.SetText(quest.expReward.ToString());
//    goldRewardText.SetText(quest.goldReward.ToString());
//}
//public void AcceptQuest()
//{
//    questWindow.SetActive(false);
//    /*To Do: return to the game play state*/
//    quest.isActive = true;
//    player.questList.Add(quest); // add a new quest to the quest list

//}
//public void DeclineQuest()
//{
//    questWindow.SetActive(false);
//    /*To Do: return to the game play state*/
//    quest.isActive = false;


//}
