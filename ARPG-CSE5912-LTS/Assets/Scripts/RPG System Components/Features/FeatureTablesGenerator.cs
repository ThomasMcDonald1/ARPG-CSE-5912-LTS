using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeatureTablesGenerator : MonoBehaviour
{
    List<LootLabels.GearTypes> weaponsReqList;
    List<LootLabels.GearTypes> armorsReqList;
    List<LootLabels.GearTypes> armorsAndShieldReqList;
    List<LootLabels.GearTypes> allReqList;
    [HideInInspector] public PrefixTables prefixTables;
    [HideInInspector] public SuffixTables suffixTables;
    RarePrefixFeaturesLists rarePrefixFeaturesLists;
    EpicPrefixFeaturesLists epicPrefixFeaturesLists;
    LegendaryPrefixFeaturesLists legendaryPrefixFeaturesLists;
    RareAndEpicSuffixFeaturesLists rareAndEpicSuffixFeaturesLists;
    LegendarySuffixFeaturesLists legendarySuffixFeaturesLists;

    public void Awake()
    {
        prefixTables = GetComponent<PrefixTables>();
        suffixTables = GetComponent<SuffixTables>();
        rarePrefixFeaturesLists = GetComponent<RarePrefixFeaturesLists>();
        epicPrefixFeaturesLists = GetComponent<EpicPrefixFeaturesLists>();
        legendaryPrefixFeaturesLists = GetComponent<LegendaryPrefixFeaturesLists>();
        rareAndEpicSuffixFeaturesLists = GetComponent<RareAndEpicSuffixFeaturesLists>();
        legendarySuffixFeaturesLists = GetComponent<LegendarySuffixFeaturesLists>();
    }

    private void Start()
    {
        weaponsReqList = new List<LootLabels.GearTypes>() { LootLabels.GearTypes.Dagger, LootLabels.GearTypes.TwoHandedSword, LootLabels.GearTypes.Sword };
        armorsReqList = new List<LootLabels.GearTypes>() { LootLabels.GearTypes.Helm, LootLabels.GearTypes.Boots, LootLabels.GearTypes.Armor};
        armorsAndShieldReqList = new List<LootLabels.GearTypes>() { LootLabels.GearTypes.Helm, LootLabels.GearTypes.Boots, LootLabels.GearTypes.Armor, LootLabels.GearTypes.Shield };
        allReqList = new List<LootLabels.GearTypes>() { LootLabels.GearTypes.Dagger, LootLabels.GearTypes.TwoHandedSword, LootLabels.GearTypes.Sword, LootLabels.GearTypes.Helm, LootLabels.GearTypes.Boots, LootLabels.GearTypes.Armor, LootLabels.GearTypes.Shield, LootLabels.GearTypes.Jewelry };
        rarePrefixFeaturesLists.CreateRarePrefixFeaturesLists();
        epicPrefixFeaturesLists.CreateEpicPrefixFeaturesLists();
        legendaryPrefixFeaturesLists.CreateLegendaryPrefixFeaturesLists();
        rareAndEpicSuffixFeaturesLists.CreateRareOrEpicSuffixFeaturesLists();
        legendarySuffixFeaturesLists.CreateLegendarySuffixFeaturesLists();
        CreateRarePrefixTable();
        CreateEpicPrefixTable();
        CreateLegendaryPrefixTable();
        CreateRareAndEpicSuffixTable();
        CreateLegendarySuffixTable();
    }

    public void CreateRarePrefixTable()
    {
        PrefixSuffix punishers = new PrefixSuffix("Punisher's ", rarePrefixFeaturesLists.punishers, allReqList);
        prefixTables.rarePrefixTable.Add(punishers);
        PrefixSuffix warlocks = new PrefixSuffix("Warlock's ", rarePrefixFeaturesLists.warlocks, allReqList);
        prefixTables.rarePrefixTable.Add(warlocks);
        PrefixSuffix lorekeepers = new PrefixSuffix("Lorekeeper's ", rarePrefixFeaturesLists.lorekeepers, CreateSingleGearTypeList(LootLabels.GearTypes.Jewelry));
        prefixTables.rarePrefixTable.Add(lorekeepers);
        PrefixSuffix spellslingers = new PrefixSuffix("Spellslinger's ", rarePrefixFeaturesLists.spellslingers, allReqList);
        prefixTables.rarePrefixTable.Add(spellslingers);
        PrefixSuffix sages = new PrefixSuffix("Sage's ", rarePrefixFeaturesLists.sages, allReqList);
        prefixTables.rarePrefixTable.Add(sages);
        PrefixSuffix fieryEnchanters = new PrefixSuffix("Fiery Enchanter's ", rarePrefixFeaturesLists.fieryEnchanters, weaponsReqList);
        prefixTables.rarePrefixTable.Add(fieryEnchanters);
        PrefixSuffix icyEnchanters = new PrefixSuffix("IcyEnchanter's ", rarePrefixFeaturesLists.icyEnchanters, weaponsReqList);
        prefixTables.rarePrefixTable.Add(icyEnchanters);
        PrefixSuffix thunderingEnchanters = new PrefixSuffix("Thundering Enchanter's ", rarePrefixFeaturesLists.thunderingEnchanters, weaponsReqList);
        prefixTables.rarePrefixTable.Add(thunderingEnchanters);
        PrefixSuffix corrosiveEnchanters = new PrefixSuffix("Corrosive Enchanter's ", rarePrefixFeaturesLists.corrosiveEnchanters, weaponsReqList);
        prefixTables.rarePrefixTable.Add(corrosiveEnchanters);
        PrefixSuffix knights = new PrefixSuffix("Knight's ", rarePrefixFeaturesLists.knights, armorsReqList);
        prefixTables.rarePrefixTable.Add(knights);
        PrefixSuffix brawlers = new PrefixSuffix("Brawler's ", rarePrefixFeaturesLists.brawlers, allReqList);
        prefixTables.rarePrefixTable.Add(brawlers);
        PrefixSuffix wizards = new PrefixSuffix("Wizard's ", rarePrefixFeaturesLists.wizards, allReqList);
        prefixTables.rarePrefixTable.Add(wizards);
        PrefixSuffix fighters = new PrefixSuffix("Fighter's ", rarePrefixFeaturesLists.fighters, allReqList);
        prefixTables.rarePrefixTable.Add(fighters);
        PrefixSuffix brutalizers = new PrefixSuffix("Brutalizer's ", rarePrefixFeaturesLists.brutalizers, allReqList);
        prefixTables.rarePrefixTable.Add(brutalizers);
        PrefixSuffix evokers = new PrefixSuffix("Evoker's ", rarePrefixFeaturesLists.evokers, allReqList);
        prefixTables.rarePrefixTable.Add(evokers);
    }

    public void CreateEpicPrefixTable()
    {
        PrefixSuffix travelers = new PrefixSuffix("Traveler's ", epicPrefixFeaturesLists.travelers, CreateSingleGearTypeList(LootLabels.GearTypes.Jewelry));
        prefixTables.epicPrefixTable.Add(travelers);
        PrefixSuffix vampiric = new PrefixSuffix("Vampiric ", epicPrefixFeaturesLists.vampiric, allReqList);
        prefixTables.epicPrefixTable.Add(vampiric);
        PrefixSuffix berserkers = new PrefixSuffix("Berserker's ", epicPrefixFeaturesLists.berserkers, allReqList);
        prefixTables.epicPrefixTable.Add(berserkers);
        PrefixSuffix exploiters = new PrefixSuffix("Exploiter's ", epicPrefixFeaturesLists.exploiters, allReqList);
        prefixTables.epicPrefixTable.Add(exploiters);
        PrefixSuffix bloodletters = new PrefixSuffix("Bloodletter's ", epicPrefixFeaturesLists.bloodletters, allReqList);
        prefixTables.epicPrefixTable.Add(bloodletters);
        PrefixSuffix impalers = new PrefixSuffix("Impaler's ", epicPrefixFeaturesLists.impalers, allReqList);
        prefixTables.epicPrefixTable.Add(impalers);
        PrefixSuffix psionics = new PrefixSuffix("Psionic's ", epicPrefixFeaturesLists.psionics, allReqList);
        prefixTables.epicPrefixTable.Add(psionics);
        PrefixSuffix fierySorcerers = new PrefixSuffix("Fiery Sorcerer's ", epicPrefixFeaturesLists.fierySorcerers, weaponsReqList);
        prefixTables.epicPrefixTable.Add(fierySorcerers);
        PrefixSuffix icySorcerers = new PrefixSuffix("Icy Sorcerer's ", epicPrefixFeaturesLists.icySorcerers, weaponsReqList);
        prefixTables.epicPrefixTable.Add(icySorcerers);
        PrefixSuffix thunderingSorcerers = new PrefixSuffix("Thundering Sorcerer's ", epicPrefixFeaturesLists.thunderingSorcerers, weaponsReqList);
        prefixTables.epicPrefixTable.Add(thunderingSorcerers);
        PrefixSuffix corrosiveSorcerers = new PrefixSuffix("Corrosive Sorcerer's ", epicPrefixFeaturesLists.corrosiveSorcerers, weaponsReqList);
        prefixTables.epicPrefixTable.Add(corrosiveSorcerers);
    }

    public void CreateLegendaryPrefixTable()
    {
        PrefixSuffix mordreths = new PrefixSuffix("Mordreth's ", legendaryPrefixFeaturesLists.mordreths, weaponsReqList);
        prefixTables.legendaryPrefixTable.Add(mordreths);
        PrefixSuffix vextals = new PrefixSuffix("Vextal's ", legendaryPrefixFeaturesLists.vextals, allReqList);
        prefixTables.legendaryPrefixTable.Add(vextals);
        PrefixSuffix fezzeraks = new PrefixSuffix("Fezzerak's ", legendaryPrefixFeaturesLists.fezzeraks, allReqList);
        prefixTables.legendaryPrefixTable.Add(fezzeraks);
        PrefixSuffix dalneaus = new PrefixSuffix("Dalneau's ", legendaryPrefixFeaturesLists.dalneaus, armorsAndShieldReqList);
        prefixTables.legendaryPrefixTable.Add(dalneaus);
        PrefixSuffix zaltens = new PrefixSuffix("Zaltens's ", legendaryPrefixFeaturesLists.zaltens, allReqList);
        prefixTables.legendaryPrefixTable.Add(zaltens);
        PrefixSuffix aldrichs = new PrefixSuffix("Aldrich's ", legendaryPrefixFeaturesLists.aldrichs, armorsReqList);
        prefixTables.legendaryPrefixTable.Add(aldrichs);
        PrefixSuffix vleks = new PrefixSuffix("Vlek's ", legendaryPrefixFeaturesLists.vleks, allReqList);
        prefixTables.legendaryPrefixTable.Add(vleks);
        PrefixSuffix ivorens = new PrefixSuffix("Ivoren's ", legendaryPrefixFeaturesLists.ivorens, allReqList);
        prefixTables.legendaryPrefixTable.Add(ivorens);
        PrefixSuffix vlimliks = new PrefixSuffix("Vlimlik's ", legendaryPrefixFeaturesLists.vlimliks, CreateSingleGearTypeList(LootLabels.GearTypes.Shield));
        prefixTables.legendaryPrefixTable.Add(vlimliks);
        PrefixSuffix barkors = new PrefixSuffix("Barkor's ", legendaryPrefixFeaturesLists.barkors, allReqList);
        prefixTables.legendaryPrefixTable.Add(barkors);
        PrefixSuffix fordrands = new PrefixSuffix("Fordrand's ", legendaryPrefixFeaturesLists.fordrands, CreateSingleGearTypeList(LootLabels.GearTypes.Shield));
        prefixTables.legendaryPrefixTable.Add(fordrands);
    }

    public void CreateRareAndEpicSuffixTable()
    {
        PrefixSuffix thorns = new PrefixSuffix(" of Thorns", rareAndEpicSuffixFeaturesLists.thorns, armorsAndShieldReqList);
        suffixTables.rareAndEpicSuffixTable.Add(thorns);
        PrefixSuffix theBear = new PrefixSuffix(" of the Bear", rareAndEpicSuffixFeaturesLists.theBear, allReqList);
        suffixTables.rareAndEpicSuffixTable.Add(theBear);
        PrefixSuffix theOwl = new PrefixSuffix(" of the Owl", rareAndEpicSuffixFeaturesLists.theOwl, allReqList);
        suffixTables.rareAndEpicSuffixTable.Add(theOwl);
        PrefixSuffix theBull = new PrefixSuffix(" of the Bull", rareAndEpicSuffixFeaturesLists.theBull, allReqList);
        suffixTables.rareAndEpicSuffixTable.Add(theBull);
        PrefixSuffix theCrab = new PrefixSuffix(" of the Crab", rareAndEpicSuffixFeaturesLists.theCrab, armorsAndShieldReqList);
        suffixTables.rareAndEpicSuffixTable.Add(theCrab);
        PrefixSuffix theTurtle = new PrefixSuffix(" of the Turtle", rareAndEpicSuffixFeaturesLists.theTurtle, armorsAndShieldReqList);
        suffixTables.rareAndEpicSuffixTable.Add(theTurtle);
        PrefixSuffix frenziedStrikes = new PrefixSuffix(" of Frenzied Strikes", rareAndEpicSuffixFeaturesLists.frenziedStrikes, allReqList);
        suffixTables.rareAndEpicSuffixTable.Add(frenziedStrikes);
        PrefixSuffix pinpointStrikes = new PrefixSuffix(" of Pinpoint Strikes", rareAndEpicSuffixFeaturesLists.pinpointStrikes, allReqList);
        suffixTables.rareAndEpicSuffixTable.Add(pinpointStrikes);
        PrefixSuffix brutality = new PrefixSuffix(" of Brutality", rareAndEpicSuffixFeaturesLists.brutality, allReqList);
        suffixTables.rareAndEpicSuffixTable.Add(brutality);
        PrefixSuffix piercingStrikes = new PrefixSuffix(" of Piercing Strikes", rareAndEpicSuffixFeaturesLists.piercingStrikes, allReqList);
        suffixTables.rareAndEpicSuffixTable.Add(piercingStrikes);
        PrefixSuffix elementalMastery = new PrefixSuffix(" of Elemental Mastery", rareAndEpicSuffixFeaturesLists.elementalMastery, allReqList);
        suffixTables.rareAndEpicSuffixTable.Add(elementalMastery);
        PrefixSuffix theHammer = new PrefixSuffix(" of the Hammer", rareAndEpicSuffixFeaturesLists.theHammer, allReqList);
        suffixTables.rareAndEpicSuffixTable.Add(theHammer);
        PrefixSuffix theSpirits = new PrefixSuffix(" of the Spirits", rareAndEpicSuffixFeaturesLists.theSpirits, allReqList);
        suffixTables.rareAndEpicSuffixTable.Add(theSpirits);
        PrefixSuffix theQuick = new PrefixSuffix(" of the Quick", rareAndEpicSuffixFeaturesLists.theQuick, CreateDoubleGearTypeList(LootLabels.GearTypes.Boots, LootLabels.GearTypes.Jewelry));
        suffixTables.rareAndEpicSuffixTable.Add(theQuick);
        PrefixSuffix observation = new PrefixSuffix(" of Observation", rareAndEpicSuffixFeaturesLists.observation, CreateSingleGearTypeList(LootLabels.GearTypes.Jewelry));
        suffixTables.rareAndEpicSuffixTable.Add(observation);
        PrefixSuffix crushing = new PrefixSuffix(" of Crushing", rareAndEpicSuffixFeaturesLists.crushing, allReqList);
        suffixTables.rareAndEpicSuffixTable.Add(crushing);
        PrefixSuffix blasting = new PrefixSuffix(" of Blasting", rareAndEpicSuffixFeaturesLists.blasting, allReqList);
        suffixTables.rareAndEpicSuffixTable.Add(blasting);
        PrefixSuffix incineration = new PrefixSuffix(" of Incineration", rareAndEpicSuffixFeaturesLists.incineration, allReqList);
        suffixTables.rareAndEpicSuffixTable.Add(incineration);
        PrefixSuffix chilling = new PrefixSuffix(" of Chilling", rareAndEpicSuffixFeaturesLists.chilling, allReqList);
        suffixTables.rareAndEpicSuffixTable.Add(chilling);
        PrefixSuffix thunder = new PrefixSuffix(" of Thunder", rareAndEpicSuffixFeaturesLists.thunder, allReqList);
        suffixTables.rareAndEpicSuffixTable.Add(thunder);
        PrefixSuffix theSnake = new PrefixSuffix(" of the Snake", rareAndEpicSuffixFeaturesLists.theSnake, allReqList);
        suffixTables.rareAndEpicSuffixTable.Add(theSnake);
        PrefixSuffix siphoning = new PrefixSuffix(" of Siphoning", rareAndEpicSuffixFeaturesLists.siphoning, allReqList);
        suffixTables.rareAndEpicSuffixTable.Add(siphoning);
        PrefixSuffix acumen = new PrefixSuffix(" of Acumen", rareAndEpicSuffixFeaturesLists.acumen, allReqList);
        suffixTables.rareAndEpicSuffixTable.Add(acumen);
        PrefixSuffix invigoration = new PrefixSuffix(" of Invigoration", rareAndEpicSuffixFeaturesLists.invigoration, allReqList);
        suffixTables.rareAndEpicSuffixTable.Add(invigoration);
        PrefixSuffix flame = new PrefixSuffix(" of Flame", rareAndEpicSuffixFeaturesLists.flame, allReqList);
        suffixTables.rareAndEpicSuffixTable.Add(flame);
        PrefixSuffix frost = new PrefixSuffix(" of Frost", rareAndEpicSuffixFeaturesLists.frost, allReqList);
        suffixTables.rareAndEpicSuffixTable.Add(frost);
        PrefixSuffix shocking = new PrefixSuffix(" of Shocking", rareAndEpicSuffixFeaturesLists.shocking, allReqList);
        suffixTables.rareAndEpicSuffixTable.Add(shocking);
        PrefixSuffix stinging = new PrefixSuffix(" of Stinging", rareAndEpicSuffixFeaturesLists.stinging, allReqList);
        suffixTables.rareAndEpicSuffixTable.Add(stinging);
        PrefixSuffix theTower = new PrefixSuffix(" of the Tower", rareAndEpicSuffixFeaturesLists.theTower, CreateSingleGearTypeList(LootLabels.GearTypes.Shield));
        suffixTables.rareAndEpicSuffixTable.Add(theTower);
        PrefixSuffix tumbling = new PrefixSuffix(" of Tumbling", rareAndEpicSuffixFeaturesLists.tumbling, CreateDoubleGearTypeList(LootLabels.GearTypes.Boots, LootLabels.GearTypes.Jewelry));
        suffixTables.rareAndEpicSuffixTable.Add(tumbling);
        PrefixSuffix fireResistance = new PrefixSuffix(" of Fire Resistance", rareAndEpicSuffixFeaturesLists.fireResistance, allReqList);
        suffixTables.rareAndEpicSuffixTable.Add(fireResistance);
        PrefixSuffix coldResistance = new PrefixSuffix(" of Cold Resistance", rareAndEpicSuffixFeaturesLists.coldResistance, allReqList);
        suffixTables.rareAndEpicSuffixTable.Add(coldResistance);
        PrefixSuffix lightningResistance = new PrefixSuffix(" of Lightning Resistance", rareAndEpicSuffixFeaturesLists.lightningResistance, allReqList);
        suffixTables.rareAndEpicSuffixTable.Add(lightningResistance);
        PrefixSuffix poisonResistance = new PrefixSuffix(" of Poison Resistance", rareAndEpicSuffixFeaturesLists.poisonResistance, allReqList);
        suffixTables.rareAndEpicSuffixTable.Add(poisonResistance);
        PrefixSuffix fireWarding = new PrefixSuffix(" of Fire Warding", rareAndEpicSuffixFeaturesLists.fireWarding, allReqList);
        suffixTables.rareAndEpicSuffixTable.Add(fireWarding);
        PrefixSuffix coldWarding = new PrefixSuffix(" of Cold Warding", rareAndEpicSuffixFeaturesLists.coldWarding, allReqList);
        suffixTables.rareAndEpicSuffixTable.Add(coldWarding);
        PrefixSuffix lightningWarding = new PrefixSuffix(" of Lightning Warding", rareAndEpicSuffixFeaturesLists.lightningWarding, allReqList);
        suffixTables.rareAndEpicSuffixTable.Add(lightningWarding);
        PrefixSuffix poisonWarding = new PrefixSuffix(" of Poison Warding", rareAndEpicSuffixFeaturesLists.poisonWarding, allReqList);
        suffixTables.rareAndEpicSuffixTable.Add(poisonWarding);
        PrefixSuffix alteredFlesh = new PrefixSuffix(" of Altered Flesh", rareAndEpicSuffixFeaturesLists.alteredFlesh, CreateSingleGearTypeList(LootLabels.GearTypes.Jewelry));
        suffixTables.rareAndEpicSuffixTable.Add(alteredFlesh);
    }

    public void CreateLegendarySuffixTable()
    {
        PrefixSuffix spiritStriking = new PrefixSuffix(" of Spirit Striking", legendarySuffixFeaturesLists.spiritStriking, weaponsReqList);
        suffixTables.legendarySuffixTable.Add(spiritStriking);
        PrefixSuffix theCheetah = new PrefixSuffix(" of the Cheetah", legendarySuffixFeaturesLists.theCheetah, CreateSingleGearTypeList(LootLabels.GearTypes.Boots));
        suffixTables.legendarySuffixTable.Add(theCheetah);
        PrefixSuffix spikes = new PrefixSuffix(" of Spikes", legendarySuffixFeaturesLists.spikes, CreateSingleGearTypeList(LootLabels.GearTypes.Shield));
        suffixTables.legendarySuffixTable.Add(spikes);
        PrefixSuffix carnage = new PrefixSuffix(" of Carnage", legendarySuffixFeaturesLists.carnage, weaponsReqList);
        suffixTables.legendarySuffixTable.Add(carnage);
        PrefixSuffix destruction = new PrefixSuffix(" of Destruction", legendarySuffixFeaturesLists.destruction, weaponsReqList);
        suffixTables.legendarySuffixTable.Add(destruction);
        PrefixSuffix clearMind = new PrefixSuffix(" of Clear Mind", legendarySuffixFeaturesLists.clearMind, allReqList);
        suffixTables.legendarySuffixTable.Add(clearMind);
        PrefixSuffix demonicVigor = new PrefixSuffix(" of Demonic Vigor", legendarySuffixFeaturesLists.demonicVigor, allReqList);
        suffixTables.legendarySuffixTable.Add(demonicVigor);
        PrefixSuffix celestialBlessings = new PrefixSuffix(" of Celestial Blessings", legendarySuffixFeaturesLists.celestialBlessings, allReqList);
        suffixTables.legendarySuffixTable.Add(celestialBlessings);
        PrefixSuffix theTiger = new PrefixSuffix(" of the Tiger", legendarySuffixFeaturesLists.theTiger, allReqList);
        suffixTables.legendarySuffixTable.Add(theTiger);
        PrefixSuffix theWall = new PrefixSuffix(" of the Wall", legendarySuffixFeaturesLists.theWall, CreateSingleGearTypeList(LootLabels.GearTypes.Shield));
        suffixTables.legendarySuffixTable.Add(theWall);
        PrefixSuffix theInferno = new PrefixSuffix(" of the Inferno", legendarySuffixFeaturesLists.theInferno, allReqList);
        suffixTables.legendarySuffixTable.Add(theInferno);
        PrefixSuffix theStorm = new PrefixSuffix(" of the Storm", legendarySuffixFeaturesLists.theStorm, allReqList);
        suffixTables.legendarySuffixTable.Add(theStorm);
        PrefixSuffix frostbite = new PrefixSuffix(" of Frostbite", legendarySuffixFeaturesLists.frostbite, allReqList);
        suffixTables.legendarySuffixTable.Add(frostbite);
        PrefixSuffix theViper = new PrefixSuffix(" of the Viper", legendarySuffixFeaturesLists.theViper, allReqList);
        suffixTables.legendarySuffixTable.Add(theViper);
        PrefixSuffix theColossus = new PrefixSuffix(" of the Colossus", legendarySuffixFeaturesLists.theColossus, allReqList);
        suffixTables.legendarySuffixTable.Add(theColossus);
    }

    private List<LootLabels.GearTypes> CreateSingleGearTypeList(LootLabels.GearTypes gearReq)
    {
        List<LootLabels.GearTypes> gearReqs = new List<LootLabels.GearTypes>
        {
            gearReq
        };
        return gearReqs;
    }

    private List<LootLabels.GearTypes> CreateDoubleGearTypeList(LootLabels.GearTypes gearReq, LootLabels.GearTypes gearReq2)
    {
        List<LootLabels.GearTypes> gearReqs = new List<LootLabels.GearTypes>
        {
            gearReq,
            gearReq2
        };
        return gearReqs;
    }
}
