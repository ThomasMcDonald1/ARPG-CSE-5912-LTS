using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
public class Waypoint : MonoBehaviour
{
    public Player player;
    public GameObject worldMap;
	public Button yourButton;
    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            worldMap.SetActive(true);
            // transform.GetChild(0).gameObject.SetActive(true);
            // yourButton.gameObject.SetActive(true);
            SceneWaypointLocations.Instance.waypointLocations.Add(SceneManager.GetActiveScene().name, transform.position);
            Debug.Log(SceneWaypointLocations.Instance.waypointLocations.Count);
        }
    }
    void OnTriggerExit(Collider col)
    {
        worldMap.SetActive(false);
    }
}
