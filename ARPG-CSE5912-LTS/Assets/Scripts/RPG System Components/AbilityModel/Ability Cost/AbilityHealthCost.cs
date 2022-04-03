using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityHealthCost : BaseAbilityCost
{
    public override bool CheckCharacterHasResourceCostForCastingAbility(Character caster)
    {
        return caster.stats[StatTypes.HP] >= cost;
    }

    public override void DeductResourceFromCaster(Character caster)
    {
        caster.stats[StatTypes.HP] -= cost;
    }
}
