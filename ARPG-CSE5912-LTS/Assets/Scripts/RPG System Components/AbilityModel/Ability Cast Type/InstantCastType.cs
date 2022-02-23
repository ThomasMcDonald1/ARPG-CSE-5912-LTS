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

    public static event EventHandler<InfoEventArgs<AbilityCast>> AbilityInstantCastWasCompletedEvent;

    public override void WaitCastTime(AbilityCast abilityCast)
    {
        CompleteCast(abilityCast);
    }

    public override void StopCasting()
    {
       
    }

    protected override void CompleteCast(AbilityCast abilityCast)
    {
       AbilityInstantCastWasCompletedEvent?.Invoke(this, new InfoEventArgs<AbilityCast>(abilityCast));
    }

    protected override void InstantiateVFX(AbilityCast abilityCast)
    {
        
    }
}
