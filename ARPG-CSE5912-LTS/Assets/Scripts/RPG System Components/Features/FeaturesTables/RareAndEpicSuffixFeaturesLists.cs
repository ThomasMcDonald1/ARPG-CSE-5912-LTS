using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RareAndEpicSuffixFeaturesLists : FeaturesLists
{
    public List<GameObject> thorns = new List<GameObject>();
    public List<GameObject> theBear = new List<GameObject>();
    public List<GameObject> theOwl = new List<GameObject>();
    public List<GameObject> theBull = new List<GameObject>();
    public List<GameObject> theCrab = new List<GameObject>();
    public List<GameObject> theTurtle = new List<GameObject>();
    public List<GameObject> frenziedStrikes = new List<GameObject>();
    public List<GameObject> pinpointStrikes = new List<GameObject>();
    public List<GameObject> brutality = new List<GameObject>();
    public List<GameObject> piercingStrikes = new List<GameObject>();
    public List<GameObject> elementalMastery = new List<GameObject>();
    public List<GameObject> theHammer = new List<GameObject>();
    public List<GameObject> theSpirits = new List<GameObject>();
    public List<GameObject> theQuick = new List<GameObject>();
    public List<GameObject> observation = new List<GameObject>();
    public List<GameObject> crushing = new List<GameObject>();
    public List<GameObject> blasting = new List<GameObject>();
    public List<GameObject> incineration = new List<GameObject>();
    public List<GameObject> chilling = new List<GameObject>();
    public List<GameObject> thunder = new List<GameObject>();
    public List<GameObject> theSnake = new List<GameObject>();
    public List<GameObject> siphoning = new List<GameObject>();
    public List<GameObject> acumen = new List<GameObject>();
    public List<GameObject> invigoration = new List<GameObject>();
    public List<GameObject> flame = new List<GameObject>();
    public List<GameObject> frost = new List<GameObject>();
    public List<GameObject> shocking = new List<GameObject>();
    public List<GameObject> stinging = new List<GameObject>();
    public List<GameObject> theTower = new List<GameObject>();
    public List<GameObject> tumbling = new List<GameObject>();
    public List<GameObject> fireResistance = new List<GameObject>();
    public List<GameObject> coldResistance = new List<GameObject>();
    public List<GameObject> lightningResistance = new List<GameObject>();
    public List<GameObject> poisonResistance = new List<GameObject>();
    public List<GameObject> fireWarding = new List<GameObject>();
    public List<GameObject> coldWarding = new List<GameObject>();
    public List<GameObject> lightningWarding = new List<GameObject>();
    public List<GameObject> poisonWarding = new List<GameObject>();
    public List<GameObject> alteredFlesh = new List<GameObject>();

    public void CreateRareOrEpicSuffixFeaturesLists()
    {
        CreateThorns();
        CreateTheBear();
        CreateTheOwl();
        CreateTheBull();
        CreateTheCrab();
        CreateTheTurtle();
        CreateFrenziedStrikes();
        CreatePinpointStrikes();
        CreateBrutality();
        CreatePiercingStrikes();
        CreateElementalMastery();
        CreateTheHammer();
        CreateTheSpirits();
        CreateTheQuick();
        CreateObservation();
        CreateCrushing();
        CreateBlasting();
        CreateIncineration();
        CreateChilling();
        CreateThunder();
        CreateTheSnake();
        CreateSiphoning();
        CreateAcumen();
        CreateInvigoration();
        CreateFlame();
        CreateFrost();
        CreateShocking();
        CreateStinging();
        CreateTheTower();
        CreateTumbling();
        CreateFireResistance();
        CreateColdResistance();
        CreateLightningResistance();
        CreatePoisonResistance();
        CreateFireWarding();
        CreateColdWarding();
        CreateLightningWarding();
        CreatePoisonWarding();
        CreateAlteredFlesh();
    }

    private void CreateThorns()
    {
        GameObject featureGO = CreateFlatStatFeature("DamageReflect");
        FlatStatModifierFeature feature = featureGO.GetComponent<FlatStatModifierFeature>();
        feature.type = StatTypes.DamageReflect;
        thorns.Add(featureGO);
    }

    private void CreateTheBear()
    {
        GameObject featureGO = CreateFlatStatFeature("MaxHP");
        FlatStatModifierFeature feature = featureGO.GetComponent<FlatStatModifierFeature>();
        feature.type = StatTypes.MaxHP;
        theBear.Add(featureGO);
    }

    private void CreateTheOwl()
    {
        GameObject featureGO = CreateFlatStatFeature("MaxMana");
        FlatStatModifierFeature feature = featureGO.GetComponent<FlatStatModifierFeature>();
        feature.type = StatTypes.MaxMana;
        theOwl.Add(featureGO);
    }

    private void CreateTheBull()
    {
        GameObject featureGO = CreateFlatStatFeature("PHYATK");
        FlatStatModifierFeature feature = featureGO.GetComponent<FlatStatModifierFeature>();
        feature.type = StatTypes.PHYATK;
        theBull.Add(featureGO);
    }

    private void CreateTheCrab()
    {
        GameObject featureGO = CreateFlatStatFeature("Armor");
        FlatStatModifierFeature feature = featureGO.GetComponent<FlatStatModifierFeature>();
        feature.type = StatTypes.Armor;
        theCrab.Add(featureGO);
    }

    private void CreateTheTurtle()
    {
        GameObject featureGO = CreateFlatStatFeature("PercentArmorBonus");
        FlatStatModifierFeature feature = featureGO.GetComponent<FlatStatModifierFeature>();
        feature.type = StatTypes.PercentArmorBonus;
        theTurtle.Add(featureGO);
    }

    private void CreateFrenziedStrikes()
    {
        GameObject featureGO = CreateFlatStatFeature("AtkSpeed");
        FlatStatModifierFeature feature = featureGO.GetComponent<FlatStatModifierFeature>();
        feature.type = StatTypes.AtkSpeed;
        frenziedStrikes.Add(featureGO);
    }

    private void CreatePinpointStrikes()
    {
        GameObject featureGO = CreateFlatStatFeature("CritChance");
        FlatStatModifierFeature feature = featureGO.GetComponent<FlatStatModifierFeature>();
        feature.type = StatTypes.CritChance;
        pinpointStrikes.Add(featureGO);
    }

    private void CreateBrutality()
    {
        GameObject featureGO = CreateFlatStatFeature("CritDamage");
        FlatStatModifierFeature feature = featureGO.GetComponent<FlatStatModifierFeature>();
        feature.type = StatTypes.CritDamage;
        brutality.Add(featureGO);
    }

    private void CreatePiercingStrikes()
    {
        GameObject featureGO = CreateFlatStatFeature("FlatArmorPen");
        FlatStatModifierFeature feature = featureGO.GetComponent<FlatStatModifierFeature>();
        feature.type = StatTypes.FlatArmorPen;
        piercingStrikes.Add(featureGO);
    }

    private void CreateElementalMastery()
    {
        GameObject featureGO = CreateFlatStatFeature("FlatMagicPen");
        FlatStatModifierFeature feature = featureGO.GetComponent<FlatStatModifierFeature>();
        feature.type = StatTypes.FlatMagicPen;
        elementalMastery.Add(featureGO);
    }

    private void CreateTheHammer()
    {
        GameObject featureGO = CreateFlatStatFeature("PercentArmorPen");
        FlatStatModifierFeature feature = featureGO.GetComponent<FlatStatModifierFeature>();
        feature.type = StatTypes.PercentArmorPen;
        theHammer.Add(featureGO);
    }

    private void CreateTheSpirits()
    {
        GameObject featureGO = CreateFlatStatFeature("PercentMagicPen");
        FlatStatModifierFeature feature = featureGO.GetComponent<FlatStatModifierFeature>();
        feature.type = StatTypes.PercentMagicPen;
        theSpirits.Add(featureGO);
    }

    private void CreateTheQuick()
    {
        GameObject featureGO = CreateFlatStatFeature("RunSpeed");
        FlatStatModifierFeature feature = featureGO.GetComponent<FlatStatModifierFeature>();
        feature.type = StatTypes.RunSpeed;
        theQuick.Add(featureGO);
    }

    private void CreateObservation()
    {
        GameObject featureGO = CreateFlatStatFeature("ExpGainMod");
        FlatStatModifierFeature feature = featureGO.GetComponent<FlatStatModifierFeature>();
        feature.type = StatTypes.ExpGainMod;
        observation.Add(featureGO);
    }

    private void CreateCrushing()
    {
        GameObject featureGO = CreateFlatStatFeature("PhysDmgBonus");
        FlatStatModifierFeature feature = featureGO.GetComponent<FlatStatModifierFeature>();
        feature.type = StatTypes.PhysDmgBonus;
        crushing.Add(featureGO);
    }

    private void CreateBlasting()
    {
        GameObject featureGO = CreateFlatStatFeature("MagDmgBonus");
        FlatStatModifierFeature feature = featureGO.GetComponent<FlatStatModifierFeature>();
        feature.type = StatTypes.MagDmgBonus;
        blasting.Add(featureGO);
    }

    private void CreateIncineration()
    {
        GameObject featureGO = CreateFlatStatFeature("FireDmgBonus");
        FlatStatModifierFeature feature = featureGO.GetComponent<FlatStatModifierFeature>();
        feature.type = StatTypes.FireDmgBonus;
        incineration.Add(featureGO);
    }

    private void CreateChilling()
    {
        GameObject featureGO = CreateFlatStatFeature("ColdDmgBonus");
        FlatStatModifierFeature feature = featureGO.GetComponent<FlatStatModifierFeature>();
        feature.type = StatTypes.ColdDmgBonus;
        chilling.Add(featureGO);
    }

    private void CreateThunder()
    {
        GameObject featureGO = CreateFlatStatFeature("LightningDmgBonus");
        FlatStatModifierFeature feature = featureGO.GetComponent<FlatStatModifierFeature>();
        feature.type = StatTypes.LightningDmgBonus;
        thunder.Add(featureGO);
    }

    private void CreateTheSnake()
    {
        GameObject featureGO = CreateFlatStatFeature("PoisonDmgBonus");
        FlatStatModifierFeature feature = featureGO.GetComponent<FlatStatModifierFeature>();
        feature.type = StatTypes.PoisonDmgBonus;
        theSnake.Add(featureGO);
    }

    private void CreateSiphoning()
    {
        GameObject featureGO = CreateFlatStatFeature("Lifesteal");
        FlatStatModifierFeature feature = featureGO.GetComponent<FlatStatModifierFeature>();
        feature.type = StatTypes.Lifesteal;
        siphoning.Add(featureGO);
    }

    private void CreateAcumen()
    {
        GameObject featureGO = CreateFlatStatFeature("CastSpeed");
        FlatStatModifierFeature feature = featureGO.GetComponent<FlatStatModifierFeature>();
        feature.type = StatTypes.CastSpeed;
        acumen.Add(featureGO);
    }

    private void CreateInvigoration()
    {
        GameObject featureGO = CreateFlatStatFeature("CooldownReduction");
        FlatStatModifierFeature feature = featureGO.GetComponent<FlatStatModifierFeature>();
        feature.type = StatTypes.CooldownReduction;
        invigoration.Add(featureGO);
    }

    private void CreateFlame()
    {
        GameObject featureGO = CreateFlatStatFeature("FireDmgOnHit");
        FlatStatModifierFeature feature = featureGO.GetComponent<FlatStatModifierFeature>();
        feature.type = StatTypes.FireDmgOnHit;
        flame.Add(featureGO);
    }

    private void CreateFrost()
    {
        GameObject featureGO = CreateFlatStatFeature("ColdDmgOnHit");
        FlatStatModifierFeature feature = featureGO.GetComponent<FlatStatModifierFeature>();
        feature.type = StatTypes.ColdDmgOnHit;
        frost.Add(featureGO);
    }
    
    private void CreateShocking()
    {
        GameObject featureGO = CreateFlatStatFeature("LightningDmgOnHit");
        FlatStatModifierFeature feature = featureGO.GetComponent<FlatStatModifierFeature>();
        feature.type = StatTypes.LightningDmgOnHit;
        shocking.Add(featureGO);
    }

    private void CreateStinging()
    {
        GameObject featureGO = CreateFlatStatFeature("PoisonDmgOnHit");
        FlatStatModifierFeature feature = featureGO.GetComponent<FlatStatModifierFeature>();
        feature.type = StatTypes.PoisonDmgOnHit;
        stinging.Add(featureGO);
    }

    private void CreateTheTower()
    {
        GameObject featureGO = CreateFlatStatFeature("BlockChance");
        FlatStatModifierFeature feature = featureGO.GetComponent<FlatStatModifierFeature>();
        feature.type = StatTypes.BlockChance;
        theTower.Add(featureGO);
    }

    private void CreateTumbling()
    {
        GameObject featureGO = CreateFlatStatFeature("Evasion");
        FlatStatModifierFeature feature = featureGO.GetComponent<FlatStatModifierFeature>();
        feature.type = StatTypes.Evasion;
        tumbling.Add(featureGO);
    }

    private void CreateFireResistance()
    {
        GameObject featureGO = CreateFlatStatFeature("FireRes");
        FlatStatModifierFeature feature = featureGO.GetComponent<FlatStatModifierFeature>();
        feature.type = StatTypes.FireRes;
        fireResistance.Add(featureGO);
    }

    private void CreateColdResistance()
    {
        GameObject featureGO = CreateFlatStatFeature("ColdRes");
        FlatStatModifierFeature feature = featureGO.GetComponent<FlatStatModifierFeature>();
        feature.type = StatTypes.ColdRes;
        coldResistance.Add(featureGO);
    }

    private void CreateLightningResistance()
    {
        GameObject featureGO = CreateFlatStatFeature("LightningRes");
        FlatStatModifierFeature feature = featureGO.GetComponent<FlatStatModifierFeature>();
        feature.type = StatTypes.LightningRes;
        lightningResistance.Add(featureGO);
    }

    private void CreatePoisonResistance()
    {
        GameObject featureGO = CreateFlatStatFeature("PoisonRes");
        FlatStatModifierFeature feature = featureGO.GetComponent<FlatStatModifierFeature>();
        feature.type = StatTypes.PoisonRes;
        poisonResistance.Add(featureGO);
    }

    private void CreateFireWarding()
    {
        GameObject featureGO = CreateFlatStatFeature("PercentFireRes");
        FlatStatModifierFeature feature = featureGO.GetComponent<FlatStatModifierFeature>();
        feature.type = StatTypes.PercentFireResistBonus;
        fireWarding.Add(featureGO);
    }

    private void CreateColdWarding()
    {
        GameObject featureGO = CreateFlatStatFeature("PercentColdRes");
        FlatStatModifierFeature feature = featureGO.GetComponent<FlatStatModifierFeature>();
        feature.type = StatTypes.PercentColdResistBonus;
        coldWarding.Add(featureGO);
    }

    private void CreateLightningWarding()
    {
        GameObject featureGO = CreateFlatStatFeature("PercentLightningRes");
        FlatStatModifierFeature feature = featureGO.GetComponent<FlatStatModifierFeature>();
        feature.type = StatTypes.PercentLightningResistBonus;
        lightningWarding.Add(featureGO);
    }

    private void CreatePoisonWarding()
    {
        GameObject featureGO = CreateFlatStatFeature("PercentPoisonRes");
        FlatStatModifierFeature feature = featureGO.GetComponent<FlatStatModifierFeature>();
        feature.type = StatTypes.PercentPoisonResistBonus;
        poisonWarding.Add(featureGO);
    }

    private void CreateAlteredFlesh()
    {
        GameObject featureGO = CreateFlatStatFeature("PercentAllRes");
        FlatStatModifierFeature feature = featureGO.GetComponent<FlatStatModifierFeature>();
        feature.type = StatTypes.PercentAllResistBonus;
        alteredFlesh.Add(featureGO);
    }
}
