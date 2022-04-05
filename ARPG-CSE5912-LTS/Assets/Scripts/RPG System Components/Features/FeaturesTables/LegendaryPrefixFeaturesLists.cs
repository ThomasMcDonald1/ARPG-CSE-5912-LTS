using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegendaryPrefixFeaturesLists : FeaturesLists
{
    public List<GameObject> mordreths = new List<GameObject>();
    public List<GameObject> vextals = new List<GameObject>();
    public List<GameObject> fezzeraks = new List<GameObject>();
    public List<GameObject> dalneaus = new List<GameObject>();
    public List<GameObject> zaltens = new List<GameObject>();
    public List<GameObject> aldrichs = new List<GameObject>();
    public List<GameObject> vleks = new List<GameObject>();
    public List<GameObject> ivorens = new List<GameObject>();
    public List<GameObject> vlimliks = new List<GameObject>();
    public List<GameObject> barkors = new List<GameObject>();
    public List<GameObject> fordrands = new List<GameObject>();

    public void CreateLegendaryPrefixFeaturesLists()
    {
        CreateMordreths();
        CreateVextals();
        CreateFezzeraks();
        CreateDalneaus();
        CreateZaltens();
        CreateAldrichs();
        CreateVleks();
        CreateIvorens();
        CreateVlimliks();
        CreateBarkors();
        CreateFordrands();
    }

    private void CreateMordreths()
    {
        GameObject featureGO = CreateFlatStatFeature("FireDmgOnHit");
        FlatStatModifierFeature feature = featureGO.GetComponent<FlatStatModifierFeature>();
        feature.type = StatTypes.FireDmgOnHit;
        mordreths.Add(featureGO);
        GameObject featureGO2 = CreateFlatStatFeature("ColdDmgOnHit");
        FlatStatModifierFeature feature2 = featureGO2.GetComponent<FlatStatModifierFeature>();
        feature2.type = StatTypes.ColdDmgOnHit;
        mordreths.Add(featureGO2);
        GameObject featureGO3 = CreateFlatStatFeature("LightningDmgOnHit");
        FlatStatModifierFeature feature3 = featureGO3.GetComponent<FlatStatModifierFeature>();
        feature3.type = StatTypes.LightningDmgOnHit;
        mordreths.Add(featureGO3);
        GameObject featureGO4 = CreateFlatStatFeature("PoisonDmgOnHit");
        FlatStatModifierFeature feature4 = featureGO4.GetComponent<FlatStatModifierFeature>();
        feature4.type = StatTypes.PoisonDmgOnHit;
        mordreths.Add(featureGO4);
        GameObject featureGO5 = CreatePercentStatFeature("AtkSpeed");
        PercentStatModifierFeature feature5 = featureGO5.GetComponent<PercentStatModifierFeature>();
        feature.type = StatTypes.AtkSpeed;
        mordreths.Add(featureGO5);
    }

    private void CreateVextals()
    {
        GameObject featureGO = CreatePercentStatFeature("PoisonDmgBonus");
        PercentStatModifierFeature feature = featureGO.GetComponent<PercentStatModifierFeature>();
        feature.type = StatTypes.PoisonDmgBonus;
        vextals.Add(featureGO);
        GameObject featureGO2 = CreatePercentStatFeature("PercentPoisonRes");
        PercentStatModifierFeature feature2 = featureGO2.GetComponent<PercentStatModifierFeature>();
        feature2.type = StatTypes.PercentPoisonResistBonus;
        vextals.Add(featureGO2);
        GameObject featureGO3 = CreateFlatStatFeature("Evasion");
        FlatStatModifierFeature feature3 = featureGO3.GetComponent<FlatStatModifierFeature>();
        feature3.type = StatTypes.Evasion;
        vextals.Add(featureGO3);
    }

    private void CreateFezzeraks()
    {
        GameObject featureGO = CreatePercentStatFeature("FireDmgBonus");
        PercentStatModifierFeature feature = featureGO.GetComponent<PercentStatModifierFeature>();
        feature.type = StatTypes.FireDmgBonus;
        fezzeraks.Add(featureGO);
        GameObject featureGO2 = CreatePercentStatFeature("PercentFireRes");
        PercentStatModifierFeature feature2 = featureGO2.GetComponent<PercentStatModifierFeature>();
        feature2.type = StatTypes.PercentFireResistBonus;
        fezzeraks.Add(featureGO2);
        GameObject featureGO3 = CreatePercentStatFeature("CastSpeed");
        PercentStatModifierFeature feature3 = featureGO3.GetComponent<PercentStatModifierFeature>();
        feature3.type = StatTypes.CastSpeed;
        fezzeraks.Add(featureGO3);
    }

    private void CreateDalneaus()
    {
        GameObject featureGO = CreatePercentStatFeature("ColdDmgBonus");
        PercentStatModifierFeature feature = featureGO.GetComponent<PercentStatModifierFeature>();
        feature.type = StatTypes.ColdDmgBonus;
        dalneaus.Add(featureGO);
        GameObject featureGO2 = CreatePercentStatFeature("PercentColdRes");
        PercentStatModifierFeature feature2 = featureGO2.GetComponent<PercentStatModifierFeature>();
        feature2.type = StatTypes.PercentColdResistBonus;
        dalneaus.Add(featureGO2);
        GameObject featureGO3 = CreatePercentStatFeature("PercentArmorBonus");
        PercentStatModifierFeature feature3 = featureGO3.GetComponent<PercentStatModifierFeature>();
        feature3.type = StatTypes.PercentArmorBonus;
        dalneaus.Add(featureGO3);
    }

    private void CreateZaltens()
    {
        GameObject featureGO = CreatePercentStatFeature("LightningDmgBonus");
        PercentStatModifierFeature feature = featureGO.GetComponent<PercentStatModifierFeature>();
        feature.type = StatTypes.LightningDmgBonus;
        zaltens.Add(featureGO);
        GameObject featureGO2 = CreatePercentStatFeature("PercentLightningRes");
        PercentStatModifierFeature feature2 = featureGO2.GetComponent<PercentStatModifierFeature>();
        feature2.type = StatTypes.PercentLightningResistBonus;
        zaltens.Add(featureGO2);
        GameObject featureGO3 = CreatePercentStatFeature("RunSpeed");
        PercentStatModifierFeature feature3 = featureGO3.GetComponent<PercentStatModifierFeature>();
        feature3.type = StatTypes.RunSpeed;
        zaltens.Add(featureGO3);
    }

    private void CreateAldrichs()
    {
        GameObject featureGO = CreatePercentStatFeature("AllResist");
        PercentStatModifierFeature feature = featureGO.GetComponent<PercentStatModifierFeature>();
        feature.type = StatTypes.PercentAllResistBonus;
        aldrichs.Add(featureGO);
        GameObject featureGO2 = CreatePercentStatFeature("MagicDmgBonus");
        PercentStatModifierFeature feature2 = featureGO2.GetComponent<PercentStatModifierFeature>();
        feature2.type = StatTypes.MagDmgBonus;
        aldrichs.Add(featureGO2);
        GameObject featureGO3 = CreatePercentStatFeature("CooldownReduction");
        PercentStatModifierFeature feature3 = featureGO3.GetComponent<PercentStatModifierFeature>();
        feature3.type = StatTypes.CooldownReduction;
        aldrichs.Add(featureGO3);
    }

    private void CreateVleks()
    {
        GameObject featureGO = CreatePercentStatFeature("CritChance");
        PercentStatModifierFeature feature = featureGO.GetComponent<PercentStatModifierFeature>();
        feature.type = StatTypes.CritChance;
        vleks.Add(featureGO);
        GameObject featureGO2 = CreatePercentStatFeature("CritDamage");
        PercentStatModifierFeature feature2 = featureGO2.GetComponent<PercentStatModifierFeature>();
        feature2.type = StatTypes.CritDamage;
        vleks.Add(featureGO2);
        GameObject featureGO3 = CreatePercentStatFeature("PercentArmorPen");
        PercentStatModifierFeature feature3 = featureGO3.GetComponent<PercentStatModifierFeature>();
        feature3.type = StatTypes.PercentArmorPen;
        vleks.Add(featureGO3);
    }

    private void CreateIvorens()
    {
        GameObject featureGO = CreatePercentStatFeature("CritChance");
        PercentStatModifierFeature feature = featureGO.GetComponent<PercentStatModifierFeature>();
        feature.type = StatTypes.CritChance;
        ivorens.Add(featureGO);
        GameObject featureGO2 = CreatePercentStatFeature("CritDamage");
        PercentStatModifierFeature feature2 = featureGO2.GetComponent<PercentStatModifierFeature>();
        feature2.type = StatTypes.CritDamage;
        ivorens.Add(featureGO2);
        GameObject featureGO3 = CreatePercentStatFeature("PercentMagicPen");
        PercentStatModifierFeature feature3 = featureGO3.GetComponent<PercentStatModifierFeature>();
        feature3.type = StatTypes.PercentMagicPen;
        ivorens.Add(featureGO3);
    }

    private void CreateVlimliks()
    {
        GameObject featureGO = CreatePercentStatFeature("PercentArmorBonus");
        PercentStatModifierFeature feature = featureGO.GetComponent<PercentStatModifierFeature>();
        feature.type = StatTypes.PercentArmorBonus;
        vlimliks.Add(featureGO);
        GameObject featureGO2 = CreateFlatStatFeature("MaxHP");
        FlatStatModifierFeature feature2 = featureGO2.GetComponent<FlatStatModifierFeature>();
        feature2.type = StatTypes.MaxHP;
        vlimliks.Add(featureGO2);
        GameObject featureGO3 = CreateFlatStatFeature("DamageReflect");
        FlatStatModifierFeature feature3 = featureGO3.GetComponent<FlatStatModifierFeature>();
        feature3.type = StatTypes.DamageReflect;
        vlimliks.Add(featureGO3);
    }

    private void CreateBarkors()
    {
        GameObject featureGO = CreateFlatStatFeature("MaxHP");
        FlatStatModifierFeature feature = featureGO.GetComponent<FlatStatModifierFeature>();
        feature.type = StatTypes.MaxHP;
        barkors.Add(featureGO);
        GameObject featureGO2 = CreatePercentStatFeature("AttackSpeed");
        PercentStatModifierFeature feature2 = featureGO2.GetComponent<PercentStatModifierFeature>();
        feature2.type = StatTypes.AtkSpeed;
        barkors.Add(featureGO2);
        GameObject featureGO3 = CreatePercentStatFeature("Lifesteal");
        PercentStatModifierFeature feature3 = featureGO3.GetComponent<PercentStatModifierFeature>();
        feature3.type = StatTypes.Lifesteal;
        barkors.Add(featureGO3);
    }

    private void CreateFordrands()
    {
        GameObject featureGO = CreateFlatStatFeature("BlockChance");
        FlatStatModifierFeature feature = featureGO.GetComponent<FlatStatModifierFeature>();
        feature.type = StatTypes.BlockChance;
        fordrands.Add(featureGO);
        GameObject featureGO2 = CreateFlatStatFeature("Armor");
        FlatStatModifierFeature feature2 = featureGO2.GetComponent<FlatStatModifierFeature>();
        feature2.type = StatTypes.Armor;
        fordrands.Add(featureGO2);
        GameObject featureGO3 = CreateFlatStatFeature("MaxHP");
        FlatStatModifierFeature feature3 = featureGO3.GetComponent<FlatStatModifierFeature>();
        feature3.type = StatTypes.MaxHP;
        fordrands.Add(featureGO3);
    }
}
