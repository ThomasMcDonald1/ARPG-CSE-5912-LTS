using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DunGen.Adapters;
using DunGen;


[CreateAssetMenu(fileName = "SaveDungeon", menuName = "ScriptableObjects/SaveDungeon", order = 1)]

public class SaveDungeon : ScriptableObject
{
    private void OnEnable()
    {
        hideFlags = HideFlags.DontUnloadUnusedAsset;
    }

    public bool generated = false;
    public int seed = -1;
    public Vector3 waypointLocation, startLocation;

    public void GenerateSeed()
    {
        if (!generated)
        {
            seed = new RandomStream().Next();

            Debug.Log("Dungeon seed: " + seed);
            generated = true;
        }
    }

    public void SetWaypointLocation(Vector3 loc)
    {
        waypointLocation = loc;
    }

    public void SetDungeonStartLocation(Vector3 loc)
    {
        startLocation = loc;
    }

}
