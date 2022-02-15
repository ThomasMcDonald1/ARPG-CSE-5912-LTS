using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CastTimerCastType : BaseCastType
{
    Coroutine castingRoutine;
    public static event EventHandler<InfoEventArgs<(Ability, RaycastHit, Character)>> AbilityCastTimeWasCompletedEvent;

    public override void WaitCastTime(Ability ability, RaycastHit hit, Character caster)
    {
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
        CompleteCast(ability, hit, caster);
        castingBar.castBarCanvas.SetActive(false);
    }

    private void FaceCasterToHitPoint(Character caster, RaycastHit hit)
    {
        caster.transform.LookAt(hit.point);
    }

    protected override void InstantiateVFX(Ability ability, RaycastHit hit, Character caster)
    {
        BaseCastType baseCastType = ability.GetComponent<BaseCastType>();
        GameObject spellcastVFXFromAbility = ability.spellcastVFXObj;
        GameObject explosionVFXFromAbility = ability.explosionVFXObj;
        GameObject instance = Instantiate(spellcastVFXFromAbility, ability.transform);

        instance.SetActive(true);
        CreateProjectile cProj = instance.GetComponent<CreateProjectile>();
        cProj.Time = baseCastType.castTime;
        StartCoroutine(WaitCastVFXTime(cProj, caster, hit, instance, ability, explosionVFXFromAbility));

        if (explosionVFXFromAbility != null)
        {
            GameObject explosionInstance = Instantiate(explosionVFXFromAbility, ability.transform);
            explosionInstance.transform.position = hit.point;
            SpecifyAbilityArea aa = ability.GetComponent<SpecifyAbilityArea>();
            if (aa != null)
            {
                explosionInstance.transform.localScale = new Vector3(aa.aoeRadius * 2, 2f, aa.aoeRadius * 2);
            }
            ParticleSystem pS = explosionInstance.GetComponent<ParticleSystem>();
            ParticleSystem.MainModule mainPS = pS.main;
            pS.Stop();
            StartCoroutine(WaitExplosionVFXTime(explosionInstance, mainPS, pS, baseCastType.castTime));
        }
    }

    public override void StopCasting()
    {
        if (castingRoutine != null)
        {
            StopCoroutine(castingRoutine);
            castingBar.castBarCanvas.SetActive(false);
            castingRoutine = null;
        }
    }

    protected override void CompleteCast(Ability ability, RaycastHit hit, Character caster)
    {
        AbilityCastTimeWasCompletedEvent?.Invoke(this, new InfoEventArgs<(Ability, RaycastHit, Character)>((ability, hit, caster)));
    }

    private IEnumerator WaitCastVFXTime(CreateProjectile cProj, Character caster, RaycastHit hit, GameObject instance, Ability ability, GameObject explosion)
    {
        Vector3 casterPos = caster.transform.position;
        Vector3 casterDir = caster.transform.forward;
        Quaternion casterRot = caster.transform.rotation;
        float spawnDistance = 1;
        Vector3 spawnPos = casterPos + casterDir * spawnDistance;
        instance.transform.position = spawnPos;
        yield return new WaitForSeconds(cProj.Time);
        if (explosion == null)
        {
            cProj.Create(spawnPos, casterRot, hit);
        }
        Destroy(instance);
    }

    private IEnumerator WaitExplosionVFXTime(GameObject instance, ParticleSystem.MainModule mainPS, ParticleSystem pS, float castTime)
    {
        yield return new WaitForSeconds(castTime);
        pS.Play();
        yield return new WaitForSeconds(mainPS.duration);
        Destroy(instance);
    }
}
