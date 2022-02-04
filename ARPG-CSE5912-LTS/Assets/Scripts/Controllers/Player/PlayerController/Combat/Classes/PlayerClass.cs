using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;
using ARPG.Movement;
using ARPG.Core;

namespace ARPG.Combat
{
    public abstract class PlayerClass : MonoBehaviour, IPlayerClass
    {
        public virtual float AttackRange { get; set; }
        public virtual Transform AttackTarget { get; set; }

        public abstract void Attack(EnemyTarget target);

        public abstract void Cancel();

        public abstract bool InTargetRange();

        public abstract void AttackSignal(bool signal);
    }
}
