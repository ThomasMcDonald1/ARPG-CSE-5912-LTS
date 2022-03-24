using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class HealthBarController : MonoBehaviour
{
    public Slider healthBar;

    private WaitForSeconds regenTick = new WaitForSeconds(1f);
    private Coroutine regen;

    public static HealthBarController instance;
    protected Stats stats;

    private void Awake()
    {
        instance = this;
        stats = GetComponent<Stats>();
    }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        healthBar.maxValue = stats[StatTypes.MaxHP];
        healthBar.value = stats[StatTypes.HP];
    }

    private void Update()
    {
        if (stats != null && healthBar != null)
            UpdateSlider();
    }

    public void AddHealth(int amt)
    {
        stats[StatTypes.HP] += amt;
    }

    public void SubtractHealth(int amt)
    {
        stats[StatTypes.HP] -= amt;

        if (regen == null)
            regen = StartCoroutine(RegenHealth());
    }

    protected virtual void UpdateSlider()
    {
        stats[StatTypes.HP] = Mathf.Clamp(stats[StatTypes.HP], 0, stats[StatTypes.MaxHP]);
        healthBar.value = stats[StatTypes.HP];
    }

    private IEnumerator RegenHealth()
    {
        yield return new WaitForSeconds(2);
        while (stats[StatTypes.HP] < stats[StatTypes.MaxHP])
        {
            stats[StatTypes.HP] += stats[StatTypes.HealthRegen];
            yield return regenTick;
        }
        regen = null;
    }

}
