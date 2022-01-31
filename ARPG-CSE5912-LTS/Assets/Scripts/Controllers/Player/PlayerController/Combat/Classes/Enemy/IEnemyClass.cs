using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace ARPG.Combat
{
    public interface IEnemyClass
    {
        public abstract bool InStopRange();

        public abstract float CurrentEnemyHealth { get; set; }
        public abstract float MaxEnemyHealth { get; set; }

        public abstract float Range { get; set; }
        public abstract float BodyRange { get; set; }
        public abstract float SightRange { get; set; }
        public abstract float Speed { get; set; }

        public abstract void SeePlayer();

        public abstract void RunToPlayer();

        public abstract void StopRun();
    }
}
