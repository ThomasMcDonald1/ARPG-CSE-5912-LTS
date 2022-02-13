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
        (int monsterLevel, int monsterType) = e.info;
        //Now just set the monsterType for the multiple data.

        int lvl = stats[StatTypes.LVL];
        int gap = monsterLevel - stats[StatTypes.LVL];
        if (lvl < 25)
        {
            stats[StatTypes.ExpGain] = (int)((float)monsterType * (float)monsterLevel * ((float)(100 + gap * 4) / 100f));
            //if player level is less than 25, for example,
            //we have basic exp = 1
            //player level = 24, monster level = 9, monsterType will cause the exp *=50.
            //the gain will be 50 * 9 * (100 - 15 * 4)% = 180
            //Gain about monster level = 9 is 180 - 450 (lvl 24 -> 9), and 450 - 594 (lvl 9 -> 1)
            //Gain about player level = 24 is 1200 - 19800 (monsterlvl 24 -> 99), and 1200 - 4 (monsterlvl 24 ->1 )
            //Gain about player level = 1 is 50 - 24354 (monsterlvl 1 -> 99)
        }
        else
        {
            stats[StatTypes.ExpGain] = (int)((float)monsterType * (float)monsterLevel *((float)monsterLevel/ (float)stats[StatTypes.LVL]));
            //if level is greater than 25, for example,
            //we have basic exp = 1
            //player level = 25, monster level = 9, monsterType will cause the exp *= 50.
            //the gain will be 50 * 9 * 9/25 = 162
            //Gain about monster level = 9 is 162 - 41 (lvl 25 -> 99)
            //Gain about player level = 25 is 1250 - 19602 (monsterlvl 25 -> 99), and 1250 - 2 (monsterlvl 25 ->1 )
            //Gain about player level = 99 is 1 - 4950 (monsterlvl 1 -> 99)
        }

        stats[StatTypes.ExpGain] *= (int)((float)(100 + stats[StatTypes.ExpGainMod])/100f); //if mod is 0, just set normal expGain
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
