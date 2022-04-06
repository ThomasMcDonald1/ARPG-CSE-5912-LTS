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
    MAGPWR, //NOT USED
    PHYDEF, //NOT USED
    MAGDEF, //NOT USED

    //Secondary & Derived stats (from attributes and gear)
    //11-24
    AttackRange, //The attack range gotten from the equipped weapon
    Armor, //The total armor from gear equipped & flat armor increase bonuses
    PercentArmorBonus, //The total armor from +%Armor bonuses from gear and other sources || Additional %, so, 1 + PercentArmorBonus * 0.01f
    AtkSpeed, //Attack speed percent modifier || Additional %, so (1 + stat * 0.01f)
    CritChance, //Chance that a physical attack will become a critical hit || Actual value, 10 CritChance stat = 10% chance to crit 
    CritDamage, //Additive percentage above the normal crit damage || Actual value, 20 CritDamage adds to base CritDamage, which becomes 220 * 0.01f = 2.2 times stronger damage
    FlatArmorPen, //add all +flat # armor penetration from outside sources to this
    FlatMagicPen, //same as above but for magic resist penetration
    PercentArmorPen, //same as above but for percent armor pen || Actual value, so 10 PercentArmorPen penetrates 10% of the target's armor
    PercentMagicPen, //same as above but for percent magic pen (Same as PercentArmorPen for elemental resistance)
    RunSpeed, //How fast the character can run || Additional %, so, baseRunSpeed * (1 + RunSpeed * 0.01f)
    HealthRegen, //How much health the character gets back per regeneration tick NOT USED
    ManaRegen, //How much energy the character gets back per regeneration tick NOT USED
    ExpGainMod, //Percentage modifier to the normal amount of exp earned from killing an enemy || Additional %, so, 1 + ExpGainMod * 0.01f
    
    //25-38
    PhysDmgBonus, //percentage modifier to the normal amount of physical damage done by an attack or ability || Additional %, so, 1 + PhysDmgBonus * 0.01f
    MagDmgBonus, //percent modifier to the normal amount of magical damage done by any spell with an elemental type other than physical || Same as above
    FireDmgBonus, //percent modifier to the normal amount of fire damage done by a fire spell || Same as above
    ColdDmgBonus, //percent modifier to the normal amount of cold damage done by a cold spell || Same as above
    LightningDmgBonus, //percent modifier to the normal amount of lightning damage done by a lightning spell || Same as above
    PoisonDmgBonus, //percent modifier to the normal amount of poison damage done by a poison spell || Same as above
    Lifesteal, //percent of weapon attack damage that is converted into healing to the character || Actual value, so, 10 Lifesteal = 10% of damage dealt is returned as health to attacker
    CastSpeed, //percentage that modifies base cast speed of a given spell || Default 0, if CastSpeed = 10 and a cast is normally 5 seconds, then new cast time is 5 - (5 * 10 * 0.01) = 4.5 seconds, so value should range from 0 to probably not more than 50 to 60 across all gear slots combined
    CooldownReduction, //percentage that modifies the cooldowns of all abilities ||  Default 0, if CDR = 10 and cooldown time is normally 5 seconds, then new cooldown time is 5 - (5 * 10 * 0.01) = 4.5 seconds, so value should range from 0 to probably not more than 50 to 60 across all gear slots combined
    CostReduction, //percentage that modifies the cost of using all abilities || NOT USED
    FireDmgOnHit, //flat fire damage added to main hand weapon attack
    ColdDmgOnHit, //flat cold damage added to main hand weapon attack
    LightningDmgOnHit, //flat lightning damage added to main hand weapon attack
    PoisonDmgOnHit, //flat poison damage added to main hand weapon attack

    //39-50
    BlockChance, //chance to mitigate damage by blocking with an equipped shield || Actual value, so 10 BlockChance = 10% chance to block an attack. Only Shields have BlockChance stat and Features, so as long as they don't exceed 10 or 20%, it should be fine
    Evasion, //chance to dodge an attack, taking 0 damage || same as BlockChance, but Evasion can be on everything, so all gear slots combined should not total more than 30 to 40% or something
    DamageReflect, //flat damage that gets returned to the attacker if character takes damage
    FireRes, //character's resistance to fire damage
    ColdRes, //character's resistance to cold damage
    LightningRes, //character's resistance to lightning damage
    PoisonRes, //character's resistance to poison damage
    PercentAllResistBonus, //a sum of all +% to all magic resist types || Additive %, so,  (1 + stat * 0.01f) = enemy's new defense
    PercentFireResistBonus, //a sum of all +% to specifically fire resist || same as above
    PercentColdResistBonus, //a sum of all +% to specifically cold resist || same as above
    PercentLightningResistBonus, //a sum of all +% to specifically lightning resist || same as above
    PercentPoisonResistBonus, //a sum of all +% to specifically poison resist || same as above

    //51-53
    ExpGain,
    MonsterType,
    SavedExp,

    //NOT USED
    //54-56
    STR,
    DEX,
    INT,

    //ADD STATUS EFFECT RESIST CHANCES HERE
    //57
    Count
}
