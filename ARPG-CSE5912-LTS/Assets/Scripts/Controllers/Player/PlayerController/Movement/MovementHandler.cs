using ARPG.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace ARPG.Movement
{
    public class MovementHandler : MonoBehaviour, IAction
    {
        public NavMeshAgent NavMeshAgent { get { return agent; } }
        private NavMeshAgent agent;

        private void Start()
        {
            agent = GetComponentInParent<NavMeshAgent>();    
        }

        public void MoveToTarget(Vector3 target)
        {
            agent.isStopped = false;
            agent.destination = target;
        }

        public void StopMoving()
        {
            agent.isStopped = true;
        }

        public void Cancel()
        {
            throw new System.NotImplementedException();
        }
    }
}

