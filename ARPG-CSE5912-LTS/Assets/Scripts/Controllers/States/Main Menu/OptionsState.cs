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
        fullScreenButton.onClick.AddListener(() => OnFullScreenSelected());
        noFullScreenButton.onClick.AddListener(() => OnFullScreenDeselected());
        volumeSlider.onValueChanged.AddListener(OnVolumeAdjusted);
        resolutionDropDown.onValueChanged.AddListener(OnResolutionSelected);
    }

    public override void Exit()
    {
        base.Exit();
        mainMenuController.optionsMenuCanvas.enabled = false;
    }

    void OnBackButtonClicked()
    {
        mainMenuController.ChangeState<MainMenuRootState>();
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
    }

    void OnFullScreenSelected()
    {
        Screen.fullScreen = true;
        Debug.Log("Full screen on");
    }

    void OnFullScreenDeselected()
    {
        Screen.fullScreen = false;
        Debug.Log("Full screen off");
    }

    void OnVolumeAdjusted(float volume)
    {
        if (audioMixer != null)
        {
            audioMixer.SetFloat("Master", volume);
            Debug.Log("Master Volume set to " + volume);
        }

        Debug.Log("Master Volume not set.");
    }
}
