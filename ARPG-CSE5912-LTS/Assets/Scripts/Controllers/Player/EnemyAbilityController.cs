using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using ARPG.Combat;
using System;

public class EnemyAbilityController : Enemy
{
    public static event EventHandler<InfoEventArgs<AbilityCast>> EnemySelectedGroundTargetLocationEvent;
    public static event EventHandler<InfoEventArgs<AbilityCast>> EnemySelectedSingleTargetEvent;

    Coroutine aoeAbilitySelectionMode;
    Coroutine singleTargetSelectionMode;
    [HideInInspector] public bool enemyInAOEAbilityTargetSelectionMode;
    [HideInInspector] public bool enemyInSingleTargetAbilitySelectionMode;
    [HideInInspector] public bool enemyNeedsToReleaseAbility;

    int groundLayerMask = 1 << 6;

    private void OnEnable()
    {
        InputController.CancelPressedEvent += OnCancelPressed;
        CastTimerCastType.AbilityCastTimeWasCompletedEvent += OnCompletedCast;
        InstantCastType.AbilityInstantCastWasCompletedEvent += OnCompletedCast;
    }

    
    private void OnCancelPressed(object sender, InfoEventArgs<int> e)
    {
        enemyInAOEAbilityTargetSelectionMode = false;
        enemyInSingleTargetAbilitySelectionMode = false;
        if (singleTargetSelectionMode != null)
            StopCoroutine(singleTargetSelectionMode);
        if (aoeAbilitySelectionMode != null)
            StopCoroutine(aoeAbilitySelectionMode);
        gameplayStateController.aoeReticleCylinder.SetActive(false);
    }
    

    void OnCompletedCast(object sender, InfoEventArgs<AbilityCast> e)
    {
        Debug.Log("Enemy Cast was completed");
        DeductCastingCost(e.info);
        GetColliders(e.info);
    }

    public void EnemyQueueAbilityCastSelectionRequired(AbilityCast abilityCast)
    {
        if (abilityCast.requiresCharacterUnderCursor)
        {
            enemyInAOEAbilityTargetSelectionMode = true;
            singleTargetSelectionMode = StartCoroutine(WaitForEnemyClick(abilityCast));
        }
        else
        {
            enemyInAOEAbilityTargetSelectionMode = true;
            aoeAbilitySelectionMode = StartCoroutine(WaitForEnemyClickAOE(abilityCast));
        }
    }

    private IEnumerator WaitForEnemyClick(AbilityCast abilityCast)
    {
        enemyInSingleTargetAbilitySelectionMode = true;
        while (enemyInSingleTargetAbilitySelectionMode)
        {
            Vector3 direction = AttackTarget.position - transform.position;
            Ray ray = new Ray(transform.position, direction);

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                GameObject go = hit.collider.gameObject;
                Character target = go.GetComponent<Character>();
                //TODO: If ability requires enemy or ally to be clicked, excluding the other, check that first
                if (target != null)
                {
                    enemyInSingleTargetAbilitySelectionMode = false;
                    abilityCast.hit = hit;
                    bool targetInRange = CheckCharacterInRange(target);
                    if (!targetInRange)
                    {
                        Debug.Log("Character Not in range");
                        EnemySelectedSingleTargetEvent?.Invoke(this, new InfoEventArgs<AbilityCast>(abilityCast));
                    }
                }
            }

            yield return null;
        }
    }

    private IEnumerator WaitForEnemyClickAOE(AbilityCast abilityCast)
    {
        bool enemyHasNotClicked = true;

        while (enemyHasNotClicked)
        {
            if (abilityCast.abilityArea != null)
            {
                abilityCast.abilityArea.DisplayAOEArea();
            }
            if (!enemyNeedsToReleaseAbility)
            {
                enemyHasNotClicked = false;
                if (abilityCast.abilityArea != null)
                {
                    Vector3 direction = AttackTarget.position - transform.position;
                    Ray ray = new Ray(transform.position, direction);
                    RaycastHit hit;

                    if (Physics.Raycast(ray, out hit, groundLayerMask))
                    {
                        abilityCast.hit = hit;
                        EnemySelectedGroundTargetLocationEvent?.Invoke(this, new InfoEventArgs<AbilityCast>(abilityCast));
                    }
                    abilityCast.abilityArea.abilityAreaNeedsShown = false;
                    gameplayStateController.aoeReticleCylinder.SetActive(false);
                }
            }
            //TODO: Check for other input that would stop this current ability cast, like queueing up a different ability instead, or pressing escape
            yield return null;
        }
    }
}
