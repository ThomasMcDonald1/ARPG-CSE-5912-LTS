using ARPG.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ARPG.Combat
{
    public class Paths : EnemyController
    {
        public bool canSee = false;
        protected override void Start()
        {
            base.Start();
            Range = 5f;
            BodyRange = 1.5f;
            SightRange = 90f;
            Speed = 2f;
            agent.speed = Speed;
            stats[StatTypes.MonsterType] = 1; //testing
        }
        protected override void Update()
        {
            UpdateAnimator();
            //base.Update();
            if (transform.parent.parent.GetComponent<Animator>().GetBool("Dead") == false)
            {
                if (transform.parent.parent.gameObject.GetComponent<SageOfSixPaths>().stats[StatTypes.HP] <= 0)
                {
                    if (transform.parent.parent.GetComponent<Animator>().GetBool("Dead") == false)
                    {
                        //If sage dies, it will dies for real
                        Dead();
                        GetComponent<Animator>().SetBool("Dead", true);
                        //get rid of enemy canvas
                        GetComponent<Transform>().GetChild(2).gameObject.SetActive(false);

                    }
                }
                else
                {
                    //this is to prevent the path from moving if it is temporaraly dead
                    if (GetComponent<Animator>().GetBool("Dead") == false)
                    {
                        SeePlayer();
                    }
                }
            }

            //if it died but sage is alive, it will revive, temporary death
            if (stats[StatTypes.HP] <= 0)
            {
                GetComponent<Animator>().SetBool("Dead", true);
                StartCoroutine(Revive());
            }
        }
        IEnumerator Revive()
        {

            //yield on a new YieldInstruction that waits for 5 seconds.
            yield return new WaitForSeconds(5);

            GetComponent<Animator>().SetBool("Dead", false);
            stats[StatTypes.HP] = stats[StatTypes.MaxHP];
        }
        protected override void SeePlayer()
        {
            Vector3 realDirection = transform.forward;
            Vector3 direction = AttackTarget.position - transform.position;
            float angle = Vector3.Angle(direction, realDirection);

            //updates path's canSee based on what it can see individualy
            if (InTargetRange() && angle < SightRange)
            {
                canSee = true;
            }
            else
            {
                canSee = false;
            }

            //if sage can see, path can see
            if (InTargetRange() || transform.parent.parent.gameObject.GetComponent<SageOfSixPaths>().canSee == true)
            {
                if ((angle < SightRange || transform.parent.parent.gameObject.GetComponent<SageOfSixPaths>().canSee == true) && !InStopRange())
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
                    Patrol();
                }
            }
            else
            {
                Patrol();
            }
        }
        public override string GetClassTypeName()
        {
            return "EnemyPath";
        }
    }

}

