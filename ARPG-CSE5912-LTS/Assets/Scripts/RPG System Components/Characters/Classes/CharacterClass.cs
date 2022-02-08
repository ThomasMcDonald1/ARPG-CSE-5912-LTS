using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterClass : MonoBehaviour
{
    public abstract void SetBaseStats();
    public abstract void AddGrowthStats();
    public abstract void SetUsableEquipment();
    public abstract void SetLearnableAbilities();

}
