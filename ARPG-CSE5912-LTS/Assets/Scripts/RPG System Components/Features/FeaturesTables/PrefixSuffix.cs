using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefixSuffix
{
    public string Name;
    public List<Feature> Features;
    public List<LootLabels.GearTypes> GearTypeRequirements;
    public PrefixSuffix(string name, List<Feature> features, List<LootLabels.GearTypes> gearTypeRequirements)
    {
        Name = name;
        Features = features;
        GearTypeRequirements = gearTypeRequirements;
    }
}
