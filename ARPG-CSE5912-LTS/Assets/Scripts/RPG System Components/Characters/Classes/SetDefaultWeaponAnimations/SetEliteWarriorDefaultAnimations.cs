using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetEliteWarriorDefaultAnimations : MonoBehaviour
{
    void Start()
    {
        GetComponent<SetAnimationType>().ChangeToTwoHandedSword();
    }
}
