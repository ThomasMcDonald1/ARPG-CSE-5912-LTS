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
    [HideInInspector] public float reducedCastTime;

    public abstract void WaitCastTime(AbilityCast abilityCast);
    protected abstract void InstantiateSpellcastVFX(AbilityCast abilityCast);
    public abstract void StopCasting();
    protected abstract void CompleteCast(AbilityCast abilityCast);
    public abstract Type GetCastType();
    private void Awake()
    {
        gameplayStateController = GetComponentInParent<GameplayStateController>();
        if (gameplayStateController == null)
        {
            gameplayStateController = FindObjectOfType<GameplayStateController>();
        }
        castingBar = gameplayStateController.castingBar;
        castingBar.castBarCanvas.SetActive(false);
    }

    public void GetReducedCastTime(AbilityCast abilityCast)
    {
        abilityCast.castType.reducedCastTime = abilityCast.castType.castTime - (abilityCast.castType.castTime * abilityCast.caster.stats[StatTypes.CastSpeed] * 0.01f);
    }

    private void OnEnable()
    {
        Character.AbilityIsReadyToBeCastEvent += OnAbilityIsReadyToBeCast;
        MovementHandler.PlayerBeganMovingEvent += OnPlayerBeganMoving; 
    }

    private void OnPlayerBeganMoving(object sender, InfoEventArgs<int> e)
    {
        StopCasting();
    }
    private void OnEnemyBeganMoving(object sender, InfoEventArgs<int> e)
    {
        StopCasting();
    }

    void OnAbilityIsReadyToBeCast(object sender, InfoEventArgs<AbilityCast> e)
    {
        //Debug.Log("Ability is ready to be cast event received");
        e.info.castType.WaitCastTime(e.info);
    }
}