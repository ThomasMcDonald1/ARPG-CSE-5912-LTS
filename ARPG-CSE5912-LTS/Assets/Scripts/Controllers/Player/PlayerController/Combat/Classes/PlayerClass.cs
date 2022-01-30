using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;
using ARPG.Movement;

namespace ARPG.Combat
{
    public abstract class PlayerClass : MonoBehaviour, IPlayerClass
    {
        
        public abstract float AttackRange { get; set; }

        public abstract void Attack(EnemyTarget target);

        public abstract void CancelAttack();
    }
}
