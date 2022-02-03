using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using UnityEngine.UI;
public class PassiveSkills
{
    private Player player;
    private Stats playerStats;
    private PassiveNode[] passiveTree;
    private PassiveSkillInfo passiveSkillInfo;
    public PassiveSkills(Player player)
    {
        this.player = player;
        playerStats = player.GetComponent<Stats>();
        passiveSkillInfo = new PassiveSkillInfo();
        this.passiveTree = passiveSkillInfo.passiveTree;

    }
    public void UnlockPassive(string name, Transform background)
    {
        PassiveNode passiveNode = Array.Find(passiveTree, node => node.Name == name);
        if (!Unlockable(passiveNode) || passiveNode.Unlocked) return;
        var stats = passiveNode.Stats;
        var values = passiveNode.StatValues;
        for (int i = 0; i < stats.Length; i++)
        {
            playerStats[StatTypes.SKILLPOINTS] -= 1;
            playerStats[stats[i]] += values[i];
            passiveNode.Unlocked = true;
            UpdateVisual(background);
            Debug.Log($"Added {values[i]} to {stats[i]}");
        }
    }
    private bool Unlockable(PassiveNode node)
    {
        if (node.Prerequisites.Length == 0) return true;
        foreach (var prereq in node.Prerequisites)
        {
            PassiveNode passiveNode = Array.Find(passiveTree, node => node.Name == prereq);
            if (passiveNode.Unlocked) return true;
        }
        return false;
    }
    private void UpdateVisual(Transform background)
    {
        background.GetComponent<Image>().color = Color.yellow;
    }
}
