using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//to store everything about abilities being cast (besides the effects and effect-specific scripts,
//which will be looped through later, and besides the VFX)
public class AbilityCast
{
    public Character caster;
    public Ability ability;
    public BaseAbilityArea abilityArea;
    public BaseCastType castType;
    public AbilityCooldown abilityCooldown;
    public BaseAbilityCost abilityCost;
    public BaseHitRate hitRateType;
    public BaseAbilityPower abilityPower;
    public BaseAbilityRange abilityRange;

    public bool abilityRequiresCursorSelection = false;
    public bool requiresCharacterUnderCursor = false;
    public bool abilityDoesNotTargetCaster = false;

    public RaycastHit hit;

    public AbilityCast(Ability ability)
    {
        this.ability = ability;
        abilityArea = this.ability.GetComponent<BaseAbilityArea>();
        castType = this.ability.GetComponent<BaseCastType>();
        abilityCooldown = this.ability.GetComponent<AbilityCooldown>();
        abilityCost = this.ability.GetComponent<BaseAbilityCost>();
        hitRateType = this.ability.GetComponent<BaseHitRate>();
        abilityPower = this.ability.GetComponent<BaseAbilityPower>();
        abilityRange = this.ability.GetComponent<BaseAbilityRange>();
        if (this.ability.GetComponent<AbilityRequiresCursorSelection>() != null)
            abilityRequiresCursorSelection = true;
        if (this.ability.GetComponent<AbilityRequiresCharacterUnderCursor>() != null)
            requiresCharacterUnderCursor = true;
        if (this.ability.GetComponent<AbilityDoesNotTargetCaster>() != null)
            abilityDoesNotTargetCaster = true;
    }
}
