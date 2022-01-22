using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCreationState : BaseMenuState
{
    public override void Enter()
    {
        base.Enter();
        Debug.Log("entered character creation menu state");
        mainMenuController.createCharMenuCanvas.enabled = true;
        backFromCharCreateToMainButton.onClick.AddListener(() => OnBackButtonClicked());

    }

    public override void Exit()
    {
        base.Exit();
        mainMenuController.createCharMenuCanvas.enabled = false;
    }

    void OnBackButtonClicked()
    {
        mainMenuController.ChangeState<MainMenuRootState>();
    }
}
