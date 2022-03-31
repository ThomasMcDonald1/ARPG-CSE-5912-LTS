using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.AI;

public class SceneWaypointManager : MonoBehaviour
{
    public static SceneWaypointManager instance;
    public Player player;
    public Button[] buttons;
    public Dictionary<Button, string> WaypointToSceneName;
    public SceneWaypointLocations sceneWaypointLocations;
    public GameObject worldMap;
    void Awake()
    {
        DontDestroyOnLoad(this);
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        WaypointToSceneName = new Dictionary<Button, string>();
        WaypointToSceneName.Add(buttons[0], "SceneTeleport1");
        WaypointToSceneName.Add(buttons[1], "SceneTeleport2");
        foreach (var btn in buttons)
        {
            btn.onClick.AddListener(delegate { SwitchScene(btn); });
        }
    }
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    void SwitchScene(Button btn)
    {
        string sceneToLoad = WaypointToSceneName[btn];
        Debug.Log("Loading scene " + sceneToLoad);
        worldMap.SetActive(false);
        LoadingStateController.Instance.LoadScene(sceneToLoad);
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("OnSceneLoaded: " + scene.name);
        Debug.Log(sceneWaypointLocations.waypointLocations[scene.name]);
        player.GetComponent<NavMeshAgent>().Warp(sceneWaypointLocations.waypointLocations[scene.name]);
        Debug.Log("player position "  + player.transform.position);
    }
}
