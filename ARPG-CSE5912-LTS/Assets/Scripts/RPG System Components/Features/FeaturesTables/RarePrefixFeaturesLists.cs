using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RarePrefixFeaturesLists : FeaturesLists
{
    public List<GameObject> punishers = new List<GameObject>();
    public List<GameObject> warlocks = new List<GameObject>();
    public List<GameObject> lorekeepers = new List<GameObject>();
    public List<GameObject> spellslingers = new List<GameObject>();
    public List<GameObject> sages = new List<GameObject>();
    public List<GameObject> fieryEnchanters = new List<GameObject>();
    public List<GameObject> icyEnchanters = new List<GameObject>();
    public List<GameObject> thunderingEnchanters = new List<GameObject>();
    public List<GameObject> corrosiveEnchanters = new List<GameObject>();
    public List<GameObject> knights = new List<GameObject>();
    public List<GameObject> brawlers = new List<GameObject>();
    public List<GameObject> wizards = new List<GameObject>();
    public List<GameObject> fighters = new List<GameObject>();
    public List<GameObject> brutalizers = new List<GameObject>();
    public List<GameObject> evokers = new List<GameObject>();

    public void CreateRarePrefixFeaturesLists()
    {
        CreatePunishers();
        CreateWarlocks();
        CreateLorekeepers();
        CreateSpellslingers();
        CreateSages();
        CreateFieryEnchanters();
        CreateIcyEnchanters();
        CreateThunderingEnchanters();
        CreateCorrosiveEnchanters();
        CreateKnights();
        CreateBrawlers();
        CreateWizards();
        CreateFighters();
        CreateBrutalizers();
        CreateEvokers();
    }

    private void CreatePunishers()
    {
        GameObject featureGO = CreateFlatStatFeature("PunishersRarePrefix");
        FlatStatModifierFeature feature = featureGO.GetComponent<FlatStatModifierFeature>();
        feature.type = StatTypes.FlatArmorPen;
        punishers.Add(featureGO);
    }

    private void CreateWarlocks()
    {
        GameObject featureGO = CreateFlatStatFeature("WarlocksRarePrefix");
        FlatStatModifierFeature feature = featureGO.GetComponent<FlatStatModifierFeature>();
        feature.type = StatTypes.FlatMagicPen;
        warlocks.Add(featureGO);
    }

    private void CreateLorekeepers()
    {
        GameObject featureGO = CreatePercentStatFeature("LorekeepersRarePrefix");
        PercentStatModifierFeature feature = featureGO.GetComponent<PercentStatModifierFeature>();
        feature.type = StatTypes.ExpGainMod;
        lorekeepers.Add(featureGO);
    }

    private void CreateSpellslingers()
    {
        GameObject featureGO = CreatePercentStatFeature("SpellslingersRarePrefix");
        PercentStatModifierFeature feature = featureGO.GetComponent<PercentStatModifierFeature>();
        feature.type = StatTypes.CastSpeed;
        spellslingers.Add(featureGO);
    }

    private void CreateSages()
    {
        GameObject featureGO = CreatePercentStatFeature("SagesRarePrefix");
        PercentStatModifierFeature feature = featureGO.GetComponent<PercentStatModifierFeature>();
        feature.type = StatTypes.CooldownReduction;
        sages.Add(featureGO);
    }

    private void CreateFieryEnchanters()
    {
        GameObject featureGO = CreateFlatStatFeature("FieryEnchantersRarePrefix");
        FlatStatModifierFeature feature = featureGO.GetComponent<FlatStatModifierFeature>();
        feature.type = StatTypes.FireDmgOnHit;
        fieryEnchanters.Add(featureGO);
    }

    private void CreateIcyEnchanters()
    {
        GameObject featureGO = CreateFlatStatFeature("IcyEnchantersRarePrefix");
        FlatStatModifierFeature feature = featureGO.GetComponent<FlatStatModifierFeature>();
        feature.type = StatTypes.ColdDmgOnHit;
        icyEnchanters.Add(featureGO);
    }

    private void CreateThunderingEnchanters()
    {
        GameObject featureGO = CreateFlatStatFeature("ThunderingEnchantersRarePrefix");
        FlatStatModifierFeature feature = featureGO.GetComponent<FlatStatModifierFeature>();
        feature.type = StatTypes.LightningDmgOnHit;
        thunderingEnchanters.Add(featureGO);
    }

    private void CreateCorrosiveEnchanters()
    {
        GameObject featureGO = CreateFlatStatFeature("CorrosiveEnchantersRarePrefix");
        FlatStatModifierFeature feature = featureGO.GetComponent<FlatStatModifierFeature>();
        feature.type = StatTypes.PoisonDmgOnHit;
        corrosiveEnchanters.Add(featureGO);
    }

    private void CreateKnights()
    {
        GameObject featureGO = CreateFlatStatFeature("KnightsRarePrefix");
        FlatStatModifierFeature feature = featureGO.GetComponent<FlatStatModifierFeature>();
        feature.type = StatTypes.Armor;
        knights.Add(featureGO);
    }

    private void CreateBrawlers()
    {
        GameObject featureGO = CreateFlatStatFeature("BrawlersRarePrefix");
        FlatStatModifierFeature feature = featureGO.GetComponent<FlatStatModifierFeature>();
        feature.type = StatTypes.MaxHP;
        brawlers.Add(featureGO);
    }

    private void CreateWizards()
    {
        GameObject featureGO = CreateFlatStatFeature("WizardsRarePrefix");
        FlatStatModifierFeature feature = featureGO.GetComponent<FlatStatModifierFeature>();
        feature.type = StatTypes.MaxMana;
        wizards.Add(featureGO);
    }

    private void CreateFighters()
    {
        GameObject featureGO = CreateFlatStatFeature("FightersRarePrefix");
        FlatStatModifierFeature feature = featureGO.GetComponent<FlatStatModifierFeature>();
        feature.type = StatTypes.PHYATK;
        fighters.Add(featureGO);
    }

    private void CreateBrutalizers()
    {
        GameObject featureGO = CreatePercentStatFeature("BrutalizersRarePrefix");
        PercentStatModifierFeature feature = featureGO.GetComponent<PercentStatModifierFeature>();
        feature.type = StatTypes.PhysDmgBonus;
        brutalizers.Add(featureGO);
    }

    private void CreateEvokers()
    {
        GameObject featureGO = CreatePercentStatFeature("EvokersRarePrefix");
        PercentStatModifierFeature feature = featureGO.GetComponent<PercentStatModifierFeature>();
        feature.type = StatTypes.MagDmgBonus;
        evokers.Add(featureGO);
    }
}
