using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace ARPG.Combat
{
    public abstract class Enemy : Character
    {
        private GameObject GeneralClass;
        //public NavMeshAgent enemy;

        public virtual float AttackRange { get; set; }
        public virtual float Range { get; set; }
        public virtual float BodyRange { get; set; }
        public virtual float SightRange { get; set; }
        protected virtual float Speed { get; set; }

        public static event EventHandler<InfoEventArgs<(int, int)>> EnemyKillExpEvent;

        protected override void Start()
        {
            base.Start();
            agent.speed = Speed;
            GeneralClass = GameObject.Find("EnemyClass");
        }
        protected override void Update()
        {
            if (GetComponent<Animator>().GetBool("Dead") == false)
            {
                base.Update();
                if (stats[StatTypes.HP] <= 0)
                {
                    if (GetComponent<Animator>().GetBool("Dead") == false)
                    {
                        Dead();
                        GetComponent<Animator>().SetBool("Dead", true);
                        //get rid of enemy canvas
                        GetComponent<Transform>().GetChild(2).gameObject.SetActive(false);

                    }
                }
                else
                {
                    SeePlayer();
                }
                if (AttackTarget != null)
                {
                    Vector3 realDirection = transform.forward;
                    Vector3 direction = AttackTarget.position - transform.position;
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

        }

        public  void SeePlayer()
        {

            if (InTargetRange()) //need set health.
            {
                //Debug.Log("yeah");
                Vector3 realDirection = transform.forward;
                Vector3 direction = AttackTarget.position -transform.position;
                float angle = Vector3.Angle(direction, realDirection);
                if (AttackTarget.GetComponent<Stats>()[StatTypes.HP] <= 0) //When player is dead, stop hit.
                {
                    StopRun();
                }
                else if (angle < SightRange && !InStopRange())
                {
                    //Debug.Log("run");
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
            //Debug.Log("okk");
        }

        public  void RunToPlayer()
        {
            if (InTargetRange())
            {
                agent.isStopped = false;

                agent.speed = Speed;
                Debug.Log("running to player");
                agent.SetDestination(AttackTarget.position);
            }
        }

        public void StopRun()
        {
            agent.isStopped = true;
        }

        //public virtual float CurrentEnemyHealth { get; set; }
        //public virtual float MaxEnemyHealth { get; set; }
        public void Attack(EnemyTarget target)
        {
            AttackTarget = target.transform;
        }

        public void Cancel()
        {
            AttackTarget = null;
        }
        public  bool InTargetRange()
        {
            if (AttackTarget == null) return false;
            Debug.Log(Vector3.Distance(this.transform.position, AttackTarget.position));

            return Vector3.Distance(this.transform.position, AttackTarget.position) < Range;
        }
        public  bool InStopRange()
        {
            if (AttackTarget == null) return false;
            return Vector3.Distance(transform.position, AttackTarget.position) < BodyRange;
        }
        public void Hit()
        {


            if (AttackTarget != null)
            {
                float distance = Vector3.Distance(this.transform.position, AttackTarget.transform.position);
                if (distance < 2)
                {
                    AttackTarget.GetComponent<Stats>()[StatTypes.HP] -= stats[StatTypes.PHYATK];
                }
            }
        }

        public void Dead()
        {
                Debug.Log("enemy kkkkill");
                EnemyKillExpEvent?.Invoke(this, new InfoEventArgs<(int, int)>((stats[StatTypes.LVL], stats[StatTypes.MonsterType])));
                StartCoroutine(Die(10));
        }
        IEnumerator Die(int seconds)
        {
            yield return new WaitForSeconds(seconds);
            Destroy(gameObject);

        }
        public void ProduceItem()
        {
            Debug.Log("Item dropped");
        }

        public override Type GetCharacterType()
        {
            return typeof(Enemy);
        }
    }
}
