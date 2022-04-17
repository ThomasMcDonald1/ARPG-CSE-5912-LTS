using System.Collections.Generic;
using UnityEngine;
using TMPro;
using ARPG.Combat;

public class QuestLog : MonoBehaviour
{
    [SerializeField] private GameObject npcUI;//to disable npc ui
    [SerializeField]private GameObject questPrefab;
    [SerializeField] private Transform questArea;//game object in hierarchy that has children that are quests
    private Quest selectedQuest;//quest player had clicked on
    [SerializeField] private TextMeshProUGUI questDescription;
    [SerializeField] private CanvasGroup canvasGroup;//for making window invisible
    [SerializeField] private GameplayStateController gameplayStateController;//entering exiting states? temp solution?

    [SerializeField] private Lorekeeper lorekeepr;

    


    private void OnEnable()
    {
        Enemy.EnemyKillExpEvent += OnEnemyKilled;
    }
    private void OnDisable()
    {
        Enemy.EnemyKillExpEvent -= OnEnemyKilled;
    }


    //List of Quests & QuestScripts, questscripts have references of their quest, vise versa
    private List<QuestScript> questScripts = new List<QuestScript>();
    private List<Quest> quests = new List<Quest>();

    private void OnEnemyKilled(object sender, InfoEventArgs<(int, int)> e)
    {
        //Loops through everykilling goal of every quest in the quest log, and updates info such as kill count
        foreach (Quest quest in quests)
        {

            foreach (KillingGoal killingGoal in quest.KillingGoals)
            {

                killingGoal.UpdateKillCount(e.info.Item2);
            }
            if (quest.IsComplete && !quest.HasBeenCompleted())
            {
                QuestGiver.RaiseQuestCompleteEvent();
                quest.SetCompletedQuest();
            }
        }
    }

    private static QuestLog questLogInstance;//singleton pattern

    public static QuestLog QuestLogInstance
    {
        get
        {
            return questLogInstance;//wont be null because it will be assigned during awake
        }
    }
    private void Awake()
    {
        //get QuestLog object and then close window
        questLogInstance = GameObject.FindObjectOfType<QuestLog>();//Only have one questlog, should be fine to do this
        CloseQuestWindow();
    }
    public void OpenQuestWindow()
    {
        Time.timeScale = 0;
        npcUI.SetActive(false);
        canvasGroup.alpha = 1f; 
        canvasGroup.blocksRaycasts = true; 
        ////change to questlog state or character panel state?
    }
    public void CloseQuestWindow()
    {
        Time.timeScale = 1;
        npcUI.SetActive(true);
        canvasGroup.alpha = 0f; //this makes everything transparent
        canvasGroup.blocksRaycasts = false; //this prevents the UI element to receive input events
        ////change to gameplay state?
        ///*To Do: return to the game play state*/


    }
    public void AddQuest(Quest quest)
    {
        GameObject questGameObject = Instantiate(questPrefab, questArea);// Instantiating quest in the game world
        QuestScript questScript = questGameObject.GetComponent<QuestScript>();//quest prefab will originally have QuestScript attached
        questScript.QuestReference = quest;//quest script now has reference to original quest
        quest.QuestScriptReference = questScript;//quest has reference to quest script
        questGameObject.GetComponent<TextMeshProUGUI>().text = quest.Title;//assigned title to quest object
        
        //add quest and questscript to lists
        questScripts.Add(questScript);
        quests.Add(quest);
    }

    //will be called when kill event happens
    public void UpdateSelectedQuest()
    {
        ShowQuestDescription(selectedQuest);
    }
    public void ShowQuestDescription(Quest quest)
    {
        if(quest!= null)
        {
            if (selectedQuest != null && selectedQuest != quest)//make sure quest currently selected is diffrent from new one
            {
                selectedQuest.QuestScriptReference.UnselectQuest();//when quest description is shown, no quest will be selected
            }
            selectedQuest = quest;

            //For showing current progress
            string progress = string.Empty;
            //Quest can have multiple killing goals
            foreach (QuestGoal questGoal in quest.KillingGoals)
            {
                progress += questGoal.Type + ": " + questGoal.CurrentAmount + "/" + questGoal.RequiredAmount + "\n";
            }
            //string.Format for title, description, and progress
            questDescription.text = string.Format("<b>{0}</b>\n<size=15>{1}</size>\n\nProgress:\n<size=15>{2}</size>", quest.Title, quest.Description, progress);
        }
       
    }
    //will execute when killing something or picking something up, needs to check entire list of quest
    public void CheckIfComplete()
    {
        foreach(QuestScript questScript in questScripts)
        {
            //questScript.QuestReference.QuestGiverReference.UpdateQuestIcon();
            questScript.IsComplete();
        }
    }
    public bool HasQuest(Quest quest) 
    {
        //maybe need to edit later? since when quest is complete, COMPLETED will be added to the end
        return quests.Exists(x=>x.Title == quest.Title);//if quest exists in quest log
    }
    public void RemoveQuest(QuestScript questScript)
    {
        questScripts.Remove(questScript);
        Destroy(questScript.gameObject);
        quests.Remove(questScript.QuestReference);
        questDescription.text = string.Empty;
        selectedQuest = null;//unselect quest
        //questScript.QuestReference.QuestGiverReference.UpdateQuestIcon();
        //questScript = null;
    }

}


//public void AddQuest(Quest quest)
//{
//    //implement later, basically subscrube to kill event, and when event occurs, update kill count
//    //foreach(KillingGoal killingGoal in quest.KillingGoals)
//    //{
//    //kill confirmed event?

//    //  killingGoal.UpdateKillCount();

//    //}

//    //quests.Add(quest); orignal placement of code, maybe delete later

//    GameObject questGameObject = Instantiate(questPrefab, questArea);// Instantiating quest in the game world
//    QuestScript questScript = questGameObject.GetComponent<QuestScript>();//quest prefab will originally have QuestScript attached
//    questScript.QuestReference = quest;//quest script now has reference to original quest
//    quest.QuestScriptReference = questScript;//quest has reference to quest script
//    questGameObject.GetComponent<TextMeshProUGUI>().text = quest.Title;//assigned title to quest object

//    //add quest and questscript to lists
//    questScripts.Add(questScript);
//    quests.Add(quest);


//}

//public Quest quest;
//public TextMeshProUGUI titleText;