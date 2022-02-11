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
}
