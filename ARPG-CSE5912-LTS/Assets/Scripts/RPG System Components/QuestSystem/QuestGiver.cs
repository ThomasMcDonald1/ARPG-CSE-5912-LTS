using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestGiver : MonoBehaviour
{


    [SerializeField] private Quest[] quests;
    [SerializeField] private QuestLog testLog;//for testing only
    public Quest[] Quests
    {
        get
        {
            return quests;
        }
    }
    private void Awake()
    {
        //testing
        testLog.AddQuest(quests[0]);
    }
    public void TestAddQuest()
    {
        //testing
        Quest testQuest = new Quest();
        testQuest.Title = "Test Quest";
        testLog.AddQuest(testQuest);
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
