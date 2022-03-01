using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayedDamageAbilityEffect : DamageAbilityEffect
{
    private void OnEnable()
    {
        ChargeCharacterAbilityEffect.ChargeCharacterDelayedDamageReadyEvent += OnChargeDelayedDamageReady;
        ChargeGroundAbilityEffect.ChargeGroundDelayedDamageReadyEvent += OnChargeDelayedDamageReady;
    }

    protected override int OnApply(Character target, AbilityCast abilityCast)
    {
        return 0;
    }

    private void OnChargeDelayedDamageReady(object sender, InfoEventArgs<(AbilityCast, Character)> e)
    {
        if (GetComponentInParent<Ability>() == e.info.Item1.ability)
        {
            if (effectVFXObj != null)
                InstantiateEffectVFX(e.info.Item1, e.info.Item2);
            DealDamage(e.info.Item2, e.info.Item1);
        }
    }
}
