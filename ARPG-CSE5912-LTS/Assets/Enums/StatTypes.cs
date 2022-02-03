using UnityEngine;
using System.Collections;

public enum StatTypes
{
    //Progression stats
    LVL, //character's level
    EXP, //character's current exp total
    SKILLPOINTS, //The points used to spend on upgrades in the skill tree

    //Main stats (attributes and resistances)
    HEALTH, //character's current hit points
    MAXHEALTH, //character's maximum hit points
    ENERGY, //character's current mana points
    MAXENERGY, //character's maximum mana points
    STR, //character's strength
    DEX, //character's dexterity
    INT, //character's intelligence

    FIRERES, //character's resistance to fire damage
    COLDRES, //character's resistance to cold damage
    LIGHTNINGRES, //character's resistance to lightning damage
    POISONRES, //character's resistance to poison damage

    //Secondary & Derived stats (from attributes and gear)
    ARMOR, //The total armor from gear equipped
    ATKSPD, //Attack speed of equipped weapon plus bonuses from passives, etc
    CRITCHANCE, //Chance that a physical attack will become a critical hit
    CRITDAMAGE, //Additive percentage above the normal crit damage
    RUNSPEED, //How fast the character can run
    HEALTHREGEN, //How much health the character gets back per regeneration tick 
    ENERGYREGEN, //How much energy the character gets back per regeneration tick
    EXPGAINMOD, //Percentage modifier to the normal amount of exp earned from killing an enemy
    PHYSDMGBONUS, //percentage modifier to the normal amount of physical damage done by an attack or ability
    MAGDMGBONUS, //percent modifier to the normal amount of magical damage done by any spell with an elemental type other than physical
    FIREDMGBONUS, //percent modifier to the normal amount of fire damage done by a fire spell
    COLDDMGBONUS, //percent modifier to the normal amount of cold damage done by a cold spell
    LIGHTNINGDMGBONUS, //percent modifier to the normal amount of lightning damage done by a lightning spell
    POISONDMGBONUS, //percent modifier to the normal amount of poison damage done by a poison spell
    LIFESTEAL, //percent of weapon attack damage that is converted into healing to the character
    CASTSPEED, //percentage that modifies base cast speed of a given spell
    COOLDOWNREDUCTION, //percentage that modifies the cooldowns of all abilities
    COSTREDUCTION, //percentage that modifies the cost of using all abilities

    FIREDMGONHITMAIN, //flat fire damage added to main hand weapon attack
    COLDDMGONHITMAIN, //flat cold damage added to main hand weapon attack
    LIGHTNINGDMGONHITMAIN, //flat lightning damage added to main hand weapon attack
    POISONDMGONHITMAIN, //flat poison damage added to main hand weapon attack
    FIREDMGONHITOFF, //flat fire damage added to offhand weapon attack
    COLDDMGONHITOFF, //flat cold damage added to offhand weapon attack
    LIGHTNINGDMGONHITOFF, //flat lightning damage added to offhand weapon attack
    POISONDMGONHITOFF, //flat poison damage added to main offhand weapon attack

    BLOCKCHANCE, //chance to mitigate damage by blocking with an equipped shield
    BLOCKAMOUNT, //how much damage will be mitigated when a shield block occurs
    DODGECHANCE, //chance to dodge an attack, taking 0 damage
    DEFLECTCHANCE, //chance to parry with weapon, reducing damage by a set amount
    DAMAGEREFLECT, //percentage of the damage the character takes that gets returned to the attacker

    //ADD STATUS EFFECT RESIST CHANCES HERE

    Count
}
