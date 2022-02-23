using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopTesting : MonoBehaviour
{
    
    [SerializeField] private Transform DamagePop;

    public void CreateDamagePop(Character target, int damageAmount, bool isCrit)
    {
        DamagePopUp damagePopUp;
        Transform damagePopUpTransform = Instantiate(DamagePop, Vector3.zero, Quaternion.identity);
        damagePopUpTransform.SetParent(target.transform.Find("EnemyCanvas"), false);
        damagePopUpTransform.position -= new Vector3(0f, 3f, 0f);
        damagePopUp = damagePopUpTransform.GetComponent<DamagePopUp>();
        damagePopUp.Setup(damageAmount);

    }
    private void OnEnable()
    {
        DamageAbilityEffect.AbilityDamageReceivedEvent += OnDamagePop;
    }

    private void OnDisable()
    {
        DamageAbilityEffect.AbilityDamageReceivedEvent -= OnDamagePop;
    }

    public void OnDamagePop(object sender, InfoEventArgs<(Character, int, bool)> e)
    {
        (Character damageTarget, int damageAmount, bool isCrit) = e.info;
        CreateDamagePop(damageTarget, damageAmount, isCrit);
    }

}
