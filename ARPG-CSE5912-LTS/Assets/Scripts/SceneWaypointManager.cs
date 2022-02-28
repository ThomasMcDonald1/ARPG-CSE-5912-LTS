using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneWaypointManager : MonoBehaviour
{
    public Button[] buttons;
    public Dictionary<Button, string> WaypointToSceneName;
    void Awake()
    {
        WaypointToSceneName.Add(buttons[0], "SceneTeleport1");
        WaypointToSceneName.Add(buttons[1], "SceneTeleport2");
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
