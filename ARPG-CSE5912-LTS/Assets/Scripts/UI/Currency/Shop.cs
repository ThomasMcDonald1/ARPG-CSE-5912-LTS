using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Shop")]
public class Shop : ScriptableObject
{
    public Ite[] itemList;
    public saleType shopSaleType;
    public enum saleType{
        Armor,
        Weapon,
        Utility,

    }
}
