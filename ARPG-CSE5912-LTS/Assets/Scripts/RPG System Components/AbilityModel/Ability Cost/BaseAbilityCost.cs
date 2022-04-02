using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseAbilityCost : MonoBehaviour
{
    public int cost;
    //public int reducedCost;

    public abstract void DeductResourceFromCaster(Character caster);

    public abstract bool CheckCharacterHasResourceCostForCastingAbility(Character caster);

    //public void GetReducedCost(AbilityCast abilityCast)
    //{
    //    if (abilityCast.caster.stats[StatTypes.CostReduction] > 0)
    //    {
    //        float reducedCost = abilityCast.abilityCost.cost - (abilityCast.abilityCost.cost * abilityCast.caster.stats[StatTypes.CostReduction] * 0.01f);
    //        abilityCast.abilityCost.reducedCost = Mathf.RoundToInt(reducedCost);
    //    }
    //}
}
