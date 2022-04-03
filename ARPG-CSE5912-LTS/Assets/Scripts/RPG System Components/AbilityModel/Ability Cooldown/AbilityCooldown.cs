using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityCooldown : MonoBehaviour
{
    public float abilityCooldown;
    [HideInInspector] public float reducedCooldown;
    ActionBar actionBar;
    Coroutine cooldownRoutine;

    private void Awake()
    {
        actionBar = GetComponentInParent<Player>().GetComponentInParent<GameplayStateController>().GetComponentInChildren<ActionBar>();
    }

    private void OnEnable()
    {
        CastTimerCastType.AbilityCastTimeWasCompletedEvent += OnCastWasCompleted;
        InstantCastType.AbilityInstantCastWasCompletedEvent += OnCastWasCompleted;
    }

    void OnCastWasCompleted(object sender, InfoEventArgs<AbilityCast> e)
    {
        if (e.info.ability.gameObject == GetComponentInParent<Ability>().gameObject)
            CooldownAbilityOnActionButtons(e.info);
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
}
