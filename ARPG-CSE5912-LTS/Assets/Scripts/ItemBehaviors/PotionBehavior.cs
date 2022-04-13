using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionBehavior : MonoBehaviour
{
    public static PotionBehavior instance;
    public bool isDefenseActive;
    public float defenseDuration;
    public int defense;
    void Start()
    {
        PotionBehavior.instance = this;
    }
    public void Update()
    {
        if (Time.time > defenseDuration)
        {
            if(isDefenseActive)
            {
                int playerStat = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Stats>()[StatTypes.Armor];
                GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Stats>()[StatTypes.Armor] = playerStat - defense;
                Debug.Log("After the potion wore off defense is now " + GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Stats>()[StatTypes.Armor]);
                isDefenseActive = false;
                defense = 0;
            }
        }
    }
}
