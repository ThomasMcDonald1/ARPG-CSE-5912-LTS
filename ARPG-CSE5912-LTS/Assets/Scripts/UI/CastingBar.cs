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

    void OnAbilityIsReadyToBeCast(object sender, InfoEventArgs<Ability> e)
    {
        castBarCanvas.SetActive(true);
        abilityIcon.sprite = e.info.icon;
        abilityNameText.text = e.info.name;
    }
}
