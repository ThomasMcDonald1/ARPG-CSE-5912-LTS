using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.EventSystems;


public class InventorySlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Image icon;
    public TextMeshProUGUI amount;
    public Button removeButton;
  
    //private float timeToWait = 0.05f;
 
    public Ite item;

    public static Action<String, Vector2> OnMouseHover;
    public static Action OnMouseLoseFocus;

    public void OnPointerEnter(PointerEventData eventData)
    {
       
        if (removeButton.interactable)
        {
            //Debug.Log("Hoever!");
            //StopAllCoroutines();
            //StartCoroutine(StartTimer());
           //TipManager.instance.ShowTip(item.name);
        }
       /* foreach (GameObject obj in objs)
        {
            InventorySlot invSlot = obj.GetComponentInChildren<InventorySlot>();
            Debug.Log("Inventory item: " + invSlot.item.name);
          
        }
       */
    }
    public void OnPointerExit(PointerEventData eventData)
    {
       //TipManager.instance.HideTip();


    }
 
   /* private IEnumerator StartTimer()
    {
        //Debug.Log("In Timer");
        yield return new WaitForSeconds(timeToWait);
        ShowMessage();
    }*/

  
    // Start is called before the first frame update
    void Start()
    {
        //HideTip();
        ClearSlot();

    }

  
 
 
    public void AddItem(Ite newItem)
    {
        item = newItem;
        icon.sprite = item.icon;
        icon.enabled = true;
        removeButton.interactable = true;
        
    }

    public void ClearSlot()
    {
            item = null;
            icon.sprite = null;
            icon.enabled = false;
            removeButton.interactable = false;
            amount.SetText("");
            
    }

    public void OnRemoveButton()
    {
        if(item != null)
            Inventory.instance.Remove(item);
    }

    public void UseItem()
    {
        if (item != null)
        {
            item.Use();
            if (item.type == Ite.ItemType.utility)
            {
                Inventory.instance.Remove(item);

            }

        }
    }

  
}
