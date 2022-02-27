using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_shop : MonoBehaviour
{
    public Transform ShopSlots;
    [SerializeField] public Shop shop;
    ShopSlot[] shopSlots;
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
    private void Update()
    {
        Debug.Log("Run Shop UI update!");
    }


}
  