using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegendaryPrefixFeaturesLists : FeaturesLists
{
    public List<Feature> mordreths = new List<Feature>();
    public List<Feature> vextals = new List<Feature>();
    public List<Feature> fezzeraks = new List<Feature>();
    public List<Feature> dalneaus = new List<Feature>();
    public List<Feature> zaltens = new List<Feature>();
    public List<Feature> aldrichs = new List<Feature>();
    public List<Feature> vleks = new List<Feature>();
    public List<Feature> ivorens = new List<Feature>();
    public List<Feature> vlimliks = new List<Feature>();
    public List<Feature> barkors = new List<Feature>();
    public List<Feature> fordrands = new List<Feature>();

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
        FlatStatModifierFeature feature = CreateFlatStatFeature("FireDmgOnHit");
        feature.type = StatTypes.FireDmgOnHit;
        mordreths.Add(feature);
        FlatStatModifierFeature feature2 = CreateFlatStatFeature("ColdDmgOnHit");
        feature2.type = StatTypes.ColdDmgOnHit;
        mordreths.Add(feature2);
        FlatStatModifierFeature feature3 = CreateFlatStatFeature("LightningDmgOnHit");
        feature3.type = StatTypes.LightningDmgOnHit;
        mordreths.Add(feature3);
        FlatStatModifierFeature feature4 = CreateFlatStatFeature("PoisonDmgOnHit");
        feature4.type = StatTypes.PoisonDmgOnHit;
        mordreths.Add(feature4);
        PercentStatModifierFeature feature5 = CreatePercentStatFeature("AtkSpeed");
        feature.type = StatTypes.AtkSpeed;
        mordreths.Add(feature5);
    }

    private void CreateVextals()
    {
        PercentStatModifierFeature feature = CreatePercentStatFeature("PoisonDmgBonus");
        feature.type = StatTypes.PoisonDmgBonus;
        vextals.Add(feature);
        PercentStatModifierFeature feature2 = CreatePercentStatFeature("PercentPoisonRes");
        feature2.type = StatTypes.PercentPoisonResistBonus;
        vextals.Add(feature2);
        FlatStatModifierFeature feature3 = CreateFlatStatFeature("Evasion");
        feature3.type = StatTypes.Evasion;
        vextals.Add(feature3);
    }

    private void CreateFezzeraks()
    {
        PercentStatModifierFeature feature = CreatePercentStatFeature("FireDmgBonus");
        feature.type = StatTypes.FireDmgBonus;
        fezzeraks.Add(feature);
        PercentStatModifierFeature feature2 = CreatePercentStatFeature("PercentFireRes");
        feature2.type = StatTypes.PercentFireResistBonus;
        fezzeraks.Add(feature2);
        PercentStatModifierFeature feature3 = CreatePercentStatFeature("CastSpeed");
        feature3.type = StatTypes.CastSpeed;
        fezzeraks.Add(feature3);
    }

    private void CreateDalneaus()
    {
        PercentStatModifierFeature feature = CreatePercentStatFeature("ColdDmgBonus");
        feature.type = StatTypes.ColdDmgBonus;
        dalneaus.Add(feature);
        PercentStatModifierFeature feature2 = CreatePercentStatFeature("PercentColdRes");
        feature2.type = StatTypes.PercentColdResistBonus;
        dalneaus.Add(feature2);
        PercentStatModifierFeature feature3 = CreatePercentStatFeature("PercentArmorBonus");
        feature3.type = StatTypes.PercentArmorBonus;
        dalneaus.Add(feature3);
    }

    private void CreateZaltens()
    {
        PercentStatModifierFeature feature = CreatePercentStatFeature("LightningDmgBonus");
        feature.type = StatTypes.LightningDmgBonus;
        zaltens.Add(feature);
        PercentStatModifierFeature feature2 = CreatePercentStatFeature("PercentLightningRes");
        feature2.type = StatTypes.PercentLightningResistBonus;
        zaltens.Add(feature2);
        PercentStatModifierFeature feature3 = CreatePercentStatFeature("RunSpeed");
        feature3.type = StatTypes.RunSpeed;
        zaltens.Add(feature3);
    }

    private void CreateAldrichs()
    {
        PercentStatModifierFeature feature = CreatePercentStatFeature("AllResist");
        feature.type = StatTypes.PercentAllResistBonus;
        aldrichs.Add(feature);
        PercentStatModifierFeature feature2 = CreatePercentStatFeature("MagicDmgBonus");
        feature2.type = StatTypes.MagDmgBonus;
        aldrichs.Add(feature2);
        PercentStatModifierFeature feature3 = CreatePercentStatFeature("CooldownReduction");
        feature3.type = StatTypes.CooldownReduction;
        aldrichs.Add(feature3);
    }

    private void CreateVleks()
    {
        PercentStatModifierFeature feature = CreatePercentStatFeature("CritChance");
        feature.type = StatTypes.CritChance;
        vleks.Add(feature);
        PercentStatModifierFeature feature2 = CreatePercentStatFeature("CritDamage");
        feature2.type = StatTypes.CritDamage;
        vleks.Add(feature2);
        PercentStatModifierFeature feature3 = CreatePercentStatFeature("PercentArmorPen");
        feature3.type = StatTypes.PercentArmorPen;
        vleks.Add(feature3);
    }

    private void CreateIvorens()
    {
        PercentStatModifierFeature feature = CreatePercentStatFeature("CritChance");
        feature.type = StatTypes.CritChance;
        ivorens.Add(feature);
        PercentStatModifierFeature feature2 = CreatePercentStatFeature("CritDamage");
        feature2.type = StatTypes.CritDamage;
        ivorens.Add(feature2);
        PercentStatModifierFeature feature3 = CreatePercentStatFeature("PercentMagicPen");
        feature3.type = StatTypes.PercentMagicPen;
        ivorens.Add(feature3);
    }

    private void CreateVlimliks()
    {
        PercentStatModifierFeature feature = CreatePercentStatFeature("PercentArmorBonus");
        feature.type = StatTypes.PercentArmorBonus;
        vlimliks.Add(feature);
        FlatStatModifierFeature feature2 = CreateFlatStatFeature("MaxHP");
        feature2.type = StatTypes.MaxHP;
        vlimliks.Add(feature2);
        FlatStatModifierFeature feature3 = CreateFlatStatFeature("DamageReflect");
        feature3.type = StatTypes.DamageReflect;
        vlimliks.Add(feature3);
    }

    private void CreateBarkors()
    {
        FlatStatModifierFeature feature = CreateFlatStatFeature("MaxHP");
        feature.type = StatTypes.MaxHP;
        barkors.Add(feature);
        PercentStatModifierFeature feature2 = CreatePercentStatFeature("AttackSpeed");
        feature2.type = StatTypes.AtkSpeed;
        barkors.Add(feature2);
        PercentStatModifierFeature feature3 = CreatePercentStatFeature("Lifesteal");
        feature3.type = StatTypes.Lifesteal;
        barkors.Add(feature3);
    }

    private void CreateFordrands()
    {
        FlatStatModifierFeature feature = CreateFlatStatFeature("BlockChance");
        feature.type = StatTypes.BlockChance;
        fordrands.Add(feature);
        FlatStatModifierFeature feature2 = CreateFlatStatFeature("Armor");
        feature2.type = StatTypes.Armor;
        fordrands.Add(feature2);
        FlatStatModifierFeature feature3 = CreateFlatStatFeature("MaxHP");
        feature3.type = StatTypes.MaxHP;
        fordrands.Add(feature3);
    }
}
