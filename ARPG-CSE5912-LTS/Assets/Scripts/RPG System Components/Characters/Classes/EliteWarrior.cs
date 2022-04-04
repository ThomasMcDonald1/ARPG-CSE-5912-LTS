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
            Range = 5f;
            BodyRange = 2.5f;
            SightRange = 90f;
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