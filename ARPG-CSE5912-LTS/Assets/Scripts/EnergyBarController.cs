using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

using UnityEngine.UI;

public class EnergyBarController : MonoBehaviour
{
    public Slider enrgBar;
    public int maxEnrg =100;
    public int currentEnrg;

    private WaitForSeconds regenTick = new WaitForSeconds(0.1f);
    private Coroutine regen;

    public static EnergyBarController instance;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        currentEnrg = maxEnrg;
        enrgBar.maxValue = maxEnrg;
        enrgBar.value = maxEnrg;

    }
    
    void Update()
    {
        var keyboard = Keyboard.current;
        if (keyboard.spaceKey.wasPressedThisFrame)
        {
            EnergyBarController.instance.UseEnry(10);
           
        }
    }

    public void UseEnry(int amt)
    {
        if((currentEnrg - amt) >= 0)
        {
            currentEnrg -= amt;
            enrgBar.value = currentEnrg;
           
            if( regen != null)
            {
                StopCoroutine(regen);
            }
            regen = StartCoroutine(RegenEnrg());
        }
        else
        {
            currentEnrg = 0;
            enrgBar.value = currentEnrg;
        }
    }

    private IEnumerator RegenEnrg()
    {
        yield return new WaitForSeconds(2);
        while(currentEnrg < maxEnrg)
        {
            currentEnrg += maxEnrg / 100;
            enrgBar.value = currentEnrg;
            yield return regenTick;
        }
        regen = null;
    }
}
