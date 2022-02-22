using UnityEngine;
using UnityEngine.AI;

namespace LootLabels {
    /// <summary>
    /// Assigns the requiered player components to the navmanager
    /// </summary>
    [RequireComponent(typeof(NavMeshAgent))]
    public class SetPlayer : MonoBehaviour {

        // Use this for initialization
        void Start() {
            NavManager.singleton.PlayerTransform = GetComponent<Transform>();

            if (GetComponent<NavMeshAgent>()) {
                NavManager.singleton.PlayerNavAgent = GetComponent<NavMeshAgent>();
            }
            else {
                Debug.Log("Assign a Nav Mesh Agent to your player");
            }
        }
    }
}