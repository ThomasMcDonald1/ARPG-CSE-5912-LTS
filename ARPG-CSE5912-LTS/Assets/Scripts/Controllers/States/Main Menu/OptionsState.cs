using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class OptionsState : BaseMenuState
{
    public override void Enter()
    {
        base.Enter();
        Debug.Log("entered options menu state");
        mainMenuController.optionsMenuCanvas.enabled = true;
        backFromOptionsToMainButton.onClick.AddListener(() => OnBackButtonClicked());
        resolutionDropDown.onValueChanged.AddListener(OnResolutionSelected);
        fullScreenButton.onClick.AddListener(() => OnFullScreenSelected());
        noFullScreenButton.onClick.AddListener(() => OnFullScreenDeselected());
        musicVolumeSlider.onValueChanged.AddListener(OnMusicVolumeAdjusted);
        soundEffectsVolumeSlider.onValueChanged.AddListener(OnSoundEffectsVolumeAdjusted);

        AdjustVolumePosition();
    }

    public override void Exit()
    {
        base.Exit();
        mainMenuController.optionsMenuCanvas.enabled = false;
    }

    void AdjustVolumePosition()
    {
        musicVolumeSlider.value = PlayerPrefs.GetFloat("BGM");
        soundEffectsVolumeSlider.value = PlayerPrefs.GetFloat("SE");
    }

    void OnBackButtonClicked()
    {
        mainMenuController.ChangeState<MainMenuRootState>();

        FindObjectOfType<AudioManager>().Play("MenuClick");
    }

    void OnResolutionSelected(int selection)
    {
        switch(selection)
        {
            case 0:
                Screen.SetResolution(800, 600, Screen.fullScreen);
                break;
            case 1:
                Screen.SetResolution(1900, 1200, Screen.fullScreen);
                break;
            default:
                Screen.SetResolution(800, 600, Screen.fullScreen);
                break;
        }
        Debug.Log("Resolution option " + selection + " set");

        FindObjectOfType<AudioManager>().Play("MenuClick");
    }

    void OnFullScreenSelected()
    {
        Screen.fullScreen = true;
        Debug.Log("Full screen (on): " + Screen.fullScreen);

        FindObjectOfType<AudioManager>().Play("MenuClick");
    }

    void OnFullScreenDeselected()
    {
        Screen.fullScreen = false;
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
}
