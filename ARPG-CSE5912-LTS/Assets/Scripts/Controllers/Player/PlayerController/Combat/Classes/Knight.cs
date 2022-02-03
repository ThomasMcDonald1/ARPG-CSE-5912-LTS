using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using ARPG.Movement;
using ARPG.Core;

namespace ARPG.Combat
{
    public class Knight : PlayerClass
    {
        private GameObject GeneralClass;
        private bool signalAttack;
        public Stats statScript;
        public bool death;
        float smooth;
        float yVelocity;
        private void Start()
        {
            GeneralClass = GameObject.Find("Class");
            AttackRange = 2f;
            AttackTarget = null;
            statScript = GetComponent<Stats>();

            //rotation data
            smooth = 0.3f;
            yVelocity = 0.0f;
            GetComponent<Animator>().SetBool("StopAttack", true);
            GetComponent<Animator>().SetBool("Dead", false);

        }

        private void Update()
        {

            if (statScript.health <= 0)
            {
                GetComponent<Animator>().SetBool("Dead", true);
            }else
            {

                if (AttackTarget != null)
                {
                    GetComponent<Animator>().SetBool("StopAttack", false);
                    if (!InTargetRange())
                    {
                        GeneralClass.GetComponent<MovementHandler>().NavMeshAgent.isStopped = false;
                        GeneralClass.GetComponent<MovementHandler>().MoveToTarget(AttackTarget.position);
                    }
                    else if (InTargetRange() && signalAttack)
                    {
                        GeneralClass.GetComponent<MovementHandler>().Cancel();
                        GetComponent<Animator>().SetTrigger("AttackTrigger");
                        //rotation toward enemy
                        Quaternion rotationToLookAt = Quaternion.LookRotation(AttackTarget.transform.position - transform.position);
                        float rotationY = Mathf.SmoothDampAngle(transform.eulerAngles.y,
                        rotationToLookAt.eulerAngles.y, ref yVelocity, smooth);

                        transform.eulerAngles = new Vector3(0, rotationY, 0);
                    }
                    else if (InTargetRange() && !signalAttack)//doesnt get triggered?
                    {
                        GeneralClass.GetComponent<MovementHandler>().Cancel();
                        GetComponent<Animator>().SetTrigger("AttackTrigger");
                        Debug.Log("triggered");
                        Cancel();
                    }
                }
            }
            Debug.Log(GetComponent<Animator>().GetBool("StopAttack"));

        }



        public override void Attack(EnemyTarget target)
        {
            AttackTarget = target.transform;
        }

        public override void Cancel()
        {
            AttackTarget = null;
            GetComponent<Animator>().SetBool("StopAttack", true);


        }

        


        public override bool InTargetRange()
        {
            if (AttackTarget == null) return false;
            return Vector3.Distance(GeneralClass.transform.position, AttackTarget.position) < AttackRange;
        }

        public override void AttackSignal(bool signal)
        {
            signalAttack = signal;
        }
        
        // Needed for the animation event
        public void Hit()
        {
            if (AttackTarget != null)
            {
                AttackTarget.GetComponent<Stats>().health -= statScript.attackDmg;
            }
        }

        public void Dead()
        {
            //
        }

        public void ProduceItem()
        {
            Debug.Log("Item or experience off");
        }
    }
}
