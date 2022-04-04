using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeatureTablesGenerator : MonoBehaviour
{
    List<LootLabels.GearTypes> weaponsReqList;
    List<LootLabels.GearTypes> armorsReqList;
    List<LootLabels.GearTypes> armorsAndShieldReqList;
    List<LootLabels.GearTypes> allReqList;
    PrefixTables prefixTables;
    RarePrefixFeaturesLists rarePrefixFeaturesLists;
    EpicPrefixFeaturesLists epicPrefixFeaturesLists;
    LegendaryPrefixFeaturesLists legendaryPrefixFeaturesLists;

    public void Awake()
    {
        prefixTables = GetComponent<PrefixTables>();
        rarePrefixFeaturesLists = GetComponent<RarePrefixFeaturesLists>();
        epicPrefixFeaturesLists = GetComponent<EpicPrefixFeaturesLists>();
        legendaryPrefixFeaturesLists = GetComponent<LegendaryPrefixFeaturesLists>();
    }

    private void Start()
    {
        weaponsReqList = new List<LootLabels.GearTypes>() { LootLabels.GearTypes.Dagger, LootLabels.GearTypes.TwoHandedSword, LootLabels.GearTypes.Sword };
        armorsReqList = new List<LootLabels.GearTypes>() { LootLabels.GearTypes.Helm, LootLabels.GearTypes.Boots, LootLabels.GearTypes.Armor, LootLabels.GearTypes.Legs };
        armorsAndShieldReqList = new List<LootLabels.GearTypes>() { LootLabels.GearTypes.Helm, LootLabels.GearTypes.Boots, LootLabels.GearTypes.Armor, LootLabels.GearTypes.Legs, LootLabels.GearTypes.Shield };
        allReqList = new List<LootLabels.GearTypes>() { LootLabels.GearTypes.Dagger, LootLabels.GearTypes.TwoHandedSword, LootLabels.GearTypes.Sword, LootLabels.GearTypes.Helm, LootLabels.GearTypes.Boots, LootLabels.GearTypes.Armor, LootLabels.GearTypes.Legs, LootLabels.GearTypes.Shield, LootLabels.GearTypes.Jewelry };
        rarePrefixFeaturesLists.CreateRarePrefixFeaturesLists();
        CreateRarePrefixTable();
        CreateRareSuffixTable();
        CreateEpicPrefixTable();
        CreateEpicSuffixTable();
        CreateLegendaryPrefixTable();
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
        prefixTables.legendarySuffixTable.Add(vextals);
        PrefixSuffix fezzeraks = new PrefixSuffix("Fezzerak's ", legendaryPrefixFeaturesLists.fezzeraks, allReqList);
        prefixTables.legendarySuffixTable.Add(fezzeraks);
        PrefixSuffix dalneaus = new PrefixSuffix("Dalneau's ", legendaryPrefixFeaturesLists.dalneaus, armorsAndShieldReqList);
        prefixTables.legendarySuffixTable.Add(dalneaus);
        PrefixSuffix zaltens = new PrefixSuffix("Zaltens's ", legendaryPrefixFeaturesLists.zaltens, allReqList);
        prefixTables.legendarySuffixTable.Add(zaltens);
        PrefixSuffix aldrichs = new PrefixSuffix("Aldrich's ", legendaryPrefixFeaturesLists.aldrichs, armorsReqList);
        prefixTables.legendarySuffixTable.Add(aldrichs);
        PrefixSuffix vleks = new PrefixSuffix("Vlek's ", legendaryPrefixFeaturesLists.vleks, allReqList);
        prefixTables.legendarySuffixTable.Add(vleks);
        PrefixSuffix ivorens = new PrefixSuffix("Ivoren's ", legendaryPrefixFeaturesLists.ivorens, allReqList);
        prefixTables.legendarySuffixTable.Add(ivorens);
        PrefixSuffix vlimliks = new PrefixSuffix("Vlimlik's ", legendaryPrefixFeaturesLists.vlimliks, CreateSingleGearTypeList(LootLabels.GearTypes.Shield));
        prefixTables.legendarySuffixTable.Add(vlimliks);
        PrefixSuffix barkors = new PrefixSuffix("Barkor's ", legendaryPrefixFeaturesLists.barkors, allReqList);
        prefixTables.legendarySuffixTable.Add(barkors);
        PrefixSuffix fordrands = new PrefixSuffix("Fordrand's ", legendaryPrefixFeaturesLists.fordrands, CreateSingleGearTypeList(LootLabels.GearTypes.Shield));
        prefixTables.legendarySuffixTable.Add(fordrands);
    }

    public void CreateRareSuffixTable()
    {

    }

    public void CreateEpicSuffixTable()
    {

    }

    public void CreateLegendarySuffixTable()
    {

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
