using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace ARPG.Combat
{
    public abstract class EnemyClass : MonoBehaviour, IPlayerClass, IEnemyClass
    {
        public virtual float AttackRange { get; set; }
        public virtual Transform AttackTarget { get; set; }

        public abstract void Attack(EnemyTarget target);
        public abstract void Cancel(); //far from the born position

        public abstract bool InTargetRange();
        public abstract bool InStopRange();

        public abstract void AttackSignal(bool signal);

        public virtual float CurrentEnemyHealth { get; set; }
        public virtual float MaxEnemyHealth { get; set; }

        public virtual float Range { get; set; }
        public virtual float BodyRange { get; set; }
        public virtual float SightRange { get; set; }
        public virtual float Speed { get; set; }

        public abstract void SeePlayer();

        public abstract void RunToPlayer();

        public abstract void StopRun();
    }
}
