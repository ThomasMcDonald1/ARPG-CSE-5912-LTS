using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class TipManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI itemNameText;
    [SerializeField] TextMeshProUGUI itemInfoSlot1;
    [SerializeField] TextMeshProUGUI itemInfoSlot2;
    [SerializeField] TextMeshProUGUI itemInfoSlot3;
    [SerializeField] TextMeshProUGUI itemInfoSlot4;
    [SerializeField] TextMeshProUGUI itemInfoSlot5;
    [SerializeField] TextMeshProUGUI itemInfoSlot6;
    [SerializeField] TextMeshProUGUI itemInfoSlot7;
    [SerializeField] TextMeshProUGUI itemInfoSlot8;
    [SerializeField] TextMeshProUGUI itemInfoSlot9;
    [SerializeField] TextMeshProUGUI itemInfoSlot10;
    [SerializeField] TextMeshProUGUI itemInfoSlot11;
    [SerializeField] TextMeshProUGUI itemInfoSlot12;
    [SerializeField] TextMeshProUGUI itemInfoSlot13;
    [SerializeField] TextMeshProUGUI itemInfoSlot14;

    [SerializeField] List<GameObject> itemInfoSlotTexts;

    public RectTransform tipWindow;
    List<TextMeshProUGUI> prefixItemInfoList;
    List<TextMeshProUGUI> suffixItemInfoList;

    public static TipManager instance;

    void Awake()
    {
        instance = this;
        prefixItemInfoList = new List<TextMeshProUGUI> { itemInfoSlot5, itemInfoSlot6, itemInfoSlot7, itemInfoSlot8, itemInfoSlot9 };
        suffixItemInfoList = new List<TextMeshProUGUI> { itemInfoSlot10, itemInfoSlot11, itemInfoSlot12, itemInfoSlot13, itemInfoSlot14 };
        Debug.Log(prefixItemInfoList.Count);
        Debug.Log(suffixItemInfoList.Count);
    }
   

    // Start is called before the first frame update
    void Start()
    {
        HideTip();
        ShowText();
    }

   public void ShowInventoryTooltip(Ite item)
    {
        Debug.Log("Item name: " + item.name);
        itemNameText.text = item.name;
        itemNameText.color = item.itemNameColor;
        if (item is WeaponEquipment weapon)
        {
            itemInfoSlot1.text = "Attack Range: " + weapon.attackRange.ToString();
            itemInfoSlot2.text = "Attack Speed: " + weapon.attackSpeed.ToString();
            itemInfoSlot3.text = weapon.minimumDamage.ToString() + " - " + weapon.maximumDamage.ToString() + " Physical Damage";
            itemInfoSlot4.text = "Crit Chance: " + weapon.critChance.ToString() + "%";
            itemInfoSlot10.text = "Required Level: " + weapon.levelRequiredToEquip.ToString();
        }
        else if (item is LightArmorEquipment lightArmor)
        {
        }
        else if (item is MediumArmorEquipment mediumArmor)
        {
        }
        else if (item is HeavyArmorEquipment heavyArmor)
        {
        }
        else if (item is ShieldEquipment shieldEquipment)
        {
        }
        else if (item is JewelryEquipment jewelryEquipment)
        {
        }
        if (item is Equipment equipment)
        {
            for (int i = 0; i < equipment.prefix.FeaturesGOs.Count; i++)
            {
                FlatStatModifierFeature feature = equipment.prefix.FeaturesGOs[i].GetComponent<FlatStatModifierFeature>();
                if (feature != null)
                    prefixItemInfoList[i].text = "Increases " + feature.type.ToString() + " by " + feature.flatAmount.ToString();
            }

            for (int i = 0; i < equipment.suffix.FeaturesGOs.Count; i++)
            {
                FlatStatModifierFeature feature = equipment.suffix.FeaturesGOs[i].GetComponent<FlatStatModifierFeature>();
                if (feature != null)
                    suffixItemInfoList[i].text = "Increases " + feature.type.ToString() + " by " + feature.flatAmount.ToString();
            }
        }

        AdjustSize();
        Vector2 mousePos = Mouse.current.position.ReadValue();
        if (mousePos.x <= Screen.width / 2)
            tipWindow.transform.position = new Vector2(mousePos.x + tipWindow.sizeDelta.x / 2, mousePos.y);
        else
            tipWindow.transform.position = new Vector2(mousePos.x - tipWindow.sizeDelta.x / 2, mousePos.y);

        tipWindow.gameObject.SetActive(true);    
    }
    public void HideTip()
    {
        itemNameText.text = default;
        tipWindow.gameObject.SetActive(false);
    }

    private void AdjustSize()
    {
        foreach(GameObject go in itemInfoSlotTexts)
        {
            if(go.GetComponent<TextMeshProUGUI>().text == "New Text")
            {
                go.SetActive(false);
            }
        }
    }

    private void ShowText()
    {
        foreach (GameObject go in itemInfoSlotTexts)
        {
            go.SetActive(true);
        }
    }
}
