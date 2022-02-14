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
                new string[] { "ATKSPD2", "HPREGEN4" }
            ),
            new PassiveNode(
                "ATKSPD4",
                new StatTypes[] { StatTypes.AtkSpeed },
                new int[] { 5 },
                new string[] { "ATKSPD3", "HPREGEN4", "STR5" }
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
                new string[] { "DEX4", "DEX5", "CRIT2" }
            ),
        };
    }
}
