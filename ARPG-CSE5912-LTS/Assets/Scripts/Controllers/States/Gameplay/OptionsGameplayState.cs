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
        gameplayStateController.npcNamesCanvasObj.SetActive(false);
        gameplayStateController.gameplayUICanvasObj.SetActive(false);
        exitOptionsToPauseButton.onClick.AddListener(() => OnBackToPauseClicked());
        resolutionDropDown.onValueChanged.AddListener(OnResolutionSelected);
        fullScreenButton.onClick.AddListener(() => OnFullScreenSelected());
        noFullScreenButton.onClick.AddListener(() => OnFullScreenDeselected());
        musicVolumeSlider.onValueChanged.AddListener(OnMusicVolumeAdjusted);
        soundEffectsVolumeSlider.onValueChanged.AddListener(OnSoundEffectsVolumeAdjusted);

        AdjustMenuAppearance();
    }

    public override void Exit()
    {
        base.Exit();
        gameplayStateController.pauseMenuCanvas.enabled = true;
        gameplayStateController.optionsMenuCanvas.enabled = false;
        gameplayStateController.gameplayUICanvasObj.SetActive(true);
        gameplayStateController.npcNamesCanvasObj.SetActive(true);
        Time.timeScale = 1;
    }

    void OnBackToPauseClicked()
    {
        gameplayStateController.ChangeState<PauseGameState>();
        FindObjectOfType<AudioManager>().Play("MenuClick");
    }

    void AdjustMenuAppearance()
    {
        resolutionDropDown.value = PlayerPrefs.GetInt("Resolution");
        musicVolumeSlider.value = PlayerPrefs.GetFloat("BGM");
        soundEffectsVolumeSlider.value = PlayerPrefs.GetFloat("SE");
    }

    void OnResolutionSelected(int selection)
    {
        switch (selection)
        {
            case 0:
                Screen.SetResolution(800, 600, Screen.fullScreen);
                break;
            case 1:
                Screen.SetResolution(1280, 720, Screen.fullScreen);
                break;
            case 2:
                Screen.SetResolution(1600, 1900, Screen.fullScreen);
                break;
            case 3:
                Screen.SetResolution(1900, 1200, Screen.fullScreen);
                break;
            default:
                Screen.SetResolution(800, 600, Screen.fullScreen);
                break;
        }
        Debug.Log("Resolution option " + selection + " set");

        PlayerPrefs.SetInt("Resolution", selection);

        FindObjectOfType<AudioManager>().Play("MenuClick");
    }

    void OnFullScreenSelected()
    {
        Screen.fullScreen = true;

        PlayerPrefs.SetInt("FullScreen", 1);

        Debug.Log("Full screen (on): " + Screen.fullScreen);

        FindObjectOfType<AudioManager>().Play("MenuClick");
    }

    void OnFullScreenDeselected()
    {
        Screen.fullScreen = false;

        PlayerPrefs.SetInt("FullScreen", 0);

        Debug.Log("Full screen (off): " + Screen.fullScreen);

        FindObjectOfType<AudioManager>().Play("MenuClick");
    }

    void OnMusicVolumeAdjusted(float volume)
    {
        FindObjectOfType<AudioManager>().AdjustMusicVolume(volume);
        Debug.Log("Music volume set to " + volume);
    }

    void OnSoundEffectsVolumeAdjusted(float volume)
    {
        FindObjectOfType<AudioManager>().AdjustSoundEffectVolume(volume);
        Debug.Log("Sound effects volume set to " + volume);

        FindObjectOfType<AudioManager>().Play("MenuClick");
    }


    protected override void OnClick(object sender, InfoEventArgs<RaycastHit> e)
    {

    }

    protected override void OnClickCanceled(object sender, InfoEventArgs<RaycastHit> e)
    {

    }

    protected override void OnCancelPressed(object sender, InfoEventArgs<int> e)
    {

    }

}
