using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitRateGuaranteed : BaseHitRate
{
    public override int Calculate(Character target)
    {
        return Final(0);
    }

    public override int CalculateBlock(Character target)
    {
        return Final(0);
    }
}
