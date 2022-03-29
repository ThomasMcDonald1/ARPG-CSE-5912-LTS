using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Equipment", menuName = "Inventory/Equipment/Weapon")]

public class WeaponEquipment : Equipment
{
    public enum weaponType
    {
        twohandsword,
        righthandsword,
        lefthandsword,
        bow,
        staff,
        dagger,
    };

    public weaponType typeOfWeapon;

    public int attackRange;
    public int minDamage;
    public int maxDamage;
    public int attackSpeedMod;
    public int critChanceBonus;


    public override void Use()
    {
        base.Use();
        playerStat[StatTypes.PHYATK] += maxDamage;
        //switch (typeOfWeapon)
        //{
        //    case weaponType.twohandsword:
        //        animcontroller.ChangeToTwoHandedSword();
        //        //playerStat = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Stats>();
        //        //UseHealingPotion();
        //        break;
        //    case weaponType.righthandsword:
        //        GameObject nu = (GameObject)Instantiate(prefab);
        //        nu.transform.parent = rightHand.transform;
        //        animcontroller.ChangeToOnlySwordRight();
        //        //playerStat = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Stats>();
        //        //UseEnergyPotion();
        //        break;
        //    default:
        //        Debug.Log("Don't know what this weapon does");
        //        break;
        //}
    }
}
