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
    private int shopSlotLength =0;
    // public static UI_shop instance;
    private void Awake()
    {
        shopSlots = ShopSlots.GetComponentsInChildren<ShopSlot>();

    }
    public void initializeShop(Shop shop)
    {
        
        Ite[] itemList = shop.itemList;

        shopSlotLength = itemList.Length;


        for (int i = 0; i < itemList.Length; i++)
        {
            shopSlots[i].InitializeSlot(itemList[i]);

        }
    }
    public void resetShop()
    {
        for (int i = 0; i < shopSlotLength; i++)
        {
            shopSlots[i].resetSlot();

        }
    }
  

}
  