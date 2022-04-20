using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayedDamageAbilityEffect : DamageAbilityEffect
{
    private void OnEnable()
    {
        ChargeCharacterAbilityEffect.ChargeCharacterDelayedDamageReadyEvent += OnDelayedDamageReady;
        ChargeGroundAbilityEffect.ChargeGroundDelayedDamageReadyEvent += OnDelayedDamageReady;
        LeapAbilityEffect.LeapDelayedDamageReadyEvent += OnDelayedDamageReady;
        PullAbilityEffect.PullDelayedDamageReadyEvent += OnDelayedDamageReady;
    }

    protected override int OnApply(Character target, AbilityCast abilityCast)
    {
        return 0;
    }

    private void OnDelayedDamageReady(object sender, InfoEventArgs<(AbilityCast, Character)> e)
    {
        if (this!=null && GetComponentInParent<Ability>() == e.info.Item1.ability)
        {
            if (effectVFXObj != null)
                InstantiateEffectVFX(e.info.Item1, e.info.Item2);
            DealDamage(e.info.Item2, e.info.Item1);
        }
    }
}
