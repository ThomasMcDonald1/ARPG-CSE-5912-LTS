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
        InstantiateVFX(abilityCast);
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
        GameObject effectVFX = abilityCast.ability.effectVFXObj;
        GameObject instance = Instantiate(effectVFX, abilityCast.ability.transform);
        StartCoroutine(ShowVFX(abilityCast, instance));
    }

    private IEnumerator ShowVFX(AbilityCast abilityCast, GameObject vfx)
    {
        Vector3 casterPos = abilityCast.caster.transform.position;
        Vector3 casterDir = abilityCast.caster.transform.forward;
        Quaternion casterRot = abilityCast.caster.transform.rotation;
        float spawnDistance = 1;
        Vector3 spawnPos = casterPos + casterDir * spawnDistance;
        vfx.transform.position = spawnPos;
        yield return new WaitForSeconds(1);
        Destroy(vfx);
    }
}
