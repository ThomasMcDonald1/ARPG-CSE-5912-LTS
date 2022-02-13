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
        GameObject spellcastVFX = null;
        for (int i = 0; i < caster.spellVFXHolderObj.transform.childCount; i++)
        {
            Transform child = caster.spellVFXHolderObj.transform.GetChild(i);
            if (child.name == spellcastVFXFromAbility.name)
            {
                spellcastVFX = child.gameObject;
            }
        }
        if (spellcastVFX != null)
        {
            CreateProjectile cProj = spellcastVFX.GetComponent<CreateProjectile>();
            cProj.Time = baseCastType.castTime;
            StartCoroutine(WaitCastVFXTime(cProj, caster, spellcastVFX, hit));
        }
    }

    private Vector3 DetermineVelocity(Rigidbody proj, RaycastHit hit)
    {
        Character character = GetComponentInParent<Character>();
        Vector3 casterPos = character.GetComponentInChildren<Animator>().transform.position;
        float height = 5;
        float gravity = -18;

        float displacementY = hit.point.y - proj.transform.position.y;
        Vector3 displacementXZ = new Vector3(hit.point.x - casterPos.x, 0, hit.point.z - casterPos.z);

        Vector3 velocityY = Vector3.up * Mathf.Sqrt(-2 * gravity * height);
        Vector3 velocityXZ = displacementXZ / (Mathf.Sqrt(-2 * height / gravity) + Mathf.Sqrt(2 * (displacementY - height) / gravity));

        return velocityXZ + velocityY;
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

    private IEnumerator WaitCastVFXTime(CreateProjectile cProj, Character caster, GameObject spellcastVFX, RaycastHit hit)
    {
        Vector3 casterPos = caster.transform.position;
        Vector3 casterDir = caster.transform.forward;
        Quaternion casterRot = caster.transform.rotation;
        float spawnDistance = 1;
        Vector3 spawnPos = casterPos + casterDir * spawnDistance;
        spellcastVFX.transform.position = spawnPos;
        spellcastVFX.SetActive(true);
        yield return new WaitForSeconds(cProj.Time);
        Projectile projectile = cProj.Create(caster, spawnPos, casterRot);
        projectile.rb.velocity = DetermineVelocity(projectile.rb, hit);
        spellcastVFX.SetActive(false);
    }
}
