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
    public static event EventHandler<InfoEventArgs<(Ability, RaycastHit, Character)>> AbilityCastTimeWasCompletedEvent;

    public override void WaitCastTime(Ability ability, RaycastHit hit, Character caster)
    {
        if (castingRoutine == null)
            castingRoutine = StartCoroutine(CastTimeCoroutine(ability, hit, caster));
    }

    private IEnumerator CastTimeCoroutine(Ability ability, RaycastHit hit, Character caster)
    {
        BaseCastType baseCastType = ability.GetComponent<BaseCastType>();
        FaceCasterToHitPoint(caster, hit);
        InstantiateVFX(ability, hit, caster);
        float displayTime = baseCastType.castTime;
        float rate = 1.0f / baseCastType.castTime;
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
        CompleteCast(ability, hit, caster);
    }

    private void FaceCasterToHitPoint(Character caster, RaycastHit hit)
    {
        caster.transform.LookAt(hit.point);
    }

    protected override void InstantiateVFX(Ability ability, RaycastHit hit, Character caster)
    {
        BaseCastType baseCastType = ability.GetComponent<BaseCastType>();
        GameObject spellcastVFXFromAbility = ability.spellcastVFXObj;
        GameObject effectVFXFromAbility = ability.effectVFXObj;
        castingVFXInstance = Instantiate(spellcastVFXFromAbility, ability.transform);
        castingVFXInstance.SetActive(true);
        vfxRoutine = StartCoroutine(WaitCastVFXTime(caster, hit, castingVFXInstance, effectVFXFromAbility, baseCastType.castTime, ability));
    }

    public override void StopCasting()
    {
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

    protected override void CompleteCast(Ability ability, RaycastHit hit, Character caster)
    {
        AbilityCastTimeWasCompletedEvent?.Invoke(this, new InfoEventArgs<(Ability, RaycastHit, Character)>((ability, hit, caster)));
    }

    private IEnumerator WaitCastVFXTime(Character caster, RaycastHit hit, GameObject instance, GameObject effectVFX, float castTime, Ability ability)
    {
        CreateProjectile cProj = instance.GetComponent<CreateProjectile>();
        Vector3 casterPos = caster.transform.position;
        Vector3 casterDir = caster.transform.forward;
        Quaternion casterRot = caster.transform.rotation;
        float spawnDistance = 1;
        Vector3 spawnPos = casterPos + casterDir * spawnDistance;
        instance.transform.position = spawnPos;
        yield return new WaitForSeconds(castTime);
        castingRoutine = null;
        if (cProj != null && effectVFX == null)
        {
            cProj.Time = castTime;
            cProj.Create(spawnPos, casterRot, hit);
            Destroy(instance);
        }
        else
        {
            Destroy(instance);
            GameObject explosionInstance = Instantiate(effectVFX);
            ParticleSystem pS = explosionInstance.GetComponent<ParticleSystem>();
            explosionInstance.transform.position = hit.point;
            SpecifyAbilityArea aa = ability.GetComponent<SpecifyAbilityArea>();
            if (aa != null)
            {
                explosionInstance.transform.localScale = new Vector3(aa.aoeRadius * 2, 2f, aa.aoeRadius * 2);
            }
            yield return new WaitForSeconds(pS.main.duration);
            Destroy(explosionInstance);
        }
    }
}
