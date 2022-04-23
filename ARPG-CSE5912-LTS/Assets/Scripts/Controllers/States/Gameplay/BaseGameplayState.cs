using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using TMPro;
using UnityEngine.EventSystems;
using System.Linq;
using TMPro;
using UnityEngine.Audio;

public class BaseGameplayState : State
{
    protected GameplayStateController gameplayStateController;

    public Button pauseMenuButton;
    public Button resumeGameButton;
    public Button inGameOptionsButton;
    public Button exitOptionsToPauseButton, confirmOptionsButton, resetOptionsButton;
    public Button exitToMainMenuButton;
    public Button exitGameButton;
    public Button charaPanelButton;
    public Button exitPanelToGameButton;
    public Button yesRespawnButton;
    public Button noRespawnButton;
    public Button exitAbilityShopButton;
    public Button changeToAbilityMenu;
    public Button confirmPassiveTreeButton;
    public Button closePassiveTreeButton;
    public Button skillNotificationButton;

    public AudioMixer audioMixer;
    public Button backFromOptionsToMainButton;
    public Button fullScreenButton, noFullScreenButton;
    public TMP_Dropdown resolutionDropDown;
    public Slider musicVolumeSlider, soundEffectsVolumeSlider;

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
        exitAbilityShopButton = gameplayStateController.exitAbilityMenuObj.GetComponent<Button>();
        changeToAbilityMenu = gameplayStateController.openAbilityMenuObj.GetComponent<Button>();
        skillNotificationButton = gameplayStateController.skillNotificationButtonObj.GetComponent<Button>();

        confirmOptionsButton = gameplayStateController.confirmOptionsButtonObj.GetComponent<Button>();
        resetOptionsButton = gameplayStateController.resetOptionsButtonObj.GetComponent<Button>();
        resolutionDropDown = gameplayStateController.resolutionDropDownObj.GetComponent<TMP_Dropdown>();
        fullScreenButton = gameplayStateController.fullScreenButtonObj.GetComponent<Button>();
        noFullScreenButton = gameplayStateController.noFullScreenButtonObj.GetComponent<Button>();
        musicVolumeSlider = gameplayStateController.musicVolumeSliderObj.GetComponent<Slider>();
        soundEffectsVolumeSlider = gameplayStateController.soundEffectsVolumeSliderObj.GetComponent<Slider>();


        gameplayStateController.customCharacter.UpdatePlayerModel(gameplayStateController.playerModel);
        yesRespawnButton = gameplayStateController.yesRespawnButtonObj.GetComponent<Button>();
        noRespawnButton = gameplayStateController.noRespawnButtonObj.GetComponent<Button>();
        confirmPassiveTreeButton = gameplayStateController.confirmPassiveTreeButton.GetComponent<Button>();
        closePassiveTreeButton = gameplayStateController.closePassiveTreeButton.GetComponent<Button>();

        backFromOptionsToMainButton = gameplayStateController.exitOptionsToPauseButtonObj.GetComponent<Button>();
        resolutionDropDown = gameplayStateController.resolutionDropDownObj.GetComponent<TMP_Dropdown>();
        fullScreenButton = gameplayStateController.fullScreenButtonObj.GetComponent<Button>();
        noFullScreenButton = gameplayStateController.noFullScreenButtonObj.GetComponent<Button>();
        musicVolumeSlider = gameplayStateController.musicVolumeSliderObj.GetComponent<Slider>();
        soundEffectsVolumeSlider = gameplayStateController.soundEffectsVolumeSliderObj.GetComponent<Slider>();
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
        InputController.OpenPassiveTreeEvent += OnOpenPassiveTreePressed;
        InputController.UIElementHoveredEvent += OnUIElementHovered;

        // Testing mouse wheel
        InputController.DetectMouseScrollWheelEvent += OnMouseScrollMoved;
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
    protected virtual void OnOpenPassiveTreePressed(object sender, InfoEventArgs<int> e)
    {

    }

    protected virtual void OnMouseScrollMoved(object sender, InfoEventArgs<float> e)
    {

    }

    protected virtual void OnUIElementHovered(object sender, InfoEventArgs<List<RaycastResult>> e)
    {

    }
}
