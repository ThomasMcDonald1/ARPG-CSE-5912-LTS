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
            Range = 90f;
            BodyRange = 2.5f;
            AbilityRange = 80f;
            FarAwayRange = 4f;
            SightRange = 100f;
            Speed = 1f;
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