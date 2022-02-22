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
    private Connections[] connections;
    private List<PassiveNode> passiveNodesReadyForUnlock;
    private List<PassiveNode> unlockedNodes;
    private GameObject passiveSkillsGameObject;
    public PassiveSkills(Player player, Connections[] connections, GameObject passiveSkills)
    {
        this.player = player;
        this.connections = connections;
        playerStats = player.GetComponent<Stats>();
        passiveSkillInfo = new PassiveSkillInfo();
        this.passiveTree = passiveSkillInfo.passiveTree;
        passiveNodesReadyForUnlock = new List<PassiveNode>();
        unlockedNodes = new List<PassiveNode>();
        passiveSkillsGameObject = passiveSkills;
    }
    public void UnlockPassives()
    {
        foreach (var node in passiveNodesReadyForUnlock)
        {
            var stats = node.Stats;
            var values = node.StatValues;
            for (int i = 0; i < stats.Length; i++)
            {
                // playerStats[StatTypes.SKILLPOINTS] -= 1;
                playerStats[stats[i]] += values[i];
                node.Unlocked = true;
                Debug.Log($"Added {values[i]} to {stats[i]}");
            }
        }
        unlockedNodes.AddRange(passiveNodesReadyForUnlock);
        passiveNodesReadyForUnlock.Clear();
    }
    public void PassivesReadyForUnlock(string name, Transform background)
    {
        PassiveNode passiveNode = Array.Find(passiveTree, node => node.Name == name);
        if (!Unlockable(passiveNode) || passiveNode.Unlocked) return;
        passiveNode.Unlocked = true;
        passiveNodesReadyForUnlock.Add(passiveNode);
        UpdateVisual(background);
    }
    private bool Unlockable(PassiveNode node)
    {
        // if (playerStats[StatTypes.SKILLPINTS] < 1) return false;
        if (node.Unlockable == true) return true;
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
        foreach (var c in connections)
        {
            c.UpdateConnectionVisual(passiveTree);
        }
    }
    public void ResetVisualCloseButton()
    {
        foreach (var node in passiveNodesReadyForUnlock)
        {
            node.Unlocked = false;
            Debug.Log(passiveSkillsGameObject.name);
            Transform nodeObject = passiveSkillsGameObject.transform.Find(node.Name);
            Transform background = nodeObject.Find("Background");
            background.GetComponent<Image>().color = new Color(0.6415094f, 0.6076183f, 0.6076183f, 1);
            foreach (var c in connections)
            {
                c.ResetConnectionVisual(passiveTree);
            }
        }
    }
    public void FullyResetPassiveTree()
    {
        foreach (var node in unlockedNodes)
        {
            node.Unlocked = false;
            Debug.Log(passiveSkillsGameObject.name);
            Transform nodeObject = passiveSkillsGameObject.transform.Find(node.Name);
            Transform background = nodeObject.Find("Background");
            background.GetComponent<Image>().color = new Color(0.6415094f, 0.6076183f, 0.6076183f, 1);
            foreach (var c in connections)
            {
                c.ResetConnectionVisual(passiveTree);
            }
        }
    }
}
