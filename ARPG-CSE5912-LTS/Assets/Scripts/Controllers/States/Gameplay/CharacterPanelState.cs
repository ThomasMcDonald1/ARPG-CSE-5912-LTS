using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CharacterPanelState : BaseGameplayState
{

    public override void Enter()
    {
        base.Enter();
        Debug.Log("entered character panel state");
        Time.timeScale = 0;
        gameplayStateController.characterPanelCanvas.enabled = true;
        foreach (Sound s in FindObjectOfType<AudioManager>().sounds)
        {
            if (s.name != "Theme") s.source.Stop();
        }
        exitPanelToGameButton.onClick.AddListener(() => OnBackButtonClicked());
        exitInventoryButton.onClick.AddListener(() => CloseInventory());
        changeToInventoryMenu.onClick.AddListener(() => InventoryStartUp());

    }

    public override void Exit()
    {
        base.Exit();
        gameplayStateController.characterPanelCanvas.enabled = false;
        Time.timeScale = 1;
    }

    void OnBackButtonClicked()
    {
        gameplayStateController.ChangeState<GameplayState>();
        FindObjectOfType<AudioManager>().Play("MenuClick");
    }


    void InventoryStartUp()
    {
        gameplayStateController.inventoryCanvas.enabled = true;
        gameplayStateController.characterPanelCanvas.enabled = false;
    }
    void CloseInventory()
    {
        gameplayStateController.inventoryCanvas.enabled = false;

        gameplayStateController.ChangeState<GameplayState>();


        //characterPanel.enabled = false;


    }

    protected override void OnUIElementHovered(object sender, InfoEventArgs<List<RaycastResult>> e)
    {
        //figure out if the raycast results contain an item or ability
        foreach (RaycastResult result in e.info)
        {
            GameObject go = result.gameObject;
            Debug.Log("GameObject: " + go.name);
            InventorySlot slot = go.GetComponent<InventorySlot>();
            if (slot != null)
            {
                Debug.Log("Item hovered over: " + slot.name);
                //TODO: Display item tooltip
            }
        }
    }
}