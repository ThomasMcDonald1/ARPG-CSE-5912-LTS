using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestGiver : MonoBehaviour
{
    public Quest quest;
    public Player player;//maybe needs to change?

    public GameObject questWindow;
    public TextMesh titleText;
    public TextMesh descpritionText;
    public TextMesh expRewardText;
    public TextMesh goldRewardText;


    public void OpenQuestWindow()
    {
        questWindow.SetActive(true);
        titleText.text = this.quest.title;
        descpritionText.text = this.quest.description;
        expRewardText.text = quest.expReward.ToString(); ;
        goldRewardText.text = this.quest.goldReward.ToString();
    }
    public void AcceptQuest()
    {
        questWindow.SetActive(false);
        quest.isActive = true;
        //give to player, player needs to have list of quests
    }



}
