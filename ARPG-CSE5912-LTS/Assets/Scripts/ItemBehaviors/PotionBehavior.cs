using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionBehavior : MonoBehaviour
{
    public static PotionBehavior instance;
    public bool isDefenseActive;
    public bool isSpeedActive;
    public float defenseDuration;
    public float speedDuration;
    public int speed;
    public int defense;
    public ParticleSystem defenseParticle;
    void Start()
    {
        PotionBehavior.instance = this;
        defenseParticle.Stop();
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
                defenseParticle.Stop();
            }
        }
        if(Time.time > speedDuration)
        {
            if (isSpeedActive)
            {
                int playerStat = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Stats>()[StatTypes.RunSpeed];
                GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Stats>()[StatTypes.RunSpeed] = playerStat - speed;
                Debug.Log("After the potion wore off defense is now " + GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Stats>()[StatTypes.RunSpeed]);
                isSpeedActive = false;
                speed = 0;
            }
        }
    }
}
