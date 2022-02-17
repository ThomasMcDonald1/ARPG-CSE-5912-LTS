using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Connections : MonoBehaviour
{
    [SerializeField] string nodeID1;
    [SerializeField] string nodeID2;

    public void UpdateConnection(PassiveNode[] passiveTree)
    {
        if (CheckBothNodesUnlocked(passiveTree))
        {
            GetComponent<Image>().color = Color.yellow;
        }
    }
    public bool CheckBothNodesUnlocked(PassiveNode[] passiveTree)
    {
        PassiveNode node1 = Array.Find(passiveTree, node => node.Name == nodeID1);
        PassiveNode node2 = Array.Find(passiveTree, node => node.Name == nodeID2);
        if (node1.Unlocked && node2.Unlocked) return true;
        return false;
    }
}
