using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
public class Waypoint : MonoBehaviour
{
    private GameObject player;

    private void Awake()
    {
        player = GameObject.FindWithTag("Player");
    }

    public void EnableWaypointUI()
    {

    }
}
