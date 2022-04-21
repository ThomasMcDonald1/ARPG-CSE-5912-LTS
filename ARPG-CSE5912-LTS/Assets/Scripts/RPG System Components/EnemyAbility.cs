using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ARPG.Combat;

public class EnemyAbility
{
    public Ability abilityAssigned;
    Enemy enemy;
    BaseAbilityCost abilityCost;
    public float cooldownTimer;
    public bool abilityOnCooldown = false;

    public float GetCurrentTime()
    {
        return cooldownTimer;
    }

    public void DecrementCooldownTimer()
    {
        cooldownTimer -= Time.deltaTime;
    }
}
