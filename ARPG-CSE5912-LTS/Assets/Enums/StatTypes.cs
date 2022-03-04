using UnityEngine;
using System.Collections;

public enum StatTypes
{
    //Progression stats 
    //0-2
    LVL, //character's level
    EXP, //character's current exp total
    SkillPoints, //points received upon leveling for the talent tree system

    //3-10
    //Main stats (attributes and resistances) 
    HP, //character's current hit points
    MaxHP, //character's maximum hit points
    Mana, //character's current mana points
    MaxMana, //character's maximum mana points
    PHYATK,
    MAGPWR,
    PHYDEF,
    MAGDEF,

    //11-13
    STR, //character's strength, NOT USED
    DEX, //character's dexterity, NOT USED
    INT, //character's intelligence, NOT USED

    //Secondary & Derived stats (from attributes and gear)
    //14-45
    AttackRange, //The attack range gotten from the equipped weapon
    Armor, //The total armor from gear equipped & flat armor increase bonuses
    PercentArmorBonus, //The total armor from +%Armor bonuses from gear and other sources
    AtkSpeed, //Attack speed of equipped weapon plus bonuses from passives, etc
    CritChance, //Chance that a physical attack will become a critical hit
    CritDamage, //Additive percentage above the normal crit damage
    FlatArmorPen, //add all +flat # armor penetration from outside sources to this
    FlatMagicPen, //same as above but for magic resist penetration
    PercentArmorPen, //same as above but for percent armor pen
    PercentMagicPen, //same as above but for percent magic pen
    RunSpeed, //How fast the character can run
    HealthRegen, //How much health the character gets back per regeneration tick NOT USED
    ManaRegen, //How much energy the character gets back per regeneration tick NOT USED
    ExpGainMod, //Percentage modifier to the normal amount of exp earned from killing an enemy
    PhysDmgBonus, //percentage modifier to the normal amount of physical damage done by an attack or ability
    MagDmgBonus, //percent modifier to the normal amount of magical damage done by any spell with an elemental type other than physical
    FireDmgBonus, //percent modifier to the normal amount of fire damage done by a fire spell
    ColdDmgBonus, //percent modifier to the normal amount of cold damage done by a cold spell
    LightningDmgBonus, //percent modifier to the normal amount of lightning damage done by a lightning spell
    PoisonDmgBonus, //percent modifier to the normal amount of poison damage done by a poison spell
    Lifesteal, //percent of weapon attack damage that is converted into healing to the character
    CastSpeed, //percentage that modifies base cast speed of a given spell
    CooldownReduction, //percentage that modifies the cooldowns of all abilities
    CostReduction, //percentage that modifies the cost of using all abilities
    FireDmgOnHitMain, //flat fire damage added to main hand weapon attack
    ColdDmgOnHitMain, //flat cold damage added to main hand weapon attack
    LightningDmgOnHitMain, //flat lightning damage added to main hand weapon attack
    PoisonDmgOnHitMain, //flat poison damage added to main hand weapon attack
    FireDmgOnHitOff, //flat fire damage added to offhand weapon attack
    ColdDmgOnHitOff, //flat cold damage added to offhand weapon attack
    LightningDmgOnHitOff, //flat lightning damage added to offhand weapon attack
    PoisonDmgOnHitOff, //flat poison damage added to main offhand weapon attack

    //46-59
    BlockChance, //chance to mitigate damage by blocking with an equipped shield NOT USED?
    BlockAmount, //how much damage will be mitigated when a shield block occurs NOT USED?
    DodgeChance, //chance to dodge an attack, taking 0 damage
    DeflectChance, //chance to parry with weapon, reducing damage by a set amount NOT USED
    DamageReflect, //percentage of the damage the character takes that gets returned to the attacker
    FireRes, //character's resistance to fire damage
    ColdRes, //character's resistance to cold damage
    LightningRes, //character's resistance to lightning damage
    PoisonRes, //character's resistance to poison damage
    PercentAllResistBonus, //a sum of all +% to all magic resist types
    PercentFireResistBonus, //a sum of all +% to specifically fire resist
    PercentColdResistBonus, //a sum of all +% to specifically cold resist
    PercentLightningResistBonus, //a sum of all +% to specifically lightning resist
    PercentPoisonResistBonus, //a sum of all +% to specifically poison resist

    //60-62
    ExpGain,
    MonsterType,
    SavedExp,

    //ADD STATUS EFFECT RESIST CHANCES HERE
    //62
    Count
}
