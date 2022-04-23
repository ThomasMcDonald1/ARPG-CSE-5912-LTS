using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemDropHandler : MonoBehaviour, IDropHandler
{
    GraphicRaycaster charPanelRaycaster;
    private void Awake()
    {
        charPanelRaycaster = GetComponentInParent<GraphicRaycaster>();
    }

    public void OnDrop(PointerEventData eventData)
    {
        //List<RaycastResult> results = new List<RaycastResult>();
        //charPanelRaycaster.Raycast(eventData, results);
        //foreach (RaycastResult result in results)
        //{
        //    GameObject go = result.gameObject;
        //    EquipSlot slot = go.GetComponent<EquipSlot>();
        //    if (slot != null)
        //    {
        //        Debug.Log("Slot dropped onto: " + slot.gameObject.name);
        //    }
        //}
    }


}
