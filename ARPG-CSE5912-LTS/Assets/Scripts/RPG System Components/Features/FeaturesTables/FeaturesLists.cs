using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeaturesLists: MonoBehaviour
{
    public GameObject CreateFlatStatFeature(string name)
    {
        GameObject featureGO = new GameObject(name);
        featureGO.AddComponent<FlatStatModifierFeature>();
        FlatStatModifierFeature feature = featureGO.GetComponent<FlatStatModifierFeature>();
        return featureGO;
    }

    public GameObject CreatePercentStatFeature(string name)
    {
        GameObject featureGO = new GameObject(name);
        featureGO.AddComponent<PercentStatModifierFeature>();
        PercentStatModifierFeature feature = featureGO.GetComponent<PercentStatModifierFeature>();
        return featureGO;
    }
}
