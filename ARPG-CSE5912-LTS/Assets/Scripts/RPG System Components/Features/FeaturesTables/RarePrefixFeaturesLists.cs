using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RarePrefixFeaturesLists : FeaturesLists
{
    public List<Feature> punishers = new List<Feature>();
    public List<Feature> warlocks = new List<Feature>();
    public List<Feature> lorekeepers = new List<Feature>();
    public List<Feature> spellslingers = new List<Feature>();
    public List<Feature> sages = new List<Feature>();
    public List<Feature> fieryEnchanters = new List<Feature>();
    public List<Feature> icyEnchanters = new List<Feature>();
    public List<Feature> thunderingEnchanters = new List<Feature>();
    public List<Feature> corrosiveEnchanters = new List<Feature>();
    public List<Feature> knights = new List<Feature>();
    public List<Feature> brawlers = new List<Feature>();
    public List<Feature> wizards = new List<Feature>();
    public List<Feature> fighters = new List<Feature>();
    public List<Feature> brutalizers = new List<Feature>();
    public List<Feature> evokers = new List<Feature>();

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
        FlatStatModifierFeature feature = CreateFlatStatFeature("PunishersRarePrefix");
        feature.type = StatTypes.FlatArmorPen;
        punishers.Add(feature);
    }

    private void CreateWarlocks()
    {
        FlatStatModifierFeature feature = CreateFlatStatFeature("WarlocksRarePrefix");
        feature.type = StatTypes.FlatMagicPen;
        warlocks.Add(feature);
    }

    private void CreateLorekeepers()
    {
        PercentStatModifierFeature feature = CreatePercentStatFeature("LorekeepersRarePrefix");
        feature.type = StatTypes.ExpGainMod;
        lorekeepers.Add(feature);
    }

    private void CreateSpellslingers()
    {
        PercentStatModifierFeature feature = CreatePercentStatFeature("SpellslingersRarePrefix");
        feature.type = StatTypes.CastSpeed;
        spellslingers.Add(feature);
    }

    private void CreateSages()
    {
        PercentStatModifierFeature feature = CreatePercentStatFeature("SagesRarePrefix");
        feature.type = StatTypes.CooldownReduction;
        sages.Add(feature);
    }

    private void CreateFieryEnchanters()
    {
        FlatStatModifierFeature feature = CreateFlatStatFeature("FieryEnchantersRarePrefix");
        feature.type = StatTypes.FireDmgOnHit;
        fieryEnchanters.Add(feature);
    }

    private void CreateIcyEnchanters()
    {
        FlatStatModifierFeature feature = CreateFlatStatFeature("IcyEnchantersRarePrefix");
        feature.type = StatTypes.ColdDmgOnHit;
        icyEnchanters.Add(feature);
    }

    private void CreateThunderingEnchanters()
    {
        FlatStatModifierFeature feature = CreateFlatStatFeature("ThunderingEnchantersRarePrefix");
        feature.type = StatTypes.LightningDmgOnHit;
        thunderingEnchanters.Add(feature);
    }

    private void CreateCorrosiveEnchanters()
    {
        FlatStatModifierFeature feature = CreateFlatStatFeature("CorrosiveEnchantersRarePrefix");
        feature.type = StatTypes.PoisonDmgOnHit;
        corrosiveEnchanters.Add(feature);
    }

    private void CreateKnights()
    {
        FlatStatModifierFeature feature = CreateFlatStatFeature("KnightsRarePrefix");
        feature.type = StatTypes.Armor;
        knights.Add(feature);
    }

    private void CreateBrawlers()
    {
        FlatStatModifierFeature feature = CreateFlatStatFeature("BrawlersRarePrefix");
        feature.type = StatTypes.MaxHP;
        brawlers.Add(feature);
    }

    private void CreateWizards()
    {
        FlatStatModifierFeature feature = CreateFlatStatFeature("WizardsRarePrefix");
        feature.type = StatTypes.MaxMana;
        wizards.Add(feature);
    }

    private void CreateFighters()
    {
        FlatStatModifierFeature feature = CreateFlatStatFeature("FightersRarePrefix");
        feature.type = StatTypes.PHYATK;
        fighters.Add(feature);
    }

    private void CreateBrutalizers()
    {
        PercentStatModifierFeature feature = CreatePercentStatFeature("BrutalizersRarePrefix");
        feature.type = StatTypes.PhysDmgBonus;
        brutalizers.Add(feature);
    }

    private void CreateEvokers()
    {
        PercentStatModifierFeature feature = CreatePercentStatFeature("EvokersRarePrefix");
        feature.type = StatTypes.MagDmgBonus;
        evokers.Add(feature);
    }
}
