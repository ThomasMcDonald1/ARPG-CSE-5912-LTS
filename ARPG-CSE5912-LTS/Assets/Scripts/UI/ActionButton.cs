using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionButton : MonoBehaviour
{
    public Ability abilityAssigned;
    [SerializeField] GameObject iconObj;
    Image iconObjImage;

    private void Awake()
    {
        iconObjImage = iconObj.GetComponent<Image>();
    }

    public void SetIcon()
    {
        if (abilityAssigned != null)
            iconObjImage.sprite = abilityAssigned.icon;
    }
}
