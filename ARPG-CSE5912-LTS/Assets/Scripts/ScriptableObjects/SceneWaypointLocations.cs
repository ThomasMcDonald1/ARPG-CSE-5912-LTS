using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "SceneWaypointLocations", menuName = "ScriptableObjects/SceneWaypoint")]
public class SceneWaypointLocations : ScriptableObject, ISerializationCallbackReceiver
{
    public Dictionary<string, Vector3> waypointLocations;
    public void OnAfterDeserialize()
    {
        waypointLocations = new Dictionary<string, Vector3>();
        waypointLocations.Add("SceneTeleport1", new Vector3(196.45f, 6f, 201.9f));
        waypointLocations.Add("SceneTeleport2", new Vector3(144.6679f, 2.066114f, 167.1145f));
    }
    public void OnBeforeSerialize() { }
}
