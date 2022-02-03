using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class BaseCastType : MonoBehaviour
{
    GameplayStateController gameplayStateController;
    [HideInInspector] public CastingBar castingBar;
    public float castTime;

    public abstract void WaitCastTime(Ability ability);
    public abstract void StopCasting();

    public static event EventHandler<InfoEventArgs<Ability>> AbilityCastTimeWasCompletedEvent;

    private void Awake()
    {
        gameplayStateController = GetComponentInParent<GameplayStateController>();
        castingBar = gameplayStateController.castingBar;
        castingBar.castBarCanvas.SetActive(false);
    }

    private void OnEnable()
    {
        Character.AbilityIsReadyToBeCastEvent += OnAbilityIsReadyToBeCast;
        Player.PlayerBeganMovingEvent += OnPlayerBeganMoving;
    }

    private void OnPlayerBeganMoving(object sender, InfoEventArgs<int> e)
    {
        StopCasting();
    }

    void OnAbilityIsReadyToBeCast(object sender, InfoEventArgs<Ability> e)
    {
        WaitCastTime(e.info);
    }

    public void CompleteCast(Ability ability)
    {
        AbilityCastTimeWasCompletedEvent?.Invoke(this, new InfoEventArgs<Ability>(ability));
    }

}
