using ARPG.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ARPG.Combat
{
    public class EvilWizard : EnemyController
    {
        protected override void Start()
        {
            base.Start();
            Range = 10f;
            BodyRange = 5f;
            SightRange = 360f;
            Speed = 3f;
            agent.speed = Speed;
            stats[StatTypes.MonsterType] = 1; //testing
            cooldownTimer = 6;
        }

        public override string GetClassTypeName()
        {
            return "Bandit";
        }

        protected override void SeePlayer()
        {
            if (InTargetRange())
            {
                Vector3 realDirection = transform.forward;
                Vector3 direction = AttackTarget.position - transform.position;
                float angle = Vector3.Angle(direction, realDirection);

                if (angle < SightRange && !InStopRange())
                {
                    GetComponent<Animator>().SetBool("Summon", false);

                    RunToPlayer();

                }
                else if (angle < SightRange && InStopRange())
                {
                    Quaternion rotate = Quaternion.LookRotation(AttackTarget.transform.position - transform.position);
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, rotate, 50f * Time.deltaTime);
                    transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);

                    StopRun();
                    GetComponent<Animator>().SetBool("Summon",true);
                    if (AttackTarget.GetComponent<Stats>()[StatTypes.HP] <= 0) //When player is dead, stop hit.
                    {
                        StopRun();
                    }
                }
                else
                {
                    GetComponent<Animator>().SetBool("Summon", false);

                    Patrol();
                }
            }
            else
            {
                GetComponent<Animator>().SetBool("Summon", false);

                Patrol();
            }
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
