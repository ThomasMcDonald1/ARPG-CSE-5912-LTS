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
    private GameObject skillNotification;
    private Text remainingPoints;
    private AudioManager audioManager;
    public PassiveSkills(Player player, Connections[] connections, GameObject passiveSkills, GameObject skillNotification, Text remainingPoints, AudioManager audioManager)
    {
        this.player = player;
        this.connections = connections;
        playerStats = player.GetComponent<Stats>();
        passiveSkillInfo = new PassiveSkillInfo();
        this.passiveTree = passiveSkillInfo.passiveTree;
        passiveNodesReadyForUnlock = new List<PassiveNode>();
        unlockedNodes = new List<PassiveNode>();
        passiveSkillsGameObject = passiveSkills;
        this.skillNotification = skillNotification;
        this.remainingPoints = remainingPoints;
        this.audioManager = audioManager;
    }
    public void UnlockPassives()
    {
        foreach (var node in passiveNodesReadyForUnlock)
        {
            var stats = node.Stats;
            var values = node.StatValues;
            for (int i = 0; i < stats.Length; i++)
            {
                playerStats[stats[i]] += values[i];
                node.Unlocked = true;
                Debug.Log($"Added {values[i]} to {stats[i]}");
            }
        }
        unlockedNodes.AddRange(passiveNodesReadyForUnlock);
        passiveNodesReadyForUnlock.Clear();
        if (playerStats[StatTypes.SkillPoints] < 1)
        {
            skillNotification.SetActive(false);
        }
    }
    public void PassivesReadyForUnlock(string name, Transform background)
    {
        audioManager.Play("MenuClick");
        Debug.Log("cotingnignidnindindnign");
        PassiveNode passiveNode = Array.Find(passiveTree, node => node.Name == name);
        if (!Unlockable(passiveNode) || passiveNode.Unlocked) return;
        passiveNode.Unlocked = true;
        playerStats[StatTypes.SkillPoints] -= 1;
        remainingPoints.text = playerStats[StatTypes.SkillPoints].ToString();
        passiveNodesReadyForUnlock.Add(passiveNode);
        UpdateVisual(background);
    }
    private bool Unlockable(PassiveNode node)
    {
        if (playerStats[StatTypes.SkillPoints] < 1) return false;
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
            playerStats[StatTypes.SkillPoints] += 1;
            Transform nodeObject = passiveSkillsGameObject.transform.Find(node.Name);
            Transform background = nodeObject.Find("Background");
            background.GetComponent<Image>().color = new Color(0.6415094f, 0.6076183f, 0.6076183f, 1);
            foreach (var c in connections)
            {
                c.ResetConnectionVisual(passiveTree);
            }
        }
        passiveNodesReadyForUnlock.Clear();
        remainingPoints.text = playerStats[StatTypes.SkillPoints].ToString();
    }
    public void FullyResetPassiveTree()
    {
        ResetPassiveTreeStats();
        ResetPassiveTreeVisuals();
        unlockedNodes.Clear();
    }
    void ResetPassiveTreeVisuals()
    {
        foreach (var node in unlockedNodes)
        {
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
    void ResetPassiveTreeStats()
    {
        foreach (var node in unlockedNodes)
        {
            var stats = node.Stats;
            var values = node.StatValues;
            for (int i = 0; i < stats.Length; i++)
            {
                // playerStats[StatTypes.SKILLPOINTS] -= 1;
                playerStats[stats[i]] -= values[i];
                node.Unlocked = false;
            }

        }
    }
}
