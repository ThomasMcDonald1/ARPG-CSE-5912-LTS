using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class KnockbackAbilityEffect : BaseAbilityEffect
{
    public float knockbackXZDisplacement;
    public float knockbackHeight;

    protected override int OnApply(Character target, AbilityCast abilityCast)
    {
        Rigidbody rb = target.GetComponent<Rigidbody>();
        NavMeshAgent agent = target.GetComponent<NavMeshAgent>();

        if (effectVFXObj != null)
            InstantiateEffectVFX(abilityCast, target);

        Vector3 origin = GetEffectOrigin(abilityCast, target);
        Vector3 dir = (target.transform.position - origin).normalized;
        Vector3 destination = dir * knockbackXZDisplacement;
        float gravity = -18f;
        Vector3 velocityY = Vector3.up * Mathf.Sqrt(-2 * gravity * knockbackHeight);

        if (rb != null && agent != null)
        {
            rb.isKinematic = false;
            //agent.updatePosition = false;
            //agent.updateRotation = false;
            agent.enabled = false;
            Debug.Log("Enemy is kinematic? " + rb.isKinematic);
            rb.velocity = destination * (5 / knockbackXZDisplacement) + velocityY;
            StartCoroutine(WaitForLanding(rb, agent));
        }

        return 0;
    }

    private IEnumerator WaitForLanding(Rigidbody rb, NavMeshAgent agent)
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
        //agent.updatePosition = true;
        //agent.updateRotation = true;
        agent.enabled = true;
        rb.isKinematic = true;
        Debug.Log("Enemy is kinematic? " + rb.isKinematic);
    }
}
