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
    //private float timeToWait = 0.05f;
    [SerializeField] public  TextMeshProUGUI tipText;
    [SerializeField] public RectTransform tipWindow;
    Ite item;

    public static Action<String, Vector2> OnMouseHover;
    public static Action OnMouseLoseFocus;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (removeButton.interactable)
        {
            Debug.Log("Hoever!");
            StopAllCoroutines();
            //StartCoroutine(StartTimer());
            ShowMessage();
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        StopAllCoroutines();
        HideTip();
    }
    private void ShowMessage()
    {
        tipToShow = item.name;
        switch ((int)item.type)
        {
            case(int) Ite.ItemType.weapon:
                //tipToShow += "\n" +"Damage: "+ item.attackDamage;
                break;
            case (int)Ite.ItemType.armor:
                //tipToShow += "\n" + "Defend rate: " + item.defendRate;
                break;
            case (int)Ite.ItemType.utility:
                //tipToShow += "\n" +  item.utilityUsage;
                break;
        }


        ShowTip(tipToShow,Input.mousePosition);
    }

   /* private IEnumerator StartTimer()
    {
        //Debug.Log("In Timer");
        yield return new WaitForSeconds(timeToWait);
        ShowMessage();
    }*/

    private void OnEnable()
    {
        OnMouseHover -= ShowTip;
        OnMouseLoseFocus -= HideTip;

    }

    // Start is called before the first frame update
    void Start()
    {
        HideTip();
       
    }

    public void ShowTip(String tip, Vector2 mousePos)
    {
        Debug.Log("In ShowMessage");

        tipText.text = tip;
        tipWindow.sizeDelta = new Vector2(tipText.preferredWidth > 200 ? 200 : tipText.preferredWidth, tipText.preferredHeight);
        tipWindow.gameObject.SetActive(true);
        tipWindow.transform.position = new Vector2(mousePos.x+ tipWindow.sizeDelta.x/2, mousePos.y);


    }
    public void HideTip()
    {
        Debug.Log("Hide ShowMessage");

        tipText.text = default;
        tipWindow.gameObject.SetActive(false);
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
        if(item != null)
        {
            item.Use();
        }
    }

  
}
