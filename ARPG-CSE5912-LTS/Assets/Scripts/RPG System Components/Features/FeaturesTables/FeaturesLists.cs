using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeaturesLists: MonoBehaviour
{
    public FlatStatModifierFeature CreateFlatStatFeature(string name)
    {
        GameObject featureGO = new GameObject(name);
        featureGO.AddComponent<FlatStatModifierFeature>();
        FlatStatModifierFeature feature = featureGO.GetComponent<FlatStatModifierFeature>();
        return feature;
    }

    public PercentStatModifierFeature CreatePercentStatFeature(string name)
    {
        GameObject featureGO = new GameObject(name);
        featureGO.AddComponent<PercentStatModifierFeature>();
        PercentStatModifierFeature feature = featureGO.GetComponent<PercentStatModifierFeature>();
        return feature;
    }
}
