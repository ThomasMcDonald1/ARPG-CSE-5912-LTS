using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using System.Linq;
using System;
using Newtonsoft.Json;

public class DefaultStatReader : MonoBehaviour
{
    public static DefaultStatReader Instance;
    public enum CharacterIndex {
        Player,
        EnemyKnight,
        EliteWarrior,
        Kilixis,
        SageOfSixPaths,
        Stout
    }
    public TextAsset defaultStats;
    public IEnumerable<string> statNames;
    private CharacterStats characterStatsList;
    [System.Serializable]
    public class Character
    {
        public string name;
        public StatList stats;
    }
    [System.Serializable]
    public class StatList
    {
        public int LVL;
        public int EXP;
        public int SkillPoints;

        public int HP;
        public int MaxHP;
        public int Mana;
        public int MaxMana;
        public int PHYATK;

        public int AttackRange;
        public int Armor;
        public int PercentArmorBonus;
        public int AtkSpeed;
        public int CritChance;
        public int CritDamage;
        public int FlatArmorPen;
        public int FlatMagicPen;
        public int PercentArmorPen;
        public int PercentMagicPen;
        public int RunSpeed;
        public int HealthRegen;
        public int ManaRegen;
        public int ExpGainMod;
        public int PhysDmgBonus;
        public int MagDmgBonus;
        public int FireDmgBonus;
        public int ColdDmgBonus;
        public int LightningDmgBonus;
        public int PoisonDmgBonus;
        public int Lifesteal;
        public int CastSpeed;
        public int CooldownReduction;
        public int FireDmgOnHit;
        public int ColdDmgOnHit;
        public int LightningDmgOnHit;
        public int PoisonDmgOnHit;

        public int BlockChance;
        public int Evasion;
        public int DamageReflect;
        public int FireRes;
        public int ColdRes;
        public int LightningRes;
        public int PoisonRes;
        public int PercentAllResistBonus;
        public int PercentFireResistBonus;
        public int PercentColdResistBonus;
        public int PercentLightningResistBonus;
        public int PercentPoisonResistBonus;
    }
    [System.Serializable]
    public class CharacterStats
    {
        public Character[] result;
    }

    void Start()
    {
        DontDestroyOnLoad(this);
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        characterStatsList = JsonUtility.FromJson<CharacterStats>("{\"result\":" + defaultStats.ToString() + "}");
        statNames = typeof(StatList).GetFields(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public).Select(f => f.Name);
    }
    public void InitializeStats(CharacterIndex index)
    {
        string objToInitialize = characterStatsList.result[(int)index].name;
        GameObject obj = GameObject.Find(objToInitialize);
        Stats objStats = obj.GetComponent<Stats>();
        foreach (string stat in statNames)
        {
            Enum.TryParse(stat, out StatTypes statName);
            StatList statList = characterStatsList.result[(int)index].stats;
            int statValue = (int)statList.GetType().GetField(stat).GetValue(statList);
            objStats[statName] = statValue;
            // Debug.Log($"{statName} : {statValue}");
        }
    }
    public void InitializeStats(CharacterIndex index, GameObject obj)
    {
        string objToInitialize = characterStatsList.result[(int)index].name;
        Stats objStats = obj.GetComponent<Stats>();
        foreach (string stat in statNames)
        {
            Enum.TryParse(stat, out StatTypes statName);
            StatList statList = characterStatsList.result[(int)index].stats;
            int statValue = (int)statList.GetType().GetField(stat).GetValue(statList);
            objStats[statName] = statValue;
            // Debug.Log($"{statName} : {statValue}");
        }
    }
    public void ScaleEnemyStats(CharacterIndex index, GameObject obj)
    {
        // Debug.Log(obj.name);
        Stats objStats = obj.GetComponent<Stats>();
        double currentLevel = objStats[StatTypes.LVL];
        foreach (string stat in statNames)
        {
            if (stat == "LVL") continue;
            Enum.TryParse(stat, out StatTypes statName);
            StatList statList = characterStatsList.result[(int)index].stats;
            int startingStat = (int)statList.GetType().GetField(stat).GetValue(statList);
            objStats[statName] = (int)(startingStat * Math.Pow(1.1, currentLevel-1));
            // Debug.Log($"{statName} : {(int)(startingStat * Math.Pow(1.1, currentLevel-1))}");
        }
    }
}
