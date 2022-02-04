using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseCastType : MonoBehaviour
{
    public int castTime;

    //TODO: Not sure how to go about doing this yet. Need to do different kinds of casts depending on what type of ability it is
    // 1) casts could be instant
    // 2) casts could require standing still as you wait for a cast timer to finish, then the full effect could happen at the end of the cast
    // 3) casts could be channeled, having a pulsating effect that takes place several times within the cast
    //For now, I have it set as an abstract class where the concrete classes that implement it will have to figure out how the ability should be cast
    public abstract void WaitCastTime();
}
