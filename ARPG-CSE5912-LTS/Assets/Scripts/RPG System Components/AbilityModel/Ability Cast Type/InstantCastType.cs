using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantCastType : BaseCastType
{
    Coroutine instantCastRoutine;
    public static event EventHandler<InfoEventArgs<(AbilityCast, Ability)>> AbilityInstantCastWasCompletedEvent;
    
    public override Type GetCastType()
    {
        return typeof(InstantCastType);
    }

    public override void WaitCastTime(AbilityCast abilityCast)
    {
        if (instantCastRoutine == null)
        {
            instantCastRoutine = StartCoroutine(InstantCastCoroutine(abilityCast));
        }
    }

    public override void StopCasting()
    {
       
    }

    protected override void CompleteCast(AbilityCast abilityCast, Ability ability)
    {
        Debug.Log("Cast was completed from Instant Cast Type");
        AbilityInstantCastWasCompletedEvent?.Invoke(this, new InfoEventArgs<(AbilityCast, Ability)>((abilityCast, ability)));
    }

    protected override void InstantiateSpellcastVFX(AbilityCast abilityCast)
    {

    }

    private IEnumerator InstantCastCoroutine(AbilityCast abilityCast)
    {
        yield return new WaitForEndOfFrame();
        CompleteCast(abilityCast, GetComponent<Ability>());
        instantCastRoutine = null;
    }
}
