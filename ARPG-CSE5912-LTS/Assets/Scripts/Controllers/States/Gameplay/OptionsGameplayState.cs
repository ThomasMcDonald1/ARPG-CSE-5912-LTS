using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsGameplayState : BaseGameplayState
{
    public override void Enter()
    {
        base.Enter();
        Debug.Log("Game options selected");
        Time.timeScale = 0;
        gameplayStateController.pauseMenuCanvas.enabled = false;
        gameplayStateController.optionsMenuCanvas.enabled = true;
        exitOptionsToPauseButton.onClick.AddListener(() => OnBackToPauseClicked());
    }

    public override void Exit()
    {
        base.Exit();
        gameplayStateController.pauseMenuCanvas.enabled = true;
        gameplayStateController.optionsMenuCanvas.enabled = false;
        Time.timeScale = 1;
    }

    void OnBackToPauseClicked()
    {
        gameplayStateController.ChangeState<PauseGameState>();
    }

    protected override void OnClick(object sender, InfoEventArgs<RaycastHit> e)
    {

    }

    protected override void OnClickCanceled(object sender, InfoEventArgs<bool> e)
    {

    }

    protected override void OnCancelPressed(object sender, InfoEventArgs<int> e)
    {

    }

}