using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CastTimerCastType : BaseCastType
{
    Coroutine castingRoutine;

    public override void WaitCastTime(Ability ability)
    {
        castingRoutine = StartCoroutine(CastTimeCoroutine(ability));
    }

    private IEnumerator CastTimeCoroutine(Ability ability)
    {
        BaseCastType baseCastType = ability.GetComponent<BaseCastType>();
        //float castTimer = 0;
        //while (castTimer < baseCastType.castTime)
        //{

        //}
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
        //TODO: Fire event that cast wasn't canceled aka cast should go off
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
}
