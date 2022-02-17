using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveSkillInfo
{
    public PassiveNode[] passiveTree;
    public PassiveSkillInfo()
    {
        CreatePassiveInfo();
    }

    public void CreatePassiveInfo()
    {
        passiveTree = new PassiveNode[]
        {
            new PassiveNode(
                "HEALTH1",
                new StatTypes[] { StatTypes.MaxHP},
                new int[] { 20 },
                new string[] { "STR1", "HPREGEN1"},
                true
            ),
            new PassiveNode(
                "STR1",
                new StatTypes[] { StatTypes.STR },
                new int[] { 5 },
                new string[] { "HEALTH1", "STR2", "ARMOR1" }
            ),
            new PassiveNode(
                "STR2",
                new StatTypes[] { StatTypes.STR },
                new int[] { 10 },
                new string[] { "STR1", "ARMOR1", "HPREGEN2", "STR3" }
            ),
            new PassiveNode(
                "STR3",
                new StatTypes[] { StatTypes.STR },
                new int[] { 10 },
                new string[] { "STR2", "STR4" }
            ),
            new PassiveNode(
                "STR4",
                new StatTypes[] { StatTypes.STR },
                new int[] { 10 },
                new string[] { "STR3", "HPREGEN4", "ARMOR3" }
            ),
            new PassiveNode(
                "STR5",
                new StatTypes[] { StatTypes.STR },
                new int[] { 10 },
                new string[] { "STR4", "ATKSPD4", "ARMOR3", "DODGE4", "LIFESTEAL4" }
            ),
            new PassiveNode(
                "ARMOR1",
                new StatTypes[] { StatTypes.PHYDEF },
                new int[] { 10 },
                new string[] { "STR1" }
            ),
            new PassiveNode(
                "ARMOR2",
                new StatTypes[] { StatTypes.PHYDEF },
                new int[] { 10 },
                new string[] { "ARMOR1", "ARMOR3" }
            ),
            new PassiveNode(
                "ARMOR3",
                new StatTypes[] { StatTypes.PHYDEF },
                new int[] { 10 },
                new string[] { "ARMOR2", "STR4", "DODGE4", "MOVE2" }
            ),
            new PassiveNode(
                "HPREGEN1",
                new StatTypes[] { StatTypes.HealthRegen },
                new int[] { 2 },
                new string[] { "HEALTH1" }
            ),
            new PassiveNode(
                "HPREGEN2",
                new StatTypes[] { StatTypes.HealthRegen },
                new int[] { 3 },
                new string[] { "HPREGEN1", "HPREGEN3", "ATKSPD1", "STR2" }
            ),
            new PassiveNode(
                "HPREGEN3",
                new StatTypes[] { StatTypes.HealthRegen },
                new int[] { 4 },
                new string[] { "HPREGEN2", "HPREGEN4" }
            ),
            new PassiveNode(
                "HPREGEN4",
                new StatTypes[] { StatTypes.HealthRegen },
                new int[] { 4 },
                new string[] { "HPREGEN3", "ATKSPD3", "STR4" }
            ),
            new PassiveNode(
                "ATKSPD1",
                new StatTypes[] { StatTypes.AtkSpeed },
                new int[] { 5 },
                new string[] { "HPREGEN1", "HPREGEN2", "ATKSPD2" }
            ),
            new PassiveNode(
                "ATKSPD2",
                new StatTypes[] { StatTypes.AtkSpeed },
                new int[] { 5 },
                new string[] { "ATKSPD1", "ATKSPD3" }
            ),
            new PassiveNode(
                "ATKSPD3",
                new StatTypes[] { StatTypes.AtkSpeed },
                new int[] { 5 },
                new string[] { "ATKSPD2", "HPREGEN4", "BLOCKCHANCE3", "BLOCKCHANCE4" }
            ),
            new PassiveNode(
                "ATKSPD4",
                new StatTypes[] { StatTypes.AtkSpeed },
                new int[] { 5 },
                new string[] { "ATKSPD3", "HPREGEN4", "STR5", "BLOCKCHANCE3", "CASTSPD4" }
            ),
            new PassiveNode(
                "MOVE1",
                new StatTypes[] { StatTypes.RunSpeed },
                new int[] { 5 },
                new string[] { "DODGE1", "DODGE2", "MOVE2" }
            ),
            new PassiveNode(
                "MOVE2",
                new StatTypes[] { StatTypes.RunSpeed },
                new int[] { 5 },
                new string[] { "MOVE1", "ARMOR3" }
            ),
            new PassiveNode(
                "DODGE1",
                new StatTypes[] { StatTypes.DodgeChance },
                new int[] { 5 },
                new string[] { "DODGE2", "LIFESTEAL1", "MOVE1" }
            ),
            new PassiveNode(
                "DODGE2",
                new StatTypes[] { StatTypes.DodgeChance },
                new int[] { 5 },
                new string[] { "DODGE1", "DODGE3", "LIFESTEAL2" }
            ),
            new PassiveNode(
                "DODGE3",
                new StatTypes[] { StatTypes.DodgeChance },
                new int[] { 5 },
                new string[] { "DODGE2", "DODGE4" }
            ),
            new PassiveNode(
                "DODGE4",
                new StatTypes[] { StatTypes.DodgeChance },
                new int[] { 5 },
                new string[] { "DODGE3", "ARMOR3", "STR5", "LIFESTEAL3" }
            ),
            new PassiveNode(
                "DODGE4",
                new StatTypes[] { StatTypes.DodgeChance },
                new int[] { 5 },
                new string[] { "DODGE3", "ARMOR3", "STR5", "LIFESTEAL3" }
            ),
            new PassiveNode(
                "LIFESTEAL1",
                new StatTypes[] { StatTypes.Lifesteal },
                new int[] { 5 },
                new string[] { "DODGE1", "DEX1" }
            ),
            new PassiveNode(
                "LIFESTEAL2",
                new StatTypes[] { StatTypes.Lifesteal },
                new int[] { 5 },
                new string[] { "DODGE2", "DEX2" }
            ),
            new PassiveNode(
                "LIFESTEAL3",
                new StatTypes[] { StatTypes.Lifesteal },
                new int[] { 5 },
                new string[] { "DODGE4", "DEX4" }
            ),
            new PassiveNode(
                "LIFESTEAL4",
                new StatTypes[] { StatTypes.Lifesteal },
                new int[] { 5 },
                new string[] { "STR5", "DEX5" }
            ),
            new PassiveNode(
                "DEX1",
                new StatTypes[] { StatTypes.DEX },
                new int[] { 5 },
                new string[] { "LIFESTEAL1", "DEX2" }
            ),
            new PassiveNode(
                "DEX2",
                new StatTypes[] { StatTypes.DEX },
                new int[] { 5 },
                new string[] { "DEX1", "DEX3", "LIFESTEAL2", "CRIT1" }
            ),
            new PassiveNode(
                "DEX3",
                new StatTypes[] { StatTypes.DEX },
                new int[] { 5 },
                new string[] { "DEX2", "DEX4" }
            ),
            new PassiveNode(
                "DEX4",
                new StatTypes[] { StatTypes.DEX },
                new int[] { 5 },
                new string[] { "DEX3", "DEX5", "LIFESTEAL3", "CRIT3" }
            ),
            new PassiveNode(
                "DEX5",
                new StatTypes[] { StatTypes.DEX },
                new int[] { 5 },
                new string[] { "DEX4", "LIFESTEAL4", "CRIT3" }
            ),
            new PassiveNode(
                "CRIT1",
                new StatTypes[] { StatTypes.DEX },
                new int[] { 5 },
                new string[] { "CRIT2", "DEX1", "DEX2" }
            ),
            new PassiveNode(
                "CRIT2",
                new StatTypes[] { StatTypes.DEX },
                new int[] { 5 },
                new string[] { "CRIT1", "CRIT3" }
            ),
            new PassiveNode(
                "CRIT3",
                new StatTypes[] { StatTypes.DEX },
                new int[] { 5 },
                new string[] { "MP4", "CRIT2", "MPREGEN2", "DEX4", "DEX5" }
            ),
            new PassiveNode(
                "MP1",
                new StatTypes[] { StatTypes.MaxMana },
                new int[] { 5 },
                new string[] { "MP2", "CDR1", "MPREGEN1" }
            ),
            new PassiveNode(
                "MP2",
                new StatTypes[] { StatTypes.MaxMana },
                new int[] { 5 },
                new string[] { "MP1", "MP3", "CDR2", "MPREGEN1" }
            ),
            new PassiveNode(
                "MP3",
                new StatTypes[] { StatTypes.MaxMana },
                new int[] { 5 },
                new string[] { "MP2", "MP4" }
            ),
            new PassiveNode(
                "MP4",
                new StatTypes[] { StatTypes.MaxMana },
                new int[] { 5 },
                new string[] { "MP3", "CRIT3", "CASTSPD3", "DEX5" }
            ),
            new PassiveNode(
                "CASTSPD1",
                new StatTypes[] { StatTypes.CastSpeed },
                new int[] { 5 },
                new string[] { "CDR1", "INT1", "CASTSPD2" }
            ),
            new PassiveNode(
                "CASTSPD2",
                new StatTypes[] { StatTypes.CastSpeed },
                new int[] { 5 },
                new string[] { "CDR2", "INT1", "CASTSPD1", "CASTSPD2" }
            ),
            new PassiveNode(
                "CASTSPD3",
                new StatTypes[] { StatTypes.CastSpeed },
                new int[] { 5 },
                new string[] { "MP4", "INT3", "CASTSPD2", "CASTSPD4" }
            ),
            new PassiveNode(
                "CASTSPD4",
                new StatTypes[] { StatTypes.CastSpeed },
                new int[] { 5 },
                new string[] { "INT3", "INT4", "CASTSPD3", "DEX5", "ATKSPD4" }
            ),
            new PassiveNode(
                "MPREGEN1",
                new StatTypes[] { StatTypes.ManaRegen },
                new int[] { 5 },
                new string[] { "MP1", "MP2", "MPREGEN2" }
            ),
            new PassiveNode(
                "MPREGEN2",
                new StatTypes[] { StatTypes.ManaRegen },
                new int[] { 5 },
                new string[] { "CRIT3", "MPREGEN1" }
            ),
            new PassiveNode(
                "CDR1",
                new StatTypes[] { StatTypes.CooldownReduction },
                new int[] { 5 },
                new string[] { "CASTSPD1", "MP1" }
            ),
            new PassiveNode(
                "CDR2",
                new StatTypes[] { StatTypes.CooldownReduction },
                new int[] { 5 },
                new string[] { "CASTSPD2", "MP2" }
            ),
            new PassiveNode(
                "INT1",
                new StatTypes[] { StatTypes.INT },
                new int[] { 5 },
                new string[] { "CASTSPD1", "CASTSPD2", "INT2" }
            ),
            new PassiveNode(
                "INT2",
                new StatTypes[] { StatTypes.INT },
                new int[] { 5 },
                new string[] { "INT1", "INT3" }
            ),
            new PassiveNode(
                "INT3",
                new StatTypes[] { StatTypes.INT },
                new int[] { 5 },
                new string[] { "INT2", "INT4", "CASTSPD3", "CASTSPD4", "CRITDMG3" }
            ),
            new PassiveNode(
                "INT4",
                new StatTypes[] { StatTypes.INT },
                new int[] { 5 },
                new string[] { "INT3", "CASTSPD4", "CRITDMG2", "BLOCKCHANCE3" }
            ),
            new PassiveNode(
                "BLOCKCHANCE1",
                new StatTypes[] { StatTypes.BlockChance },
                new int[] { 5 },
                new string[] { "BLOCKCHANCE4", "BLOCKCHANCE2", "CRITDMG1" }
            ),
            new PassiveNode(
                "BLOCKCHANCE2",
                new StatTypes[] { StatTypes.BlockChance },
                new int[] { 5 },
                new string[] { "BLOCKCHANCE1", "BLOCKCHANCE3", "BLOCKCHANCE4", "CRITDMG2" }
            ),
            new PassiveNode(
                "BLOCKCHANCE3",
                new StatTypes[] { StatTypes.BlockChance },
                new int[] { 5 },
                new string[] { "ATKSPD3", "ATKSPD4", "BLOCKCHANCE2", "INT4" }
            ),
            new PassiveNode(
                "BLOCKCHANCE4",
                new StatTypes[] { StatTypes.BlockChance },
                new int[] { 5 },
                new string[] { "ATKSPD3", "BLOCKCHANCE1", "BLOCKCHANCE2" }
            ),
            new PassiveNode(
                "CRITDMG1",
                new StatTypes[] { StatTypes.CritDamage },
                new int[] { 5 },
                new string[] { "CRITDMG2", "CRITDMG3", "BLOCKCHANCE1" }
            ),
            new PassiveNode(
                "CRITDMG2",
                new StatTypes[] { StatTypes.CritDamage },
                new int[] { 5 },
                new string[] { "CRITDMG1", "CRITDMG3", "BLOCKCHANCE2", "INT4" }
            ),
            new PassiveNode(
                "CRITDMG3",
                new StatTypes[] { StatTypes.CritDamage },
                new int[] { 5 },
                new string[] { "CRITDMG1", "CRITDMG2", "INT3" }
            ),
        };
    }
}







