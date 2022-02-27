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
            AttackRange = 13.0f;
            Range = 20f;
            //    //optimized body range
            BodyRange = 3f;
            SightRange = 90f;
            Speed = 1f;




        }

        //protected override void Update()
        //{
        //    base.Update();
        //}
    }

}