using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeatureTablesGenerator : MonoBehaviour
{
    List<LootLabels.GearTypes> weaponsReqList;
    List<LootLabels.GearTypes> armorsReqList;
    List<LootLabels.GearTypes> allReqList;
    PrefixTables prefixTables;
    RarePrefixFeaturesLists rarePrefixFeaturesLists;

    public void Awake()
    {
        prefixTables = GetComponent<PrefixTables>();
        rarePrefixFeaturesLists = GetComponent<RarePrefixFeaturesLists>();
    }

    private void Start()
    {
        weaponsReqList = new List<LootLabels.GearTypes>() { LootLabels.GearTypes.Dagger, LootLabels.GearTypes.TwoHandedSword, LootLabels.GearTypes.Sword };
        armorsReqList = new List<LootLabels.GearTypes>() { LootLabels.GearTypes.Helm, LootLabels.GearTypes.Boots, LootLabels.GearTypes.Chest, LootLabels.GearTypes.Legs };
        allReqList = new List<LootLabels.GearTypes>() { LootLabels.GearTypes.Dagger, LootLabels.GearTypes.TwoHandedSword, LootLabels.GearTypes.Sword, LootLabels.GearTypes.Helm, LootLabels.GearTypes.Boots, LootLabels.GearTypes.Chest, LootLabels.GearTypes.Legs, LootLabels.GearTypes.Shield, LootLabels.GearTypes.Jewelry };
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

    }

    public void CreateLegendaryPrefixTable()
    {

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
