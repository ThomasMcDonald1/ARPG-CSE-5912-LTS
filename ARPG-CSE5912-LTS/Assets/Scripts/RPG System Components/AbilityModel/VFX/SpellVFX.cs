using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellVFX : MonoBehaviour
{
    public List<GameObject> spellsVFXList;

    private void Awake()
    {
        spellsVFXList = new List<GameObject>();
    }

    private void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            spellsVFXList.Add(transform.GetChild(i).gameObject);
        }
    }
}
