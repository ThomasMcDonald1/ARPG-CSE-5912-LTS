using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PercentStatModifierFeature : Feature
{
    public StatTypes type;
    public int percentAmount;

    Stats stats
    {
        get
        {
            return Target.GetComponent<Stats>();
        }
    }

    protected override void OnActivate()
    {
        //Debug.Log("Multiplying " + type + "by " + percentAmount + " percent");
        stats[type] = Mathf.RoundToInt(stats[type] * percentAmount * 0.01f);
    }

    protected override void OnDeactivate()
    {
        if (percentAmount != 0)
            stats[type] = Mathf.RoundToInt(stats[type] / percentAmount * 0.01f);
    }
}
