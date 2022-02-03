using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarController : MonoBehaviour
{
    public Image healthBar;
    //public float maxHealth = 100f;
    //public float currHealth;
    Stats stats;
    float lerpSpd;
    // PlayerController_Script Player;  for updating current player health, not using at this point

    private void Start()
    {
        // currHealth = maxHealth;
       
        stats = GameObject.FindGameObjectWithTag("Player").GetComponent<Stats>();

        //maxHealth = stats.maxHealth;
        //stats.health = stats.maxHealth;
        //currHealth = stats.health;
       
    }

    private void Update()
    {
        if (stats[StatTypes.HEALTH] > stats[StatTypes.MAXHEALTH]) stats[StatTypes.HEALTH] = stats[StatTypes.MAXHEALTH];
        lerpSpd = 10f * Time.deltaTime;
        HealthBarFiller();
        colorChanger();
        //currHealth = stats.health; 

    }

    /* change the health bar with a lerp speed*/
    public void HealthBarFiller()
    {
        //Debug.Log("Health" + stats[StatTypes.HEALTH]);
        Debug.Log("Max Health" + stats[StatTypes.MAXHEALTH]);
        //need float division
        healthBar.fillAmount = Mathf.Lerp(healthBar.fillAmount, (float)stats[StatTypes.HEALTH] / (float)stats[StatTypes.MAXHEALTH], lerpSpd);
    }
    public void colorChanger()
    {
        //need fload division
        Color healthC = Color.Lerp(Color.red, Color.green, (float)stats[StatTypes.HEALTH] / (float)stats[StatTypes.MAXHEALTH]);
        healthBar.color = healthC;

    }
    //public void HitDamage(float damageRate)
    //{
    //    if(stats[StatTypes.HEALTH] > 0)
    //    {
    //        stats[StatTypes.HEALTH] -= damageRate;
    //    }
    //}
    //public void healing(float healingRate)
    //{
    //    if (stats[StatTypes.HEALTH] < 100)
    //    {

    //        stats[StatTypes.HEALTH] += healingRate;
    //        if (stats[StatTypes.HEALTH] > 100)
    //        {
    //            stats[StatTypes.HEALTH] = 100;
    //        }
    //    }
    //}
}
