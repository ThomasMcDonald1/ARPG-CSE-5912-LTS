using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ARPG.NPC
{
    public class Friendly : MonoBehaviour, IFriendly
    {
        private GameObject Player;

        void Start()
        {
            Player = GameObject.Find("Player");
        }
        public void InitiateDialogue()
        {

        }

        public void LookAt(GameObject target)
        {
            
        }
    }
}
