using UnityEngine;
using TMPro;

//Quest attached to game object in game world
public class QuestScript : MonoBehaviour
{
    //Quest that it is controlling
    public Quest QuestReference { get; set; }//quest that it is controlling
    private bool isMarkedComplete = false;

    //when mouse clicks on quest
    public void SelectQuest()
    {
        Debug.Log("Quest clicked on!");
        GetComponent<TextMeshProUGUI>().color = Color.yellow;
        QuestLog.QuestLogInstance.ShowQuestDescription(QuestReference);
    }

    public void UnselectQuest()
    {
        GetComponent<TextMeshProUGUI>().color = Color.white;

    }

    //mark quest as complete if complete, called when quest log checks quest completeness
    public void IsComplete()
    {
        if (QuestReference.IsComplete && !isMarkedComplete)
        {
            isMarkedComplete = true;
            GetComponent<TextMeshProUGUI>().text += " COMPLETE";
        }
        else if (!QuestReference.IsComplete)
        {
            isMarkedComplete = false;
            GetComponent<TextMeshProUGUI>().text = QuestReference.Title;
        }
    }
}
