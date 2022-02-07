using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantCastType : BaseCastType
{
    //Character character;

    //private void Awake()
    //{
    //    character = GetComponentInParent<Character>();
    //}

    public override void WaitCastTime(Ability ability)
    {
        CompleteCast(ability);
    }

    public override void StopCasting()
    {
       
    }
}
