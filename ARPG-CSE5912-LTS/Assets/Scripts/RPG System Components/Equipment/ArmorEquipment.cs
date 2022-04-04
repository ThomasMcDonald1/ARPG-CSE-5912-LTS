using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Equipment", menuName = "Inventory/Equipment/Armor")]
public class ArmorEquipment : Equipment
{
    //TODO: if Heavy armor, add Armor.
    //TODO: if Medium armor, add half Armor, half Evasion
    //TODO: if Light armor, add Evasion
    public override void Use()
    {
        base.Use();
        GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Stats>()[StatTypes.PHYDEF] += defense;
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
