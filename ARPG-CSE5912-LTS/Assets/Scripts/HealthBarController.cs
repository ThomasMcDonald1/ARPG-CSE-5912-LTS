using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarController : MonoBehaviour
{
    public Image healthBar;
    public float maxHealth = 100f;
    public float currHealth;

    float lerpSpd;
    // PlayerController_Script Player;  for updating current player health, not using at this point

    private void Start()
    {
        currHealth = maxHealth;
        
    }

    private void Update()
    {
        if (currHealth > maxHealth) currHealth = maxHealth;
        lerpSpd = 3f * Time.deltaTime;
        HealthBarFiller();
        colorChanger();

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
    public void Healing(float healingRate)
    {
        if (currHealth > 0 && currHealth<maxHealth)
        {
            currHealth += healingRate;
        }
    }
}
