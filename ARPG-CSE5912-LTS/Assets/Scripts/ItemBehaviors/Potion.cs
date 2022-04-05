using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Potion", menuName = "Inventory/Potion")]
public class Potion : Ite
{
    public enum potionType
    {
        health,
        mana,
        teleport,
    }
    ;

    public int mana;
    public int health;
    public int defense;
    public int speed;

    public potionType typeOfPotion;
   public void Update()
    {
        Debug.Log("I'm still here");
    }
    public override void Use()
    {
        //Debug.Log("Use potion!");
        base.Use();
        switch ((int)typeOfPotion)
        {
            case (int)potionType.health:
                UseHealingPotion();
                break;
            case (int)potionType.mana:
                UseEnergyPotion();
                break;
            default:
                Debug.Log("Don't know what this potion does");
                break;
        }
    }

    public void UseHealingPotion()
    {
       // Debug.Log("ADD HEALTH!!");
        int playerStat = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Stats>()[StatTypes.HP];
        playerStat += 1000;
        playerStat = Mathf.Clamp(playerStat, 0, GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Stats>()[StatTypes.MaxHP]);
        GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Stats>()[StatTypes.HP] = playerStat;
    }
    public void UseEnergyPotion()
    {
       // Debug.Log("ADD Energy!!");

        int playerStat = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Stats>()[StatTypes.Mana];

        playerStat += 100;
        playerStat = Mathf.Clamp(playerStat, 0, GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Stats>()[StatTypes.MaxMana]);
        GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Stats>()[StatTypes.Mana] = playerStat;
    }
}
