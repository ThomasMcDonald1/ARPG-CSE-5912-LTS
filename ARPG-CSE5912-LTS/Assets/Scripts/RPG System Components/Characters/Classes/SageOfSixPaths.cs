using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ARPG.Combat
{


    public class SageOfSixPaths : Enemy
    {
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
            stats[StatTypes.MonsterType] = 1; //testing
        }
        protected override void Update()
        {
            for(int i = 0; i < pathsVision.Length; i++)
            {
                pathsVision[i] = transform.GetChild(3).transform.GetChild(i).GetComponent<Paths>().canSee;
            }
            for (int i = 0; i < pathsVision.Length; i++)
            {
                if(pathsVision[i] == true)
                {
                    canSee = true;
                }
                else if (i == pathsVision.Length - 1)
                {
                    canSee = false;
                }
            }

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

