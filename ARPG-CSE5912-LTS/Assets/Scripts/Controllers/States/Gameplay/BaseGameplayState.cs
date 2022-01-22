using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseGameplayState : State
{
    protected GameplayStateController gameplayStateController;

    public Button pauseMenuButton;
    public Button resumeGameButton;
    public Button inGameOptionsButton;
    public Button exitToMainMenuButton;
    public Button exitGameButton;

    protected virtual void Awake()
    {
        gameplayStateController = GetComponent<GameplayStateController>();
        pauseMenuButton = gameplayStateController.pauseMenuButtonObj.GetComponent<Button>();
        resumeGameButton = gameplayStateController.resumeGameButtonObj.GetComponent<Button>();
        inGameOptionsButton = gameplayStateController.inGameOptionsButtonObj.GetComponent<Button>();
        exitToMainMenuButton = gameplayStateController.exitToMainMenuButtonObj.GetComponent<Button>();
        exitGameButton = gameplayStateController.exitGameButtonObj.GetComponent<Button>();
    }
}
