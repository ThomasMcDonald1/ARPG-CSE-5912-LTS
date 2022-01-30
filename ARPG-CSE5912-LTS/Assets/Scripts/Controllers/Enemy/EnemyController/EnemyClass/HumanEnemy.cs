using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HumanEnemy : EnemyClass
{
   

    public override void Attack()
    {

    }

    public override void CancelAttack()
    {

    }

    public NavMeshAgent enemy;
    public override float currentEnemyHealth { get; set; }
    public override float maxEnemyHealth { get; set; }

    public Transform playerTrans;
    public float currentPlayerHealth;
    public float currentPlayEnergy;



    private float realDistance;
    private Vector3 realDirection;
    public override float range { get; set; }
    public override float bodyRange { get; set; }
    public override float attackRange { get; set; }
    public override float sightRange { get; set; }//sight angle

    private void Start()
    {
        currentEnemyHealth = 100.0f;
        maxEnemyHealth = 100.0f;
        range = 20.0f;
        bodyRange = 4.0f;
        attackRange = 13.0f;
        sightRange = 90f;
    }

    private void Update()
    {
        seePlayer();

    }
    public override void seePlayer()
    {
        Vector3 direction = playerTrans.position - transform.position;

        realDistance = Vector3.Distance(playerTrans.position, transform.position);
        realDirection = transform.forward;
        float angle = Vector3.Angle(direction, realDirection);
        if (realDistance < range && realDistance > bodyRange) //need set health.
        {
            if (angle < sightRange)
            {
                runToPlayer();
            }
        }
        Debug.Log("okk");
    }

    public override void runToPlayer()
    {
        //if set speed, here.
        enemy.SetDestination(playerTrans.position);
    }


    /*
    public virtual void attack()
    {
        if (currentEnemyHealth > 0 && currentPlayerHealth > 0)
        {
            currentPlayerHealth ??
            currentPlayEnergy ??
        }
    }
    */
}

