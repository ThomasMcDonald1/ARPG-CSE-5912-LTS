using UnityEngine;

[System.Serializable]
public abstract class QuestGoal
{
    [SerializeField] private int requiredAmount;
    [SerializeField] private int currentAmount;
    //using string to compare is more efficient, very important that it matches the prefab name
    [SerializeField] private string className;

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
    public string ClassName
    {
        get
        {
            return className;
        }
        set
        {
            className = value;
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
    public void UpdateKillCount(string killedClassName)//pass in enemy?
    {
        if(ClassName.Equals(killedClassName))
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
