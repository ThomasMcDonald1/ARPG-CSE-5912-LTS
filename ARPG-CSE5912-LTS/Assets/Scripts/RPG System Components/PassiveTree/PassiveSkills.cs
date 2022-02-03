using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class PassiveSkills : MonoBehaviour
{
    private Player player;
    private Stats playerStats;
    Dictionary<string, StatTypes[]> passiveToStatTypes = new Dictionary<string, StatTypes[]>();
    Dictionary<string, int[]> passiveValues = new Dictionary<string, int[]>();
    public PassiveSkills(Player player)
    {
        this.player = player;
        playerStats = player.GetComponent<Stats>();
        passiveToStatTypes.Add("Health1", new StatTypes[] { StatTypes.HEALTH });
        passiveValues.Add("Health1", new int[] { 5 });
    }
    public void UnlockPassive(string name)
    {
        var stats = passiveToStatTypes[name];
        var values = passiveValues[name];
        for (int i = 0; i < stats.Length; i++)
        {
            playerStats[stats[i]] += values[i];
            Debug.Log($"Added {values[i]} to playerStats[states[i]]");
        }
    }
}
