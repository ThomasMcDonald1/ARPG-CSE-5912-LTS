using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ARPG.Combat;

public class AbilityCooldown : MonoBehaviour
{
    Character character;
    public float abilityCooldown;
    [HideInInspector] public float reducedCooldown;
    ActionBar actionBar;
    List<EnemyAbility> EnemyAttackTypeList;
    Coroutine cooldownRoutine;
    Coroutine enemyCooldownRoutine;

    private void Awake()
    {
        if (character is Player)
        {
            actionBar = GetComponentInParent<Player>().GetComponentInParent<GameplayStateController>().GetComponentInChildren<ActionBar>();
        }
        else if (character is Enemy)
        {
            if (character is EnemyKnight)
            {
                EnemyAttackTypeList = GetComponentInParent<EnemyKnight>().EnemyAttackTypeList;
            }
            //add more for more enemy
        }
    }

    private void OnEnable()
    {
        CastTimerCastType.AbilityCastTimeWasCompletedEvent += OnCastWasCompleted;
        InstantCastType.AbilityInstantCastWasCompletedEvent += OnCastWasCompleted;
    }

    void OnCastWasCompleted(object sender, InfoEventArgs<AbilityCast> e)
    {
        if (e.info.ability.gameObject == GetComponentInParent<Ability>().gameObject)
        {
            if (character is Player)
            {
                CooldownAbilityOnActionButtons(e.info);
            }
            else
            {
                CooldownAbilityOnEnemyList(e.info);
            }
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
                actionButton.cooldownTimer = reducedCooldown;
                actionButton.cooldownText.gameObject.SetActive(true);
                if (cooldownRoutine == null)
                    cooldownRoutine = StartCoroutine(CooldownAbility(actionButton));
            }
        }
    }

    public void CooldownAbilityOnEnemyList(AbilityCast abilityCast)
    {
        foreach (EnemyAbility enemyAbility in EnemyAttackTypeList)
        {
            if (enemyAbility.abilityAssigned == abilityCast.ability)
            {
                enemyAbility.cooldownTimer = abilityCooldown;
                if (enemyCooldownRoutine == null)
                    enemyCooldownRoutine = StartCoroutine(EnemyCooldownAbility(enemyAbility));
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
