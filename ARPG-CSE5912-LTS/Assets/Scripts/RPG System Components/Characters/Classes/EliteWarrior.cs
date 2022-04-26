using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;
using ARPG.Core;

namespace ARPG.Combat
{
    public class EliteWarrior : EnemyController
    {

        protected override void Start()
        {
            base.Start();
            Range = 150f;
            BodyRange = 2.5f;
            AbilityRange = 80;
            SightRange = 100f;
            Speed = 1.5f;
            stats[StatTypes.MonsterType] = 2; //testing
            stats[StatTypes.PHYATK] = 200;//testing
            agent.speed = Speed;
        }
        public override string GetClassTypeName()
        {
            return "EliteWarrior";
        }

        protected override void Update()
        {
            base.Update();
        }
    }

}