using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SaveSlot", menuName = "ScriptableObjects/SaveSlot", order = 1)]

public class SaveSlot : ScriptableObject
{
    public bool containsData;
    public int slotNumber;
    public CustomCharacter characterData;
    
    //insert other items to save here

}
