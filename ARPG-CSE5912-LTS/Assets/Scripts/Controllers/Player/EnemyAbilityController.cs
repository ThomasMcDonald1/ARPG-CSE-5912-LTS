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

    [HideInInspector] public bool enemyInAOEAbilityTargetSelectionMode;
    [HideInInspector] public bool enemyInSingleTargetAbilitySelectionMode;

    int groundLayerMask = 1 << 6;

    private void OnEnable()
    {
        CastTimerCastType.AbilityCastTimeWasCompletedEvent += OnCompletedCast;
        InstantCastType.AbilityInstantCastWasCompletedEvent += OnCompletedCast;
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
            EnemyClick(abilityCast);
        }
        else
        {
            EnemyClickAOE(abilityCast);
        }
    }

    private void EnemyClick(AbilityCast abilityCast)
    {
        Ray ray = new Ray(AttackTarget.position + Vector3.up, Vector3.down);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            GameObject go = hit.collider.gameObject;
            Character target = go.GetComponent<Character>();
            //TODO: If ability requires enemy or ally to be clicked, excluding the other, check that first
            if (target != null)
            {
                abilityCast.hit = hit;
                bool targetInRange = CheckCharacterInRange(target);
                if (!targetInRange)
                {
                    Debug.Log("Character Not in range");
                    EnemySelectedSingleTargetEvent?.Invoke(this, new InfoEventArgs<AbilityCast>(abilityCast));
                }
            }



        }
    }

    private void EnemyClickAOE(AbilityCast abilityCast)
    {
        if (abilityCast.abilityArea != null)
        {
            abilityCast.abilityArea.DisplayAOEArea();
        }

        if (abilityCast.abilityArea != null)
        {
            Ray ray = new Ray(AttackTarget.position + Vector3.up, Vector3.down);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, groundLayerMask))
            {
                abilityCast.hit = hit;
                EnemySelectedGroundTargetLocationEvent?.Invoke(this, new InfoEventArgs<AbilityCast>(abilityCast));
            }
            abilityCast.abilityArea.abilityAreaNeedsShown = false;

        }
    }
}
