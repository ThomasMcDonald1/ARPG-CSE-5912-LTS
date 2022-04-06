
using ARPG.Core;
using System;
using UnityEngine;

namespace ARPG.Combat
{
    public class Bandit : EnemyController
    {
        private void OnEnable()
        {
            BasicAttackDamageAbilityEffect.DamageEnemyEvent += OnDamageRun;
        }

        private void OnDisable()
        {
            BasicAttackDamageAbilityEffect.DamageEnemyEvent -= OnDamageRun;
        }
        public void OnDamageRun(object sender, EventArgs e)
        {
            if (GetComponent<Animator>().GetBool("Dead") == false)
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
        protected override void Start()
        {
            base.Start();
            Range = 5f;
            BodyRange = 0.5f;
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
            if (GetComponent<Animator>().GetBool("Dead") == false)
            {
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
    }
}
