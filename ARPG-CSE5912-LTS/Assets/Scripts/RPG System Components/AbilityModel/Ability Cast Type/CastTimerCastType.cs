using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CastTimerCastType : BaseCastType
{
    Coroutine castingRoutine;
    Coroutine vfxRoutine;
    GameObject castingVFXInstance;
    public static event EventHandler<InfoEventArgs<Ability>> AbilityBeganBeingCastEvent;
    public static event EventHandler<InfoEventArgs<int>> AbilityCastWasCancelledEvent;
    public static event EventHandler<InfoEventArgs<AbilityCast>> AbilityCastTimeWasCompletedEvent;

    public override void WaitCastTime(AbilityCast abilityCast)
    {
        if (castingRoutine == null)
            castingRoutine = StartCoroutine(CastTimeCoroutine(abilityCast));
    }

    protected override void InstantiateVFX(AbilityCast abilityCast)
    {
        GameObject spellcastVFXFromAbility = abilityCast.ability.spellcastVFXObj;
        GameObject effectVFXFromAbility = abilityCast.ability.effectVFXObj;
        castingVFXInstance = Instantiate(spellcastVFXFromAbility, abilityCast.ability.transform);
        castingVFXInstance.SetActive(true);
        vfxRoutine = StartCoroutine(WaitCastVFXTime(abilityCast, castingVFXInstance, effectVFXFromAbility));
    }
    protected override void CompleteCast(AbilityCast abilityCast)
    {
        Debug.Log("Completing cast");
        AbilityCastTimeWasCompletedEvent?.Invoke(this, new InfoEventArgs<AbilityCast>(abilityCast));
    }

    public override void StopCasting()
    {
        AbilityCastWasCancelledEvent?.Invoke(this, new InfoEventArgs<int>(0));
        if (castingRoutine != null)
        {
            StopCoroutine(castingRoutine);
            castingBar.castBarCanvas.SetActive(false);
            castingRoutine = null;
        }
        if (vfxRoutine != null)
        {
            StopCoroutine(vfxRoutine);
            vfxRoutine = null;
            Destroy(castingVFXInstance);
        }
    }

    private void FaceCasterToHitPoint(Character caster, RaycastHit hit)
    {
        caster.transform.LookAt(hit.point);
    }

    private IEnumerator CastTimeCoroutine(AbilityCast abilityCast)
    {
        AbilityBeganBeingCastEvent?.Invoke(this, new InfoEventArgs<Ability>(abilityCast.ability));
        FaceCasterToHitPoint(abilityCast.caster, abilityCast.hit);
        InstantiateVFX(abilityCast);
        float displayTime = abilityCast.castType.castTime;
        float rate = 1.0f / abilityCast.castType.castTime;
        float progress = 0.0f;
        castingBar.castingBarSlider.value = 0f;
        while (progress < 1.0f)
        {
            castingBar.castTimeText.text = displayTime.ToString("0.00");
            castingBar.castingBarSlider.value = Mathf.Lerp(0, 1, progress);

            displayTime -= rate * Time.deltaTime;
            progress += rate * Time.deltaTime;

            yield return null;
        }
        castingBar.castBarCanvas.SetActive(false);
        CompleteCast(abilityCast);
    }

    private IEnumerator WaitCastVFXTime(AbilityCast abilityCast, GameObject instance, GameObject effectVFX)
    {
        CreateProjectile cProj = instance.GetComponent<CreateProjectile>();
        Vector3 casterPos = abilityCast.caster.transform.position;
        Vector3 casterDir = abilityCast.caster.transform.forward;
        Quaternion casterRot = abilityCast.caster.transform.rotation;
        float spawnDistance = 1;
        Vector3 spawnPos = casterPos + casterDir * spawnDistance;
        instance.transform.position = spawnPos;
        yield return new WaitForSeconds(castTime);
        castingRoutine = null;
        if (cProj != null && effectVFX == null)
        {
            cProj.Time = castTime;
            cProj.Create(spawnPos, casterRot, abilityCast.hit);
            Destroy(instance);
        }
        else
        {
            Destroy(instance);
            GameObject explosionInstance = Instantiate(effectVFX);
            ParticleSystem pS = explosionInstance.GetComponent<ParticleSystem>();
            explosionInstance.transform.position = abilityCast.hit.point;
            SpecifyAbilityArea aa = abilityCast.ability.GetComponent<SpecifyAbilityArea>();
            if (aa != null)
            {
                explosionInstance.transform.localScale = new Vector3(aa.aoeRadius * 2, 2f, aa.aoeRadius * 2);
            }
            yield return new WaitForSeconds(pS.main.duration);
            Destroy(explosionInstance);
        }
    }
}
