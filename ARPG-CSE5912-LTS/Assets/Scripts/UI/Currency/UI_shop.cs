using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_shop : MonoBehaviour
{
    public Transform ShopSlots;
    public Shop shop;
    ShopSlot[] shopSlots;
    public static UI_shop instance;
    private void Awake()
    {
        
        Ite[] itemList = shop.itemList;
        shopSlots = ShopSlots.GetComponentsInChildren<ShopSlot>();
       
   
       
        
        //shopItemTemplate.gameObject.SetActive(false);
        for (int i = 0; i < itemList.Length; i++)
        {
            Debug.Log("shopSlots length: "+shopSlots.Length);
            shopSlots[i].InitializeSlot(itemList[i]);

        }
    }
  

}
  