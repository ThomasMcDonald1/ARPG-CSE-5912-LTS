using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace ARPG.Combat
{
    public class HumanEnemy : Enemy
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
            statScript[StatTypes.MAXHEALTH] = 2600;
            statScript[StatTypes.HEALTH] = statScript[StatTypes.MAXHEALTH];
            statScript[StatTypes.PHYATK] = 120;
            statScript[StatTypes.PHYDEF] = 30;
            statScript[StatTypes.ATKSPD] = 120;
        }

        protected override void Update()
        {
            base.Update();
        }
    }

}
