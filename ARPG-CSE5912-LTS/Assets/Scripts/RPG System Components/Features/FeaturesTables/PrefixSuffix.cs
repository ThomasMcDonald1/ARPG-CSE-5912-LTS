using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class PrefixSuffix
{
    public string Name;
    public List<GameObject> FeaturesGOs;
    public List<LootLabels.GearTypes> GearTypeRequirements;
    public PrefixSuffix(string name, List<GameObject> featuresGOs, List<LootLabels.GearTypes> gearTypeRequirements)
    {
        Name = name;
        FeaturesGOs = featuresGOs;
        GearTypeRequirements = gearTypeRequirements;
    }
}
