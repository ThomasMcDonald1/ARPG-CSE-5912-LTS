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
        GameObject featureGO5 = CreateFlatStatFeature("AtkSpeed");
        FlatStatModifierFeature feature5 = featureGO5.GetComponent<FlatStatModifierFeature>();
        feature5.type = StatTypes.AtkSpeed;
        mordreths.Add(featureGO5);
    }

    private void CreateVextals()
    {
        GameObject featureGO = CreateFlatStatFeature("PoisonDmgBonus");
        FlatStatModifierFeature feature = featureGO.GetComponent<FlatStatModifierFeature>();
        feature.type = StatTypes.PoisonDmgBonus;
        vextals.Add(featureGO);
        GameObject featureGO2 = CreateFlatStatFeature("PercentPoisonRes");
        FlatStatModifierFeature feature2 = featureGO2.GetComponent<FlatStatModifierFeature>();
        feature2.type = StatTypes.PercentPoisonResistBonus;
        vextals.Add(featureGO2);
        GameObject featureGO3 = CreateFlatStatFeature("Evasion");
        FlatStatModifierFeature feature3 = featureGO3.GetComponent<FlatStatModifierFeature>();
        feature3.type = StatTypes.Evasion;
        vextals.Add(featureGO3);
    }

    private void CreateFezzeraks()
    {
        GameObject featureGO = CreateFlatStatFeature("FireDmgBonus");
        FlatStatModifierFeature feature = featureGO.GetComponent<FlatStatModifierFeature>();
        feature.type = StatTypes.FireDmgBonus;
        fezzeraks.Add(featureGO);
        GameObject featureGO2 = CreateFlatStatFeature("PercentFireRes");
        FlatStatModifierFeature feature2 = featureGO2.GetComponent<FlatStatModifierFeature>();
        feature2.type = StatTypes.PercentFireResistBonus;
        fezzeraks.Add(featureGO2);
        GameObject featureGO3 = CreateFlatStatFeature("CastSpeed");
        FlatStatModifierFeature feature3 = featureGO3.GetComponent<FlatStatModifierFeature>();
        feature3.type = StatTypes.CastSpeed;
        fezzeraks.Add(featureGO3);
    }

    private void CreateDalneaus()
    {
        GameObject featureGO = CreateFlatStatFeature("ColdDmgBonus");
        FlatStatModifierFeature feature = featureGO.GetComponent<FlatStatModifierFeature>();
        feature.type = StatTypes.ColdDmgBonus;
        dalneaus.Add(featureGO);
        GameObject featureGO2 = CreateFlatStatFeature("PercentColdRes");
        FlatStatModifierFeature feature2 = featureGO2.GetComponent<FlatStatModifierFeature>();
        feature2.type = StatTypes.PercentColdResistBonus;
        dalneaus.Add(featureGO2);
        GameObject featureGO3 = CreateFlatStatFeature("PercentArmorBonus");
        FlatStatModifierFeature feature3 = featureGO3.GetComponent<FlatStatModifierFeature>();
        feature3.type = StatTypes.PercentArmorBonus;
        dalneaus.Add(featureGO3);
    }

    private void CreateZaltens()
    {
        GameObject featureGO = CreateFlatStatFeature("LightningDmgBonus");
        FlatStatModifierFeature feature = featureGO.GetComponent<FlatStatModifierFeature>();
        feature.type = StatTypes.LightningDmgBonus;
        zaltens.Add(featureGO);
        GameObject featureGO2 = CreateFlatStatFeature("PercentLightningRes");
        FlatStatModifierFeature feature2 = featureGO2.GetComponent<FlatStatModifierFeature>();
        feature2.type = StatTypes.PercentLightningResistBonus;
        zaltens.Add(featureGO2);
        GameObject featureGO3 = CreateFlatStatFeature("RunSpeed");
        FlatStatModifierFeature feature3 = featureGO3.GetComponent<FlatStatModifierFeature>();
        feature3.type = StatTypes.RunSpeed;
        zaltens.Add(featureGO3);
    }

    private void CreateAldrichs()
    {
        GameObject featureGO = CreateFlatStatFeature("AllResist");
        FlatStatModifierFeature feature = featureGO.GetComponent<FlatStatModifierFeature>();
        feature.type = StatTypes.PercentAllResistBonus;
        aldrichs.Add(featureGO);
        GameObject featureGO2 = CreateFlatStatFeature("MagicDmgBonus");
        FlatStatModifierFeature feature2 = featureGO2.GetComponent<FlatStatModifierFeature>();
        feature2.type = StatTypes.MagDmgBonus;
        aldrichs.Add(featureGO2);
        GameObject featureGO3 = CreateFlatStatFeature("CooldownReduction");
        FlatStatModifierFeature feature3 = featureGO3.GetComponent<FlatStatModifierFeature>();
        feature3.type = StatTypes.CooldownReduction;
        aldrichs.Add(featureGO3);
    }

    private void CreateVleks()
    {
        GameObject featureGO = CreateFlatStatFeature("CritChance");
        FlatStatModifierFeature feature = featureGO.GetComponent<FlatStatModifierFeature>();
        feature.type = StatTypes.CritChance;
        vleks.Add(featureGO);
        GameObject featureGO2 = CreateFlatStatFeature("CritDamage");
        FlatStatModifierFeature feature2 = featureGO2.GetComponent<FlatStatModifierFeature>();
        feature2.type = StatTypes.CritDamage;
        vleks.Add(featureGO2);
        GameObject featureGO3 = CreateFlatStatFeature("PercentArmorPen");
        FlatStatModifierFeature feature3 = featureGO3.GetComponent<FlatStatModifierFeature>();
        feature3.type = StatTypes.PercentArmorPen;
        vleks.Add(featureGO3);
    }

    private void CreateIvorens()
    {
        GameObject featureGO = CreateFlatStatFeature("CritChance");
        FlatStatModifierFeature feature = featureGO.GetComponent<FlatStatModifierFeature>();
        feature.type = StatTypes.CritChance;
        ivorens.Add(featureGO);
        GameObject featureGO2 = CreateFlatStatFeature("CritDamage");
        FlatStatModifierFeature feature2 = featureGO2.GetComponent<FlatStatModifierFeature>();
        feature2.type = StatTypes.CritDamage;
        ivorens.Add(featureGO2);
        GameObject featureGO3 = CreateFlatStatFeature("PercentMagicPen");
        FlatStatModifierFeature feature3 = featureGO3.GetComponent<FlatStatModifierFeature>();
        feature3.type = StatTypes.PercentMagicPen;
        ivorens.Add(featureGO3);
    }

    private void CreateVlimliks()
    {
        GameObject featureGO = CreateFlatStatFeature("PercentArmorBonus");
        FlatStatModifierFeature feature = featureGO.GetComponent<FlatStatModifierFeature>();
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
        GameObject featureGO2 = CreateFlatStatFeature("AttackSpeed");
        FlatStatModifierFeature feature2 = featureGO2.GetComponent<FlatStatModifierFeature>();
        feature2.type = StatTypes.AtkSpeed;
        barkors.Add(featureGO2);
        GameObject featureGO3 = CreateFlatStatFeature("Lifesteal");
        FlatStatModifierFeature feature3 = featureGO3.GetComponent<FlatStatModifierFeature>();
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
