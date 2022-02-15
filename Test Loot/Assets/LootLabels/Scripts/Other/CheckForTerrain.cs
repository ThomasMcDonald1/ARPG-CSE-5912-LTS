using UnityEngine;
using UnityEngine.EventSystems;

namespace LootLabels {
    /// <summary>
    /// Checks if we are targetting terrain so the state can be changed in the navmanager
    /// </summary>
    public class CheckForTerrain : MonoBehaviour, IPointerDownHandler {
        
        public void OnPointerDown(PointerEventData eventData) {
            NavManager.singleton.SetTerrainState();
            //Debug.Log("terrain click");
        }
    }
}