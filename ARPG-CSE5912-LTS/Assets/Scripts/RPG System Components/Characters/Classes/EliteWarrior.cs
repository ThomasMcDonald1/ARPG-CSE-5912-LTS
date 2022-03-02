using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;
namespace ARPG.Combat
{
    public class EliteWarrior : Enemy
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

        //protected override void Update()
        //{
        //    base.Update();
        //}
    }

}