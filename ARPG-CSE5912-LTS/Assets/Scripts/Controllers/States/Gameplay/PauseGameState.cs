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
        gameplayStateController.controls.Gameplay.Cancel.performed += OnEscapePressed;
    }

    public override void Exit()
    {
        base.Exit();
        gameplayStateController.pauseMenuCanvas.enabled = false;
        Time.timeScale = 1;
        gameplayStateController.controls.Gameplay.Cancel.performed -= OnEscapePressed;
    }

    void OnResumeGameClicked()
    {
        ResumeGame();
    }

    void OnEscapePressed(InputAction.CallbackContext context)
    {
        ResumeGame();
    }

    void ResumeGame()
    {
        gameplayStateController.ChangeState<GameplayState>();
    }
}