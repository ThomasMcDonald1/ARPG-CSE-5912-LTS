using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityEnergyCost : BaseAbilityCost
{
    public override bool CheckCharacterHasResourceCostForCastingAbility(Character caster)
    {
        return caster.stats[StatTypes.Mana] >= cost;
    }

    public override void DeductResourceFromCaster(Character caster)
    {
        caster.stats[StatTypes.Mana] -= cost;
    }
}
