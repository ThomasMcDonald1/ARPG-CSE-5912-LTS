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
                new string[] { "FIRERES1", "ARMORPEN1"},
                true
            ),
            new PassiveNode(
                "FIRERES1",
                new StatTypes[] { StatTypes.FireRes },
                new int[] { 5 },
                new string[] { "HEALTH1", "FIRERES2", "ARMOR1" }
            ),
            new PassiveNode(
                "FIRERES2",
                new StatTypes[] { StatTypes.FireRes },
                new int[] { 10 },
                new string[] { "FIRERES1", "ARMOR1", "ARMORPEN2", "FIRERES3" }
            ),
            new PassiveNode(
                "FIRERES3",
                new StatTypes[] { StatTypes.FireRes },
                new int[] { 10 },
                new string[] { "FIRERES2", "FIRERES4" }
            ),
            new PassiveNode(
                "FIRERES4",
                new StatTypes[] { StatTypes.FireRes },
                new int[] { 10 },
                new string[] { "FIRERES3", "ARMORPEN4", "ARMOR3" }
            ),
            new PassiveNode(
                "FIRERES5",
                new StatTypes[] { StatTypes.FireRes },
                new int[] { 10 },
                new string[] { "FIRERES4", "ATKSPD4", "ARMOR3", "DODGE4", "LIFESTEAL4" }
            ),
            new PassiveNode(
                "ARMOR1",
                new StatTypes[] { StatTypes.PHYDEF },
                new int[] { 10 },
                new string[] { "FIRERES1" }
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
                new string[] { "ARMOR2", "FIRERES4", "DODGE4", "MOVE2" }
            ),
            new PassiveNode(
                "ARMORPEN1",
                new StatTypes[] { StatTypes.FlatArmorPen },
                new int[] { 2 },
                new string[] { "HEALTH1" }
            ),
            new PassiveNode(
                "ARMORPEN2",
                new StatTypes[] { StatTypes.FlatArmorPen },
                new int[] { 3 },
                new string[] { "ARMORPEN1", "ARMORPEN3", "ATKSPD1", "FIRERES2" }
            ),
            new PassiveNode(
                "ARMORPEN3",
                new StatTypes[] { StatTypes.FlatArmorPen },
                new int[] { 4 },
                new string[] { "ARMORPEN2", "ARMORPEN4" }
            ),
            new PassiveNode(
                "ARMORPEN4",
                new StatTypes[] { StatTypes.FlatArmorPen },
                new int[] { 4 },
                new string[] { "ARMORPEN3", "ATKSPD3", "FIRERES4" }
            ),
            new PassiveNode(
                "ATKSPD1",
                new StatTypes[] { StatTypes.AtkSpeed },
                new int[] { 5 },
                new string[] { "ARMORPEN1", "ARMORPEN2", "ATKSPD2" }
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
                new string[] { "ATKSPD2", "ATKSPD4", "ARMORPEN4", "BLOCKCHANCE3", "BLOCKCHANCE4" }
            ),
            new PassiveNode(
                "ATKSPD4",
                new StatTypes[] { StatTypes.AtkSpeed },
                new int[] { 5 },
                new string[] { "ATKSPD3", "ARMORPEN4", "FIRERES5", "BLOCKCHANCE3", "CASTSPD4" }
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
                new StatTypes[] { StatTypes.Evasion },
                new int[] { 5 },
                new string[] { "DODGE2", "LIFESTEAL1", "MOVE1" }
            ),
            new PassiveNode(
                "DODGE2",
                new StatTypes[] { StatTypes.Evasion },
                new int[] { 5 },
                new string[] { "DODGE1", "DODGE3", "LIFESTEAL2" }
            ),
            new PassiveNode(
                "DODGE3",
                new StatTypes[] { StatTypes.Evasion },
                new int[] { 5 },
                new string[] { "DODGE2", "DODGE4" }
            ),
            new PassiveNode(
                "DODGE4",
                new StatTypes[] { StatTypes.Evasion },
                new int[] { 5 },
                new string[] { "DODGE3", "ARMOR3", "FIRERES5", "LIFESTEAL3" }
            ),
            new PassiveNode(
                "DODGE4",
                new StatTypes[] { StatTypes.Evasion },
                new int[] { 5 },
                new string[] { "DODGE3", "ARMOR3", "FIRERES5", "LIFESTEAL3" }
            ),
            new PassiveNode(
                "LIFESTEAL1",
                new StatTypes[] { StatTypes.Lifesteal },
                new int[] { 5 },
                new string[] { "DODGE1", "COLDRES1" }
            ),
            new PassiveNode(
                "LIFESTEAL2",
                new StatTypes[] { StatTypes.Lifesteal },
                new int[] { 5 },
                new string[] { "DODGE2", "COLDRES2" }
            ),
            new PassiveNode(
                "LIFESTEAL3",
                new StatTypes[] { StatTypes.Lifesteal },
                new int[] { 5 },
                new string[] { "DODGE4", "COLDRES4" }
            ),
            new PassiveNode(
                "LIFESTEAL4",
                new StatTypes[] { StatTypes.Lifesteal },
                new int[] { 5 },
                new string[] { "FIRERES5", "COLDRES5" }
            ),
            new PassiveNode(
                "COLDRES1",
                new StatTypes[] { StatTypes.ColdRes },
                new int[] { 5 },
                new string[] { "LIFESTEAL1", "COLDRES2" }
            ),
            new PassiveNode(
                "COLDRES2",
                new StatTypes[] { StatTypes.ColdRes },
                new int[] { 5 },
                new string[] { "COLDRES1", "COLDRES3", "LIFESTEAL2", "CRIT1" }
            ),
            new PassiveNode(
                "COLDRES3",
                new StatTypes[] { StatTypes.ColdRes },
                new int[] { 5 },
                new string[] { "COLDRES2", "COLDRES4" }
            ),
            new PassiveNode(
                "COLDRES4",
                new StatTypes[] { StatTypes.ColdRes },
                new int[] { 5 },
                new string[] { "COLDRES3", "COLDRES5", "LIFESTEAL3", "CRIT3" }
            ),
            new PassiveNode(
                "COLDRES5",
                new StatTypes[] { StatTypes.ColdRes },
                new int[] { 5 },
                new string[] { "COLDRES4", "LIFESTEAL4", "CRIT3" }
            ),
            new PassiveNode(
                "CRIT1",
                new StatTypes[] { StatTypes.ColdRes },
                new int[] { 5 },
                new string[] { "CRIT2", "COLDRES1", "COLDRES2" }
            ),
            new PassiveNode(
                "CRIT2",
                new StatTypes[] { StatTypes.ColdRes },
                new int[] { 5 },
                new string[] { "CRIT1", "CRIT3" }
            ),
            new PassiveNode(
                "CRIT3",
                new StatTypes[] { StatTypes.ColdRes },
                new int[] { 5 },
                new string[] { "MP4", "CRIT2", "POISONRES2", "COLDRES4", "COLDRES5" }
            ),
            new PassiveNode(
                "MP1",
                new StatTypes[] { StatTypes.MaxMana },
                new int[] { 5 },
                new string[] { "MP2", "CDR1", "POISONRES1" }
            ),
            new PassiveNode(
                "MP2",
                new StatTypes[] { StatTypes.MaxMana },
                new int[] { 5 },
                new string[] { "MP1", "MP3", "CDR2", "POISONRES1" }
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
                new string[] { "MP3", "CRIT3", "CASTSPD3", "COLDRES5" }
            ),
            new PassiveNode(
                "CASTSPD1",
                new StatTypes[] { StatTypes.CastSpeed },
                new int[] { 5 },
                new string[] { "CDR1", "LIGHTNINGRES1", "CASTSPD2" }
            ),
            new PassiveNode(
                "CASTSPD2",
                new StatTypes[] { StatTypes.CastSpeed },
                new int[] { 5 },
                new string[] { "CDR2", "LIGHTNINGRES1", "CASTSPD1", "CASTSPD2" }
            ),
            new PassiveNode(
                "CASTSPD3",
                new StatTypes[] { StatTypes.CastSpeed },
                new int[] { 5 },
                new string[] { "MP4", "LIGHTNINGRES3", "CASTSPD2", "CASTSPD4" }
            ),
            new PassiveNode(
                "CASTSPD4",
                new StatTypes[] { StatTypes.CastSpeed },
                new int[] { 5 },
                new string[] { "LIGHTNINGRES3", "LIGHTNINGRES4", "CASTSPD3", "COLDRES5", "ATKSPD4" }
            ),
            new PassiveNode(
                "POISONRES1",
                new StatTypes[] { StatTypes.PoisonRes },
                new int[] { 5 },
                new string[] { "MP1", "MP2", "POISONRES2" }
            ),
            new PassiveNode(
                "POISONRES2",
                new StatTypes[] { StatTypes.PoisonRes },
                new int[] { 5 },
                new string[] { "CRIT3", "POISONRES1" }
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
                "LIGHTNINGRES1",
                new StatTypes[] { StatTypes.LightningRes },
                new int[] { 5 },
                new string[] { "CASTSPD1", "CASTSPD2", "LIGHTNINGRES2" }
            ),
            new PassiveNode(
                "LIGHTNINGRES2",
                new StatTypes[] { StatTypes.LightningRes },
                new int[] { 5 },
                new string[] { "LIGHTNINGRES1", "LIGHTNINGRES3" }
            ),
            new PassiveNode(
                "LIGHTNINGRES3",
                new StatTypes[] { StatTypes.LightningRes },
                new int[] { 5 },
                new string[] { "LIGHTNINGRES2", "LIGHTNINGRES4", "CASTSPD3", "CASTSPD4", "CRITDMG3" }
            ),
            new PassiveNode(
                "LIGHTNINGRES4",
                new StatTypes[] { StatTypes.LightningRes },
                new int[] { 5 },
                new string[] { "LIGHTNINGRES3", "CASTSPD4", "CRITDMG2", "BLOCKCHANCE3" }
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
                new string[] { "ATKSPD3", "ATKSPD4", "BLOCKCHANCE2", "LIGHTNINGRES4" }
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
                new string[] { "CRITDMG1", "CRITDMG3", "BLOCKCHANCE2", "LIGHTNINGRES4" }
            ),
            new PassiveNode(
                "CRITDMG3",
                new StatTypes[] { StatTypes.CritDamage },
                new int[] { 5 },
                new string[] { "CRITDMG1", "CRITDMG2", "LIGHTNINGRES3" }
            ),
        };
    }
}







