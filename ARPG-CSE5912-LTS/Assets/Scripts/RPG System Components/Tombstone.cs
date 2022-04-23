using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tombstone : MonoBehaviour
{
    public static Tombstone Instance { get; private set; }
    Stats playerStats;
    private int heldExp = 0;
    public GameObject tombstoneModel;
    private int dungeonNumOfDeath = -1;
    private Vector3 playerDeathPos;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
        playerStats = GetComponentInParent<GameplayStateController>().GetComponentInChildren<Player>().GetComponent<Stats>();
        tombstoneModel.SetActive(false);
    }

    public void HoldTempExpLoss(int expLoss)
    {
        heldExp = expLoss;
    }

    public void GiveBackLostExp()
    {
        playerStats[StatTypes.EXP] += heldExp;
        heldExp = 0;
        dungeonNumOfDeath = -1;
        tombstoneModel.SetActive(false);
    }

    public void RememberDungeonNumOfPlayerDeath(int dungeonNum)
    {
        dungeonNumOfDeath = dungeonNum;
    }

    public int GetDungeonNumOfPlayerDeath()
    {
        Debug.Log("Dungeon death number: " + dungeonNumOfDeath);
        return dungeonNumOfDeath;
    }

    public void RememberPlayerDeathPosition(Vector3 pos)
    {
        playerDeathPos = pos;
    }

    public Vector3 GetPlayerDeathPosition()
    {
        return playerDeathPos;
    }

    public void PlaceTombstone()
    {
        Debug.Log("Placing Tombstone");
        tombstoneModel.SetActive(true);
        var pos = GetPlayerDeathPosition();
        transform.position = new Vector3(pos.x, 3.87f, pos.z);

    }
}
