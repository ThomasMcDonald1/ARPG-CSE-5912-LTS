using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;
namespace ARPG.Combat
{
    public class EnemyKnight : Enemy
    {


        protected override void Start()
        {
          base.Start();
            AttackRange = 5;
            Range = 50f;
        //    //optimized body range
            BodyRange = 1.5f;
            SightRange = 90f;
            Speed = 2f;




        }

        //protected override void Update()
        //{
        //    base.Update();
        //}
    }

}
