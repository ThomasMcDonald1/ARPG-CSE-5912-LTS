using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseAbilityCost : MonoBehaviour
{
    public int cost;
    public Ability ability;

    public void Awake()
    {
        ability = GetComponent<Ability>();
    }
}
