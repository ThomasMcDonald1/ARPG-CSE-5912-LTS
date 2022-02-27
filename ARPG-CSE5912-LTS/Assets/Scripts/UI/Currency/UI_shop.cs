using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_shop:MonoBehaviour
{
    public Transform ShopSlots;

    public static UI_shop instance;
    ShopSlot[] shopSlots;
    // public static UI_shop instance;
    private void Awake()
    {
        shopSlots = ShopSlots.GetComponentsInChildren<ShopSlot>();

    }
    public void initializeShop(Shop shop)
    {
        
        Ite[] itemList = shop.itemList;
       
   
       
        
        //shopItemTemplate.gameObject.SetActive(false);
        for (int i = 0; i < itemList.Length; i++)
        {
            Debug.Log("shopSlots length: "+shopSlots.Length);
            shopSlots[i].InitializeSlot(itemList[i]);

        }
    }
  

}
  