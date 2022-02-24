using UnityEngine;

[System.Serializable]
public abstract class QuestGoal
{
    [SerializeField] private int requiredAmount;
    [SerializeField] private int currentAmount;
    //using string to compare is more efficient, very important that it matches the prefab name
    [SerializeField] private int type;

    public int RequiredAmount
    {
        get
        {
            return requiredAmount;
        }
        set
        {
            requiredAmount = value;
        }
    }
    public int CurrentAmount
    {
        get
        {
            return currentAmount;
        }
        set
        {
            currentAmount = value;
        }
    }
    public int Type
    {
        get
        {
            return type;
        }
        set
        {
            type = value;
        }
    }
    public bool IsComplete
    {
        get
        {
            return currentAmount >= requiredAmount;
        }
    }
}

[System.Serializable]
public class KillingGoal : QuestGoal
{
    //will be called when event happens
    public void UpdateKillCount(int enemyType)//pass in enemy?
    {
        //TODO    
        //if Type = enemy type,  currentAmount ++, CheckCompleted, UpdateSelected,
        if(Type == enemyType)
        {
            CurrentAmount++;
            QuestLog.QuestLogInstance.CheckIfComplete();
            QuestLog.QuestLogInstance.UpdateSelectedQuest();

        }
        
    }

}


//public GoalType goalType;
//public enum GoalType
//{
//    Kill,
//    Gathering
//}
