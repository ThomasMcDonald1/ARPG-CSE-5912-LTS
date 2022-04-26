
using ARPG.Core;
using System;
using UnityEngine;

namespace ARPG.Combat
{
    public class Bandit : EnemyController
    {
        private int runCounter = 0;
        private bool flightMode = false;

        //protected override void OnEnable()
        //{
        //    BasicAttackDamageAbilityEffect.BasicAttackDamageReceivedEvent += OnLowHealthRun;
        //}

        //protected override void OnDisable()
        //{
        //    BasicAttackDamageAbilityEffect.BasicAttackDamageReceivedEvent -= OnLowHealthRun;
        //}
        //public void OnLowHealthRun(object sender, InfoEventArgs<(Character,int, bool)> e)
        //{
        //    if (e.info.Item1 == this)
        //    {
        //        float healthPercent = stats[StatTypes.HP] / (float)stats[StatTypes.MaxHP];
        //        if (animator.GetBool("Dead") == false && healthPercent < 0.5f)
        //        {
        //            //Debug.Log("percent" + healthPercent);
        //            //look away from the player
        //            transform.rotation = Quaternion.LookRotation(transform.position - target.transform.position);

        //            //point away from player
        //            Vector3 runTo = transform.position + transform.forward * 10;
        //            // And get it to head towards the found NavMesh position
        //            agent.SetDestination(runTo);
        //        }
        //    }
        //}
        protected override void Start()
        {
            base.Start();
            Range = 10f;
            BodyRange = 1.4f;
            SightRange = 180;
            Speed = 3.5f;
            agent.speed = Speed;
            stats[StatTypes.MonsterType] = 1; //testing
            stats[StatTypes.PHYATK] = 200;//testing
            cooldownTimer = 6;
            float attackSpeed = 1 + (stats[StatTypes.AtkSpeed] * 0.01f);

        }

        public override string GetClassTypeName()
        {
            return "Bandit";
        }
        protected override void Update()
        {
        }
        private void FixedUpdate()
        {
            if (animator.GetBool("Dead") == false)
            {
                if (Vector3.Distance(transform.position, player.transform.position) <= 18)
                {
                    if (player == null)
                    {
                        player = GameObject.FindGameObjectWithTag("Player");
                    }
                    if (stats[StatTypes.HP] <= 0)
                    {
                        Dead();
                        animator.SetBool("Dead", true);
                        //get rid of enemy canvas
                        transform.GetChild(2).gameObject.SetActive(false);
                    }
                    if (flightMode)
                    {
                        //called 50 times per sec
                        if (runCounter == 50)
                        {
                            //Debug.Log("percent" + healthPercent);
                            //look away from the player
                            transform.rotation = Quaternion.LookRotation(transform.position - target.transform.position);

                            //point away from player
                            Vector3 runTo = transform.position + transform.forward * 5;
                            // And get it to head towards the found NavMesh position
                            agent.SetDestination(runTo);
                            runCounter = 0;
                        }
                        runCounter++;
                    }
                    else if (stats[StatTypes.HP] / (float)stats[StatTypes.MaxHP] < 0.5f)
                    {
                        flightMode = true;
                        agent.speed = agent.speed + 1;
                    }
                    else
                    {
                        SeePlayer();
                    }
                    UpdateAnimator();
                }
            }
        }

    }
}

