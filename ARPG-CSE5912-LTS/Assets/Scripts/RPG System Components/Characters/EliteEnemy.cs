using ARPG.Core;
using LootLabels;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EliteEnemy : EnemyController
{
    public LootSource lootSource = LootSource.Elite;
    public Type type = Type.Rare;
}
