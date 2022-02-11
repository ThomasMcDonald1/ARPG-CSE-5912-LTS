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
                new string[0],
                true
            ),
            new PassiveNode(
                "STR1",
                new StatTypes[] { StatTypes.STR },
                new int[] { 5 },
                new string[] { "HEALTH1" }
            ),
            new PassiveNode(
                "STR2",
                new StatTypes[] { StatTypes.STR },
                new int[] { 10 },
                new string[] { "STR1", "ARMOR1" }
            ),
            new PassiveNode(
                "STR3",
                new StatTypes[] { StatTypes.STR },
                new int[] { 10 },
                new string[] { "STR2" }
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
                new string[] { "ARMOR1" }
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
                new string[] { "HPREGEN1" }
            ),
            new PassiveNode(
                "HPREGEN3",
                new StatTypes[] { StatTypes.HealthRegen },
                new int[] { 4 },
                new string[] { "HPREGEN2" }
            ),
            new PassiveNode(
                "ATKSPD1",
                new StatTypes[] { StatTypes.AtkSpeed },
                new int[] { 5 },
                new string[] { "HPREGEN1" }
            ),
            new PassiveNode(
                "ATKSPD2",
                new StatTypes[] { StatTypes.AtkSpeed },
                new int[] { 5 },
                new string[] { "ATKSPD1" }
            ),
        };
    }
}
