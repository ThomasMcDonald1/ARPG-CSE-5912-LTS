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
        GameObject featureGO = CreatePercentStatFeature("TravelersEpicPrefix");
        PercentStatModifierFeature feature = featureGO.GetComponent<PercentStatModifierFeature>();
        feature.type = StatTypes.RunSpeed;
        travelers.Add(featureGO);
    }

    private void CreateVampiric()
    {
        GameObject featureGO = CreatePercentStatFeature("VampiricEpicPrefix");
        PercentStatModifierFeature feature = featureGO.GetComponent<PercentStatModifierFeature>();
        feature.type = StatTypes.Lifesteal;
        vampiric.Add(featureGO);
    }

    private void CreateBerserkers()
    {
        GameObject featureGO = CreatePercentStatFeature("BerserkersEpicPrefix");
        PercentStatModifierFeature feature = featureGO.GetComponent<PercentStatModifierFeature>();
        feature.type = StatTypes.AtkSpeed;
        berserkers.Add(featureGO);
    }

    private void CreateExploiters()
    {
        GameObject featureGO = CreatePercentStatFeature("ExploitersEpicPrefix");
        PercentStatModifierFeature feature = featureGO.GetComponent<PercentStatModifierFeature>();
        feature.type = StatTypes.CritChance;
        exploiters.Add(featureGO);
    }

    private void CreateBloodletters()
    {
        GameObject featureGO = CreatePercentStatFeature("BloodlettersEpicPrefix");
        PercentStatModifierFeature feature = featureGO.GetComponent<PercentStatModifierFeature>();
        feature.type = StatTypes.CritDamage;
        bloodletters.Add(featureGO);
    }

    private void CreateImpalers()
    {
        GameObject featureGO = CreatePercentStatFeature("ImpalersEpicPrefix");
        PercentStatModifierFeature feature = featureGO.GetComponent<PercentStatModifierFeature>();
        feature.type = StatTypes.PercentArmorPen;
        impalers.Add(featureGO);
    }

    private void CreatePsionics()
    {
        GameObject featureGO = CreatePercentStatFeature("PsionicsEpicPrefix");
        PercentStatModifierFeature feature = featureGO.GetComponent<PercentStatModifierFeature>();
        feature.type = StatTypes.PercentMagicPen;
        psionics.Add(featureGO);
    }

    private void CreateFierySorcerers()
    {
        GameObject featureGO = CreateFlatStatFeature("FireDmgOnHit");
        PercentStatModifierFeature feature = featureGO.GetComponent<PercentStatModifierFeature>();
        feature.type = StatTypes.FireDmgOnHit;
        fierySorcerers.Add(featureGO);
        GameObject featureGO2 = CreateFlatStatFeature("FireDmgBonus");
        PercentStatModifierFeature feature2 = featureGO2.GetComponent<PercentStatModifierFeature>();
        feature2.type = StatTypes.FireDmgBonus;
        fierySorcerers.Add(featureGO2);
    }

    private void CreateIcySorcerers()
    {
        GameObject featureGO = CreateFlatStatFeature("ColdDmgOnHit");
        PercentStatModifierFeature feature = featureGO.GetComponent<PercentStatModifierFeature>();
        feature.type = StatTypes.ColdDmgOnHit;
        fierySorcerers.Add(featureGO);
        GameObject featureGO2 = CreateFlatStatFeature("ColdDmgBonus");
        PercentStatModifierFeature feature2 = featureGO2.GetComponent<PercentStatModifierFeature>();
        feature2.type = StatTypes.ColdDmgBonus;
        fierySorcerers.Add(featureGO2);
    }

    private void CreateThunderingSorcerers()
    {
        GameObject featureGO = CreateFlatStatFeature("LightningDmgOnHit");
        PercentStatModifierFeature feature = featureGO.GetComponent<PercentStatModifierFeature>();
        feature.type = StatTypes.LightningDmgOnHit;
        fierySorcerers.Add(featureGO);
        GameObject featureGO2 = CreateFlatStatFeature("LightningDmgBonus");
        PercentStatModifierFeature feature2 = featureGO2.GetComponent<PercentStatModifierFeature>();
        feature2.type = StatTypes.LightningDmgBonus;
        fierySorcerers.Add(featureGO2);
    }

    private void CreateCorrosiveSorcerers()
    {
        GameObject featureGO = CreateFlatStatFeature("PoisonDmgOnHit");
        PercentStatModifierFeature feature = featureGO.GetComponent<PercentStatModifierFeature>();
        feature.type = StatTypes.PoisonDmgOnHit;
        fierySorcerers.Add(featureGO);
        GameObject featureGO2 = CreateFlatStatFeature("PoisonDmgBonus");
        PercentStatModifierFeature feature2 = featureGO2.GetComponent<PercentStatModifierFeature>();
        feature2.type = StatTypes.PoisonDmgBonus;
        fierySorcerers.Add(featureGO2);
    }
}
