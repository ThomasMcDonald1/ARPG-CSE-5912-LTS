using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterClass : MonoBehaviour
{
    public string description;

    public static readonly StatTypes[] baseStatTypes = new StatTypes[]
    {
        StatTypes.MaxHP,
        StatTypes.HealthRegen,
        StatTypes.MaxMana,
        StatTypes.ManaRegen,
        StatTypes.PHYATK,
        StatTypes.MAGPWR,
        StatTypes.PHYDEF,
        StatTypes.MAGDEF,
    };

    public int[] baseStats = new int[baseStatTypes.Length];
    public float[] growthStats = new float[baseStatTypes.Length];

    Stats stats;

    public void LoadBaseStats()
    {
        for (int i = 0; i < baseStatTypes.Length; i++)
        {
            StatTypes type = baseStatTypes[i];
            stats.InitializeValue(type, baseStats[i]);
        }
        stats.InitializeValue(StatTypes.HP, stats[StatTypes.MaxHP]);
        stats.InitializeValue(StatTypes.Mana, stats[StatTypes.MaxMana]);
    }

    /*
     * Upon a character level changes, Invoke the event CharacterLeveledUpEvent
     * from whatever script levels are given to characters,
     * and pass into it the character's level before leveling up/down. For instance,
     * if the character is level 1, but the character just gained 9 levels, perhaps
     * from being set to level 10 from enemy scaling calculations, then Invoke the
     * event and pass in 1. This will then apply all growth stats for the 9 levels.
     */
    protected virtual void OnLevelChanged(object sender, InfoEventArgs<int> e)
    {
        int oldValue = e.info;
        int newValue = stats[StatTypes.LVL];
        if (oldValue < newValue)
            for (int i = oldValue; i < newValue; i++)
                LevelUpWithGrowthStats();
        else if (newValue < oldValue)
            for (int i = newValue; i < oldValue; i++)
                LevelDownWithGrowthStats();
    }

    //The following two functions can be simplified if growth stats don't have decimal places
    void LevelUpWithGrowthStats()
    {
        for (int i = 0; i < baseStatTypes.Length; i++)
        {
            StatTypes type = baseStatTypes[i];
            int whole = Mathf.FloorToInt(growthStats[i]);
            float fraction = growthStats[i] - whole;
            int value = stats[type];
            value += whole;
            if (Random.value > (1f - fraction))
                value++;
            stats.InitializeValue(type, value);
        }
        stats.InitializeValue(StatTypes.HP, stats[StatTypes.MaxHP]);
        stats.InitializeValue(StatTypes.Mana, stats[StatTypes.MaxMana]);
    }

    void LevelDownWithGrowthStats()
    {
        for (int i = 0; i < baseStatTypes.Length; i++)
        {
            StatTypes type = baseStatTypes[i];
            int whole = Mathf.FloorToInt(growthStats[i]);
            float fraction = growthStats[i] - whole;
            int value = stats[type];
            value -= whole;
            if (Random.value < (1f - fraction))
                value--;
            stats.InitializeValue(type, value);
        }
        stats.InitializeValue(StatTypes.HP, stats[StatTypes.MaxHP]);
        stats.InitializeValue(StatTypes.Mana, stats[StatTypes.MaxMana]);
    }
}
