using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CastingBar : MonoBehaviour
{
    public Slider castingBarSlider;
    public GameObject castBarCanvas;
    public TextMeshProUGUI castTimeText;

    [SerializeField] TextMeshProUGUI abilityNameText;
    [SerializeField] Image abilityIcon;

    private void Awake()
    {
        castBarCanvas.SetActive(false);
    }

    private void OnEnable()
    {
        Character.AbilityIsReadyToBeCastEvent += OnAbilityIsReadyToBeCast;
    }

    void OnAbilityIsReadyToBeCast(object sender, InfoEventArgs<AbilityCast> e)
    {
        if (e.info.castType.GetCastType() == typeof(CastTimerCastType))
        {
            castBarCanvas.SetActive(true);
            abilityIcon.sprite = e.info.ability.icon;
            abilityNameText.text = e.info.ability.name;
        }
    }
}