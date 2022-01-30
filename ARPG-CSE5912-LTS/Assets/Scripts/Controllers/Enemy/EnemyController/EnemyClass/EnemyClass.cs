using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyClass: MonoBehaviour
{
    public abstract float currentEnemyHealth { get; set; }
    public abstract float maxEnemyHealth { get; set; }

    public abstract float range { get; set; }
    public abstract float bodyRange { get; set; }
    public abstract float sightRange { get; set; }
    public abstract float attackRange { get; set; }

    public abstract void seePlayer();

    public abstract void runToPlayer();

    public abstract void Attack();

    public abstract void CancelAttack(); //far from the born position

}
