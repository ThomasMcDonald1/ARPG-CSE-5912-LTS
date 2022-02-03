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

        /*
         * TODO:
         * 1) castType.castTime is the cast time
         * 2) enable the castbar canvas
         * 3) make the spell icon be the ability's icon
         * 4) keep getting this castTime as it decrements from within a function in the CastType concrete classes
         * 5) make the fill go down as the castTime goes down
         * 6) make the cast timer text update the whole time
         * 7) when it reaches 0 or below, disable the castbar canvas
         */
    }
}
