using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ItemDragHandler : MonoBehaviour, IDragHandler, IEndDragHandler
{
    [SerializeField] GraphicRaycaster charPanelRaycaster;
    InventorySlot invSlot;
    EquipManager equipManager;

    private void Awake()
    {
        invSlot = GetComponentInParent<InventorySlot>();    
        equipManager = GetComponentInParent<GameplayStateController>().GetComponentInChildren<EquipManager>();
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Mouse.current.position.ReadValue();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.localPosition = Vector3.zero;
        Debug.Log("Dragged");
        List<RaycastResult> results = new List<RaycastResult>();
        charPanelRaycaster.Raycast(eventData, results);
        Debug.Log(eventData);
        foreach (RaycastResult result in results)
        {
            GameObject go = result.gameObject;
            Debug.Log(go.name);
            EquipSlot slot = go.GetComponent<EquipSlot>();
            if (slot != null)
            {
                Debug.Log("Slot dropped onto: " + slot.gameObject.name);
                Equipment equipment = (Equipment)invSlot.item;
                if (equipment != null)
                {
                    WeaponEquipment weapon = (WeaponEquipment)equipment;
                    if (weapon != null)
                    {
                        if (slot.name == "MainHand")
                        {
                            weapon.equipSlot = EquipmentSlot.MainHand;
                        }
                        else if (slot.name == "Offhand")
                        {
                            weapon.equipSlot = EquipmentSlot.OffHand;
                        }
                    }
                    invSlot.UseItem();
                }
            }
        }
    }
}
