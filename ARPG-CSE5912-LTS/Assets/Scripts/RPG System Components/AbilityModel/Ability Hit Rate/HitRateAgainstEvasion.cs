using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitRateAgainstEvasion : BaseHitRate
{
    public override int Calculate(Character target)
    {
        //TODO: Check for automatic hits or misses and return Final(0) or Final(100) respectively

        Stats stats = target.GetComponent<Stats>();
        int evade = (int)Mathf.Clamp(stats[StatTypes.Evasion], 0, 100);
        //TODO: Modify the evasion number when taking into account status effects, 
        //or other things which might change a character's evasion

        return Final(evade);
    }

    public override int CalculateBlock(Character target)
    {
        int block = (int)Mathf.Clamp(target.stats[StatTypes.BlockChance], 0, 100);
        return Final(block);
    }

    //TODO: Add methods here for modifying the evasion number
}
