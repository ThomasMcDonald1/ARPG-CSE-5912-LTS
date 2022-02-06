using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace ARPG.Combat
{
    public class EnemyKnight : Enemy
    {


        protected override void Start()
        {
            base.Start();
            AttackRange = 13.0f;
            Range = 20.0f;
            BodyRange = 1.5f;
            SightRange = 90f;
            Speed = 2f;

            //Stats
            stats[StatTypes.MAXHEALTH] = 2600;
            stats[StatTypes.HEALTH] = stats[StatTypes.MAXHEALTH];
            stats[StatTypes.PHYATK] = 120;
            stats[StatTypes.PHYDEF] = 30;
            stats[StatTypes.ATKSPD] = 120;
        }

        protected override void Update()
        {
            base.Update();
        }
    }

}
