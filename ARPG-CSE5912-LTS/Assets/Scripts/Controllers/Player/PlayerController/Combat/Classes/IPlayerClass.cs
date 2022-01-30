using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using ARPG.Movement;

namespace ARPG.Combat
{
    public interface IPlayerClass
    {
        float AttackRange { get; set; }
        public abstract void Attack(EnemyTarget target);
        public abstract void CancelAttack();
    }
}
