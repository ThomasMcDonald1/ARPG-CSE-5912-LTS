using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EpicPrefixFeaturesLists : FeaturesLists
{
    public List<GameObject> travelers = new List<GameObject>();
    public List<GameObject> vampiric = new List<GameObject>();
    public List<GameObject> berserkers = new List<GameObject>();
    public List<GameObject> exploiters = new List<GameObject>();
    public List<GameObject> bloodletters = new List<GameObject>();
    public List<GameObject> impalers = new List<GameObject>();
    public List<GameObject> psionics = new List<GameObject>();
    public List<GameObject> fierySorcerers = new List<GameObject>();
    public List<GameObject> icySorcerers = new List<GameObject>();
    public List<GameObject> thunderingSorcerers = new List<GameObject>();
    public List<GameObject> corrosiveSorcerers = new List<GameObject>();

    public void CreateEpicPrefixFeaturesLists()
    {
        CreateTravelers();
        CreateVampiric();
        CreateBerserkers();
        CreateExploiters();
        CreateBloodletters();
        CreateImpalers();
        CreatePsionics();
        CreateFierySorcerers();
        CreateIcySorcerers();
        CreateThunderingSorcerers();
        CreateCorrosiveSorcerers();
    }

    private void CreateTravelers()
    {
        GameObject featureGO = CreateFlatStatFeature("RunSpeed");
        FlatStatModifierFeature feature = featureGO.GetComponent<FlatStatModifierFeature>();
        feature.type = StatTypes.RunSpeed;
        travelers.Add(featureGO);
    }

    private void CreateVampiric()
    {
        GameObject featureGO = CreateFlatStatFeature("Lifesteal");
        FlatStatModifierFeature feature = featureGO.GetComponent<FlatStatModifierFeature>();
        feature.type = StatTypes.Lifesteal;
        vampiric.Add(featureGO);
    }

    private void CreateBerserkers()
    {
        GameObject featureGO = CreateFlatStatFeature("AtkSpeed");
        FlatStatModifierFeature feature = featureGO.GetComponent<FlatStatModifierFeature>();
        feature.type = StatTypes.AtkSpeed;
        berserkers.Add(featureGO);
    }

    private void CreateExploiters()
    {
        GameObject featureGO = CreateFlatStatFeature("CritChance");
        FlatStatModifierFeature feature = featureGO.GetComponent<FlatStatModifierFeature>();
        feature.type = StatTypes.CritChance;
        exploiters.Add(featureGO);
    }

    private void CreateBloodletters()
    {
        GameObject featureGO = CreateFlatStatFeature("CritDamage");
        FlatStatModifierFeature feature = featureGO.GetComponent<FlatStatModifierFeature>();
        feature.type = StatTypes.CritDamage;
        bloodletters.Add(featureGO);
    }

    private void CreateImpalers()
    {
        GameObject featureGO = CreateFlatStatFeature("PercentArmorPen");
        FlatStatModifierFeature feature = featureGO.GetComponent<FlatStatModifierFeature>();
        feature.type = StatTypes.PercentArmorPen;
        impalers.Add(featureGO);
    }

    private void CreatePsionics()
    {
        GameObject featureGO = CreateFlatStatFeature("PercentMagicPen");
        FlatStatModifierFeature feature = featureGO.GetComponent<FlatStatModifierFeature>();
        feature.type = StatTypes.PercentMagicPen;
        psionics.Add(featureGO);
    }

    private void CreateFierySorcerers()
    {
        GameObject featureGO = CreateFlatStatFeature("FireDmgOnHit");
        FlatStatModifierFeature feature = featureGO.GetComponent<FlatStatModifierFeature>();
        feature.type = StatTypes.FireDmgOnHit;
        fierySorcerers.Add(featureGO);
        GameObject featureGO2 = CreateFlatStatFeature("FireDmgBonus");
        FlatStatModifierFeature feature2 = featureGO2.GetComponent<FlatStatModifierFeature>();
        feature2.type = StatTypes.FireDmgBonus;
        fierySorcerers.Add(featureGO2);
    }

    private void CreateIcySorcerers()
    {
        GameObject featureGO = CreateFlatStatFeature("ColdDmgOnHit");
        FlatStatModifierFeature feature = featureGO.GetComponent<FlatStatModifierFeature>();
        feature.type = StatTypes.ColdDmgOnHit;
        fierySorcerers.Add(featureGO);
        GameObject featureGO2 = CreateFlatStatFeature("ColdDmgBonus");
        FlatStatModifierFeature feature2 = featureGO2.GetComponent<FlatStatModifierFeature>();
        feature2.type = StatTypes.ColdDmgBonus;
        fierySorcerers.Add(featureGO2);
    }

    private void CreateThunderingSorcerers()
    {
        GameObject featureGO = CreateFlatStatFeature("LightningDmgOnHit");
        FlatStatModifierFeature feature = featureGO.GetComponent<FlatStatModifierFeature>();
        feature.type = StatTypes.LightningDmgOnHit;
        fierySorcerers.Add(featureGO);
        GameObject featureGO2 = CreateFlatStatFeature("LightningDmgBonus");
        FlatStatModifierFeature feature2 = featureGO2.GetComponent<FlatStatModifierFeature>();
        feature2.type = StatTypes.LightningDmgBonus;
        fierySorcerers.Add(featureGO2);
    }

    private void CreateCorrosiveSorcerers()
    {
        GameObject featureGO = CreateFlatStatFeature("PoisonDmgOnHit");
        FlatStatModifierFeature feature = featureGO.GetComponent<FlatStatModifierFeature>();
        feature.type = StatTypes.PoisonDmgOnHit;
        fierySorcerers.Add(featureGO);
        GameObject featureGO2 = CreateFlatStatFeature("PoisonDmgBonus");
        FlatStatModifierFeature feature2 = featureGO2.GetComponent<FlatStatModifierFeature>();
        feature2.type = StatTypes.PoisonDmgBonus;
        fierySorcerers.Add(featureGO2);
    }
}
