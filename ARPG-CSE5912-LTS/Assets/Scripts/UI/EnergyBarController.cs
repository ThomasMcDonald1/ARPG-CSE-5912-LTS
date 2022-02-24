using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

using UnityEngine.UI;

public class EnergyBarController : MonoBehaviour
{
    public Slider enrgBar;

    private WaitForSeconds regenTick = new WaitForSeconds(1f);
    private Coroutine regen;

    public static EnergyBarController instance;
    Stats stats;

    private void Awake()
    {
        instance = this;
        stats = GetComponent<Stats>();
    }

    // Start is called before the first frame update
    void Start()
    {
        enrgBar.maxValue = stats[StatTypes.MaxMana];
        enrgBar.value = stats[StatTypes.MaxMana];
    }

    private void Update()
    {
        if (stats != null && enrgBar != null)
            UpdateSlider();
    }

    public void AddMana(int amt)
    {
        stats[StatTypes.Mana] += amt;
    }

    public void SubtractMana(int amt)
    {
        stats[StatTypes.Mana] -= amt;
        if (regen == null)
            regen = StartCoroutine(RegenEnergy());
    }

    private void UpdateSlider()
    {
        stats[StatTypes.Mana] = Mathf.Clamp(stats[StatTypes.Mana], 0, stats[StatTypes.MaxMana]);
        enrgBar.value = stats[StatTypes.Mana];
    }

    private IEnumerator RegenEnergy()
    {
        //yield return new WaitForSeconds(1);
        while (stats[StatTypes.Mana] < stats[StatTypes.MaxMana])
        {
            stats[StatTypes.Mana] += stats[StatTypes.ManaRegen];
            yield return regenTick;
        }
        regen = null;
    }
}
