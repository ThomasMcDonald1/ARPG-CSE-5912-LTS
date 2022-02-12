using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class QuestGoal
{
    public GoalType goalType;
    public int requestAmt;
    public int currAmt;

    public bool CheckComplete()
    {
        return currAmt >= requestAmt;
    }
    
}
public enum GoalType
{
    Kill,
    Gathering

}

