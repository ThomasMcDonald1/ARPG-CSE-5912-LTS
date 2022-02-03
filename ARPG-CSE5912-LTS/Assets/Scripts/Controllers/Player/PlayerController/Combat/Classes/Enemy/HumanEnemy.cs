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

        float smooth;
        float yVelocity;

        private void Start()
        {
            GeneralClass = GameObject.Find("EnemyClass");
            statScript = GetComponent<Stats>();

            AttackRange = 13.0f;
            AttackTarget = null;

            Range = 20.0f;
            BodyRange = 1.5f;
            SightRange = 90f;

            Speed = 2f;

            //rotation data
            smooth = 0.3f;
            yVelocity = 0.0f;

            //Stats
            statScript[StatTypes.MAXHEALTH] = 2600;
            statScript[StatTypes.HEALTH] = statScript[StatTypes.MAXHEALTH];
            statScript[StatTypes.PHYATK] = 120;
            statScript[StatTypes.PHYDEF] = 30;
            statScript[StatTypes.ATKSPD] = 120;
        }

        private void Update()
        {
            if (statScript[StatTypes.HEALTH] <= 0)
            {
                GetComponent<Animator>().SetBool("Dead", true);
                GetComponent<Transform>().GetChild(2).gameObject.SetActive(false);
            }
            else
            {
                SeePlayer();
            }
            if (AttackTarget != null) {
                Vector3 realDirection = GeneralClass.transform.forward;
                Vector3 direction = AttackTarget.position - GeneralClass.transform.position;
                float angle = Vector3.Angle(direction, realDirection);
                if (angle < SightRange && InStopRange())
                {
                    Quaternion rotationToLookAt = Quaternion.LookRotation(AttackTarget.transform.position - transform.position);
                    float rotationY = Mathf.SmoothDampAngle(transform.eulerAngles.y,
                    rotationToLookAt.eulerAngles.y, ref yVelocity, smooth);
                    transform.eulerAngles = new Vector3(0, rotationY, 0);

                }
            }

        }

   
        IEnumerator DestroyHealthBar()
        {
            //Print the time of when the function is first called.
            //Debug.Log("Started Coroutine at timestamp : " + Time.time);

            //yield on a new YieldInstruction that waits for 2 seconds.
            yield return new WaitForSeconds(1.5f);

            //After we have waited 2 seconds print the time again.
            //Debug.Log("Finished Coroutine at timestamp : " + Time.time);
            GetComponent<Transform>().GetChild(2).gameObject.SetActive(false);
        }


        public override void SeePlayer()
        {
            if (InTargetRange()) //need set health.
            {
                Debug.Log("yeah");
                Vector3 realDirection = GeneralClass.transform.forward;
                Vector3 direction = AttackTarget.position - GeneralClass.transform.position;
                float angle = Vector3.Angle(direction, realDirection);
                if (AttackTarget.GetComponent<Stats>()[StatTypes.HEALTH] <= 0) //When player is dead, stop hit.
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
                    //Quaternion rotationToLookAt = Quaternion.LookRotation(AttackTarget.transform.position - transform.position);
                    GetComponent<Animator>().SetTrigger("AttackTrigger");
                    //float rotationY = Mathf.SmoothDampAngle(transform.eulerAngles.y, rotationToLookAt.eulerAngles.y, ref yVelocity, smooth);

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
                float distance = Vector3.Distance(this.transform.position, AttackTarget.transform.position);
                if (distance < 2)
                {
                    AttackTarget.GetComponent<Stats>()[StatTypes.HEALTH] -= statScript[StatTypes.PHYATK];
                }
            }
        }

        public void Dead()
        {
            //nead a dead animation before destroy.
            if (statScript[StatTypes.HEALTH] <= 0)
            {
                Destroy(gameObject);
            }
        }

        public void ProduceItem()
        {
            Debug.Log("Item dropped");
        }
    }

}
