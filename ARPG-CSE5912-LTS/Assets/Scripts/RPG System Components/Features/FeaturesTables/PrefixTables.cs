using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefixTables: MonoBehaviour
{
    public List<PrefixSuffix> rarePrefixTable = new List<PrefixSuffix>();
    public List<PrefixSuffix> epicPrefixTable = new List<PrefixSuffix>();
    public List<PrefixSuffix> legendaryPrefixTable = new List<PrefixSuffix>();

    public PrefixSuffix GetRandomPrefixForRarityAndGearType(LootLabels.Rarity rarity, LootLabels.GearTypes gearType)
    {
        PrefixSuffix prefix;

        switch (rarity)
        {
            case LootLabels.Rarity.Rare:
                prefix = GetRandomRarePrefixForGearType(gearType);
                break;
            case LootLabels.Rarity.Epic:
                int tableToChoose = Random.Range(0, 2);
                if (tableToChoose == 0)
                    prefix = GetRandomRarePrefixForGearType(gearType);
                else
                    prefix = GetRandomEpicPrefixForGearType(gearType);
                break;
            case LootLabels.Rarity.Legendary:
                prefix = GetRandomLegendaryPrefixForGearType(gearType);
                break;
            default:
                prefix = GetRandomRarePrefixForGearType(gearType);
                break;
        }
        return prefix;
    }

    private PrefixSuffix GetRandomRarePrefixForGearType(LootLabels.GearTypes gearType)
    {
        int random = GetRandom(rarePrefixTable, gearType);

        return rarePrefixTable[random];
    }

    private PrefixSuffix GetRandomEpicPrefixForGearType(LootLabels.GearTypes gearType)
    {
        int random = GetRandom(epicPrefixTable, gearType);
        return epicPrefixTable[random];
    }

    private PrefixSuffix GetRandomLegendaryPrefixForGearType(LootLabels.GearTypes gearType)
    {
        int random = GetRandom(legendaryPrefixTable, gearType);
        return legendaryPrefixTable[random];
    }

    private int GetRandom(List<PrefixSuffix> prefixTable, LootLabels.GearTypes gearType)
    {
        int random = Random.Range(0, prefixTable.Count);
        while (!prefixTable[random].GearTypeRequirements.Contains(gearType))
            random = Random.Range(0, prefixTable.Count);
        return random;
    }
}
