using UnityEngine;

[System.Serializable]
public abstract class QuestGoal
{
    [SerializeField] private int requiredAmount;
    [SerializeField] private int currentAmount;
    //using string to compare is more efficient, very important that it matches the prefab name
    [SerializeField] private string type;
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
    public string Type
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
    public bool CheckComplete()
    {
        return currentAmount >= requiredAmount;
    }
}

[System.Serializable]
public class KillingGoal : QuestGoal
{

}
//public GoalType goalType;
//public enum GoalType
//{
//    Kill,
//    Gathering
//}
