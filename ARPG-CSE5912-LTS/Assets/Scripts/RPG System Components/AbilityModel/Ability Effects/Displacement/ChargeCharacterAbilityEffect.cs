using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChargeCharacterAbilityEffect : BaseAbilityEffect
{
    private const float CHARGE_SPEED = 2.5f;
    public static event EventHandler<InfoEventArgs<(AbilityCast, Character)>> ChargeCharacterDelayedDamageReadyEvent;
    protected override int OnApply(Character target, AbilityCast abilityCast)
    {
        float dist = Vector3.Distance(target.transform.position, abilityCast.caster.transform.position) - 1.2f;
        NavMeshAgent agent = abilityCast.caster.GetComponent<NavMeshAgent>();
        agent.enabled = false;
        StartCoroutine(ChargeToTarget(agent, abilityCast, target, dist));

        return 0;
    }

    public IEnumerator ChargeToTarget(NavMeshAgent agent, AbilityCast abilityCast, Character target, float dist)
    {   
        Debug.Log("Charging");
        abilityCast.caster.transform.LookAt(target.transform.position);
        while (Vector3.Distance(target.transform.position, abilityCast.caster.transform.position) > 1.25f)
        {
            abilityCast.caster.transform.position = Vector3.MoveTowards(abilityCast.caster.transform.position, target.transform.position, dist * Time.deltaTime * CHARGE_SPEED);
            yield return null;
        }
        agent.enabled = true;
        ChargeCharacterDelayedDamageReadyEvent?.Invoke(this, new InfoEventArgs<(AbilityCast, Character)>((abilityCast, target)));
    }
}
