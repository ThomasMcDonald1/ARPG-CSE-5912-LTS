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
            new PassiveNode("Health1", new StatTypes[] { StatTypes.HEALTH }, new int[] { 5 }, new string[0], true),
            new PassiveNode("Dex1", new StatTypes[] { StatTypes.DEX }, new int[] { 5 }, new string[0], true),
            new PassiveNode("Int1", new StatTypes[] { StatTypes.INT }, new int[] { 5 }, new string[0], true),
            new PassiveNode("Nimble", new StatTypes[] { StatTypes.CASTSPEED, StatTypes.MAGDMGBONUS }, new int[] { 10, 20 }, new string[] { "Dex1", "Int1"})
        };
    }
}
