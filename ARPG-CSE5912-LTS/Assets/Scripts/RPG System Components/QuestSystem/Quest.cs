using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Quest 
{
    public bool isActive;
    // Start is called before the first frame update
    public string title;
    public string description;
    public int expReward;
    public int goldReward;

    public QuestGoal goal;

    /*
     * TODO: we will imply the complete in other places 
     * 
     */
    public void Complete()
    {
        isActive = false;
        Debug.Log(title + " is complete!");
    }
}
