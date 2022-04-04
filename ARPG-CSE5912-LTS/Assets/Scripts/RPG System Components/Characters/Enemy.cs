using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace ARPG.Combat
{
    public abstract class Enemy : Character
    {
        Animator animator;

        public virtual float Range { get; set; }
        public virtual float BodyRange { get; set; }
        public virtual float SightRange { get; set; }
        protected virtual float Speed { get; set; }

        public static event EventHandler<InfoEventArgs<(int, int)>> EnemyKillExpEvent;

        private void Awake()
        {
            animator = GetComponent<Animator>();
        }

        protected override void Start()
        {
            base.Start();
        }
        protected override void Update()
        {
            float attackSpeed = 1 + (stats[StatTypes.AtkSpeed] * 0.01f);
            animator.SetFloat("AttackSpeed", attackSpeed);
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
            }

        }

        public void RaiseEnemyKillExpEvent(Enemy enemy, int monsterLevel, int monsterType) //(stats[StatTypes.LVL], stats[StatTypes.MonsterType]))
        {
            EnemyKillExpEvent?.Invoke(enemy, new InfoEventArgs<(int, int)>((monsterLevel, monsterType)));
        }

        public virtual  void SeePlayer()

        {

            if (InTargetRange()) 
            {
                Vector3 realDirection = transform.forward;
                Vector3 direction = AttackTarget.position -transform.position;
                float angle = Vector3.Angle(direction, realDirection);

                if (angle < SightRange && !InStopRange())
                {
                    RunToPlayer();
                }
                else if (angle < SightRange && InStopRange())
                {
                    StopRun();
                    if (GetComponent<Animator>().GetBool("AttackingMainHand"))
                    {
                        GetComponent<Animator>().SetTrigger("AttackMainHandTrigger");
                        //Debug.Log(GetComponent<Animator>().GetBool("AttackingMainHand"));
                    }
                    else
                    {
                        GetComponent<Animator>().SetTrigger("AttackOffHandTrigger");
                        //Debug.Log(GetComponent<Animator>().GetBool("AttackingMainHand"));
                    }
                    if (AttackTarget.GetComponent<Stats>()[StatTypes.HP] <= 0) //When player is dead, stop hit.
                    {
                        StopRun();
                    }
                }
                else
                {
                    Patrol ();
                }
            }
            else
            {
                Patrol();
            }
        }

        protected void Patrol()
        {
            agent.isStopped = false;
            if (!agent.pathPending && agent.remainingDistance < 0.5f)
            {
                agent.SetDestination(RandomNavmeshDestination(5f));
            }
        }
        public Vector3 RandomNavmeshDestination(float radius)
        {
            Vector3 randomDirection = Random.insideUnitSphere * radius;
            randomDirection += transform.position;
            NavMeshHit hit;
            Vector3 finalPosition = Vector3.zero;
            if (NavMesh.SamplePosition(randomDirection, out hit, radius, 1))
            {
                finalPosition = hit.position;
            }
            return finalPosition;
        }

        public virtual  void RunToPlayer()
        {

                agent.isStopped = false;
                agent.SetDestination(AttackTarget.position);
            
        }

        public void StopRun()
        {
            agent.isStopped = true;
        }

        public virtual bool InTargetRange()
        {
            return Vector3.Distance(this.transform.position, AttackTarget.position) < Range;
        }
        public  bool InStopRange()
        {
            return Vector3.Distance(transform.position, AttackTarget.position) < BodyRange;
        }

        // From animation Event
        public void Hit()
        {
            //float distance = Vector3.Distance(this.transform.position, AttackTarget.transform.position);
            //if (distance < BodyRange)
            //{
            //    AttackTarget.GetComponent<Stats>()[StatTypes.HP] -= stats[StatTypes.PHYATK];
            //    QueueBasicAttack(basicAttackAbility, AttackTarget.GetComponent<Character>());
            //}
        }

        // From animation Event
        public void EndMainHandAttack()
        {
            //Debug.Log("Being called EndMainHandAttack?");
            if (GetComponent<Animator>().GetBool("CanDualWield")) { GetComponent<Animator>().SetBool("AttackingMainHand", false); }
        }

        // From animation event
        public void EndOffHandAttack()
        {
            //Debug.Log("Being called EndOffHandAttack?");
            GetComponent<Animator>().SetBool("AttackingMainHand", true);
        }

        public void Dead()
        {
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
    //public void Cancel()
    //{
    //    AttackTarget = null;
    //}


    //public virtual float AttackRange { get; set; }
    //private GameObject GeneralClass;
    ////public NavMeshAgent enemy;
    //protected override void Start()
    //{
    //    base.Start();
    //    //agent.speed = Speed;
    //    GeneralClass = GameObject.Find("EnemyClass");
    //}
    //protected override void Update()
    //{
    //    if (GetComponent<Animator>().GetBool("Dead") == false)
    //    {
    //        base.Update();
    //        if (stats[StatTypes.HP] <= 0)
    //        {
    //            if (GetComponent<Animator>().GetBool("Dead") == false)
    //            {
    //                Dead();
    //                GetComponent<Animator>().SetBool("Dead", true);
    //                //get rid of enemy canvas
    //                GetComponent<Transform>().GetChild(2).gameObject.SetActive(false);

    //            }
    //        }
    //        else
    //        {
    //            SeePlayer();
    //        }
    //        //if (AttackTarget != null)//will not be null
    //        //{
    //        //    Vector3 realDirection = transform.forward;
    //        //    Vector3 direction = AttackTarget.position - transform.position;
    //        //    float angle = Vector3.Angle(direction, realDirection);
    //        //    if (angle < SightRange && InStopRange())
    //        //    {
    //        //        Quaternion rotationToLookAt = Quaternion.LookRotation(AttackTarget.transform.position - transform.position);
    //        //        float rotationY = Mathf.SmoothDampAngle(transform.eulerAngles.y,
    //        //        rotationToLookAt.eulerAngles.y, ref yVelocity, smooth);
    //        //        transform.eulerAngles = new Vector3(0, rotationY, 0);

    //        //    }
    //        //}
    //    }
    //public void Attack(EnemyTarget target)
    //{
    //    AttackTarget = target.transform;
    //}

    //}
}
