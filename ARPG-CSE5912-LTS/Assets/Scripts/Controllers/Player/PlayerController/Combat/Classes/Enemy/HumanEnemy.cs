using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace ARPG.Combat
{
    public class HumanEnemy : EnemyClass
    {
        private GameObject GeneralClass;
        private bool signalAttack;

        public NavMeshAgent enemy;

        public Stats statScript;

        private void Start()
        {
            GeneralClass = GameObject.Find("EnemyClass");
            statScript = GetComponent<Stats>();

            AttackRange = 13.0f;
            AttackTarget = null;

            Range = 20.0f;
            BodyRange = 1.0f;
            SightRange = 90f;

            Speed = 2f;
        }

        private void Update()
        {
            if (statScript.health <= 0)
            {
                GetComponent<Animator>().SetBool("Dead", true);
            }

            SeePlayer();
            
        }


        public override void SeePlayer()
        {
            if (InTargetRange()) //need set health.
            {
                Debug.Log("yeah");
                Vector3 realDirection = GeneralClass.transform.forward;
                Vector3 direction = AttackTarget.position - GeneralClass.transform.position;
                float angle = Vector3.Angle(direction, realDirection);
                if (AttackTarget.GetComponent<Stats>().health <= 0) //When player is dead, stop hit.
                {
                    StopRun();
                }else if (angle < SightRange && !InStopRange())
                {
                    Debug.Log("run");
                    RunToPlayer();
                }
                else if (angle < SightRange && InStopRange())
                {
                    StopRun();
                    GetComponent<Animator>().SetTrigger("AttackTrigger");
                }
                else
                {
                    StopRun();
                }
            }
            Debug.Log("okk");
        }

        public override void RunToPlayer()
        {
            if (InTargetRange())
            {
                enemy.isStopped = false;

                enemy.speed = Speed;
                enemy.SetDestination(AttackTarget.position);
            }
        }

        public override void StopRun()
        {
            enemy.isStopped = true;
        }

        public override void Attack(EnemyTarget target)
        {
            AttackTarget = target.transform;
        }

        public override void Cancel()
        {
            AttackTarget = null;
        }

        public override bool InTargetRange()
        {
            if (AttackTarget == null) return false;
            return Vector3.Distance(GeneralClass.transform.position, AttackTarget.position) < AttackRange;
        }

        public override bool InStopRange()
        {
            if (AttackTarget == null) return false;
            return Vector3.Distance(GeneralClass.transform.position, AttackTarget.position) < BodyRange;
        }

        public override void AttackSignal(bool signal)
        {
            signalAttack = signal;
        }

        public void Hit()
        {
            if (AttackTarget != null)
            {
                AttackTarget.GetComponent<Stats>().health -= statScript.attackDmg;
            }
        }

        public void Dead()
        {
            //nead a dead animation before destroy.
            if (statScript.health <= 0)
            {
                Destroy(gameObject);
            }
        }
    }

}
