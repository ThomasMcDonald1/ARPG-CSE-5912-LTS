using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class LevelController : MonoBehaviour
{
    public Stats stats;
    public GameObject skillNotification;
    public Text remainingPoints;
    public const int minLevel = 1;
    public const int maxLevel = 99;
    public const int minExp = 0;
    public const int maxExp = 999999;
    private Player player;
    public int LVL
    {
        get { return stats[StatTypes.LVL]; }
    }

    public int EXP
    {
        get { return stats[StatTypes.EXP]; }
    }

    void Awake()
    {
        player = FindObjectOfType<Player>();
    }
    public void Init(int level)
    {
        stats.SetValue(StatTypes.LVL, level, false);
        stats.SetValue(StatTypes.EXP, ExperienceForLevel(level), false);
    }


    public float TotalExperiencePercent
    {
        get
        {
                return (float)stats[StatTypes.EXP] / (float) maxExp;
        }
    }

    public float CurrentExperiencePercent
    {
        get
        {
            if (currentLevelExpToNext(stats[StatTypes.LVL]) > 0)
            {
                return (float)currentLevelExp(stats[StatTypes.EXP], stats[StatTypes.LVL]) / (float)currentLevelExpToNext(stats[StatTypes.LVL]);

            }
            else
            {
                return 0;
            }
        }
    }


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
        AbilityShopSlot.SkillpointUsedToBuyAbilityEvent += OnSkillPointUsedToBuyAbility;
    }

    private void OnDisable()
    {
        ExperienceManager.ExpWillBeGivenEvent -= OnExpWillBeGiven;
        ExperienceManager.ExpHasBeenGivenEvent -= OnExpHasBeenGiven;
        AbilityShopSlot.SkillpointUsedToBuyAbilityEvent -= OnSkillPointUsedToBuyAbility;
    }

    private void OnSkillPointUsedToBuyAbility(object sender, InfoEventArgs<int> e)
    {
        if (stats[StatTypes.SkillPoints] == 0)
            skillNotification.SetActive(false);
    }

    public void OnExpWillBeGiven(object sender, InfoEventArgs<(int, int,string)> e)
    {
        int monsterLevel = e.info.Item1;
        int monsterType = e.info.Item2;
        //Now just set the monsterType for the multiple data.

        int lvl = stats[StatTypes.LVL];
        int gap = monsterLevel - stats[StatTypes.LVL];
        int typeConstant = 1;
        switch (monsterType)
        {
            case 1: //normal
                typeConstant = 50;
                break;
            case 2: //elite
                typeConstant = 100;
                break;
            case 3: //boss
                typeConstant = 500;
                break;
            default:
                Debug.Log("Need to set up monster type for enemy");
                break;
        }
        if (lvl < 25)
        {
            stats[StatTypes.ExpGain] = (int)((float)typeConstant * (float)monsterLevel * ((float)(100 + gap * 4) / 100f));
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
            stats[StatTypes.ExpGain] = (int)((float)typeConstant * (float)monsterLevel *((float)monsterLevel/ (float)stats[StatTypes.LVL]));
            //if level is greater than 25, for example,
            //we have basic exp = 1
            //player level = 25, monster level = 9, monsterType will cause the exp *= 50.
            //the gain will be 50 * 9 * 9/25 = 162
            //Gain about monster level = 9 is 162 - 41 (lvl 25 -> 99)
            //Gain about player level = 25 is 1250 - 19602 (monsterlvl 25 -> 99), and 1250 - 2 (monsterlvl 25 ->1 )
            //Gain about player level = 99 is 1 - 4950 (monsterlvl 1 -> 99)
        }

        stats[StatTypes.ExpGain] = (int)((float)stats[StatTypes.ExpGain] * (float)(100 + stats[StatTypes.ExpGainMod])/100f); //if mod is 0, just set normal expGain
        stats[StatTypes.EXP] += stats[StatTypes.ExpGain];
        stats[StatTypes.ExpGain] = 0; //not sure if set 0 here, need more test
        Debug.Log("setexp");
    }

    public void OnExpHasBeenGiven(object sender, InfoEventArgs<(int, int,string)> e)
    {
        int pastLVL = (int)stats[StatTypes.LVL];
        stats.SetValue(StatTypes.LVL, LevelForExperience(EXP), false);
        int currectLVL = (int)stats[StatTypes.LVL];
        stats[StatTypes.SkillPoints] += (currectLVL - pastLVL);
        if (currectLVL - pastLVL > 0)
        {
            remainingPoints.text = stats[StatTypes.SkillPoints].ToString();
            Vector3 pPos = player.transform.position;
            NavMeshHit hit;
            if (NavMesh.SamplePosition(pPos, out hit, 1.0f, NavMesh.AllAreas))
            {
                GameObject x = Instantiate(Resources.Load("LevelUpAnimation") as GameObject, hit.position, Quaternion.identity);
                x.transform.parent = player.transform;
                ParticleSystem parts = x.GetComponent<ParticleSystem>();
                Destroy(x, parts.main.duration);

            }
            FindObjectOfType<AudioManager>().Play("LevelUp");
            skillNotification.SetActive(true);
            stats[StatTypes.HP] = stats[StatTypes.MaxHP];
            stats[StatTypes.Mana] = stats[StatTypes.MaxMana];
            //TODO: Play sound effect or show vfx
        }
    }

    //public void OnSavedExpEvent(object sender, InfoEventArgs<(int, int)> e)
    //{
    //    Debug.Log("save");
    //    stats[StatTypes.SavedExp] += stats[StatTypes.EXP];
    //    stats.SetValue(StatTypes.EXP, 0, false);
    //    stats.SetValue(StatTypes.LVL, LevelForExperience(EXP), false);
    //}

    //public void OnGetBackExpEvent(object sender, InfoEventArgs<(int, int)> e)
    //{
    //    stats[StatTypes.SavedExp] = (int)((float)stats[StatTypes.SavedExp] * 0.7f);
    //    //I am now take away all exp and maybe will change for only this level exp.
    //    stats.SetValue(StatTypes.EXP, stats[StatTypes.SavedExp], false);
    //    stats.SetValue(StatTypes.LVL, LevelForExperience(EXP), false);
    //}

    //public void OnEmptyExpEvent(object sender, InfoEventArgs<(int, int)> e)
    //{
    //    stats.SetValue(StatTypes.SavedExp, 0, false);
    //    stats.SetValue(StatTypes.EXP, 0, false);
    //    stats.SetValue(StatTypes.LVL, LevelForExperience(EXP), false);
    //}

    //From the script
    public static float EaseInQuad(float start, float end, float value)
    {
        end -= start;
        return end * value * value + start; //Change a little from the easeEquation script.
    }

}
