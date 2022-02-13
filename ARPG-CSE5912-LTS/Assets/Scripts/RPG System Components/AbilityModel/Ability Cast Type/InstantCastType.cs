using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantCastType : BaseCastType
{
    //Character character;

    //private void Awake()
    //{
    //    character = GetComponentInParent<Character>();
    //}

    public static event EventHandler<InfoEventArgs<(Ability, RaycastHit, Character)>> AbilityInstantCastWasCompletedEvent;

    public override void WaitCastTime(Ability ability, RaycastHit hit, Character character)
    {
        CompleteCast(ability, hit, character);
    }

    public override void StopCasting()
    {
       
    }

    protected override void CompleteCast(Ability ability, RaycastHit hit, Character caster)
    {
       AbilityInstantCastWasCompletedEvent?.Invoke(this, new InfoEventArgs<(Ability, RaycastHit, Character)>((ability, hit, caster)));
    }

    protected override void InstantiateVFX(Ability ability, RaycastHit hit, Character caster)
    {
        
    }
}
