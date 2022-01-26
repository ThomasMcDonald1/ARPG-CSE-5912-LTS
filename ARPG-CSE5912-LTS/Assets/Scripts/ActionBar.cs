using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionBar : MonoBehaviour
{
    public GameObject healthBar;
    // Start is called before the first frame update
    void Start()
    {
        healthBar = GameObject.FindGameObjectWithTag("HealthBar");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Healing()
    {
        if(healthBar.GetComponent<HealthBarController>().currHealth == 100)
        {
            Debug.Log("True");
        }
    }
}
