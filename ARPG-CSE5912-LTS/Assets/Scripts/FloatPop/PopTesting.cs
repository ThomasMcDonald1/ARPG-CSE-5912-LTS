using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopTesting : MonoBehaviour
{
    
    [SerializeField] private Transform Pop;

    public void CreatePop(Character target, int Amount, bool isCrit, bool isDamage)
    {
        PopUp PopUp;
        Transform PopUpTransform = Instantiate(Pop, Vector3.zero, Quaternion.identity);
        PopUpTransform.SetParent(target.transform.Find("EnemyCanvas"), false);
        PopUpTransform.position -= new Vector3(0f, 3f, 0f);
        PopUp = PopUpTransform.GetComponent<PopUp>();
        PopUp.Setup(Amount, isCrit, isDamage);

    }
    private void OnEnable()
    {
        DamageAbilityEffect.AbilityDamageReceivedEvent += OnDamagePop;
        //A heal comes here
    }

    private void OnDisable()
    {
        DamageAbilityEffect.AbilityDamageReceivedEvent -= OnDamagePop;
        //A heal comes here
    }

    public void OnDamagePop(object sender, InfoEventArgs<(Character, int, bool)> e)
    {
        (Character damageTarget, int damageAmount, bool isCrit) = e.info;
        CreatePop(damageTarget, damageAmount, isCrit, true);
    }
    //A heal comes here

}
