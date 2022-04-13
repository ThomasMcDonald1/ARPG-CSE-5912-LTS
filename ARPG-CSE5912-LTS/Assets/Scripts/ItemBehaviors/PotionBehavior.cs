using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionBehavior : MonoBehaviour
{
    public static PotionBehavior instance;
    public bool isDefenseActive;
    void Start()
    {
        PotionBehavior.instance = this;
    }
}
