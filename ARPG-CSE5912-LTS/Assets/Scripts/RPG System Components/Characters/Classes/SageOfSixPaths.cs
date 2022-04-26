using ARPG.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ARPG.Combat
{
    public class SageOfSixPaths : EnemyController
    {
        //if sage can see, all of its paths can see, if one path can see, sage can see
        public bool canSee = false;
        public bool[] pathsVision = new bool[] { false,false,false,false,false};

        protected override void Start()
        {
            base.Start();
            Range = 5f;
            BodyRange = 1.5f;
            SightRange = 90f;
            Speed = 2f;
            agent.speed = Speed;
            stats[StatTypes.MonsterType] = 2; //testing
        }
        protected override void Update()
        {
            //updates vision of all the paths
            for(int i = 0; i < pathsVision.Length; i++)
            {
                pathsVision[i] = transform.GetChild(3).transform.GetChild(i).GetComponent<Paths>().canSee;
            }
            canSee = false;
            //if sage can see, all of its paths can see, if one path can see, sage can see, if no paths can see, sage can not see
            for (int i = 0; i < pathsVision.Length; i++)
            {
                if(pathsVision[i] == true)
                {
                    canSee = true;
                    break;
                }
            }

            if (animator.GetBool("Dead") == false)
            {
                if (stats[StatTypes.HP] <= 0)
                {
                    if (animator.GetBool("Dead") == false)
                    {
                        Dead();
                        animator.SetBool("Dead", true);
                        //get rid of enemy canvas
                        transform.GetChild(2).gameObject.SetActive(false);
                    }
                }
            }
        }

        public override string GetClassTypeName()
        {
            return "EnemySage";
        }
    }
}

