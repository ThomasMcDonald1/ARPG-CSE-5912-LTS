using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseAbilityEffect : MonoBehaviour
{
    //Use this value to change the damage done by everything in the game globally
    public const float globalDamageBalanceAdjustment = 1f;

    protected const int minDamage = -99999;
    protected const int maxDamage = 99999;


    public static event EventHandler<InfoEventArgs<Character>> AbilityMissedTargetEvent;

    //TODO: Not sure where the best place to add damage predictions for an ability is...perhaps here? such as:
    // public abstract int Predict();
    //then have the specific AbilityEffect classes calculated their damage versus a certain "default" target of the
    //same level as player character, or something. This function would be called when hovering over an ability to populate a tooltip
    

    //Apply the ability's effect to the target
    public void Apply(Character target)
    {
        //if (GetComponent<AbilityEffectTarget>().IsTarget(target) == false)
        //    return;

        if (GetComponent<BaseHitRate>().RollForHit(target))
        {
            OnApply(target);
        }
        else
        {
            AbilityMissedTargetEvent?.Invoke(this, new InfoEventArgs<Character>(target));
        }
    }

    protected abstract int OnApply(Character target);

    /// <summary>
    /// Grab a stat from the character
    /// </summary>
    protected virtual float GetStat(Character character, StatTypes statType)
    {
        //TODO: Listen for any events that the value should be modified
        float finalValue = character.stats.GetValue(statType);
        
        //TODO: if value needs to be modified due to equipped gear, etc, do modifications here. May require more classes or events being broadcast
        //finalValue += valueOfSummedUpMods;

        return finalValue;
    }

    public bool RollForCrit(Character caster)
    {
        int roll = UnityEngine.Random.Range(0, 101);
        int chance = caster.stats[StatTypes.CritChance];
        return chance >= roll;
    }
}
