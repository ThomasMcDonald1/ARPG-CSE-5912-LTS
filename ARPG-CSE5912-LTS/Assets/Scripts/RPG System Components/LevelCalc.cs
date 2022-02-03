using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCalc : MonoBehaviour
{
    Stats stats;

    public const int minLevel = 1;
    public const int maxLevel = 99;
    public const int maxExp = 999999;

    private void Awake()
    {
        stats = GetComponent<Stats>();    
    }

    public int LVL
    {
        get { return stats[StatTypes.LVL]; }
    }

    public int EXP
    {
        get { return stats[StatTypes.EXP];}
    }

    public float LevelPercent
    {
        get { return (float)(LVL - minLevel) / (float)(maxLevel - minLevel); }
    }

    private void OnEnable()
    {
        Stats.ExpWillBeGivenEvent += OnExpWillBeGiven;
        Stats.ExpHasBeenGivenEvent += OnExpHasBeenGiven;
    }

    private void OnDisable()
    {
        Stats.ExpWillBeGivenEvent -= OnExpWillBeGiven;
        Stats.ExpHasBeenGivenEvent -= OnExpHasBeenGiven;
    }

    private void OnExpWillBeGiven(object sender, InfoEventArgs<int> e)
    {
        //TODO: If exp will be given, but the player has some kind of equipment that will increase the amount of exp rewarded,
        //then apply the modification here
    }

    private void OnExpHasBeenGiven(object sender, InfoEventArgs<int> e)
    {
        stats.SetValue(StatTypes.LVL, LevelForExperience(EXP), false);
    }

    public static int ExperienceForLevel(int level)
    {
        float levelPercent = Mathf.Clamp01((float)(level - minLevel) / (float) (maxLevel - minLevel));
        return (int)EasingEquations.EaseInQuad(0, maxExp, levelPercent);
    }

    public static int LevelForExperience(int exp)
    {
        int lvl = maxLevel;
        for (; lvl >= minLevel; --lvl)
            if (exp >= ExperienceForLevel(lvl))
                break;
        return lvl;
    }

    public void Init(int level)
    {
        stats.SetValue(StatTypes.LVL, level, false);
        stats.SetValue(StatTypes.EXP, ExperienceForLevel(level), false);
    }
}
