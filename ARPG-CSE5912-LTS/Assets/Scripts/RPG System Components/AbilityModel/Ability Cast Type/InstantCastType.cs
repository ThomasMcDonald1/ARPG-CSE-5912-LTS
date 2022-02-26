using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantCastType : BaseCastType
{
    public static event EventHandler<InfoEventArgs<AbilityCast>> AbilityInstantCastWasCompletedEvent;

    public override Type GetCastType()
    {
        return typeof(InstantCastType);
    }

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

    protected override void InstantiateSpellcastVFX(AbilityCast abilityCast)
    {

    }
}
