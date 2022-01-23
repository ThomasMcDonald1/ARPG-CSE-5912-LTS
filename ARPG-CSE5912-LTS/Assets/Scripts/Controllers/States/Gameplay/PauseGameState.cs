using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseGameState : BaseGameplayState
{
    public override void Enter()
    {
        base.Enter();
        Debug.Log("Game paused");
        Time.timeScale = 0;
        gameplayStateController.pauseMenuCanvas.enabled = true;
        resumeGameButton.onClick.AddListener(() => OnResumeGameClicked());
        inGameOptionsButton.onClick.AddListener(() => OnOptionsClicked());
    }

    public override void Exit()
    {
        base.Exit();
        gameplayStateController.pauseMenuCanvas.enabled = false;
        Time.timeScale = 1;
    }

    void OnResumeGameClicked()
    {
        ResumeGame();
    }

    void OnOptionsClicked()
    {
        gameplayStateController.ChangeState<OptionsGameplayState>();
    }

    protected override void OnClick(object sender, InfoEventArgs<RaycastHit> e)
    {

    }

    protected override void OnClickCanceled(object sender, InfoEventArgs<bool> e)
    {

    }

    protected override void OnCancelPressed(object sender, InfoEventArgs<int> e)
    {
        ResumeGame();
    }

    void ResumeGame()
    {
        gameplayStateController.ChangeState<GameplayState>();
    }
}