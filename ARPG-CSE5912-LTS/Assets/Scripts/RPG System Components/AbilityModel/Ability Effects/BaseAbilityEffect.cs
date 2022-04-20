using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseAbilityEffect : MonoBehaviour
{
    //Use this value to change the damage done by everything in the game globally
    public const float globalDamageBalanceAdjustment = 1f;
    public string effectOrigin;
    public GameObject effectVFXObj;

    protected const int minDamage = -99999;
    protected const int maxDamage = 99999;

    public static event EventHandler<InfoEventArgs<Character>> AbilityMissedTargetEvent;
    public static event EventHandler<InfoEventArgs<Character>> AbilityWasBlockedEvent;

    //TODO: Not sure where the best place to add damage predictions for an ability is...perhaps here? such as:
    // public abstract int Predict();
    //then have the specific AbilityEffect classes calculated their damage versus a certain "default" target of the
    //same level as player character, or something. This function would be called when hovering over an ability to populate a tooltip
    

    //Apply the ability's effect to the target
    public void Apply(Character target, AbilityCast abilityCast)
    {
        //if (GetComponent<AbilityEffectTarget>().IsTarget(target) == false)
        //    return;
        BaseHitRate hitRate = GetComponent<BaseHitRate>();

        if (hitRate.RollForHit(target))
        {
            //Debug.Log(target.stats);
            if (abilityCast.ability == abilityCast.caster.basicAttackAbility && target.stats[StatTypes.BlockChance] > 0 && hitRate.RollForBlock(target))
                OnApply(target, abilityCast);
            else if (abilityCast.ability == abilityCast.caster.basicAttackAbility && target.stats[StatTypes.BlockChance] > 0 && !hitRate.RollForBlock(target))
                AbilityWasBlockedEvent?.Invoke(this, new InfoEventArgs<Character>(target));
            else
               OnApply(target, abilityCast);

        }
        else
        {
            AbilityMissedTargetEvent?.Invoke(this, new InfoEventArgs<Character>(target));
        }
    }

    protected abstract int OnApply(Character target, AbilityCast abilityCast);

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

    public Vector3 GetEffectOrigin(AbilityCast abilityCast, Character target)
    {
        Vector3 origin = new Vector3();
        effectOrigin = effectOrigin.ToLower();
        switch (effectOrigin)
        {
            case "caster":
                origin = abilityCast.caster.transform.position;
                break;
            case "click":
                origin = abilityCast.hit.point;
                break;
            case "target":
                origin = target.transform.position;
                break;
            default:
                break;
        }
        return origin;
    }

    public void InstantiateEffectVFX(AbilityCast abilityCast, Character target)
    {
        if (effectOrigin.ToLower() != "target" && !abilityCast.abilityVFXFired || effectOrigin.ToLower() == "target")
        {
            abilityCast.abilityVFXFired = true;
            Debug.Log("Creating Effect VFX");
            GameObject instance = Instantiate(effectVFXObj);
            Vector3 vfxPos = GetEffectOrigin(abilityCast, target);
            instance.transform.position = vfxPos;

            ParticleSystem pS = instance.GetComponent<ParticleSystem>();
            SpecifyAbilityArea aa = abilityCast.ability.GetComponent<SpecifyAbilityArea>();
            PointBlankAbilityArea pbaoe = abilityCast.ability.GetComponent<PointBlankAbilityArea>();

            if (aa != null)
            {
                instance.transform.localScale = new Vector3(aa.aoeRadius * 2, 2f, aa.aoeRadius * 2);
            }
            else if (pbaoe != null)
            {
                instance.transform.localScale = new Vector3(pbaoe.aoeRadius * 2, 2f, pbaoe.aoeRadius * 2);
            }
            StartCoroutine(ShowVFX(pS, instance));
        }  
    }

    private IEnumerator ShowVFX(ParticleSystem pS, GameObject instance)
    {
        yield return new WaitForSeconds(pS.main.duration);
        Destroy(instance);
    }
}
