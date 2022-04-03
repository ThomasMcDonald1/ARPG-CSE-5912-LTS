using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseHitRate : MonoBehaviour
{
    //TODO: Add events here for checking for if something automatically hits, automatically misses, etc

    protected Character attacker;

    protected virtual void Start()
    {
        attacker = GetComponentInParent<Character>();
    }

    /// <summary>
    /// Returns a value in the range of 0 to 100 as a percent chance of
    /// an ability succeeding to hit
    /// </summary>
    public abstract int Calculate(Character target);
    public abstract int CalculateBlock(Character target);

    public virtual bool RollForHit(Character target)
    {
        int roll = Random.Range(0, 101);
        int chance = Calculate(target);
        //Debug.Log("Hit roll: " + roll.ToString() + ", hit chance: " + chance.ToString() + ". Hit target? " + (roll <= chance).ToString());
        return roll <= chance;
    }

    public virtual bool RollForBlock(Character target)
    {
        int roll = Random.Range(0, 101);
        int chance = CalculateBlock(target);
        return roll <= chance;
    }

    //TODO: Write methods for what happens on an automatic hit, automatic miss, etc. here

    protected virtual int Final(int evade)
    {
        return 100 - evade;
    }
}
