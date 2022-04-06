using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBossController : EnemyAbilityController
{
    GameObject PlayerTarget;
    protected override void Start()
    {
        base.Start();
        PlayerTarget = GameObject.FindWithTag("Player");
    }

    protected override void Update()
    {
        base.Update();
        UpdateAnimator();
    }

    private void UpdateAnimator()
    {
        Vector3 velocity = GetComponent<NavMeshAgent>().velocity;
        Vector3 localVelocity = transform.InverseTransformDirection(velocity);
        float speed = localVelocity.z;
        GetComponent<Animator>().SetFloat("ForwardSpeed", speed);
    }
}

