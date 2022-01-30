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

        private void Start()
        {
            GeneralClass = GameObject.Find("Class");
            AttackRange = 2f;
            AttackTarget = null;
        }

        private void Update()
        {
            if (AttackTarget != null)
            {
                if (!InTargetRange())
                {
                    GeneralClass.GetComponent<MovementHandler>().NavMeshAgent.isStopped = false;
                    GeneralClass.GetComponent<MovementHandler>().MoveToTarget(AttackTarget.position);
                }
                else if (InTargetRange() && signalAttack)
                {
                    GeneralClass.GetComponent<MovementHandler>().Cancel();
                    GetComponent<Animator>().SetTrigger("AttackTrigger");
                }
                else if (InTargetRange() && !signalAttack)
                {
                    GeneralClass.GetComponent<MovementHandler>().Cancel();
                    GetComponent<Animator>().SetTrigger("AttackTrigger");
                    Cancel();
                }
            }
        }

        


        public override void Attack(EnemyTarget target)
        {
            AttackTarget = target.transform;
        }

        public override void Cancel()
        {
            AttackTarget = null;
        }

        // Needed for the animation hit event
        void Hit()
        {

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
    }
}
