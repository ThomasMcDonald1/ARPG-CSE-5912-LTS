using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class BaseCastType : MonoBehaviour
{
    GameplayStateController gameplayStateController;
    public CastingBar castingBar;
    public float castTime;

    //TODO: Not sure how to go about doing this yet. Need to do different kinds of casts depending on what type of ability it is
    // 1) casts could be instant
    // 2) casts could require standing still as you wait for a cast timer to finish, then the full effect could happen at the end of the cast
    // 3) casts could be channeled, having a pulsating effect that takes place several times within the cast
    //For now, I have it set as an abstract class where the concrete classes that implement it will have to figure out how the ability should be cast
    public abstract void WaitCastTime(Ability ability);
    public abstract void StopCasting();

    private void Awake()
    {
        gameplayStateController = GetComponentInParent<GameplayStateController>();
        castingBar = gameplayStateController.castingBar;
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
}
