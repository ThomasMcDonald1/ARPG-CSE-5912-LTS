using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using ARPG.Movement;
using System;

public class PlayerAbilityController : Player
{
    public static event EventHandler<InfoEventArgs<AbilityCast>> PlayerSelectedGroundTargetLocationEvent;
    public static event EventHandler<InfoEventArgs<AbilityCast>> PlayerSelectedSingleTargetEvent;

    Coroutine aoeAbilitySelectionMode;
    Coroutine singleTargetSelectionMode;
    [HideInInspector] public bool playerInAOEAbilityTargetSelectionMode;
    [HideInInspector] public bool playerInSingleTargetAbilitySelectionMode;
    [HideInInspector] public bool playerNeedsToReleaseMouseButton;

    int groundLayerMask = 1 << 6;

    private void OnEnable()
    {
        InputController.CancelPressedEvent += OnCancelPressed;
        CastTimerCastType.AbilityCastTimeWasCompletedEvent += OnCompletedCast;
        InstantCastType.AbilityInstantCastWasCompletedEvent += OnCompletedCast;
    }

    private void OnCancelPressed(object sender, InfoEventArgs<int> e)
    {
        playerInAOEAbilityTargetSelectionMode = false;
        playerInSingleTargetAbilitySelectionMode = false;
        if (singleTargetSelectionMode != null)
            StopCoroutine(singleTargetSelectionMode);
        if (aoeAbilitySelectionMode != null)
            StopCoroutine(aoeAbilitySelectionMode);
        cursorChanger.ChangeCursorToDefaultGraphic();
        gameplayStateController.aoeReticleCylinder.SetActive(false);
    }

    void OnCompletedCast(object sender, InfoEventArgs<AbilityCast> e)
    {
        if (e.info.caster == this)
        {
            Debug.Log("Player cast was completed");
            DeductCastingCost(e.info);
            GetColliders(e.info);
        }
    }

    public void PlayerQueueAbilityCastSelectionRequired(AbilityCast abilityCast)
    {
        cursorChanger.ChangeCursorToSelectionGraphic();

        if (abilityCast.requiresCharacterUnderCursor)
        {
            playerInAOEAbilityTargetSelectionMode = true;
            singleTargetSelectionMode = StartCoroutine(WaitForPlayerClick(abilityCast));
        }
        else
        {
            playerInAOEAbilityTargetSelectionMode = true;
            aoeAbilitySelectionMode = StartCoroutine(WaitForPlayerClickAOE(abilityCast));
        }
    }

    private IEnumerator WaitForPlayerClick(AbilityCast abilityCast)
    {
        playerInSingleTargetAbilitySelectionMode = true;
        while (playerInSingleTargetAbilitySelectionMode)
        {
            Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                GameObject go = hit.collider.gameObject;
                Character target = go.GetComponent<Character>();
                //TODO: If ability requires enemy or ally to be clicked, excluding the other, check that first
                if (target != null && Mouse.current.leftButton.wasReleasedThisFrame)
                {
                    playerInSingleTargetAbilitySelectionMode = false;
                    cursorChanger.ChangeCursorToDefaultGraphic();
                    abilityCast.hit = hit;
                    bool targetInRange = CheckCharacterInRange(target);
                    if (!targetInRange)
                    {
                        Debug.Log("Not in range");
                        PlayerSelectedSingleTargetEvent?.Invoke(this, new InfoEventArgs<AbilityCast>(abilityCast));
                    }
                }
            }

            yield return null;
        }
    }

    private IEnumerator WaitForPlayerClickAOE(AbilityCast abilityCast)
    {
        bool playerHasNotClicked = true;

        while (playerHasNotClicked)
        {
            if (abilityCast.abilityArea != null)
            {
                abilityCast.abilityArea.DisplayAOEArea();
            }
            if (!playerNeedsToReleaseMouseButton && Mouse.current.leftButton.wasReleasedThisFrame)
            {
                playerHasNotClicked = false;
                cursorChanger.ChangeCursorToDefaultGraphic();
                if (abilityCast.abilityArea != null)
                {
                    Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
                    RaycastHit hit;

                    if (Physics.Raycast(ray, out hit, groundLayerMask))
                    {
                        abilityCast.hit = hit;
                        PlayerSelectedGroundTargetLocationEvent?.Invoke(this, new InfoEventArgs<AbilityCast>(abilityCast));
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
