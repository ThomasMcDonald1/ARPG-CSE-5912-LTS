using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ARPG.Combat;

public class AbilityCooldown : MonoBehaviour
{
    public float abilityCooldown;
    [HideInInspector] public float reducedCooldown;
    ActionBar actionBar;
    //List<EnemyAbility> EnemyAttackTypeList;
    Coroutine cooldownRoutine;
    Coroutine enemyCooldownRoutine;

    private void Awake()
    {
        actionBar = FindObjectOfType<GameplayStateController>().GetComponentInChildren<ActionBar>();
        //add more for more enemy <---probably not necessary?
    }

    private void OnEnable()
    {
        CastTimerCastType.AbilityCastTimeWasCompletedEvent += OnCastWasCompleted;
        InstantCastType.AbilityInstantCastWasCompletedEvent += OnCastWasCompleted;
    }

    void OnCastWasCompleted(object sender, InfoEventArgs<AbilityCast> e)
    {

        if (this != null && e.info.caster is Player && GetComponent<Ability>() == e.info.ability)
        {
            //Debug.Log("Cooling down player abilities");
            CooldownAbilityOnActionButtons(e.info);
        }
        else if (this != null && GetComponent<Ability>() == e.info.ability)
        {
            CooldownAbilityOnEnemyList(e.info);
        }
    }

    public void GetReducedCooldown(AbilityCast abilityCast)
    {
        abilityCast.abilityCooldown.reducedCooldown = abilityCast.abilityCooldown.abilityCooldown - (abilityCast.abilityCooldown.abilityCooldown * abilityCast.caster.stats[StatTypes.CooldownReduction] * 0.01f);
    }

    public void CooldownAbilityOnActionButtons(AbilityCast abilityCast)
    {
        foreach (ActionButton actionButton in actionBar.actionButtons)
        {
            if (actionButton.abilityAssigned == abilityCast.ability)
            {
                //Debug.Log("Cooling down " + actionButton.abilityAssigned + " with cooldown " + reducedCooldown);
                actionButton.cooldownTimer = reducedCooldown;
                actionButton.cooldownText.gameObject.SetActive(true);
                if (cooldownRoutine == null)
                    cooldownRoutine = StartCoroutine(CooldownAbility(actionButton));
            }
        }
    }

    public void CooldownAbilityOnEnemyList(AbilityCast abilityCast)
    {
        Enemy enemyCaster = (Enemy)abilityCast.caster;
        if (enemyCaster != null && enemyCaster.EnemyAttackTypeList != null && enemyCaster.EnemyAttackTypeList.Count > 0)
        {
            foreach (EnemyAbility enemyAbility in enemyCaster.EnemyAttackTypeList)
            {
                if (enemyAbility.abilityAssigned == abilityCast.ability)
                {
                    enemyAbility.cooldownTimer = reducedCooldown;
                    if (enemyCooldownRoutine == null)
                        enemyCooldownRoutine = StartCoroutine(EnemyCooldownAbility(enemyAbility));
                }
            }
        }
    }

    private IEnumerator CooldownAbility(ActionButton actionButton)
    {
        actionButton.abilityInSlotOnCooldown = true;
        while (actionButton.cooldownTimer > 0)
        {
            actionButton.cooldownText.text = Mathf.RoundToInt(actionButton.cooldownTimer).ToString();
            actionButton.cooldownFill.fillAmount = actionButton.cooldownTimer / reducedCooldown;
            actionButton.DecrementCooldownTimer();
            yield return null;
        }
        actionButton.cooldownText.gameObject.SetActive(false);
        actionButton.abilityInSlotOnCooldown = false;
        cooldownRoutine = null;
    }

    private IEnumerator EnemyCooldownAbility(EnemyAbility enemyAbility)
    {
        enemyAbility.abilityOnCooldown = true;
        while (enemyAbility.cooldownTimer > 0)
        {
            enemyAbility.DecrementCooldownTimer();
            yield return null;
        }
        enemyAbility.abilityOnCooldown = false;
        cooldownRoutine = null;
    }
}
