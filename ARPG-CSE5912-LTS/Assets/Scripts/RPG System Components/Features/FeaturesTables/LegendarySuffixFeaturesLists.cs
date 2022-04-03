using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegendarySuffixFeaturesLists : FeaturesLists
{
    public List<GameObject> spiritStriking = new List<GameObject>();
    public List<GameObject> theCheetah = new List<GameObject>();
    public List<GameObject> spikes = new List<GameObject>();
    public List<GameObject> carnage = new List<GameObject>();
    public List<GameObject> destruction = new List<GameObject>();
    public List<GameObject> clearMind = new List<GameObject>();
    public List<GameObject> demonicVigor = new List<GameObject>();
    public List<GameObject> celestialBlessings = new List<GameObject>();
    public List<GameObject> theTiger = new List<GameObject>();
    public List<GameObject> theWall = new List<GameObject>();
    public List<GameObject> theInferno = new List<GameObject>();
    public List<GameObject> theStorm = new List<GameObject>();
    public List<GameObject> frostbite = new List<GameObject>();
    public List<GameObject> theViper = new List<GameObject>();
    public List<GameObject> theColossus = new List<GameObject>();

    public void CreateLegendarySuffixFeaturesLists()
    {
        CreateSpiritStriking();
        CreateTheCheetah();
        CreateSpikes();
        CreateCarnage();
        CreateDestruction();
        CreateClearMind();
        CreateDemonicVigor();
        CreateCelestialBlessings();
        CreateTheTiger();
        CreateTheWall();
        CreateTheInferno();
        CreateTheStorm();
        CreateFrostbite();
        CreateTheViper();
        CreateTheColossus();
    }

    private void CreateSpiritStriking()
    {
        GameObject featureGO = CreateFlatStatFeature("AttackRange");
        FlatStatModifierFeature feature = featureGO.GetComponent<FlatStatModifierFeature>();
        feature.type = StatTypes.AttackRange;
        spiritStriking.Add(featureGO);
        GameObject featureGO2 = CreateFlatStatFeature("ColdDmgOnHit");
        FlatStatModifierFeature feature2 = featureGO2.GetComponent<FlatStatModifierFeature>();
        feature2.type = StatTypes.ColdDmgOnHit;
        spiritStriking.Add(featureGO2);
    }

    private void CreateTheCheetah()
    {
        GameObject featureGO = CreatePercentStatFeature("RunSpeed");
        PercentStatModifierFeature feature = featureGO.GetComponent<PercentStatModifierFeature>();
        feature.type = StatTypes.RunSpeed;
        theCheetah.Add(featureGO);
        GameObject featureGO2 = CreatePercentStatFeature("AtkSpeed");
        PercentStatModifierFeature feature2 = featureGO2.GetComponent<PercentStatModifierFeature>();
        feature2.type = StatTypes.AtkSpeed;
        theCheetah.Add(featureGO2);
    }

    private void CreateSpikes()
    {
        GameObject featureGO = CreateFlatStatFeature("DamageReflect");
        FlatStatModifierFeature feature = featureGO.GetComponent<FlatStatModifierFeature>();
        feature.type = StatTypes.DamageReflect;
        spikes.Add(featureGO);
        GameObject featureGO2 = CreateFlatStatFeature("Armor");
        FlatStatModifierFeature feature2 = featureGO2.GetComponent<FlatStatModifierFeature>();
        feature2.type = StatTypes.Armor;
        spikes.Add(featureGO2);
    }

    private void CreateCarnage()
    {
        GameObject featureGO = CreatePercentStatFeature("CritChance");
        PercentStatModifierFeature feature = featureGO.GetComponent<PercentStatModifierFeature>();
        feature.type = StatTypes.CritChance;
        carnage.Add(featureGO);
        GameObject featureGO2 = CreatePercentStatFeature("AtkSpeed");
        PercentStatModifierFeature feature2 = featureGO2.GetComponent<PercentStatModifierFeature>();
        feature2.type = StatTypes.AtkSpeed;
        carnage.Add(featureGO2);
    }

    private void CreateDestruction()
    {
        GameObject featureGO = CreateFlatStatFeature("PHYATK");
        FlatStatModifierFeature feature = featureGO.GetComponent<FlatStatModifierFeature>();
        feature.type = StatTypes.PHYATK;
        destruction.Add(featureGO);
        GameObject featureGO2 = CreatePercentStatFeature("CritDamage");
        PercentStatModifierFeature feature2 = featureGO2.GetComponent<PercentStatModifierFeature>();
        feature2.type = StatTypes.CritDamage;
        destruction.Add(featureGO2);
    }

    private void CreateClearMind()
    {
        GameObject featureGO = CreateFlatStatFeature("MaxMana");
        FlatStatModifierFeature feature = featureGO.GetComponent<FlatStatModifierFeature>();
        feature.type = StatTypes.MaxMana;
        clearMind.Add(featureGO);
        GameObject featureGO2 = CreatePercentStatFeature("CastSpeed");
        PercentStatModifierFeature feature2 = featureGO2.GetComponent<PercentStatModifierFeature>();
        feature2.type = StatTypes.CastSpeed;
        clearMind.Add(featureGO2);
    }

    private void CreateDemonicVigor()
    {
        GameObject featureGO = CreatePercentStatFeature("Lifesteal");
        PercentStatModifierFeature feature = featureGO.GetComponent<PercentStatModifierFeature>();
        feature.type = StatTypes.Lifesteal;
        demonicVigor.Add(featureGO);
        GameObject featureGO2 = CreatePercentStatFeature("PercentArmorPen");
        PercentStatModifierFeature feature2 = featureGO2.GetComponent<PercentStatModifierFeature>();
        feature2.type = StatTypes.PercentArmorPen;
        demonicVigor.Add(featureGO2);
    }

    private void CreateCelestialBlessings()
    {
        GameObject featureGO = CreatePercentStatFeature("CooldownReduction");
        PercentStatModifierFeature feature = featureGO.GetComponent<PercentStatModifierFeature>();
        feature.type = StatTypes.CooldownReduction;
        celestialBlessings.Add(featureGO);
        GameObject featureGO2 = CreatePercentStatFeature("MagDmgBonus");
        PercentStatModifierFeature feature2 = featureGO2.GetComponent<PercentStatModifierFeature>();
        feature2.type = StatTypes.MagDmgBonus;
        celestialBlessings.Add(featureGO2);
    }

    private void CreateTheTiger()
    {
        GameObject featureGO = CreatePercentStatFeature("Evasion");
        PercentStatModifierFeature feature = featureGO.GetComponent<PercentStatModifierFeature>();
        feature.type = StatTypes.Evasion;
        theTiger.Add(featureGO);
        GameObject featureGO2 = CreatePercentStatFeature("CritDamage");
        PercentStatModifierFeature feature2 = featureGO2.GetComponent<PercentStatModifierFeature>();
        feature2.type = StatTypes.CritDamage;
        theTiger.Add(featureGO2);
    }

    private void CreateTheWall()
    {
        GameObject featureGO = CreatePercentStatFeature("BlockChance");
        PercentStatModifierFeature feature = featureGO.GetComponent<PercentStatModifierFeature>();
        feature.type = StatTypes.BlockChance;
        theWall.Add(featureGO);
        GameObject featureGO2 = CreatePercentStatFeature("PercentAllResist");
        PercentStatModifierFeature feature2 = featureGO2.GetComponent<PercentStatModifierFeature>();
        feature2.type = StatTypes.PercentAllResistBonus;
        theWall.Add(featureGO2);
    }

    private void CreateTheInferno()
    {
        GameObject featureGO = CreateFlatStatFeature("ColdRes");
        FlatStatModifierFeature feature = featureGO.GetComponent<FlatStatModifierFeature>();
        feature.type = StatTypes.ColdRes;
        theInferno.Add(featureGO);
        GameObject featureGO2 = CreatePercentStatFeature("FireDmgBonus");
        PercentStatModifierFeature feature2 = featureGO2.GetComponent<PercentStatModifierFeature>();
        feature2.type = StatTypes.FireDmgBonus;
        theInferno.Add(featureGO2);
    }

    private void CreateTheStorm()
    {
        GameObject featureGO = CreateFlatStatFeature("PoisonRes");
        FlatStatModifierFeature feature = featureGO.GetComponent<FlatStatModifierFeature>();
        feature.type = StatTypes.PoisonRes;
        theStorm.Add(featureGO);
        GameObject featureGO2 = CreatePercentStatFeature("LightningDmgBonus");
        PercentStatModifierFeature feature2 = featureGO2.GetComponent<PercentStatModifierFeature>();
        feature2.type = StatTypes.LightningDmgBonus;
        theStorm.Add(featureGO2);
    }

    private void CreateFrostbite()
    {
        GameObject featureGO = CreateFlatStatFeature("FireRes");
        FlatStatModifierFeature feature = featureGO.GetComponent<FlatStatModifierFeature>();
        feature.type = StatTypes.FireRes;
        frostbite.Add(featureGO);
        GameObject featureGO2 = CreatePercentStatFeature("ColdDmgBonus");
        PercentStatModifierFeature feature2 = featureGO2.GetComponent<PercentStatModifierFeature>();
        feature2.type = StatTypes.ColdDmgBonus;
        frostbite.Add(featureGO2);
    }

    private void CreateTheViper()
    {
        GameObject featureGO = CreateFlatStatFeature("LightningRes");
        FlatStatModifierFeature feature = featureGO.GetComponent<FlatStatModifierFeature>();
        feature.type = StatTypes.LightningRes;
        theViper.Add(featureGO);
        GameObject featureGO2 = CreatePercentStatFeature("PoisonDmgBonus");
        PercentStatModifierFeature feature2 = featureGO2.GetComponent<PercentStatModifierFeature>();
        feature2.type = StatTypes.PoisonDmgBonus;
        theViper.Add(featureGO2);
    }

    private void CreateTheColossus()
    {
        GameObject featureGO = CreateFlatStatFeature("MaxHP");
        FlatStatModifierFeature feature = featureGO.GetComponent<FlatStatModifierFeature>();
        feature.type = StatTypes.MaxHP;
        theColossus.Add(featureGO);
        GameObject featureGO2 = CreatePercentStatFeature("PercentArmorBonus");
        PercentStatModifierFeature feature2 = featureGO2.GetComponent<PercentStatModifierFeature>();
        feature2.type = StatTypes.PercentArmorBonus;
        theColossus.Add(featureGO2);
    }
}
