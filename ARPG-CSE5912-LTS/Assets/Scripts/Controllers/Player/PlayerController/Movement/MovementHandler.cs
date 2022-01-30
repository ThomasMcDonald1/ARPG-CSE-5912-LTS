using ARPG.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace ARPG.Movement
{
    public class MovementHandler : MonoBehaviour
    {
        public NavMeshAgent NavMeshAgent { get { return agent; } }
        NavMeshAgent agent;

        private void Start()
        {
            agent = GetComponentInParent<NavMeshAgent>();    
        }

        public void MoveToTarget(Vector3 target)
        {
            agent.destination = target;
            agent.isStopped = false;
        }

        public void Cancel()
        {
            agent.isStopped = true;
        }
    }
}

