using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatModifierFeature : Feature
{
    public StatTypes type;
    //Only use one of these per feature
    public int flatAmount;
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
        if (flatAmount > 0 || flatAmount < 0)
            stats[type] += flatAmount;
        else if (percentAmount > 0)
            stats[type] *= percentAmount;
    }

    protected override void OnDeactivate()
    {
        if (flatAmount > 0 || flatAmount < 0)
            stats[type] -= flatAmount;
        else if (percentAmount > 0)
            stats[type] /= percentAmount;
    }
}
