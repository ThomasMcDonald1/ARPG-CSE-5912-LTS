using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuffixTables : MonoBehaviour
{
    public List<PrefixSuffix> rareAndEpicSuffixTable = new List<PrefixSuffix>();
    public List<PrefixSuffix> legendarySuffixTable = new List<PrefixSuffix>();

    public PrefixSuffix GetRandomSuffixForRarityAndGearType(LootLabels.Rarity rarity, LootLabels.GearTypes gearType)
    {
        PrefixSuffix suffix; 

        switch (rarity)
        {
            case LootLabels.Rarity.Legendary:
                suffix = GetRandomLegendarySuffixForGearType(gearType);
                break;
            default:
                suffix = GetRandomRareOrEpicSuffixForGearType(gearType);
                break;
        }
        return suffix;
    }

    private PrefixSuffix GetRandomRareOrEpicSuffixForGearType(LootLabels.GearTypes gearType)
    {
        int random = GetRandom(rareAndEpicSuffixTable, gearType);
        return rareAndEpicSuffixTable[random];
    }

    private PrefixSuffix GetRandomLegendarySuffixForGearType(LootLabels.GearTypes gearType)
    {
        int random = GetRandom(legendarySuffixTable, gearType);
        return legendarySuffixTable[random];
    }

    private int GetRandom(List<PrefixSuffix> suffixTable, LootLabels.GearTypes gearType)
    {
        int random = Random.Range(0, suffixTable.Count);
        while (!suffixTable[random].GearTypeRequirements.Contains(gearType))
            random = Random.Range(0, suffixTable.Count);
        return random;
    }
}
