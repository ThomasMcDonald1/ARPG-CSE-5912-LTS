using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionButton : MonoBehaviour
{
    public Ability abilityAssigned;
    [SerializeField] GameObject iconObj;
    Image iconObjImage;
    [SerializeField] Player player;
    BaseAbilityCost abilityCost;

    protected void Awake()
    {
        iconObjImage = iconObj.GetComponent<Image>();
        iconObjImage.enabled = false;
    }

    public void SetIcon()
    {
        if (abilityAssigned != null)
        {
            iconObjImage.sprite = abilityAssigned.icon;
            iconObjImage.enabled = true;
        }
    }

    public void LockSlotFromCostDeficit()
    {
        if (abilityAssigned != null)
        {
            abilityCost = abilityAssigned.GetComponent<BaseAbilityCost>();
            bool abilityCanBePerformed = abilityCost.CheckCharacterHasResourceCostForCastingAbility(player);

            if (!abilityCanBePerformed)
            {
                iconObjImage.color = new Color32(106, 106, 106, 100);
            }
            else
            {
                iconObjImage.color = Color.white;
            }
        }
    }
}
