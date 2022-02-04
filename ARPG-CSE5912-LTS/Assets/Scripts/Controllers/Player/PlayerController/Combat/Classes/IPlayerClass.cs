using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using ARPG.Movement;
using ARPG.Core;

namespace ARPG.Combat
{
    public interface IPlayerClass
    {
        public float AttackRange { get; set; }
        public Transform AttackTarget { get; set; }
        public abstract void Attack(EnemyTarget target);

        public abstract void Cancel();

        public abstract bool InTargetRange();

        public abstract void AttackSignal(bool signal);
    }
}
