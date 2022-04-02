using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlatStatModifierFeature : Feature
{
    public StatTypes type;
    public int flatAmount;

    Stats stats
    {
        get
        {
            return Target.GetComponent<Stats>();
        }
    }

    protected override void OnActivate()
    {
        //Debug.Log("Increasing " + type + "by " + flatAmount);
        stats[type] += flatAmount;
    }

    protected override void OnDeactivate()
    {
        stats[type] -= flatAmount;
    }
}
