using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using System.Linq;

public class DefaultStatReader : MonoBehaviour
{
    public TextAsset defaultStats;
    // Start is called before the first frame update
    [System.Serializable]
    public class Character
    {
        public string name;
        public Stats stats;
    }
    [System.Serializable]
    public class Stats
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
        public int CostReduction;
        public int FireDmgOnHitMain;
        public int ColdDmgOnHitMain;
        public int LightningDmgOnHitMain;
        public int PoisonDmgOnHitMain;
        public int FireDmgOnHitOff;
        public int ColdDmgOnHitOff;
        public int LightningDmgOnHitOff;
        public int PoisonDmgOnHitOff;

        public int BlockChance;
        public int BlockAmount;
        public int DodgeChance;
        public int DeflectChance;
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
    public class PlayerStats
    {
        public Character[] result;
    }

    // Update is called once per frame
    void Start()
    {
        var playerStats = JsonUtility.FromJson<PlayerStats>("{\"result\":" + defaultStats.ToString() + "}");
        IEnumerable<string> variableNames = typeof(Stats).GetFields(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public).Select(f => f.Name);
        foreach (var x in variableNames)
        {

        }
    }
}
