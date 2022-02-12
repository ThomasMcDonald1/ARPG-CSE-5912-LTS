using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    public Stats stats;

    public const int minLevel = 1;
    public const int maxLevel = 99;
    public const int minExp = 0;
    public const int maxExp = 999999;

    public int LVL
    {
        get { return stats[StatTypes.LVL]; }
    }

    public int EXP
    {
        get { return stats[StatTypes.EXP]; }
    }

    public void Init(int level)
    {
        stats.SetValue(StatTypes.LVL, level, false);
        stats.SetValue(StatTypes.EXP, ExperienceForLevel(level), false);
    }

    /*
    public float CurrentExperiencePercent
    {
        get
        {
            return (float)EXP / (float)currentLevelExp(LVL);
        }
    }
    */

    public static int currentLevelExpToNext(int level)
    {
        return ExperienceForLevel(level + 1) - ExperienceForLevel(level); //enter current level, the EXP need to next level
    }

    public static int currentLevelExp(int exp, int level)
    {
        return exp - ExperienceForLevel(level);
    }

    public static int ExperienceForLevel(int level)
    {
        float levelPercent = Mathf.Clamp01((float)(level - minLevel) / (float)(maxLevel - minLevel));
        return (int)EaseInQuad(minExp, maxExp, levelPercent);
    }

    public static int LevelForExperience(int exp)
    {
        int lvl = maxLevel;
        for (; lvl >= minLevel; --lvl)
            if (exp >= ExperienceForLevel(lvl))
                break;
        return lvl;
    }

    private void OnEnable()
    {
        ExperienceManager.ExpWillBeGivenEvent += OnExpWillBeGiven;
        ExperienceManager.ExpHasBeenGivenEvent += OnExpHasBeenGiven;
    }

    private void OnDisable()
    {
        ExperienceManager.ExpWillBeGivenEvent -= OnExpWillBeGiven;
        ExperienceManager.ExpHasBeenGivenEvent -= OnExpHasBeenGiven;
    }

    public void OnExpWillBeGiven(object sender, InfoEventArgs<(int, int)> e)
    {
        //TODO: If exp will be given, but the player has some kind of equipment that will increase the amount of exp rewarded,
        //then apply the modification here
        stats[StatTypes.EXP] += stats[StatTypes.ExpGain];
        Debug.Log("setexp");
    }

    public void OnExpHasBeenGiven(object sender, InfoEventArgs<(int, int)> e)
    {
        stats.SetValue(StatTypes.LVL, LevelForExperience(EXP), false);
        Debug.Log("setlevel");
    }

    //From the script
    public static float EaseInQuad(float start, float end, float value)
    {
        end -= start;
        return end * value * value + start; //Change a little from the easeEquation script.
    }

}
