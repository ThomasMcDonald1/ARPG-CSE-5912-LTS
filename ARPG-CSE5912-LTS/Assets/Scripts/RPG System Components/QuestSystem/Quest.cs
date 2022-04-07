using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Quest 
{
    [SerializeField] private string title;
    [SerializeField] private string description;
    [SerializeField] private int questExp;

    //Quest can have many type of goals, there can be mulltiple goals of the same type
    [SerializeField] private KillingGoal[] killingGoals;
    //Add gathering goals array?


    public QuestScript QuestScriptReference { get; set; }//scipt that controls this quest
    public QuestGiver QuestGiverReference { get; set; }//needs to know what questgiver it came from
   
    public string Title
    {
        get
        {
            return title;
        }
        set
        {
            title = value;
        }
    }
    public string Description
    {
        get
        {
            return description;
        }
        set
        {
            description = value;
        }
    }
    public int QuestExp
    {
        get
        {
            return questExp;
        }
        set
        {
            questExp = value;
        }
    }
    public KillingGoal[] KillingGoals
    {
        get
        {
            return killingGoals;
        }
    }
    //Add GatheringGoals?

    //Property that tells if quest is complete
    public bool IsComplete
    {
        get
        {
            foreach(QuestGoal questGoal in killingGoals)
            {
                if (!questGoal.IsComplete)
                {
                    return false;
                }
            }
            //Check if all goals of GatheringGoal are complete?

            return true;
        }
    }




    //public bool isActive;

    ///*
    // * TODO: we will imply the complete in other places 
    // * 
    // */
    //public void Complete()
    //{
    //    isActive = false;
    //    Debug.Log(title + " is complete!");
    //}

    //public string description;
    //public int expReward;
    //public int goldReward;

    //public QuestGoal goal;
}
