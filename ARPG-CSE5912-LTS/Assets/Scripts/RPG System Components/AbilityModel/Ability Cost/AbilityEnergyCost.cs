using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityEnergyCost : BaseAbilityCost
{
    

    // If the character successfully used the ability, reduce the character's Energy
    void OnDidPerform(object sender, object args)
    {
        Stats stats = GetComponentInParent<Stats>();
        stats[StatTypes.ENERGY] -= cost;
    }
}
