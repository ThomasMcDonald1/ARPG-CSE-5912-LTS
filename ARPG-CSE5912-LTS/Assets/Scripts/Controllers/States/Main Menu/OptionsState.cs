using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsState : BaseMenuState
{
    public override void Enter()
    {
        base.Enter();
        Debug.Log("entered options menu state");
        mainMenuController.optionsMenuCanvas.enabled = true;
        backFromOptionsToMainButton.onClick.AddListener(() => OnBackButtonClicked());
    }

    public override void Exit()
    {
        base.Exit();
        mainMenuController.optionsMenuCanvas.enabled = false;
    }

    void OnBackButtonClicked()
    {
        mainMenuController.ChangeState<MainMenuRootState>();
    }
}
