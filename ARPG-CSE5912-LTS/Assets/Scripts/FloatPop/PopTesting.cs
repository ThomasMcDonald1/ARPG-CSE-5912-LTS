using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopTesting : MonoBehaviour
{
    [SerializeField] private Transform Pop;
    public static readonly Color abilityDamageColor = Color.yellow;
    public static readonly Color basicAttackDamageColor = Color.white;
    public static readonly Color healingColor = new Color(0.1f, 0.8f, 0.4f, 1f); //green
    public static readonly Color missColor = new Color(0.8f, 0.8f, 0.8f, 1f); //light gray
    public static readonly Color fireColor = new Color(1f, 0.6f, 0f, 1f); //orange
    public static readonly Color iceColor = new Color(0f, 0.93f, 0.99f, 1f); //cyan
    public static readonly Color lightningColor = new Color(0.83f, 0.17f, 0.75f, 1f); //purple
    public static readonly Color poisonColor = new Color(0.75f, 1f, 0f, 1f); //yellow-green

    public void CreatePop(Character target, string text, bool isCrit, Color color)
    {
        PopUp PopUp;
        Transform PopUpTransform = Instantiate(Pop, Vector3.zero, Quaternion.identity);
        //Debug.Log(target);
        if (target is Player)
        {
            PopUpTransform.SetParent(target.transform.Find("PlayerCanvas"), false);
        }
        else
        {
            PopUpTransform.SetParent(target.transform.Find("EnemyCanvas"), false);
        }
        PopUpTransform.position -= new Vector3(0f, 2f, 0f);
        PopUp = PopUpTransform.GetComponent<PopUp>();
        PopUp.Setup(text, isCrit, color);

    }
    private void OnEnable()
    {
        DamageAbilityEffect.AbilityDamageReceivedEvent += OnDamagePop;
        HealingAbilityEffect.AbilityHealingReceivedEvent += OnHealingPop;
        BaseAbilityEffect.AbilityMissedTargetEvent += OnMissingPop;
        BasicAttackDamageAbilityEffect.BasicAttackDamageReceivedEvent += OnBasicDamagePop;
        BasicAttackDamageAbilityEffect.BasicAttackHealingReceivedEvent += OnHealingPop;
        OnHitElementalDamageAbilityEffect.AbilityOnHitDamageReceivedEvent += OnElementalDamagePop;
        BasicAttackDamageAbilityEffect.ThornsDamageReceivedEvent += OnDamagePop;
        BaseAbilityEffect.AbilityWasBlockedEvent += OnBlockedPop;
    }

    private void OnDisable()
    {
        DamageAbilityEffect.AbilityDamageReceivedEvent -= OnDamagePop;
        HealingAbilityEffect.AbilityHealingReceivedEvent -= OnHealingPop;
        BaseAbilityEffect.AbilityMissedTargetEvent -= OnMissingPop;
        BasicAttackDamageAbilityEffect.BasicAttackDamageReceivedEvent -= OnBasicDamagePop;
        BasicAttackDamageAbilityEffect.BasicAttackHealingReceivedEvent -= OnHealingPop;
        OnHitElementalDamageAbilityEffect.AbilityOnHitDamageReceivedEvent -= OnElementalDamagePop;
        BasicAttackDamageAbilityEffect.ThornsDamageReceivedEvent -= OnDamagePop;
        BaseAbilityEffect.AbilityWasBlockedEvent -= OnBlockedPop;
    }

    public void OnDamagePop(object sender, InfoEventArgs<(Character, int, bool)> e)
    {
        (Character damageTarget, int damageAmount, bool isCrit) = e.info;
        if (!(damageTarget is Player))
            CreatePop(damageTarget, damageAmount.ToString(), isCrit, abilityDamageColor);
    }

    public void OnHealingPop(object sender, InfoEventArgs<(Character, int, bool)> e)
    {
        (Character healingTarget, int healingAmount, bool isCrit) = e.info;
        if (healingTarget is Player)
            CreatePop(healingTarget, healingAmount.ToString(), isCrit, healingColor);
    }

    public void OnMissingPop(object sender, InfoEventArgs<Character> e)
    {
        Character missingTarget = e.info;
        if (!(missingTarget is Player))
            CreatePop(missingTarget, "Miss", false, missColor); 
    }

    public void OnBlockedPop(object sender, InfoEventArgs<Character> e)
    {
        Character blockChar = e.info;
        if (!(blockChar is Player))
            CreatePop(blockChar, "Blocked", false, missColor);
    }

    public void OnBasicDamagePop(object sender, InfoEventArgs<(Character, int, bool)> e)
    {
        (Character damageTarget, int damageAmount, bool isCrit) = e.info;
        if (!(damageTarget is Player))
            CreatePop(damageTarget, damageAmount.ToString(), isCrit, basicAttackDamageColor);
    }

    public void OnElementalDamagePop(object sender, InfoEventArgs<(Character, int, BaseAbilityEffectElement)> e)
    {
        (Character damageTarget, int damageAmount, BaseAbilityEffectElement element) = e.info;
        if (!(damageTarget is Player))
        {
            Color color;
            if (element is FireAbilityEffectElement)
                color = fireColor;
            else if (element is ColdAbilityEffectElement)
                color = iceColor;
            else if (element is LightningAbilityEffectElement)
                color = lightningColor;
            else
                color = poisonColor;

            CreatePop(damageTarget, damageAmount.ToString(), false, color);
        }
    }
}
