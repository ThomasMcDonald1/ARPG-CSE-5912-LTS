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
    public string tipToShow;
 
    public Ite item;

    private float timeToWait = 5f;



    public void OnPointerEnter(PointerEventData eventData)
    {
        if (removeButton.interactable)
        {
            //Debug.Log("Hoever!");
            //StopAllCoroutines();
            //StartCoroutine(StartTimer());
            ShowMessage();
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // StopAllCoroutines();
        tipToShow = "";
        TipManager.OnMouseLoseFocus();
    }
    private void ShowMessage()
    {
       
        TipManager.OnMouseHover(item, Input.mousePosition);

        
    }

    private IEnumerator StartTimer()
    {
        //Debug.Log("In Timer");
        yield return new WaitForSeconds(timeToWait);
        ShowMessage();
    }

  
    

    // Start is called before the first frame update
    void Start()
    {
      
        
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
