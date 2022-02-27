using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using TMPro;
public class ShopSlot : MonoBehaviour
{
    public Image icon;
    public TextMeshProUGUI cost;
    public TextMeshProUGUI itemName;
    public Button purchaseButton;
    public Image goldIcon;
    [SerializeField] PlayerMoney playerMoney;
    public Ite item;
    
    public void InitializeSlot(Ite newItem)
    {
       
        item = newItem;
        icon.gameObject.SetActive(true);
        cost.gameObject.SetActive(true);
        itemName.gameObject.SetActive(true);
        purchaseButton.gameObject.SetActive(true);
        goldIcon.gameObject.SetActive(true);

        icon.sprite = item.icon;
        icon.enabled = true;
        itemName.SetText(item.name);
        cost.SetText(item.cost.ToString());


    }
    public void purchase()
    {

        playerMoney.spendMoney(item.cost);
        switch((int)item.type)
        {
            case (int)Ite.ItemType.armor:
                Inventory.instance.Add(item, Inventory.instance.armorItems);
                break;
            case (int)Ite.ItemType.weapon:
                //Debug.Log("Inventory: " + Inventory.instance.weaponItems);
                Inventory.instance.Add(item, Inventory.instance.weaponItems);
                break;
            case (int)Ite.ItemType.utility:
                Inventory.instance.Add(item, Inventory.instance.utilItems);
                break;
        }
       

    }

}
