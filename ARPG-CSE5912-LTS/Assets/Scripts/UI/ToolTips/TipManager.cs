using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class TipManager : MonoBehaviour
{
    //Name of item
    [SerializeField] TextMeshProUGUI itemNameText;

    //Base stats of item
    [SerializeField] TextMeshProUGUI itemInfoSlot1;
    [SerializeField] TextMeshProUGUI itemInfoSlot2;
    [SerializeField] TextMeshProUGUI itemInfoSlot3;
    [SerializeField] TextMeshProUGUI itemInfoSlot4;

    //Level req
    [SerializeField] TextMeshProUGUI itemInfoSlot5;

    //Prefix features
    [SerializeField] TextMeshProUGUI itemInfoSlot6;
    [SerializeField] TextMeshProUGUI itemInfoSlot7;
    [SerializeField] TextMeshProUGUI itemInfoSlot8;
    [SerializeField] TextMeshProUGUI itemInfoSlot9;
    [SerializeField] TextMeshProUGUI itemInfoSlot10;

    //Suffix features
    [SerializeField] TextMeshProUGUI itemInfoSlot11;
    [SerializeField] TextMeshProUGUI itemInfoSlot12;
    [SerializeField] TextMeshProUGUI itemInfoSlot13;
    [SerializeField] TextMeshProUGUI itemInfoSlot14;
    [SerializeField] TextMeshProUGUI itemInfoSlot15;

    //All of them in a list for turning on / off easily
    [SerializeField] List<GameObject> itemInfoSlotTexts;

    public RectTransform tipWindow;

    List<TextMeshProUGUI> prefixItemInfoList;
    List<TextMeshProUGUI> suffixItemInfoList;

    public static TipManager instance;

    void Awake()
    {
        instance = this;
        prefixItemInfoList = new List<TextMeshProUGUI> { itemInfoSlot6, itemInfoSlot7, itemInfoSlot8, itemInfoSlot9, itemInfoSlot10 };
        suffixItemInfoList = new List<TextMeshProUGUI> { itemInfoSlot11, itemInfoSlot12, itemInfoSlot13, itemInfoSlot14, itemInfoSlot15 };
    }
   
    // Start is called before the first frame update
    void Start()
    {
        HideWindow();
    }

   public void ShowInventoryTooltip(Ite item)
    {
        ShowText();
        foreach (GameObject go in itemInfoSlotTexts)
        {
            go.GetComponent<TextMeshProUGUI>().text = default;
        }
        itemNameText.text = item.name;
        itemNameText.color = item.itemNameColor;
        if (item is WeaponEquipment)
        {
            WeaponEquipment weapon = (WeaponEquipment)item;
            //itemInfoSlot1.text = weapon.typeOfWeapon.ToString(); // this will display properly once we change the weapon types
            itemInfoSlot2.text = "Attack Speed: " + weapon.attackSpeed.ToString();
            itemInfoSlot3.text = "Physical Damage: " + weapon.minimumDamage.ToString() + " - " + weapon.maximumDamage.ToString();
            itemInfoSlot4.text = "Crit Chance: " + weapon.critChance.ToString() + "%";
        }
        else if (item is LightArmorEquipment lightArmor)
        {
            itemInfoSlot1.text = "Evasion: " + lightArmor.evasion.ToString();
        }
        else if (item is MediumArmorEquipment mediumArmor)
        {
            itemInfoSlot1.text = "Armor: " + mediumArmor.armor.ToString();
            itemInfoSlot2.text = "Evasion: " + mediumArmor.evasion.ToString();
        }
        else if (item is HeavyArmorEquipment heavyArmor)
        {
            itemInfoSlot1.text = "Armor: " + heavyArmor.armor.ToString();
        }
        else if (item is ShieldEquipment shieldEquipment)
        {
            itemInfoSlot1.text = "Armor: " + shieldEquipment.armor.ToString();
            itemInfoSlot2.text = "Block Chance: " + shieldEquipment.blockChance.ToString();
        }
        if (item is Equipment equipment)
        {
            itemInfoSlot5.text = "Required Level: " + equipment.levelRequiredToEquip.ToString();

            if (equipment.prefix != null)
            {
                for (int i = 0; i < equipment.prefix.FeaturesGOs.Count; i++)
                {
                    FlatStatModifierFeature feature = equipment.prefix.FeaturesGOs[i].GetComponent<FlatStatModifierFeature>();
                    if (feature != null)
                    {
                        prefixItemInfoList[i].text = "Increases " + feature.type.ToString() + " by " + feature.flatAmount.ToString();
                        prefixItemInfoList[i].color = new Color(0.58f, 0.76f, 0.85f);
                    }
                }
            }

            if (equipment.suffix != null)
            {
                for (int i = 0; i < equipment.suffix.FeaturesGOs.Count; i++)
                {
                    FlatStatModifierFeature feature = equipment.suffix.FeaturesGOs[i].GetComponent<FlatStatModifierFeature>();
                    if (feature != null)
                    {
                        suffixItemInfoList[i].text = "Increases " + feature.type.ToString() + " by " + feature.flatAmount.ToString();
                        suffixItemInfoList[i].color = new Color(0.58f, 0.76f, 0.85f);
                    }
                }
            }
        }

        AdjustSize();
        Vector2 mousePos = Mouse.current.position.ReadValue();
        if (mousePos.x <= Screen.width / 2 && mousePos.y >= Screen.height / 2)
            tipWindow.transform.position = new Vector2(mousePos.x + tipWindow.sizeDelta.x / 2, mousePos.y - tipWindow.sizeDelta.y / 2);
        else if (mousePos.x > Screen.width / 2 && mousePos.y >= Screen.height / 2)
            tipWindow.transform.position = new Vector2(mousePos.x - tipWindow.sizeDelta.x / 2, mousePos.y - tipWindow.sizeDelta.y / 2);
        else if (mousePos.x <= Screen.width / 2 && mousePos.y <= Screen.height / 2)
            tipWindow.transform.position = new Vector2(mousePos.x + tipWindow.sizeDelta.x / 2, mousePos.y + tipWindow.sizeDelta.y / 2);
        else
            tipWindow.transform.position = new Vector2(mousePos.x + tipWindow.sizeDelta.x / 2, mousePos.y + tipWindow.sizeDelta.y / 2);

        tipWindow.gameObject.SetActive(true);    
    }

    public void HideWindow()
    {
        tipWindow.gameObject.SetActive(false);
    }

    private void AdjustSize()
    {
        foreach(GameObject go in itemInfoSlotTexts)
        {
            if(go.GetComponent<TextMeshProUGUI>().text == default)
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
