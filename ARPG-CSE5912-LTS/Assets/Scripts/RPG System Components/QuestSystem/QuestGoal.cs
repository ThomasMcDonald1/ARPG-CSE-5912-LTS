using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QuestGoal 
{
    public GoalType goalType;
    public int currentAmount;
    public int goalAmount;

    public bool IsSatisfied()
    {
        return currentAmount >= goalAmount;
    }

    public void IncreaseKillCount()
    {
        if(goalType == GoalType.Killing)
        {
            currentAmount++;
        }
    }
}

public enum GoalType
{
    Killing
}
