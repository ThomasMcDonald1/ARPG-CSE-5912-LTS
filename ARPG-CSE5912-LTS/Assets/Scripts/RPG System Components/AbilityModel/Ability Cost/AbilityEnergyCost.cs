using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityEnergyCost : BaseAbilityCost
{
    //TODO: Listen for events on if the ability can be performed or not


    // If the caster doesn't have enough Energy to use the ability, disable its usage and gray out its ability icon
    void OnCanPerformCheck(object sender, object args)
    {
        Stats stats = GetComponentInParent<Stats>();
        if (stats[StatTypes.ENERGY] < cost)
        {
            //TODO: Disable
        }
    }

    // If the character successfully used the ability, reduce the character's Energy
    void OnDidPerform(object sender, object args)
    {
        Stats stats = GetComponentInParent<Stats>();
        stats[StatTypes.ENERGY] -= cost;
    }
}
