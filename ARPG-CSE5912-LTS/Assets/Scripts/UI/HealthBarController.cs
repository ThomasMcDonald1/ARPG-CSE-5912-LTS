using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarController : MonoBehaviour
{
    public Image healthBar;
    public float maxHealth = 100f;
    public float currHealth;
    Stats stats;
    float lerpSpd;
    // PlayerController_Script Player;  for updating current player health, not using at this point

    private void Start()
    {
        // currHealth = maxHealth;
       
            stats = GameObject.FindGameObjectWithTag("Player").GetComponent<Stats>();

        maxHealth = stats.maxHealth;
        stats.health = stats.maxHealth;
        currHealth = stats.health;
       
    }

    private void Update()
    {
        //if (currHealth > maxHealth) currHealth = maxHealth;
        lerpSpd = 10f * Time.deltaTime;
        HealthBarFiller();
        colorChanger();
        currHealth = stats.health; 

    }

    /* change the health bar with a lerp speed*/
    public void HealthBarFiller()
    {
        healthBar.fillAmount = Mathf.Lerp(healthBar.fillAmount, currHealth / maxHealth, lerpSpd);
    }
    public void colorChanger()
    {
        Color healthC = Color.Lerp(Color.red, Color.green, (currHealth / maxHealth));
        healthBar.color = healthC;
        
    }
    public void HitDamage(float damageRate)
    {
        if(currHealth > 0)
        {
            currHealth -= damageRate;
        }
    }
    public void healing(float healingRate)
    {
        if (currHealth <100)
        {
            
            currHealth += healingRate;
            if (currHealth > 100)
            {
                currHealth = 100;
            }
        }
    }
}
