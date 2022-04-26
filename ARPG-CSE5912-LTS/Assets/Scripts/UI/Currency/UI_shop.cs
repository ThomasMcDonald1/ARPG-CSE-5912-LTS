using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_shop:MonoBehaviour
{
    public Transform ShopSlots;
    [SerializeField] public Button purchaseButton;
    [SerializeField] public Button SaleButton;
    [SerializeField] public GameObject PurchaseContainer;
    [SerializeField] public GameObject SaleContainer;
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
        Debug.Log("Item List length: " + itemList.Length);

        for (int i = 0; i < itemList.Length; i++)
        {
            //Debug.Log("Shop item: " + itemList[i].name);
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
    public void clickOnSaleButton()
    {
        Debug.Log("Click On Sale!");
        SaleButton.Select();
        PurchaseContainer.SetActive(false);
        SaleContainer.SetActive(true);
        UI_Sale.instance.updateUI();
    }
    public void clickOnPurchaseButton()
    {
        Debug.Log("Click On Purchase!");

        purchaseButton.Select();
        PurchaseContainer.SetActive(true);
        SaleContainer.SetActive(false);
    }

}
  