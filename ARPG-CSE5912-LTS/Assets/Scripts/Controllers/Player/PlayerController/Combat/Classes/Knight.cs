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
        Player player;
        public override float AttackRange { get; set; }
        private float attackRange;

        private GameObject GeneralClass;
        Transform target;

        private void Start()
        {
            GeneralClass = GameObject.Find("Class");
            attackRange = 2f;
        }

        private void Update()
        {
            if (target != null)
            {
                if (Vector3.Distance(GeneralClass.transform.position, target.position) > attackRange)
                {
                    GeneralClass.GetComponent<MovementHandler>().NavMeshAgent.isStopped = false;
                    GeneralClass.GetComponent<MovementHandler>().MoveToTarget(target.position);
                }
                else
                {
                    GeneralClass.GetComponent<MovementHandler>().NavMeshAgent.isStopped = true;
                }
            }
        }

        public override void Attack(EnemyTarget target)
        {
            this.target = target.transform;
        }

        public override void CancelAttack()
        {
            target = null;
        }
    }
}
