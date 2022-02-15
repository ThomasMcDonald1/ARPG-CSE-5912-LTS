using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestLog : MonoBehaviour
{
    public Quest quest;

    public GameObject questWindow;
    public GameObject npcUI;
    public TextMeshProUGUI titleText;
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
}