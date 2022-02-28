using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "SceneWaypoint", menuName = "ScriptableObjects/SceneWaypoint")]
public class SceneWaypoint : ScriptableObject
{
    [SerializeField]
    Button[] buttons;
    public Dictionary<Button, string> WaypointToSceneNames;
    void Awake()
    {
        WaypointToSceneNames.Add(buttons[0], "SceneTeleport1");
        WaypointToSceneNames.Add(buttons[1], "SceneTeleport2");
        foreach (var btn in buttons)
        {
            btn.onClick.AddListener(SwitchScene);
        }
    }
    void SwitchScene()
    {
        Debug.Log("Hello");
    }
}
