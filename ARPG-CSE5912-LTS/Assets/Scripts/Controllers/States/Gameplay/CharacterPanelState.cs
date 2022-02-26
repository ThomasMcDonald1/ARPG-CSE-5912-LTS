using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterPanelState : BaseGameplayState
{

    public override void Enter()
    {
        base.Enter();
        //Debug.Log("entered character panel state");
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
}