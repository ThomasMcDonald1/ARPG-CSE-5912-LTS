using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseAbilityPower : MonoBehaviour
{
    public float baseDamageOrHealing;

    public abstract int GetBaseAttack(); //Currently unused, but will be useful if we add another stat to damage calculation
    public abstract float GetBaseDefense(Character target, BaseAbilityEffectElement effectElement);
    public abstract float GetPercentDefense(Character target, BaseAbilityEffectElement effectElement);
    public abstract float AdjustDefenseForFlatPenetration(Character caster);
    public abstract float AdjustDefenseForPercentPenetration(Character caster);
    public abstract float GetDamageBonusMultiplier(Character caster, BaseAbilityEffectElement element);

    protected virtual float GetStat(Character character, StatTypes statType)
    {
        //TODO: Listen for any events that the value should be modified
        float finalValue = character.stats.GetValue(statType);

        //TODO: if value needs to be modified due to equipped gear, etc, do modifications here. May require more classes or events being broadcast
        //finalValue += valueOfSummedUpMods;

        return finalValue;
    }
}
