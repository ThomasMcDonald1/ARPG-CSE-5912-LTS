
using ARPG.Core;
using System;
using UnityEngine;

namespace ARPG.Combat
{
    public class Bandit : EnemyController
    {
        protected override void OnEnable()
        {
            BasicAttackDamageAbilityEffect.BasicAttackDamageReceivedEvent += OnDamageRun;
        }

        protected override void OnDisable()
        {
            BasicAttackDamageAbilityEffect.BasicAttackDamageReceivedEvent -= OnDamageRun;
        }
        public void OnDamageRun(object sender, InfoEventArgs<(Character,int, bool)> e)
        {
            if (e.info.Item1 == this)
            {
                if (animator.GetBool("Dead") == false)
                {
                    //Debug.Log("damaged!!!!");
                    //look away from the player
                    transform.rotation = Quaternion.LookRotation(transform.position - target.transform.position);

                    //point away from player
                    Vector3 runTo = transform.position + transform.forward * 10;
                    // And get it to head towards the found NavMesh position
                    agent.SetDestination(runTo);
                }
            }
        }
        protected override void Start()
        {
            base.Start();
            Range = 5f;
            BodyRange = 1;
            SightRange = 90f;
            Speed = 3f;
            agent.speed = Speed;
            stats[StatTypes.MonsterType] = 1; //testing
            cooldownTimer = 6;
        }

        public override string GetClassTypeName()
        {
            return "Bandit";
        }


        protected override void Update()
        {
            UpdateAnimator();
            //Debug.Log(abilitiesKnown);
            float attackSpeed = 1 + (stats[StatTypes.AtkSpeed] * 0.01f);
            animator.SetFloat("AttackSpeed", attackSpeed);
            if (animator.GetBool("Dead") == false)
            {
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
    }
}
