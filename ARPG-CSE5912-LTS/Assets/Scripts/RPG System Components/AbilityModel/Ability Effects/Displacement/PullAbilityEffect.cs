using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PullAbilityEffect : BaseAbilityEffect
{
    private const float PULL_HEIGHT = 1f;

    public static event EventHandler<InfoEventArgs<(AbilityCast, Character)>> PullDelayedDamageReadyEvent;

    protected override int OnApply(Character target, AbilityCast abilityCast)
    {
        Rigidbody rb = target.GetComponent<Rigidbody>();
        NavMeshAgent agent = target.GetComponent<NavMeshAgent>();

        if (effectVFXObj != null)
            InstantiateEffectVFX(abilityCast, target);

        Vector3 origin = GetEffectOrigin(abilityCast, target);
        Vector3 displacement = origin - target.transform.position;
        displacement -= displacement.normalized;
        float gravity = -18f;
        Vector3 velocityY = Vector3.up * Mathf.Sqrt(-2 * gravity * PULL_HEIGHT);

        if (rb != null && agent != null)
        {
            rb.isKinematic = false;
            agent.enabled = false;
            rb.velocity = displacement + velocityY;
            StartCoroutine(WaitForLanding(rb, agent, abilityCast, target));
        }
        return 0;
    }

    private IEnumerator WaitForLanding(Rigidbody rb, NavMeshAgent agent, AbilityCast abilityCast, Character target)
    {
        Collider col = rb.gameObject.GetComponent<Collider>();
        bool isGrounded = false;
        yield return new WaitForSeconds(0.3f);
        while (!isGrounded)
        {
            if (Physics.Raycast(rb.transform.position, -Vector3.up, col.bounds.extents.y + 0.1f))
            {
                isGrounded = true;
            }
            yield return null;
        }
        agent.enabled = true;
        rb.isKinematic = true;
        PullDelayedDamageReadyEvent?.Invoke(this, new InfoEventArgs<(AbilityCast, Character)>((abilityCast, target)));
    }
}