
using ARPG.Core;
using System;
using UnityEngine;

namespace ARPG.Combat
{
    public class Bandit : EnemyController
    {
        protected override void OnEnable()
        {
            BasicAttackDamageAbilityEffect.BasicAttackDamageReceivedEvent += OnLowHealthRun;
        }

        protected override void OnDisable()
        {
            BasicAttackDamageAbilityEffect.BasicAttackDamageReceivedEvent -= OnLowHealthRun;
        }
        public void OnLowHealthRun(object sender, InfoEventArgs<(Character,int, bool)> e)
        {
            if (e.info.Item1 == this)
            {
                float healthPercent = stats[StatTypes.HP] / (float)stats[StatTypes.MaxHP];
                if (animator.GetBool("Dead") == false && healthPercent < 0.5f)
                {
                    //Debug.Log("percent" + healthPercent);
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
            Range = 10f;
            BodyRange = 1.4f;
            SightRange = 180;
            Speed = 3.5f;
            agent.speed = Speed;
            stats[StatTypes.MonsterType] = 1; //testing
            stats[StatTypes.PHYATK] = 200;//testing
            cooldownTimer = 6;
        }

        public override string GetClassTypeName()
        {
            return "Bandit";
        }


    }
}
