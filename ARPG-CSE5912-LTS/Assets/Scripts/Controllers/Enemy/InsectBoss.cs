using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ARPG.Combat;
using UnityEngine.AI;
using System;

public class InsectBoss : Enemy
{
    [SerializeField] float chaseDistance = 15.0f;
    [SerializeField] float longRange = 10.0f;
    [SerializeField] float meleeRange = 5.0f;
    [SerializeField] float vertexThreshold = 5.0f;

    [SerializeField] GameObject HealthBar;
    [SerializeField] PatrolPath patrolPath;

    GameObject player;
    Transform PlayerTarget;
    Vector3 PatrolToPosition;


    private int CurrentPatrolVertexIndex = 0;
    private int AttackCycle = 0;

    private void Awake()
    {
        stats = GetComponent<Stats>();
        GetComponent<Stats>()[StatTypes.MaxHP] = 500;
        GetComponent<Stats>()[StatTypes.HP] = 500;
        GetComponent<Stats>()[StatTypes.LVL] = 10;
        GetComponent<Stats>()[StatTypes.MonsterType] = 3;

        player = GameObject.FindWithTag("Player");
        PlayerTarget = null;

        if (patrolPath != null)
        {
            PatrolToPosition = patrolPath.GetVertex(CurrentPatrolVertexIndex);
        }
        else
        {
            PatrolToPosition = transform.position;
        }
    }

    new private void Update()
    {
        /*Debug.Log(GetComponent<NavMeshAgent>().pathPending);
        NavMesh.CalculatePath(transform.position, PatrolToPosition, NavMesh.AllAreas, path);
        for (int i = 0; i < path.corners.Length - 1; i++)
            Debug.DrawLine(path.corners[i], path.corners[i + 1], Color.red);*/
        if (PlayerTarget != null)
        {

        }
        UpdateAnimator();
        if (GetComponent<Stats>()[StatTypes.HP] <= 0)
        {
            GetComponent<Animator>().SetBool("Dead", true);
            //get rid of enemy canvas
            PlayerTarget = null;
        }

        if (InSightRadius() && PlayerTarget == null && GetComponent<Stats>()[StatTypes.HP] >= 0)
        {
            MakeHostile();
        }
        else if (!InSightRadius() && PlayerTarget != null && GetComponent<Stats>()[StatTypes.HP] >= 0)
        {
            MakeNonHostile();
        }

        if (PlayerTarget != null && GetComponent<Stats>()[StatTypes.HP] > 0)
        {
            Quaternion rotationToLookAt = Quaternion.LookRotation(PlayerTarget.transform.position - transform.GetChild(0).position);
            float rotationY = Mathf.SmoothDampAngle(transform.eulerAngles.y,
            rotationToLookAt.eulerAngles.y, ref yVelocity, smooth);
            transform.eulerAngles = new Vector3(0, rotationY, 0);

            if (GetComponent<Animator>().GetBool("AnimationEnded") && !InMeleeRange())
            {
                agent.isStopped = false;
                agent.SetDestination(player.transform.position);
            }

            if (InMeleeRange())
            {
                agent.isStopped = true;
                agent.SetDestination(transform.position);
                Debug.Log(AttackCycle);
                switch (AttackCycle)
                {
                    case 0:
                        GetComponent<Animator>().SetTrigger("Attack1");
                        break;
                    case 1:
                        GetComponent<Animator>().SetTrigger("Attack2");
                        break;
                    case 2:
                        GetComponent<Animator>().SetTrigger("Attack3");
                        break;
                    default:
                        GetComponent<Animator>().SetTrigger("Attack1");
                        break;
                }
            }
        }
        else
        {
            if (GetComponent<Animator>().GetBool("AnimationEnded") && GetComponent<Stats>()[StatTypes.HP] >= 0)
            {
                PatrolBehavior();
            }
        }

    }

    private bool InSightRadius()
    {
        return Vector3.Distance(player.transform.position, transform.position) < chaseDistance;
    }

    private bool InMeleeRange()
    {
        return Vector3.Distance(player.transform.position, transform.position) < meleeRange;
    }

    private void MakeHostile()
    {
        PlayerTarget = player.transform;
        GetComponent<Animator>().SetTrigger("Intimidate");
    }

    private void MakeNonHostile()
    {
        agent.isStopped = true;
        PlayerTarget = null;
        GetComponent<Animator>().SetTrigger("Intimidate");
    }

    private void PatrolBehavior()
    {
        if (patrolPath != null)
        {
            //print("For index: " + CurrentPatrolVertexIndex + " " + AtPatrolVertex());
            if (AtPatrolVertex())
            {
                SetNextVertexIndex();
                PatrolToPosition = patrolPath.GetVertex(CurrentPatrolVertexIndex);
            }
            //print("For index: " + CurrentPatrolVertexIndex + " " + AtPatrolVertex());

        }
        agent.SetDestination(PatrolToPosition);
    }

    private void SetNextVertexIndex()
    {
        CurrentPatrolVertexIndex = patrolPath.GetNextIndex(CurrentPatrolVertexIndex);
    }

    private bool AtPatrolVertex()
    {
        return Vector3.Distance(transform.position, PatrolToPosition) < vertexThreshold;
    }

    private void UpdateAnimator()
    {
        Vector3 velocity = agent.velocity;
        Vector3 localVelocity = transform.InverseTransformDirection(velocity);
        float speed = localVelocity.z;
        GetComponent<Animator>().SetFloat("forwardSpeed", speed);
    }

    // Animation Event
    void AnimationStarted()
    {
        GetComponent<Animator>().SetBool("AnimationEnded", false);
        agent.isStopped = true;
    }

    // Animation Event
    void AnimationEnded()
    {
        agent.isStopped = false;
        GetComponent<Animator>().SetBool("AnimationEnded", true);
    }

    // Animation Event
    void HitPlayer()
    {
        if (AttackCycle++ > 1) { AttackCycle = 0; }
        print("Damaged player");
    }

    // Animation Event
    void DieAnimationEnded()
    {
        HealthBar.SetActive(false);
        base.RaiseEnemyKillExpEvent(this, GetComponent<Stats>()[StatTypes.LVL], GetComponent<Stats>()[StatTypes.MonsterType], transform.GetChild(0).name);
    }

    // Gizmos for sight range (purple) and melee range (red)
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(155, 0, 255);
        Gizmos.DrawWireSphere(transform.position, chaseDistance);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, meleeRange);
    }


}
