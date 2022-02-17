using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestLog : MonoBehaviour
{
    public GameObject npcUI;//used to disable npc ui when quest log window is open
    public GameObject questWindow;
    [SerializeField]private GameObject questPrefab;
    [SerializeField] private Transform questArea;//game object in hierarchy that has children that are quests
    private Quest selectedQuest;
    [SerializeField] private TextMeshProUGUI questDescription;

    private static QuestLog questLogInstance;//singleton pattern

    public static QuestLog QuestLogInstance
    {
        get
        {
            if(questLogInstance == null){
                questLogInstance = GameObject.FindObjectOfType<QuestLog>();//Only have one questlog, should be fine to do this
            }
            return questLogInstance;
        }
    }

    public void OpenQuestWindow()
    {
        questWindow.SetActive(true);
        npcUI.SetActive(false);
    }
    public void CloseQuestWindow()
    {
        questWindow.SetActive(false);
        npcUI.SetActive(true);
        /*To Do: return to the game play state*/


    }
    public void AddQuest(Quest quest)
    {
        GameObject questGameObject = Instantiate(questPrefab, questArea);// Instantiating quest in the game world
        QuestScript questScript = questGameObject.GetComponent<QuestScript>();
        questScript.QuestReference = quest;//quest script now has reference to original quest
        quest.QuestScriptReference = questScript;//quest has reference to quest script
        questGameObject.GetComponent<TextMeshProUGUI>().text = quest.Title;
    }

    public void ShowQuestDescription(Quest quest)
    {
        if(selectedQuest != null)
        {
            selectedQuest.QuestScriptReference.UnselectQuest();//when quest description is shown, no quest will be selected
        }
        selectedQuest = quest;
        
        //For showing current progress
        string progress = string.Empty;
        //Quest can have multiple killing goals
        foreach(QuestGoal questGoal in quest.KillingGoals)
        {
            progress += questGoal.Type + ":" + questGoal.CurrentAmount + "/" + questGoal.RequiredAmount + "\n"; 
        }
        //string.Format for title, description, and progress
        questDescription.text = string.Format("<b>{0}</b>\n<size=15>{1}</size>\n\nProgress:\n<size=15>{2}</size>", quest.Title, quest.Description, progress);


    }
}
//public Quest quest;
//public TextMeshProUGUI titleText;