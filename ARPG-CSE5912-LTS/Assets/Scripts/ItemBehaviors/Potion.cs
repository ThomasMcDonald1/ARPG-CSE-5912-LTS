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
        base.Use();
        switch (typeOfPotion)
        {
            case potionType.health:
                UseHealingPotion();
                break;
            case potionType.mana:
                UseEnergyPotion();
                break;
            default:
                Debug.Log("Don't know what this potion does");
                break;
        }
    }

    public void UseHealingPotion()
    {
        //Debug.Log("ADD HEALTH!!");

        playerStat[StatTypes.HP] += 1000;
        playerStat[StatTypes.HP] = Mathf.Clamp(playerStat[StatTypes.HP], 0, playerStat[StatTypes.MaxHP]);
    }
    public void UseEnergyPotion()
    {
        //Debug.Log("ADD HEALTH!!");

        playerStat[StatTypes.Mana] += 100;
        playerStat[StatTypes.Mana] = Mathf.Clamp(playerStat[StatTypes.Mana], 0, playerStat[StatTypes.MaxMana]);
    }
}
