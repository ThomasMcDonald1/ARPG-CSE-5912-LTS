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

    public abstract void WaitCastTime(Ability ability, RaycastHit hit, Character caster);
    protected abstract void InstantiateVFX(Ability ability, RaycastHit hit, Character caster);
    public abstract void StopCasting();
    protected abstract void CompleteCast(Ability ability, RaycastHit hit, Character caster);


    private void Awake()
    {
        gameplayStateController = GetComponentInParent<GameplayStateController>();
        castingBar = gameplayStateController.castingBar;
        castingBar.castBarCanvas.SetActive(false);
    }

    private void OnEnable()
    {
        PlayerAbilityController.AbilityIsReadyToBeCastEvent += OnAbilityIsReadyToBeCast;
        MovementHandler.PlayerBeganMovingEvent += OnPlayerBeganMoving;
    }

    private void OnPlayerBeganMoving(object sender, InfoEventArgs<int> e)
    {
        StopCasting();
    }

    void OnAbilityIsReadyToBeCast(object sender, InfoEventArgs<(Ability, RaycastHit, Character)> e)
    {
        WaitCastTime(e.info.Item1, e.info.Item2, e.info.Item3);
    }
}