using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyObjects : MonoBehaviour
{
    public GameObject[] gos;
    void Awake()
    {
        foreach (var x in gos)
        {
            DontDestroyOnLoad(x);
        }
    }
}
