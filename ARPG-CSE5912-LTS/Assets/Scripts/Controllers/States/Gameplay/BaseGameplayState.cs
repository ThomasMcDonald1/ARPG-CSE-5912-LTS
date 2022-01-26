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
    public Button exitOptionsToPauseButton;
    public Button exitToMainMenuButton;
    public Button exitGameButton;
    public Button charaPanelButton;
    public Button exitPanelToGameButton;

    protected virtual void Awake()
    {
        gameplayStateController = GetComponent<GameplayStateController>();
        pauseMenuButton = gameplayStateController.pauseMenuButtonObj.GetComponent<Button>();
        resumeGameButton = gameplayStateController.resumeGameButtonObj.GetComponent<Button>();
        inGameOptionsButton = gameplayStateController.inGameOptionsButtonObj.GetComponent<Button>();
        exitToMainMenuButton = gameplayStateController.exitToMainMenuButtonObj.GetComponent<Button>();
        exitGameButton = gameplayStateController.exitGameButtonObj.GetComponent<Button>();
        exitOptionsToPauseButton = gameplayStateController.exitOptionsToPauseButtonObj.GetComponent<Button>();
        charaPanelButton = gameplayStateController.charaPanelButtonObj.GetComponent<Button>();
        exitPanelToGameButton = gameplayStateController.exitPanelToGameButtonObj.GetComponent<Button>();
    }

    protected override void AddListeners()
    {
        InputController.ClickEvent += OnClick;
        InputController.ClickCanceledEvent += OnClickCanceled;
        InputController.CancelPressedEvent += OnCancelPressed;
    }

    protected override void RemoveListeners()
    {
        InputController.ClickEvent -= OnClick;
        InputController.ClickCanceledEvent -= OnClickCanceled;
        InputController.CancelPressedEvent -= OnCancelPressed;
    }

    protected virtual void OnClick(object sender, InfoEventArgs<RaycastHit> e)
    {

    }

    protected virtual void OnClickCanceled(object sender, InfoEventArgs<bool> e)
    {

    }

    protected virtual void OnCancelPressed(object sender, InfoEventArgs<int> e)
    {

    }
}
