using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetEnemyKnightDefaultAnimations : MonoBehaviour
{
    void Start()
    {
        GetComponent<SetAnimationType>().ChangeToUnarmed();
    }
}
