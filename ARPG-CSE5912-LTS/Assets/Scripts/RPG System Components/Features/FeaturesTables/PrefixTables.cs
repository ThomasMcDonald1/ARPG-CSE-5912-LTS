using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefixTables: MonoBehaviour
{
    public List<PrefixSuffix> rarePrefixTable = new List<PrefixSuffix>();
    public List<PrefixSuffix> epicPrefixTable = new List<PrefixSuffix>();
    public List<PrefixSuffix> legendaryPrefixTable = new List<PrefixSuffix>();
    public List<PrefixSuffix> rareSuffixTable = new List<PrefixSuffix>();
    public List<PrefixSuffix> epicSuffixTable = new List<PrefixSuffix>();
    public List<PrefixSuffix> legendarySuffixTable = new List<PrefixSuffix>();

    public PrefixSuffix GetRandomPrefixForRarityAndGearType(LootLabels.Rarity rarity, LootLabels.GearTypes gearType)
    {
        PrefixSuffix prefix = new PrefixSuffix(null, null, null);

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

    private int GetRandom(List<PrefixSuffix> prefixSuffixTable, LootLabels.GearTypes gearType)
    {
        int random = Random.Range(0, prefixSuffixTable.Count);
        while (!prefixSuffixTable[random].GearTypeRequirements.Contains(gearType))
            random = Random.Range(0, prefixSuffixTable.Count);
        return random;
    }
}
