using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ARPG.NPC
{
    public interface IFriendly
    {
        public abstract void InitiateDialogue();

        public abstract void LookAt(GameObject target);
    }
}
