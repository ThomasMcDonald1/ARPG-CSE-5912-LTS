using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnHitElementalDamageAbilityEffect : BaseAbilityEffect
{
    BaseAbilityEffectElement element;
    public static event EventHandler<InfoEventArgs<(Character, int, BaseAbilityEffectElement)>> AbilityOnHitDamageReceivedEvent;


    private void Awake()
    {
        element = GetComponent<BaseAbilityEffectElement>();
    }

    protected override int OnApply(Character target, AbilityCast abilityCast)
    {
        int finalCalculatedDamage = 0;

        if (abilityCast.basicAttackHit)
        {
            Animator animator = abilityCast.caster.GetComponent<Animator>();
            bool isMainHandAttack = animator.GetBool("AttackingMainHand");
            Stats casterStats = abilityCast.caster.GetComponent<Stats>();
            StatTypes onHitElementType = GetOnHitStatForElement(element);
            float damageBonusMult = abilityCast.abilityPower.GetDamageBonusMultiplier(abilityCast.caster, element);
            float baseDamage = casterStats[onHitElementType] * (1 + damageBonusMult * 0.01f);
            if (baseDamage > 0)
            {
                //Debug.Log("Base damage: " + baseDamage + " " + onHitElementType + " with " + damageBonusMult + " damage bonus multiplier.");
                //Debug.Log("On hit element type: " + onHitElementType + ", and amount: " + casterStats[onHitElementType]);
                float casterFlatMagicPen = GetStat(abilityCast.caster, StatTypes.FlatMagicPen);
                float casterPercentMagicPen = GetStat(abilityCast.caster, StatTypes.PercentMagicPen);
                float enemyDefense = abilityCast.abilityPower.GetBaseDefense(target, element);
                enemyDefense *= (1 + abilityCast.abilityPower.GetPercentDefense(target, element) * 0.01f);
                enemyDefense *= abilityCast.abilityPower.AdjustDefenseForPercentPenetration(abilityCast.caster);
                float finalDamageWithPen = baseDamage * (120 / (120 + enemyDefense));
                float damageRandomFloor = finalDamageWithPen * 0.95f;
                float damageRandomCeiling = finalDamageWithPen * 1.05f;
                finalDamageWithPen = UnityEngine.Random.Range(damageRandomFloor, damageRandomCeiling);
                if (!isMainHandAttack)
                    finalDamageWithPen /= 2f;
                finalCalculatedDamage = Mathf.RoundToInt(finalDamageWithPen);
                finalCalculatedDamage = Mathf.Clamp(finalCalculatedDamage, minDamage, maxDamage);
                //Debug.Log("Damaging for " + finalCalculatedDamage);
                target.stats[StatTypes.HP] -= finalCalculatedDamage;
                target.stats[StatTypes.HP] = Mathf.Clamp(target.stats[StatTypes.HP], 0, target.stats[StatTypes.MaxHP]);
                AbilityOnHitDamageReceivedEvent?.Invoke(this, new InfoEventArgs<(Character, int, BaseAbilityEffectElement)>((target, finalCalculatedDamage, element)));
            }
        }
        
        return finalCalculatedDamage;
    }

    private StatTypes GetOnHitStatForElement(BaseAbilityEffectElement element)
    {
        StatTypes statType;

        if (element is FireAbilityEffectElement)
            statType = StatTypes.FireDmgOnHit;
        else if (element is ColdAbilityEffectElement)
            statType = StatTypes.ColdDmgOnHit;
        else if (element is LightningAbilityEffectElement)
            statType = StatTypes.LightningDmgOnHit;
        else
            statType = StatTypes.PoisonDmgOnHit;

        return statType;
    }
}
