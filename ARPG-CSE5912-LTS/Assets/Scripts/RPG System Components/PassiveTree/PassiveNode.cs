using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveNode
{
    public string Name;
    public StatTypes[] Stats { get; set; }
    public int[] StatValues { get; set; }
    public string[] Prerequisites { get; set; }
    public bool Unlockable { get; set; }
    public bool Unlocked { get; set; }
    public PassiveNode(string name, StatTypes[] stats, int[] statValues, string[] prerequisites,
                        bool unlockable = false, bool unlocked = false)
    {
        Name = name;
        Stats = stats;
        StatValues = statValues;
        Prerequisites = prerequisites;
        Unlockable = unlockable;
        Unlocked = unlocked;
    }
}
