using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ARPG.Combat
{


    public class SageOfSixPaths : Enemy
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
        }
        protected override void Update()
        {
            if (GetComponent<Animator>().GetBool("Dead") == false)
            {
                if (stats[StatTypes.HP] <= 0)
                {
                    if (GetComponent<Animator>().GetBool("Dead") == false)
                    {
                        Dead();
                        GetComponent<Animator>().SetBool("Dead", true);
                        //get rid of enemy canvas
                        GetComponent<Transform>().GetChild(2).gameObject.SetActive(false);

                    }
                }
            }
        }
    }
}

