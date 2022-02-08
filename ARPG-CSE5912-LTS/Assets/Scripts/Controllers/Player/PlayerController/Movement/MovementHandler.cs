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
        Player player;

        public static event EventHandler<InfoEventArgs<int>> PlayerBeganMovingEvent;


        private void Start()
        {
            agent = GetComponentInParent<NavMeshAgent>();
            player = GetComponent<Player>();
        }

        public void MoveToTarget(Vector3 target)
        {
            if (!player.playerInAOEAbilityTargetSelectionMode && !player.playerInSingleTargetAbilitySelectionMode)
            {
                PlayerBeganMovingEvent?.Invoke(this, new InfoEventArgs<int>(0));
                agent.destination = target;
                player.abilityQueued = false;
                agent.isStopped = false;
            }
        }

        public void Cancel()
        {
            agent.isStopped = true;
        }
    }
}

