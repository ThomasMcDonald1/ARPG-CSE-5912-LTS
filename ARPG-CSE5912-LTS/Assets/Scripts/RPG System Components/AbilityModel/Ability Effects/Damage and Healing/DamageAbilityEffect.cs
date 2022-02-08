using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageAbilityEffect : BaseAbilityEffect
{
    protected override int OnApply(Character target)
    {
        Character caster = GetComponentInParent<Character>();
        BaseAbilityPower power = GetComponent<BaseAbilityPower>();
        MagicalAbilityPower magPow = (MagicalAbilityPower)power;
        PhysicalAbilityPower physPow = (PhysicalAbilityPower)power;

        //To store the damage calculated
        float damage = 0;
        //Multiply this into the damage. This is a base multiplier defined for the ability (so each ability can be balanced differently)
        float multiplier = power.multiplier;

        //If the attack has base physical damage in it (before modifiers from outside sources), then calculate some physical junk
        if (physPow != null)
        {
            //Get some stats for attacker and defender like this
            float casterATK = GetStat(caster, StatTypes.STR);
            float enemyDefense = GetStat(target, StatTypes.ARMOR);
        }

        //If the attack has base magical damage in it (before modifiers from outside sources), then calculate some magic junk
        if (magPow != null)
        {
            //Get stats like above
            float casterATK = GetStat(caster, StatTypes.INT);
            //TODO: Add checks for what element an attack is (FIRE RES is just as an example)
            float enemyDefense = GetStat(target, StatTypes.FIRERES);
        }

        //TODO: damage = calculations involving caster stats and defender stats gotten above, if necessary, times the multiplier if that should happen before modifiers

        //TODO: Pull in modifiers from equipment and such, and do those calculations

        //TODO: Combine things to calculate final damage

        //round damage to an integer to return
        int finalCalculatedDamage = Mathf.RoundToInt(damage);

        //If we want an upper limit on damage, we can clamp it like this
        finalCalculatedDamage = Mathf.Clamp(finalCalculatedDamage, minDamage, maxDamage);

        //TODO: You could also modify ALL damage done in the game by multiplying this by the variable 'globalDamageBalanceAdjustment' which can be
        //changed in the BaseAbilityEffect script

        return finalCalculatedDamage;
    }
}
