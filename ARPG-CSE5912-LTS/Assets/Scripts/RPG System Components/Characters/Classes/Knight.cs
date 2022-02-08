
using UnityEngine;

public class Knight : CharacterClass
{
    Stats stats;

    private void Awake()
    {
        stats = GetComponent<Stats>();
        //TODO: This is for testing purposes only. Later, we need to have
        //the base stats of each class set and the correct one assigned to
        //the player before the game ever starts, right after character creation
        SetBaseStats();
    }


    public override void SetBaseStats()
    {
        //change attack range to get it from starting weapon
        stats.InitializeValue(StatTypes.ATTACKRANGE, 2);
        stats.InitializeValue(StatTypes.LVL, 0);
        stats.InitializeValue(StatTypes.EXP, 0);
        stats.InitializeValue(StatTypes.MAXHEALTH, 11000);
        stats.InitializeValue(StatTypes.HEALTH, stats[StatTypes.MAXHEALTH]);
        stats.InitializeValue(StatTypes.PHYATK, 120);
        stats.InitializeValue(StatTypes.PHYDEF, 30);
        stats.InitializeValue(StatTypes.ATKSPD, 12);
        Debug.Log("Base Stats Set.");
    }

    public override void AddGrowthStats()
    {
        throw new System.NotImplementedException();
    }

    public override void SetUsableEquipment()
    {
        throw new System.NotImplementedException();
    }

    public override void SetLearnableAbilities()
    {
        throw new System.NotImplementedException();
    }
}

