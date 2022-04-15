using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAttackDamageAbilityEffect : BaseAbilityEffect
{
    public static event EventHandler<InfoEventArgs<(Character, int, bool)>> BasicAttackDamageReceivedEvent;
    public static event EventHandler<InfoEventArgs<(Character, int, bool)>> BasicAttackHealingReceivedEvent;
    public static event EventHandler<InfoEventArgs<(Character, int)>> ThornsDamageReceivedEvent;
    public EquipManager equippedWeapon;
    protected override int OnApply(Character target, AbilityCast abilityCast)
    {
        Animator animator = abilityCast.caster.GetComponent<Animator>();
        bool isMainHandAttack = animator.GetBool("AttackingMainHand");
        bool wasCrit = false;
        abilityCast.basicAttackHit = true;
        //Effect elements would only be needed for magic damage basic attack weapons like staves
        //but the variable is still needed for calling abilityPower functions
        BaseAbilityEffectElement effectElement = GetComponent<BaseAbilityEffectElement>();

        //Get physical attack of the attacker
        float attack = GetStat(abilityCast.caster, StatTypes.PHYATK) * 0.4f;
        //Get the physical damage bonus percent multiplier
        float damageBonusMult = 1 + GetStat(abilityCast.caster, StatTypes.PhysDmgBonus) * 0.01f;
        //TODO: get damage range from the weapon instead of using this placeholder dagger
        float minWeaponDamage = 1;
        float maxWeaponDamage = 1;
        if (isMainHandAttack)
        {
            if (equippedWeapon != null && equippedWeapon.currentEquipment[0] != null)
            {
                WeaponEquipment weapon = (WeaponEquipment)equippedWeapon.currentEquipment[0];
                minWeaponDamage = weapon.minimumDamage;
                maxWeaponDamage = weapon.maximumDamage;
            }
        }
        else if (equippedWeapon != null && equippedWeapon.currentEquipment[1] != null)
        {
            WeaponEquipment weapon = (WeaponEquipment)equippedWeapon.currentEquipment[1];
            minWeaponDamage += weapon.minimumDamage;
            maxWeaponDamage += weapon.maximumDamage;
        }

        Debug.Log("minWeaponDamage is " + minWeaponDamage);
        Debug.Log("maxWeaponDamage is " + maxWeaponDamage);
        //Choose a random number from within the minimum and maximum weapon damage range
        float chosenWeaponDamage = UnityEngine.Random.Range(minWeaponDamage, maxWeaponDamage);
        //Calculate initial base damage from the above
        float baseDamage = (chosenWeaponDamage * attack / 10 + attack) * damageBonusMult;
        Debug.Log("Base damage: " + baseDamage);
        //Calculate the enemy's defense pre-penetration
        float enemyDefense = abilityCast.abilityPower.GetBaseDefense(target, effectElement);
        enemyDefense *= (1 + abilityCast.abilityPower.GetPercentDefense(target, effectElement));
        //Calculate enemy defense post-penetration
        enemyDefense -= abilityCast.abilityPower.AdjustDefenseForFlatPenetration(abilityCast.caster);
        enemyDefense *= abilityCast.abilityPower.AdjustDefenseForPercentPenetration(abilityCast.caster);
        //Calculate the final damage the caster would do post-penetration
        float finalDamageWithPen = baseDamage * (120 / (120 + enemyDefense));
        //Add some more randomization
        float damageRandomFloor = finalDamageWithPen * 0.95f;
        float damageRandomCeiling = finalDamageWithPen * 1.05f;
        finalDamageWithPen = UnityEngine.Random.Range(damageRandomFloor, damageRandomCeiling);
        //Determine if the attack crits
        wasCrit = RollForCrit(abilityCast.caster);
        if (wasCrit)
        {
            float critDamagePercent = (200 + abilityCast.caster.stats[StatTypes.CritDamage]) * 0.01f;
            finalDamageWithPen *= critDamagePercent;
        }
        //half damage if offhand attack
        if (!isMainHandAttack)
            finalDamageWithPen /= 2f;
        //round damage to an integer and clamp
        int finalCalculatedDamage = Mathf.RoundToInt(finalDamageWithPen);
        finalCalculatedDamage = Mathf.Clamp(finalCalculatedDamage, minDamage, maxDamage);
        Debug.Log("Final damage: " + finalCalculatedDamage);
        //Apply the damage
        target.stats[StatTypes.HP] -= finalCalculatedDamage;
        target.stats[StatTypes.HP] = Mathf.Clamp(target.stats[StatTypes.HP], 0, target.stats[StatTypes.MaxHP]);

        //Send event
        BasicAttackDamageReceivedEvent?.Invoke(this, new InfoEventArgs<(Character, int, bool)>((target, finalCalculatedDamage, wasCrit)));

        //Get the lifesteal of the attacker
        float lifesteal = GetStat(abilityCast.caster, StatTypes.Lifesteal);
        //If the attacker has lifesteal, then use it to get a % of the damage done
        if (lifesteal > 0)
        {
            float lifeToSteal = finalCalculatedDamage * (lifesteal * 0.01f);
            int lifeToStealInt = Mathf.RoundToInt(lifeToSteal);
            Debug.Log("Life stolen: " + lifeToStealInt);
            abilityCast.caster.stats[StatTypes.HP] += lifeToStealInt;
            abilityCast.caster.stats[StatTypes.HP] = Mathf.Clamp(abilityCast.caster.stats[StatTypes.HP], 0, abilityCast.caster.stats[StatTypes.MaxHP]);
            BasicAttackHealingReceivedEvent?.Invoke(this, new InfoEventArgs<(Character, int, bool)>((abilityCast.caster, lifeToStealInt, wasCrit)));
        }

        int damageReflect = Mathf.RoundToInt(GetStat(target, StatTypes.DamageReflect));
        if (damageReflect > 0)
        {
            abilityCast.caster.stats[StatTypes.HP] -= damageReflect;
            abilityCast.caster.stats[StatTypes.HP] = Mathf.Clamp(abilityCast.caster.stats[StatTypes.HP], 0, abilityCast.caster.stats[StatTypes.MaxHP]);
            ThornsDamageReceivedEvent?.Invoke(this, new InfoEventArgs<(Character, int)>((abilityCast.caster, damageReflect)));
        }

        return finalCalculatedDamage;
    }
}
