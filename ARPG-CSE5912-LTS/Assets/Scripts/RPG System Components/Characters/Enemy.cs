using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;
using LootLabels;
using TMPro;

namespace ARPG.Combat
{
    public abstract class Enemy : Character
    {
        protected virtual void OnEnable()
        {
            BasicAttackDamageAbilityEffect.BasicAttackDamageReceivedEvent += OnDamageReact;
        }

        protected virtual void OnDisable()
        {
            BasicAttackDamageAbilityEffect.BasicAttackDamageReceivedEvent -= OnDamageReact;
        }
        public void OnDamageReact(object sender, InfoEventArgs<(Character, int, bool)> e)
        {

            if (animator.GetBool("Dead") == false)
            {
                Vector3 playerPoint = FindObjectOfType<Player>().transform.position;
                Quaternion targetRotation = Quaternion.LookRotation(playerPoint - transform.position);
                float turnSpeed = 2; 
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * turnSpeed);
            }
        }
        protected Animator animator;
        [SerializeField] LootSource lootSource;
        [SerializeField] LootType lootType;

        public virtual float Range { get; set; }
        public virtual float BodyRange { get; set; }
        public virtual float SightRange { get; set; }
        protected virtual float Speed { get; set; }

        public static event EventHandler<InfoEventArgs<(int, int,string)>> EnemyKillExpEvent;

        public virtual List<EnemyAbility> EnemyAttackTypeList { get; set; } // a list for the order of enemy ability/basic attack
        public virtual float cooldownTimer { get; set; }
        protected GameObject player;
        private void Awake()
        {
            animator = GetComponent<Animator>();
            player = GameObject.FindGameObjectWithTag("Player");
        }

        protected override void Start()
        {
            base.Start();
            TextMeshProUGUI enemyUIText = transform.GetChild(2).GetChild(3).GetComponent<TextMeshProUGUI>();
            //Debug.Log("name" + transform.GetChild(0).name);
            //Debug.Log("level" + stats[StatTypes.LVL].ToString());

            enemyUIText.text = transform.GetChild(0).name + " LV " + stats[StatTypes.LVL].ToString();
            //Debug.Log("enemy is" + gameObject.name);
            //Debug.Log(abilitiesKnown);
        }
        protected override void Update()
        {
            if (player == null)
            {
                player = GameObject.FindGameObjectWithTag("Player");
            }
            //Debug.Log(abilitiesKnown);
            float attackSpeed = 1 + (stats[StatTypes.AtkSpeed] * 0.01f);
            animator.SetFloat("AttackSpeed", attackSpeed);
            if (animator.GetBool("Dead") == false)
            {
                base.Update();
                if (stats[StatTypes.HP] <= 0)
                {
                    if (animator.GetBool("Dead") == false)
                    {
                        Dead();
                        animator.SetBool("Dead", true);
                        //get rid of enemy canvas
                        transform.GetChild(2).gameObject.SetActive(false);
                    }
                }
                else
                {
                    SeePlayer();
                }
            }         
        }

        public void RaiseEnemyKillExpEvent(Enemy enemy, int monsterLevel, int monsterType, string className) //(stats[StatTypes.LVL], stats[StatTypes.MonsterType]))
        {
            EnemyKillExpEvent?.Invoke(enemy, new InfoEventArgs<(int, int,string)>((monsterLevel, monsterType,className)));
        }

        protected virtual  void SeePlayer()

        {
            animator.ResetTrigger("AttackMainHandTrigger");

            animator.ResetTrigger("AttackOffHandTrigger");
            if (InTargetRange()) 
            {
                Vector3 realDirection = transform.forward;
                Vector3 direction = AttackTarget.position -transform.position;
                float angle = Vector3.Angle(direction, realDirection);

                if (angle < SightRange && !InStopRange())
                {
                    RunToPlayer();
                    /*
                    if (EnemyAttackTypeList != null)
                    {
                        if (AttackTarget != null)
                        {
                            for (int i = 0; i++; i < EnemyAttackTypeList.Count)
                            {
                                if (EnemyAttackTypeList[i].cooldownTimer == 0)
                                {
                                    ChooseAttackType(EnemyAttackTypeList[i].abilityAssigned);
                                    break;
                                }
                            }
                        }
                    }
                    */

                }
                else if (angle < SightRange && InStopRange())
                {
                    StopRun();
                    Quaternion rotate = Quaternion.LookRotation(AttackTarget.transform.position - transform.position);
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, rotate, 500f * Time.deltaTime);
                    transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
                    if (animator.GetBool("AttackingMainHand"))
                    {
                        animator.SetTrigger("AttackMainHandTrigger");
                        //Debug.Log(GetComponent<Animator>().GetBool("AttackingMainHand"));
                    }
                    else
                    {
                        animator.SetTrigger("AttackOffHandTrigger");
                        //Debug.Log(GetComponent<Animator>().GetBool("AttackingMainHand"));
                    }
                    if (AttackTarget.GetComponent<Character>().stats[StatTypes.HP] <= 0) //When player is dead, stop hit.
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
                NavMeshPath path = new NavMeshPath();
                agent.CalculatePath(RandomNavmeshDestination(5f), path);
                agent.path = path;
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

        public virtual void RunToPlayer()
        {
            NavMeshPath path = new NavMeshPath();
            agent.CalculatePath(AttackTarget.position, path);
            agent.isStopped = false;
            agent.path = path;
        }

        public void StopRun()
        {
            agent.isStopped = true;
        }

        public virtual bool InTargetRange()
        {
            return Vector3.Distance(this.transform.position, AttackTarget.position) < Range;
        }
        public bool InStopRange()
        {
            return Vector3.Distance(transform.position, AttackTarget.position) < BodyRange;
        }

        // From animation Event
        public void Hit()
        {
            float distance = Vector3.Distance(this.transform.position, AttackTarget.transform.position);
            if (distance < BodyRange)
            {
                //Debug.Log("Attack target is: " + AttackTarget);
                //AttackTarget.GetComponent<Stats>()[StatTypes.HP] -= stats[StatTypes.PHYATK];
                QueueBasicAttack(basicAttackAbility, AttackTarget.GetComponent<Character>(), this);
            }
        }

        // From animation Event
        public void EndMainHandAttack()
        {
            //Debug.Log("Being called EndMainHandAttack?");
            if (animator.GetBool("CanDualWield")) { GetComponent<Animator>().SetBool("AttackingMainHand", false); }
        }

        // From animation event
        public void EndOffHandAttack()
        {
            //Debug.Log("Being called EndOffHandAttack?");
            animator.SetBool("AttackingMainHand", true);
        }

        public void Dead()
        {
            EnemyKillExpEvent?.Invoke(this, new InfoEventArgs<(int, int,string)>((stats[StatTypes.LVL], stats[StatTypes.MonsterType],transform.GetChild(0).name)));
            StartCoroutine(Die(10));
            LootManager.singleton.DropLoot(lootSource, transform, lootType);
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

        public LootSource GetEnemyLootSource()
        {
            return lootSource;
        }

        private void ChooseAttackType(Ability AttackType)
        {

            /*
            switch (AttackType)
            {
                case 0:
                    //ability list 0
                    break;
                case 1:
                    //ability list 1
                    break;
                case 2:
                    //basic attack, last choice
                    GetComponent<Animator>().SetTrigger("AttackTrigger");
                    break;
                default:
                    break;

            }
            */
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
