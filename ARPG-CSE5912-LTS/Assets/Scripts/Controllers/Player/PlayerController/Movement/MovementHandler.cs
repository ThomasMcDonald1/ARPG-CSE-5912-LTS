using ARPG.Core;
using System;
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
        PlayerAbilityController playerAbilityController;

        public static event EventHandler<InfoEventArgs<int>> PlayerBeganMovingEvent;


        private void Start()
        {
            agent = GetComponentInParent<NavMeshAgent>();
            playerAbilityController = GetComponent<PlayerAbilityController>();
        }

        public void MoveToTarget(Vector3 target)
        {
            if (!playerAbilityController.playerInAOEAbilityTargetSelectionMode && !playerAbilityController.playerInSingleTargetAbilitySelectionMode)
            {
                PlayerBeganMovingEvent?.Invoke(this, new InfoEventArgs<int>(0));
                agent.destination = target;
                playerAbilityController.abilityQueued = false;
                agent.isStopped = false;
            }
        }

        public void Cancel()
        {
            agent.isStopped = true;
        }
    }
}

