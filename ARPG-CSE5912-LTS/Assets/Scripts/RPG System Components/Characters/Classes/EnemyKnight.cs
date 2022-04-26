using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;
using ARPG.Core;

namespace ARPG.Combat
{
    public class EnemyKnight : EnemyController
    {
        protected override void Start()
        {
            base.Start();
            Range = 10f;
            BodyRange = 1.5f;
            AbilityRange = 70;
            SightRange = 180;
            Speed = 3f;
            agent.speed = Speed;
            stats[StatTypes.MonsterType] = 1; //testing
            cooldownTimer = 6;
            timeChecker = cooldownTimer;

            /*
            if (abilitiesKnown != null)
            {
                //Debug.Log(abilitiesKnown.Count);
                for (int i = 0; i < abilitiesKnown.Count; i++)
                {
                    EnemyAbility enemyability = new EnemyAbility();
                    enemyability.abilityAssigned = abilitiesKnown[i];
                    EnemyAttackTypeList.Add(enemyability);
                }
            }
            */
        }

        public override string GetClassTypeName()
        {
            return "EnemyKnight";
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


        protected override void Update()
        {
            if (Vector3.Distance(transform.position, player.transform.position) <= 18)
            {
                base.Update();
            }
        }
    }

}
