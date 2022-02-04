using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class ContextMenuPanel : MonoBehaviour
{
    [SerializeField] GameObject contextMenuEntriesObj;
    [SerializeField] Player player;
    [SerializeField] GameObject contextMenuEntryPrefab;
    public GameObject contextMenuPanelCanvas;

    [SerializeField] List<Ability> playerAbilities;

    private void Start()
    {
        playerAbilities = player.abilitiesKnown;
    }

    private void OnEnable()
    {
        ContextMenuEntry.AbilityAssignedToActionBarEvent += OnAbilityAssignedToActionBar;
    }

    public void OnAbilityAssignedToActionBar(object sender, InfoEventArgs<bool> e)
    {
        contextMenuPanelCanvas.SetActive(false);
    }

    public void PopulateContextMenu(ActionButton button)
    {
        ClearContextMenu();
        Debug.Log("Player abilities known list length: " + playerAbilities.Count);
        foreach(Ability ability in playerAbilities)
        {
            Sprite iconToSet = ability.icon;
            string nameToSet = ability.name;
            GameObject menuEntryObj = Instantiate(contextMenuEntryPrefab);
            menuEntryObj.transform.SetParent(contextMenuEntriesObj.transform);
            UnityEngine.UI.Image contextMenuEntryIcon = menuEntryObj.GetComponentInChildren<UnityEngine.UI.Image>();
            TextMeshProUGUI contextMenuEntryTextHolder = menuEntryObj.GetComponentInChildren<TextMeshProUGUI>();
            contextMenuEntryIcon.sprite = iconToSet;
            contextMenuEntryTextHolder.text = nameToSet;
            ContextMenuEntry menuEntry = menuEntryObj.GetComponent<ContextMenuEntry>();
            menuEntry.ability = ability;
            menuEntry.buttonToAssignAbility = button;
        }
    }

    private void ClearContextMenu()
    {
        foreach (Transform child in contextMenuEntriesObj.transform)
        {
            Destroy(child.gameObject);
        }
    }
}
