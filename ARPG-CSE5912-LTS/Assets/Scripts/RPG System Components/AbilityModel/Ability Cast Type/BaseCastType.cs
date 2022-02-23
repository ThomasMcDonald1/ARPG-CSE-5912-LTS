using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ARPG.Movement;

public abstract class BaseCastType : MonoBehaviour
{
    GameplayStateController gameplayStateController;
    [HideInInspector] public CastingBar castingBar;
    public float castTime;

    public abstract void WaitCastTime(AbilityCast abilityCast);
    protected abstract void InstantiateVFX(AbilityCast abilityCast);
    public abstract void StopCasting();
    protected abstract void CompleteCast(AbilityCast abilityCast);


    private void Awake()
    {
        gameplayStateController = GetComponentInParent<GameplayStateController>();
        castingBar = gameplayStateController.castingBar;
        castingBar.castBarCanvas.SetActive(false);
    }

    private void OnEnable()
    {
        Character.AbilityIsReadyToBeCastEvent += OnAbilityIsReadyToBeCast;
        Character.CharacterAbilityIsReadyToBeCastEvent += OnAbilityIsReadyToBeCast;
        MovementHandler.PlayerBeganMovingEvent += OnPlayerBeganMoving;
    }

    private void OnPlayerBeganMoving(object sender, InfoEventArgs<int> e)
    {
        StopCasting();
    }

    void OnAbilityIsReadyToBeCast(object sender, InfoEventArgs<AbilityCast> e)
    {
        WaitCastTime(e.info);
    }
}