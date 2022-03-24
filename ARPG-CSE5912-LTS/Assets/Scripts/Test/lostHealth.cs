using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lostHealth : MonoBehaviour
{
    [SerializeField] public Character player;

 public void loseHealth()
    {
        Debug.Log("Previous health: "+ player.stats[StatTypes.HP]);
        player.stats[StatTypes.HP] -= 2000;
        Debug.Log("Later health: " + player.stats[StatTypes.HP]);

    }
    public void loseEnergy()
    {
        Debug.Log("Previous health: " + player.stats[StatTypes.HP]);
        player.stats[StatTypes.HP] -= 2000;
        Debug.Log("Later health: " + player.stats[StatTypes.HP]);
    }
}
