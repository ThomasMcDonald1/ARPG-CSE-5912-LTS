using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChargeGroundAbilityEffect : BaseAbilityEffect
{
    private const float CHARGE_SPEED = 2.5f;
    public static event EventHandler<InfoEventArgs<(AbilityCast, Character)>> ChargeGroundDelayedDamageReadyEvent;

    protected override int OnApply(Character target, AbilityCast abilityCast)
    {
        float dist = Vector3.Distance(abilityCast.hit.point, abilityCast.caster.transform.position);
        NavMeshAgent agent = abilityCast.caster.GetComponent<NavMeshAgent>();
        agent.enabled = false;
        StartCoroutine(ChargeToLocation(agent, abilityCast, dist, target));
        return 0;
    }

    private IEnumerator ChargeToLocation(NavMeshAgent agent, AbilityCast abilityCast, float dist, Character target)
    {
        abilityCast.caster.transform.LookAt(abilityCast.hit.point);
        while (Vector3.Distance(abilityCast.hit.point, abilityCast.caster.transform.position) > 1.25f)
        {
            abilityCast.caster.transform.position = Vector3.MoveTowards(abilityCast.caster.transform.position, abilityCast.hit.point, dist * Time.deltaTime * CHARGE_SPEED);
            yield return null;
        }
        ChargeGroundDelayedDamageReadyEvent?.Invoke(this, new InfoEventArgs<(AbilityCast, Character)>((abilityCast, target)));
        agent.enabled = true;
    }
}
