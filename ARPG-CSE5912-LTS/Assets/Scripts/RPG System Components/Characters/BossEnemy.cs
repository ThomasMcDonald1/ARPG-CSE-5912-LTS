using ARPG.Core;
using LootLabels;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemy : EnemyController
{
    public LootSource lootSource = LootSource.Boss;
    public Type type = Type.Epic;
}
