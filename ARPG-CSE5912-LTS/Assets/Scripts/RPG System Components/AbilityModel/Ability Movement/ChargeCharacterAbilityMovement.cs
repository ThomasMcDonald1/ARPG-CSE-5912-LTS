using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChargeCharacterAbilityMovement : BaseAbilityMovement
{
    private const float CHARGE_SPEED = 2.5f;

    public override void QueueSpecialMovement(AbilityCast abilityCast, List<Character> targets)
    {
        float dist = Vector3.Distance(abilityCast.hit.point, abilityCast.caster.transform.position) - 1.2f;
        NavMeshAgent agent = abilityCast.caster.GetComponent<NavMeshAgent>();
        agent.isStopped = true;
        StartCoroutine(ChargeToTarget(agent, abilityCast, dist, targets));
    }

    private IEnumerator ChargeToTarget(NavMeshAgent agent, AbilityCast abilityCast, float dist, List<Character> targets)
    {
        Debug.Log("Charging");
        abilityCast.caster.transform.LookAt(abilityCast.hit.point);
        while (Vector3.Distance(abilityCast.hit.point, abilityCast.caster.transform.position) > 3f)
        {
            abilityCast.caster.transform.position = Vector3.MoveTowards(abilityCast.caster.transform.position, abilityCast.hit.point, dist * Time.deltaTime * CHARGE_SPEED);
            yield return null;
        }
        agent.isStopped = false;
        agent.destination = abilityCast.hit.point;
        CompleteSpecialMovement(abilityCast, targets);
    }
}
