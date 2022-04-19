using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopTesting : MonoBehaviour
{
    private string popType;
    [SerializeField] private Transform Pop;

    public void CreatePop(Character target, int Amount, bool isCrit, string popType, bool isBasic)
    {
        PopUp PopUp;
        Transform PopUpTransform = Instantiate(Pop, Vector3.zero, Quaternion.identity);
        Debug.Log(target);
        if (target.GetComponent<Player>() != null)
        {
            PopUpTransform.SetParent(target.transform.Find("PlayerCanvas"), false);
        }
        else
        {
            PopUpTransform.SetParent(target.transform.Find("EnemyCanvas"), false);
        }

        PopUpTransform.position -= new Vector3(0f, 2f, 0f);
        PopUp = PopUpTransform.GetComponent<PopUp>();
        PopUp.Setup(Amount, isCrit, popType, isBasic);

    }
    private void OnEnable()
    {
        DamageAbilityEffect.AbilityDamageReceivedEvent += OnDamagePop;
        HealingAbilityEffect.AbilityHealingReceivedEvent += OnHealingPop;
        BaseAbilityEffect.AbilityMissedTargetEvent += OnMissingPop;
        BasicAttackDamageAbilityEffect.BasicAttackDamageReceivedEvent += OnBasicDamagePop;
        BasicAttackDamageAbilityEffect.BasicAttackHealingReceivedEvent += OnBasicHealingPop;
    }

    private void OnDisable()
    {
        DamageAbilityEffect.AbilityDamageReceivedEvent -= OnDamagePop;
        HealingAbilityEffect.AbilityHealingReceivedEvent -= OnHealingPop;
        BaseAbilityEffect.AbilityMissedTargetEvent -= OnMissingPop;
        BasicAttackDamageAbilityEffect.BasicAttackDamageReceivedEvent -= OnBasicDamagePop;
        BasicAttackDamageAbilityEffect.BasicAttackHealingReceivedEvent -= OnBasicHealingPop;
    }

    public void OnDamagePop(object sender, InfoEventArgs<(Character, int, bool)> e)
    {
        (Character damageTarget, int damageAmount, bool isCrit) = e.info;
        CreatePop(damageTarget, damageAmount, isCrit, "damage", false);
    }

    public void OnHealingPop(object sender, InfoEventArgs<(Character, int, bool)> e)
    {
        (Character healingTarget, int healingAmount, bool isCrit) = e.info;
        CreatePop(healingTarget, healingAmount, isCrit, "healing", false);
    }

    public void OnMissingPop(object sender, InfoEventArgs<Character> e)
    {
        Character missingTarget = e.info;
        CreatePop(missingTarget, 0, true, "missing", false); //no matter the middle two things
    }

    public void OnBasicDamagePop(object sender, InfoEventArgs<(Character, int, bool)> e)
    {
        (Character damageTarget, int damageAmount, bool isCrit) = e.info;
        CreatePop(damageTarget, damageAmount, isCrit, "damage", true);
    }

    public void OnBasicHealingPop(object sender, InfoEventArgs<(Character, int, bool)> e)
    {
        (Character healingTarget, int healingAmount, bool isCrit) = e.info;
        CreatePop(healingTarget, healingAmount, isCrit, "healing", true);
    }

}
