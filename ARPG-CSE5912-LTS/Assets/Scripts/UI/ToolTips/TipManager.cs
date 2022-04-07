using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using TMPro;

public class TipManager : MonoBehaviour
{
    public TextMeshProUGUI tipText;
    public RectTransform tipWindow;

    public static Action<Ite,Vector2> OnMouseHover;
    public static Action OnMouseLoseFocus;
    public static TipManager instance;
    private void Awake()
    {
        instance = this;
    }
    private void OnEnable()
    {
        OnMouseHover += ShowTip;
        OnMouseLoseFocus += HideTip;
        
    }
    private void OnDisable()
    {
        OnMouseHover -= ShowTip;
        OnMouseLoseFocus -= HideTip;
    }
    // Start is called before the first frame update
    void Start()
    {
        HideTip();
    }

   public  void ShowTip(Ite item, Vector2 mousePos)
    {
        string tipToShow = "";
        switch ((int)item.type)
        {
            case (int)Ite.ItemType.weapon:
                WeaponEquipment eqp = (WeaponEquipment)item;
                tipToShow += item.name + "\n";
                tipToShow += "Level need to be equipped: " + eqp.levelRequiredToEquip + "\n";
                tipToShow += "Attacking Range: " + eqp.attackRange + "\n";
                tipToShow += "AttackSpeed Range: " + eqp.attackSpeed + "\n";
                tipToShow += "Physical Damage: " + eqp.minimumDamage + "-" + eqp.maximumDamage + '\n';
                tipToShow += "Critical chance: " + eqp.critChance;
                break;
            case (int)Ite.ItemType.armor:
                // tipToShow += "\n" + "Defend rate: " + item.defendRate;
                break;
            case (int)Ite.ItemType.utility:
                tipToShow += item.name;

                break;
        }
        tipText.text = item.name;
        
        tipWindow.sizeDelta = new Vector2(tipText.preferredWidth >200 ? 200:tipText.preferredWidth, tipText.preferredHeight);
        tipWindow.gameObject.SetActive(true);
        tipWindow.transform.position = new Vector2(mousePos.x + tipWindow.sizeDelta.x , mousePos.y - tipWindow.sizeDelta.x / 2);


    }
    public void HideTip()
    {
        tipText.text = default;
        tipWindow.gameObject.SetActive(false);
    }
}
