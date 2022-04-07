using ARPG.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


namespace ARPG.Combat
{
    public class EvilWizard : EnemyController
    {
        private int attackCounter = 200;
        protected override void Start()
        {
            base.Start();
            Range = 20f;
            BodyRange = 10f;
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
                    GetComponent<Animator>().SetBool("Summon", true);
                    Quaternion rotate = Quaternion.LookRotation(AttackTarget.transform.position - transform.position);
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, rotate, 500f * Time.deltaTime);
                    transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);

                    StopRun();
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
        IEnumerator Remobilize()
        {
            //Print the time of when the function is first called.
            Debug.Log("Started Coroutine at timestamp : " + Time.time);

            yield return new WaitForSeconds(3f);
            FindObjectOfType<Player>().GetComponent<NavMeshAgent>().enabled = true;
            //After we have waited 5 seconds print the time again.
            Debug.Log("Finished Coroutine at timestamp : " + Time.time);
        }
        protected override void Update()
        {
            if (GetComponent<Animator>().GetBool("Summon"))
            {
                if(attackCounter == 200)
                {
                    Debug.Log("damaged!");
                    AudioManager.instance.Play("Force");
                    float playerSpeed = FindObjectOfType<Player>().GetComponent<NavMeshAgent>().speed;
                    FindObjectOfType<Player>().GetComponent<NavMeshAgent>().enabled = false;
                    StartCoroutine(Remobilize());


                    //FindObjectOfType<Player>().GetComponent<NavMeshAgent>().speed = playerSpeed;
                    attackCounter = 0;
                }
                else
                {
                    attackCounter++;
                }
            }
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
