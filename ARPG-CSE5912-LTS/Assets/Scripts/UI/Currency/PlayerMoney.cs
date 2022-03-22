using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Currency")]

public class PlayerMoney : ScriptableObject
{
    public int money;

    public void addMoney(int income)
    {
        money += income;
    }
    public void spendMoney(int spend)
    {
        
            money -= spend;
        
       
    }
}
