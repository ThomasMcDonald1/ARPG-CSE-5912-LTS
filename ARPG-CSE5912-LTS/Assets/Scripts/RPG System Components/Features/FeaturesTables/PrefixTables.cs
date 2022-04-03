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
        int random = Random.Range(0, rarePrefixTable.Count);

        while (!rarePrefixTable[random].GearTypeRequirements.Contains(gearType))
        {
            random = Random.Range(0, rarePrefixTable.Count);
        }

        return rarePrefixTable[random];
    }

    private PrefixSuffix GetRandomEpicPrefixForGearType(LootLabels.GearTypes gearType)
    {
        //Add to here the things that will be created for epic prefix tables
        return null;
    }

    private PrefixSuffix GetRandomLegendaryPrefixForGearType(LootLabels.GearTypes gearType)
    {
        //Add to here the things that will be created for legendary prefix tables
        return null;
    }
}
