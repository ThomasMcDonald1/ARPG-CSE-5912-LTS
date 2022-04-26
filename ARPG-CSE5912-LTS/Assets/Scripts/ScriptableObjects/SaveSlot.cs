using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SaveSlot", menuName = "ScriptableObjects/SaveSlot", order = 1)]

public class SaveSlot : ScriptableObject
{
    private void OnEnable()
    {
        hideFlags = HideFlags.DontUnloadUnusedAsset;
    }

    public bool containsData;
    public int slotNumber;
    public CustomCharacter characterData;

    //inventory data
    public List<string> weaponItems; 
    public List<string> armorItems;
    public List<string> utilItems;
    public Hashtable amount;

    //equipment data
    public string[] currentEquipment = new string[6];

    //insert other items to save here


    public void ClearData()
    {
        containsData = false;
        weaponItems.Clear();
        armorItems.Clear();
        utilItems.Clear();
        amount.Clear();
        Array.Clear(currentEquipment, 0, currentEquipment.Length);
    }
}


