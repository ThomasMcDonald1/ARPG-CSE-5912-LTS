using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EpicPrefixFeaturesLists : FeaturesLists
{
    public List<Feature> travelers = new List<Feature>();
    public List<Feature> vampiric = new List<Feature>();
    public List<Feature> berserkers = new List<Feature>();
    public List<Feature> exploiters = new List<Feature>();
    public List<Feature> bloodletters = new List<Feature>();
    public List<Feature> impalers = new List<Feature>();
    public List<Feature> psionics = new List<Feature>();
    public List<Feature> fierySorcerers = new List<Feature>();
    public List<Feature> icySorcerers = new List<Feature>();
    public List<Feature> thunderingSorcerers = new List<Feature>();
    public List<Feature> corrosiveSorcerers = new List<Feature>();

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
        PercentStatModifierFeature feature = CreatePercentStatFeature("TravelersEpicPrefix");
        feature.type = StatTypes.RunSpeed;
        travelers.Add(feature);
    }

    private void CreateVampiric()
    {
        PercentStatModifierFeature feature = CreatePercentStatFeature("VampiricEpicPrefix");
        feature.type = StatTypes.Lifesteal;
        vampiric.Add(feature);
    }

    private void CreateBerserkers()
    {
        PercentStatModifierFeature feature = CreatePercentStatFeature("BerserkersEpicPrefix");
        feature.type = StatTypes.AtkSpeed;
        berserkers.Add(feature);
    }

    private void CreateExploiters()
    {
        PercentStatModifierFeature feature = CreatePercentStatFeature("ExploitersEpicPrefix");
        feature.type = StatTypes.CritChance;
        exploiters.Add(feature);
    }

    private void CreateBloodletters()
    {
        PercentStatModifierFeature feature = CreatePercentStatFeature("BloodlettersEpicPrefix");
        feature.type = StatTypes.CritDamage;
        bloodletters.Add(feature);
    }

    private void CreateImpalers()
    {
        PercentStatModifierFeature feature = CreatePercentStatFeature("ImpalersEpicPrefix");
        feature.type = StatTypes.PercentArmorPen;
        impalers.Add(feature);
    }

    private void CreatePsionics()
    {
        PercentStatModifierFeature feature = CreatePercentStatFeature("PsionicsEpicPrefix");
        feature.type = StatTypes.PercentMagicPen;
        psionics.Add(feature);
    }

    private void CreateFierySorcerers()
    {
        FlatStatModifierFeature feature = CreateFlatStatFeature("FireDmgOnHit");
        feature.type = StatTypes.FireDmgOnHit;
        fierySorcerers.Add(feature);
        FlatStatModifierFeature feature2 = CreateFlatStatFeature("FireDmgBonus");
        feature2.type = StatTypes.FireDmgBonus;
        fierySorcerers.Add(feature);
    }

    private void CreateIcySorcerers()
    {
        FlatStatModifierFeature feature = CreateFlatStatFeature("ColdDmgOnHit");
        feature.type = StatTypes.ColdDmgOnHit;
        fierySorcerers.Add(feature);
        FlatStatModifierFeature feature2 = CreateFlatStatFeature("ColdDmgBonus");
        feature2.type = StatTypes.ColdDmgBonus;
        fierySorcerers.Add(feature);
    }

    private void CreateThunderingSorcerers()
    {
        FlatStatModifierFeature feature = CreateFlatStatFeature("LightningDmgOnHit");
        feature.type = StatTypes.LightningDmgOnHit;
        fierySorcerers.Add(feature);
        FlatStatModifierFeature feature2 = CreateFlatStatFeature("LightningDmgBonus");
        feature2.type = StatTypes.LightningDmgBonus;
        fierySorcerers.Add(feature);
    }

    private void CreateCorrosiveSorcerers()
    {
        FlatStatModifierFeature feature = CreateFlatStatFeature("PoisonDmgOnHit");
        feature.type = StatTypes.PoisonDmgOnHit;
        fierySorcerers.Add(feature);
        FlatStatModifierFeature feature2 = CreateFlatStatFeature("PoisonDmgBonus");
        feature2.type = StatTypes.PoisonDmgBonus;
        fierySorcerers.Add(feature);
    }
}
