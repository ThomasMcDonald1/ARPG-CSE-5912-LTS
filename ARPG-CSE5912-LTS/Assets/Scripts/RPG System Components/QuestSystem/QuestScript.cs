using UnityEngine;
using TMPro;

//Quest attached to game object in game world
public class QuestScript : MonoBehaviour
{
    //Quest that it is controlling
    public Quest QuestReference { get; set; }//quest that it is controlling

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
}
