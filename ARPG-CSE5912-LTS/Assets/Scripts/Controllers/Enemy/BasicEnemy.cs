using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BasicEnemy : MonoBehaviour
{
    public NavMeshAgent enemy;
    public float currentEnemyHealth = 100.0f;
    public float maxEnemyHealth = 100.0f;

    public Transform playerTrans;
    public float currentPlayerHealth;
    public float currentPlayEnergy;

    

    private float realDistance;
    private Vector3 realDirection;
    private float range = 20.0f;
    private float bodyRange = 4.0f;
    private float attackRange = 13.0f;
    private float sightRange = 90f;
    /*
    public virtual void Awake()
    {

    }
    */
    private void Update()
    {
        seePlayer();
        
    }
    public virtual void seePlayer()
    {
        Vector3 direction = playerTrans.position - transform.position;
        
        realDistance = Vector3.Distance(playerTrans.position, transform.position);
        realDirection = transform.forward;
        float angle = Vector3.Angle(direction, realDirection);
        if (realDistance < range && realDistance > bodyRange) //need set health.
        {
            if (angle < sightRange){
                runToPlayer();
            }
        }
        Debug.Log("okk");
    }

    public virtual void runToPlayer()
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
