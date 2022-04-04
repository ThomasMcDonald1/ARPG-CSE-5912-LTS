using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
public class UtilityMenuPanel : MonoBehaviour
{
    [SerializeField] GameObject utilityMenuEntriesObj;
    [SerializeField] GameObject utilityMenuEntryPrefab;
    public GameObject utilityMenuPanelCanvas;
    public List<Ite> utilities = new List<Ite>();
    public List<Ite> itemNamesExited = new List<Ite>();
    public static UtilityMenuPanel instance;
     void Awake()
    {
        instance = this;
    }

    private void OnEnable()
    {
        UitilMenuEntry.UtilityAssignedToActionBarEvent += OnUtilityAssignedToActionBar;
    }

    public void OnUtilityAssignedToActionBar(object sender, InfoEventArgs<bool> e)
    {
        utilityMenuPanelCanvas.SetActive(false);
    }

    public void PopulateUtilityMenu(PotionButton button)
    {
        ClearUtilityMenu();
        utilities= Inventory.instance.utilItems;
        foreach (Ite item in utilities)
        {
            Debug.Log("Potions that are excited: " + itemNamesExited);
            if (!checkInTheButtons(item)){ 
                Sprite iconToSet = item.icon;
                string nameToSet = item.name;
                GameObject menuEntryObj = Instantiate(utilityMenuEntryPrefab);
                menuEntryObj.transform.SetParent(utilityMenuEntriesObj.transform);
                Image utilityMenuEntryIcon = menuEntryObj.GetComponentInChildren<Image>();
                TextMeshProUGUI utilityMenuEntryTextHolder = menuEntryObj.GetComponentInChildren<TextMeshProUGUI>();
                utilityMenuEntryIcon.sprite = iconToSet;
                utilityMenuEntryTextHolder.text = nameToSet;
                UitilMenuEntry menuEntry = menuEntryObj.GetComponent<UitilMenuEntry>();
                menuEntry.item = item;
                menuEntry.buttonToAssignUtil = button;
                menuEntry.amount = Inventory.instance.amount[item.name].ToString();
                //Debug.Log();
            }
        }
    }
    public bool checkInTheButtons(Ite itemname)
    {
       
        foreach (Ite item in itemNamesExited)
        {
            if(item.name.Equals(itemname.name))
            {
                return true;
                
            }
        }
        return false;
    }
        private void ClearUtilityMenu()
    {
        foreach (Transform child in utilityMenuEntriesObj.transform)
        {
            Destroy(child.gameObject);
        }
    }
}
