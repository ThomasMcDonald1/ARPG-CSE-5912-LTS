using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;
namespace ARPG.Combat
{
    public class EnemyKnight : EnemyAbilityController
    {

        protected override void Start()
        {
            base.Start();
            Range = 5f;
            BodyRange = 1.5f;
            SightRange = 90f;
            Speed = 2f;
            agent.speed = Speed;
            stats[StatTypes.MonsterType] = 1; //testing
            cooldownTimer = 6;
            
        }
        /*
        protected override void Update()
        {
            if (abilitiesKnown != null)
            {
                Debug.Log(abilitiesKnown.Count);
                for (int i = 0; i < abilitiesKnown.Count; i++)
                {
                    EnemyAbility enemyability = new EnemyAbility();
                    enemyability.abilityAssigned = abilitiesKnown[i];
                    EnemyAttackTypeList.Add(enemyability);
                }
            }
        }
        */


            //protected override void Update()
            //{
            //    base.Update();
            //}
        }

}
