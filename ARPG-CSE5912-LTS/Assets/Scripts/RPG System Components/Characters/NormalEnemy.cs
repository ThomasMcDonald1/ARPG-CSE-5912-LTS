using ARPG.Core;
using LootLabels;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalEnemy : EnemyController
{
    public LootSource lootSource = LootSource.Normal;
    public Type type = Type.Normal;
}
