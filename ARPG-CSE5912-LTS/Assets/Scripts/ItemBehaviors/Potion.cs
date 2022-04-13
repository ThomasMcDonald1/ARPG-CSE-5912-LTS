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
        defense,
    }
    ;

    public int mana;
    public int health;
    public int defense;
    public int speed;
    float duration;
    bool finished = true;

    public potionType typeOfPotion;
   public void Update()
    {
        if (Time.time > duration && finished)
        {
            switch ((int)typeOfPotion)
            {
                case (int)potionType.defense:
                    int playerStat = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Stats>()[StatTypes.Armor];
                    GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Stats>()[StatTypes.Armor] = playerStat - defense;
                    break;
                default:
                    Debug.Log("dont know how long this potion lasts");
                    break;
            }
            finished = false;
        }
        else
        {
            Debug.Log("Potion is still in effect");
        }
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
            case (int)potionType.defense:
                duration = Time.time + 120;
                UseDefensePotion();
               // IEnumerator defenseFunc = ApplyDefense(120);
               //PotionBehavior.instance.isDefenseActive = true;
                break;
            default:
                Debug.Log("Don't know what this potion does");
                break;
        }
    }

    //IEnumerator ApplyDefense( float seconds)
    //{
    //    Debug.Log("Say something.");
    //    int playerStat = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Stats>()[StatTypes.Armor];
    //    GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Stats>()[StatTypes.Armor] = playerStat + defense;
    //    yield return new WaitForSeconds(seconds);
    //    GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Stats>()[StatTypes.Armor] = playerStat - defense;
    //    PotionBehavior.instance.isDefenseActive = false;


    //    Debug.Log("Say something again 2.5 seconds later.");
    //}
    public void UseDefensePotion()
    {
        int playerStat = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Stats>()[StatTypes.Armor];
        GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Stats>()[StatTypes.Armor] = playerStat + defense;
    }
    public void UseHealingPotion()
    {
       // Debug.Log("ADD HEALTH!!");
        int playerStat = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Stats>()[StatTypes.HP];
        playerStat += health;
        playerStat = Mathf.Clamp(playerStat, 0, GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Stats>()[StatTypes.MaxHP]);
        GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Stats>()[StatTypes.HP] = playerStat;
    }
    public void UseEnergyPotion()
    {
       // Debug.Log("ADD Energy!!");

        int playerStat = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Stats>()[StatTypes.Mana];

        playerStat += mana;
        playerStat = Mathf.Clamp(playerStat, 0, GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Stats>()[StatTypes.MaxMana]);
        GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Stats>()[StatTypes.Mana] = playerStat;
    }
}
