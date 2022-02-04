using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Linq;

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
    public Button exitInventoryButton;
    public Button changeToInventoryMenu;

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
        exitInventoryButton = gameplayStateController.exitInventoryMenuObj.GetComponent<Button>();
        changeToInventoryMenu = gameplayStateController.openInventoryMenuObj.GetComponent<Button>();
    }

    protected override void AddListeners()
    {
        InputController.ClickEvent += OnClick;
        InputController.ClickCanceledEvent += OnClickCanceled;
        InputController.CancelPressedEvent += OnCancelPressed;
        InputController.SecondaryClickPressedEvent += OnSecondaryClickPressed;
        InputController.CharacterMenuPressedEvent += OnCharacterMenuPressed;
        InputController.StationaryButtonPressedEvent += OnStationaryButtonPressed;
        InputController.StationaryButtonCanceledEvent += OnStationaryButtonCanceled;
        InputController.Potion1PressedEvent += OnPotion1Pressed;
        InputController.Potion2PressedEvent += OnPotion2Pressed;
        InputController.Potion3PressedEvent += OnPotion3Pressed;
        InputController.Potion4PressedEvent += OnPotion4Pressed;
        InputController.ActionBar1PressedEvent += OnActionBar1Pressed;
        InputController.ActionBar2PressedEvent += OnActionBar2Pressed;
        InputController.ActionBar3PressedEvent += OnActionBar3Pressed;
        InputController.ActionBar4PressedEvent += OnActionBar4Pressed;
        InputController.ActionBar5PressedEvent += OnActionBar5Pressed;
        InputController.ActionBar6PressedEvent += OnActionBar6Pressed;
        InputController.ActionBar7PressedEvent += OnActionBar7Pressed;
        InputController.ActionBar8PressedEvent += OnActionBar8Pressed;
        InputController.ActionBar9PressedEvent += OnActionBar9Pressed;
        InputController.ActionBar10PressedEvent += OnActionBar10Pressed;
        InputController.ActionBar11PressedEvent += OnActionBar11Pressed;
        InputController.ActionBar12PressedEvent += OnActionBar12Pressed;
        InputController.UIElementLeftClickedEvent += OnUIElementLeftClicked;
        InputController.UIElementRightClickedEvent += OnUIElementRightClicked;
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

    protected virtual void OnClickCanceled(object sender, InfoEventArgs<RaycastHit> r)
    {

    }

    protected virtual void OnCancelPressed(object sender, InfoEventArgs<int> e)
    {

    }

    protected virtual void OnSecondaryClickPressed(object sender, InfoEventArgs<int> e)
    {

    }

    protected virtual void OnCharacterMenuPressed(object sender, InfoEventArgs<int> e)
    {

    }

    protected virtual void OnStationaryButtonPressed(object sender, InfoEventArgs<int> e)
    {

    }

    protected virtual void OnStationaryButtonCanceled(object sender, InfoEventArgs<bool> e)
    {

    }

    protected virtual void OnPotion1Pressed(object sender, InfoEventArgs<int> e)
    {

    }

    protected virtual void OnPotion2Pressed(object sender, InfoEventArgs<int> e)
    {

    }

    protected virtual void OnPotion3Pressed(object sender, InfoEventArgs<int> e)
    {

    }

    protected virtual void OnPotion4Pressed(object sender, InfoEventArgs<int> e)
    {

    }

    protected virtual void OnActionBar1Pressed(object sender, InfoEventArgs<int> e)
    {

    }

    protected virtual void OnActionBar2Pressed(object sender, InfoEventArgs<int> e)
    {

    }

    protected virtual void OnActionBar3Pressed(object sender, InfoEventArgs<int> e)
    {

    }

    protected virtual void OnActionBar4Pressed(object sender, InfoEventArgs<int> e)
    {

    }

    protected virtual void OnActionBar5Pressed(object sender, InfoEventArgs<int> e)
    {

    }

    protected virtual void OnActionBar6Pressed(object sender, InfoEventArgs<int> e)
    {

    }

    protected virtual void OnActionBar7Pressed(object sender, InfoEventArgs<int> e)
    {

    }

    protected virtual void OnActionBar8Pressed(object sender, InfoEventArgs<int> e)
    {

    }

    protected virtual void OnActionBar9Pressed(object sender, InfoEventArgs<int> e)
    {

    }

    protected virtual void OnActionBar10Pressed(object sender, InfoEventArgs<int> e)
    {

    }

    protected virtual void OnActionBar11Pressed(object sender, InfoEventArgs<int> e)
    {

    }

    protected virtual void OnActionBar12Pressed(object sender, InfoEventArgs<int> e)
    {

    }

    protected virtual void OnUIElementLeftClicked(object sender, InfoEventArgs<List<RaycastResult>> e)
    {

    }

    protected virtual void OnUIElementRightClicked(object sender, InfoEventArgs<List<RaycastResult>> e)
    {

    }
}
