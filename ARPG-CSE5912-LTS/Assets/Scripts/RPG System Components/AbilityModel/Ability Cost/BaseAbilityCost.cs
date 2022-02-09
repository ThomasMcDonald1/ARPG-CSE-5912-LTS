using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseAbilityCost : MonoBehaviour
{
    public int cost;

    public bool CheckCharacterHasResourceCostForCastingAbility(Character caster)
    {
        Stats stats = caster.GetComponent<Stats>();
        return stats[StatTypes.ENERGY] >= cost;
    }


}
