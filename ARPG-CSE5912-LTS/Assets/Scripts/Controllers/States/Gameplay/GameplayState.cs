using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameplayState : BaseGameplayState
{
    public override void Enter()
    {
        base.Enter();
        Debug.Log("entered GameplayState");
        gameplayStateController.gameplayUICanvas.enabled = true;
        pauseMenuButton.onClick.AddListener(() => OnPauseMenuClicked());
        exitToMainMenuButton.onClick.AddListener(() => OnExitToMenuClicked());
        exitGameButton.onClick.AddListener(() => OnExitGameClicked());

        gameplayStateController.controls.Gameplay.Cancel.performed += OnEscapePressed;
    }

    public override void Exit()
    {
        base.Exit();
        // Can remove this line to keep gameplay HUD visible while game is paused.
        gameplayStateController.gameplayUICanvas.enabled = false;
        gameplayStateController.controls.Gameplay.Cancel.performed -= OnEscapePressed;
    }


    void OnExitToMenuClicked()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }

    void OnExitGameClicked()
    {
        Application.Quit();
    }

    void OnPauseMenuClicked()
    {
        PauseGame();
    }

    void OnEscapePressed(InputAction.CallbackContext context)
    {
        PauseGame();
    }

    void PauseGame()
    {
        gameplayStateController.ChangeState<PauseGameState>();
    }
}
