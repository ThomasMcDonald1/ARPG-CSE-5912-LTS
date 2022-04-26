using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class TipManager : MonoBehaviour
{
    //Name of item
    [SerializeField] TextMeshProUGUI nameText;

    //Base stats of item
    [SerializeField] TextMeshProUGUI infoSlot1;
    [SerializeField] TextMeshProUGUI infoSlot2;
    [SerializeField] TextMeshProUGUI infoSlot3;
    [SerializeField] TextMeshProUGUI infoSlot4;

    //Level req
    [SerializeField] TextMeshProUGUI infoSlot5;

    //Prefix features
    [SerializeField] TextMeshProUGUI infoSlot6;
    [SerializeField] TextMeshProUGUI infoSlot7;
    [SerializeField] TextMeshProUGUI infoSlot8;
    [SerializeField] TextMeshProUGUI infoSlot9;
    [SerializeField] TextMeshProUGUI infoSlot10;

    //Suffix features
    [SerializeField] TextMeshProUGUI infoSlot11;
    [SerializeField] TextMeshProUGUI infoSlot12;
    [SerializeField] TextMeshProUGUI infoSlot13;
    [SerializeField] TextMeshProUGUI infoSlot14;
    [SerializeField] TextMeshProUGUI infoSlot15;

    //All of them in a list for turning on / off easily
    [SerializeField] List<GameObject> infoSlotTexts;

    public RectTransform tipWindow;

    List<TextMeshProUGUI> prefixItemInfoList;
    List<TextMeshProUGUI> suffixItemInfoList;

    public static TipManager instance;
    Stats playerStats;
    void Awake()
    {
        instance = this;
        prefixItemInfoList = new List<TextMeshProUGUI> { infoSlot6, infoSlot7, infoSlot8, infoSlot9, infoSlot10 };
        suffixItemInfoList = new List<TextMeshProUGUI> { infoSlot11, infoSlot12, infoSlot13, infoSlot14, infoSlot15 };
        playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<Stats>();
    }
   
    // Start is called before the first frame update
    void Start()
    {
        HideWindow();
    }

   public void ShowInventoryTooltip(Ite item)
    {
        if (item != null)
        {
            ShowText();
            foreach (GameObject go in infoSlotTexts)
            {
                go.GetComponent<TextMeshProUGUI>().text = default;
            }
            nameText.text = item.name;
            nameText.color = item.itemNameColor;
            if (nameText.alpha < 1.0f)
            {
                nameText.alpha = 1.0f;
            }
            if (nameText.color == Color.black)
            {
                nameText.color = Color.white;
            }
            if (item is WeaponEquipment weapon)
            {
                if (weapon.typeOfWeapon == WeaponEquipment.weaponType.twohandsword)
                    infoSlot1.text = "Two-Handed";
                else
                    infoSlot1.text = "One-Handed";
                infoSlot2.text = "Attack Speed: " + weapon.attackSpeed.ToString();
                infoSlot3.text = "Physical Damage: " + weapon.minimumDamage.ToString() + " - " + weapon.maximumDamage.ToString();
                infoSlot4.text = "Crit Chance: " + weapon.critChance.ToString() + "%";
            }
            else if (item is ArmorEquipment armor)
            {
                if (armor.typeOfArmor == ArmorEquipment.armorType.light)
                {
                    infoSlot1.text = "Light Armor";
                    infoSlot2.text = "Evasion: " + armor.Evasion.ToString();
                }
                else if (armor.typeOfArmor == ArmorEquipment.armorType.medium)
                {
                    infoSlot1.text = "Medium Armor";
                    infoSlot2.text = "Armor: " + armor.Armor.ToString();
                    infoSlot3.text = "Evasion: " + armor.Evasion.ToString();
                }
                else if (armor.typeOfArmor == ArmorEquipment.armorType.heavy)
                {
                    infoSlot1.text = "Heavy Armor";
                    infoSlot2.text = "Armor: " + armor.Armor.ToString();
                }
            }
            else if (item is ShieldEquipment shieldEquipment)
            {
                infoSlot1.text = "Armor: " + shieldEquipment.armor.ToString();
                infoSlot2.text = "Block Chance: " + shieldEquipment.blockChance.ToString();
            }
            if (item is Equipment equipment)
            {
                if (equipment.prefix != null)
                {
                    for (int i = 0; i < equipment.prefix.FeaturesGOs.Count; i++)
                    {
                        if(equipment.prefix.FeaturesGOs[i] != null){
                            FlatStatModifierFeature feature = equipment.prefix.FeaturesGOs[i].GetComponent<FlatStatModifierFeature>();
                            if (feature != null)
                            {
                                prefixItemInfoList[i].text = "Increases " + feature.type.ToString() + " by " + feature.flatAmount.ToString();
                                prefixItemInfoList[i].color = new Color(0.58f, 0.76f, 0.85f);
                            }
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
            AdjustTipWindowPosition();
            tipWindow.gameObject.SetActive(true);
        } 
    }

    public void ShowAbilityTooltip(Ability ability)
    {
        ShowText();
        foreach (GameObject go in infoSlotTexts)
        {
            go.GetComponent<TextMeshProUGUI>().text = default;
        }
        nameText.text = ability.name;
        nameText.color = Color.cyan;
        if (nameText.alpha < 1.0f)
        {
            nameText.alpha = 1.0f;
        }
        if (nameText.color == Color.black)
        {
            nameText.color = Color.white;
        }
        infoSlot1.text = ability.description;
        infoSlot1.color = new Color(0.8f, 0.8f, 0.8f, 1f);
        if (ability.GetComponentInChildren<BasicAttackDamageAbilityEffect>() == null)
        {
            infoSlot2.text = "Cost: " + ability.GetComponent<BaseAbilityCost>().cost.ToString();
            float playerCDR = playerStats[StatTypes.CooldownReduction];
            float abilityCD = ability.GetComponent<AbilityCooldown>().abilityCooldown;
            float reducedCDForTooltip = abilityCD - (abilityCD * playerCDR * 0.01f);
            infoSlot3.text = "Cooldown: " + reducedCDForTooltip;
            BaseCastType castType = ability.GetComponent<BaseCastType>();
            if (castType is CastTimerCastType)
            {
                float playerCastTimeReduction = playerStats[StatTypes.CastSpeed];
                float abilityCastTime = ability.GetComponent<CastTimerCastType>().castTime;
                float reducedCastTime = abilityCastTime - (abilityCastTime * playerCastTimeReduction * 0.01f);
                infoSlot4.text = "Cast Time: " + reducedCastTime.ToString();
            }
            else
                infoSlot4.text = "Cast Time: Instant";
        }
        AdjustSize();
        AdjustTipWindowPosition();
        tipWindow.gameObject.SetActive(true);
    }

    public void HideWindow()
    {
        tipWindow.gameObject.SetActive(false);
    }

    private void AdjustSize()
    {
        foreach(GameObject go in infoSlotTexts)
        {
            if(go.GetComponent<TextMeshProUGUI>().text == default)
            {
                go.SetActive(false);
            }
        }
    }

    private void ShowText()
    {
        foreach (GameObject go in infoSlotTexts)
        {
            go.SetActive(true);
        }
    }

    private void AdjustTipWindowPosition()
    {
        Vector2 mousePos = Mouse.current.position.ReadValue();
        if (GameplayStateController.Instance.CurrentState is CharacterPanelState)
        {
            if (mousePos.y >= Screen.height / 2)
                tipWindow.transform.position = new Vector2(mousePos.x - tipWindow.sizeDelta.x / 2, mousePos.y - tipWindow.sizeDelta.y / 2);
            else if (mousePos.y < Screen.height / 2)
                tipWindow.transform.position = new Vector2(mousePos.x - tipWindow.sizeDelta.x / 2, mousePos.y + tipWindow.sizeDelta.y / 2);
        }
        else
        {
            if (mousePos.x >= Screen.width / 2 && mousePos.y >= Screen.height / 2)
                tipWindow.transform.position = new Vector2(mousePos.x - tipWindow.sizeDelta.x / 2, mousePos.y - tipWindow.sizeDelta.y / 2);
            else if (mousePos.x < Screen.width / 2 && mousePos.y < Screen.height / 2)
                tipWindow.transform.position = new Vector2(mousePos.x + tipWindow.sizeDelta.x / 2, mousePos.y + tipWindow.sizeDelta.y / 2);
            else if (mousePos.x >= Screen.width / 2 && mousePos.y < Screen.height / 2)
                tipWindow.transform.position = new Vector2(mousePos.x - tipWindow.sizeDelta.x / 2, mousePos.y + tipWindow.sizeDelta.y / 2);
            else
                tipWindow.transform.position = new Vector2(mousePos.x + tipWindow.sizeDelta.x / 2, mousePos.y - tipWindow.sizeDelta.y / 2);

        }
    }
}
