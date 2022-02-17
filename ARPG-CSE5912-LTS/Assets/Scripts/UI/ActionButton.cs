using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ActionButton : MonoBehaviour
{
    public Ability abilityAssigned;
    [SerializeField] GameObject iconObj;
    Image iconObjImage;
    [SerializeField] Player player;
    BaseAbilityCost abilityCost;
    public Image cooldownFill;
    public TextMeshProUGUI cooldownText;
    public float cooldownTimer;
    public bool abilityInSlotOnCooldown = false;

    protected void Awake()
    {
        iconObjImage = iconObj.GetComponent<Image>();
        iconObjImage.enabled = false;
        cooldownText.gameObject.SetActive(false);
        cooldownFill.fillAmount = 0.0f;
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

    public float GetCurrentTime()
    {
        return cooldownTimer;
    }

    public void DecrementCooldownTimer()
    {
        cooldownTimer -= Time.deltaTime;
    }
}
