using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterPanelState : BaseGameplayState
{
    public override void Enter()
    {
        base.Enter();
        Debug.Log("entered character panel state");
        gameplayStateController.characterPanelCanvas.enabled = true;
        exitPanelToGameButton.onClick.AddListener(() => OnBackButtonClicked());
    }

    public override void Exit()
    {
        base.Exit();
        gameplayStateController.characterPanelCanvas.enabled = false;
    }

    void OnBackButtonClicked()
    {
        gameplayStateController.ChangeState<GameplayState>();
        FindObjectOfType<AudioManager>().Play("MenuClick");
    }
}
