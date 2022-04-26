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
    public List<Ite> weaponItems; 
    public List<Ite> armorItems;
    public List<Ite> utilItems;

    //equipment data
    public List<Equipment> currentEquipment;

    //insert other items to save here


    public void ClearData()
    {
        containsData = false;
        weaponItems.Clear();
        armorItems.Clear();
        utilItems.Clear();
        currentEquipment.Clear();
    }
}


