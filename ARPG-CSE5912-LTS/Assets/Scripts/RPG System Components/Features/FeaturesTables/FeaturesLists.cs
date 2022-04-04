using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeaturesLists: MonoBehaviour
{
    public GameObject CreateFlatStatFeature(string name)
    {
        GameObject featureGO = new GameObject(name);
        featureGO.transform.SetParent(gameObject.transform);
        featureGO.AddComponent<FlatStatModifierFeature>();
        return featureGO;
    }

    public GameObject CreatePercentStatFeature(string name)
    {
        GameObject featureGO = new GameObject(name);
        featureGO.transform.SetParent(gameObject.transform);
        featureGO.AddComponent<PercentStatModifierFeature>();
        return featureGO;
    }
}
