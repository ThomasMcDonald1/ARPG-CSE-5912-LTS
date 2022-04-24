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
        GetResolutionsSupported();
        backFromOptionsToMainButton.onClick.AddListener(() => OnBackButtonClicked());
        resolutionDropDown.onValueChanged.AddListener(OnResolutionSelected);
        fullScreenButton.onClick.AddListener(() => OnFullScreenSelected());
        noFullScreenButton.onClick.AddListener(() => OnFullScreenDeselected());
        musicVolumeSlider.onValueChanged.AddListener(OnMusicVolumeAdjusted);
        soundEffectsVolumeSlider.onValueChanged.AddListener(OnSoundEffectsVolumeAdjusted);
        confirmOptionsButton.onClick.AddListener(() => OnConfirmOptions());
        resetOptionsButton.onClick.AddListener(() => OnResetOptions());

        AdjustMenuAppearance();
    }

    public override void Exit()
    {
        base.Exit();
        mainMenuController.optionsMenuCanvas.enabled = false;
    }

    void GetResolutionsSupported()
    {
        var resolutions = mainMenuController.supportedRes;
        int native = 0, i=0;
        resolutionDropDown.options.Clear();
        foreach (Resolution res in resolutions)
        {
            string str = "" + res.width + " x " + res.height;
            TMPro.TMP_Dropdown.OptionData option = new TMPro.TMP_Dropdown.OptionData(str);
            resolutionDropDown.options.Add(option);
            if (res.Equals(Screen.currentResolution))
            {
                native = i;
            }
            i++;
        }

        //set default resolution option
        PlayerPrefs.SetInt("Resolution", native);
    }

    void AdjustMenuAppearance()
    {
        resolutionDropDown.value = PlayerPrefs.GetInt("Resolution");
        musicVolumeSlider.value = PlayerPrefs.GetFloat("BGM");
        soundEffectsVolumeSlider.value = PlayerPrefs.GetFloat("SE");
    }

    void OnBackButtonClicked()
    {
        mainMenuController.ChangeState<MainMenuRootState>();

        FindObjectOfType<AudioManager>().Play("MenuClick");
    }

    void OnConfirmOptions()
    {
        PlayerPrefs.SetInt("Resolution", resolutionDropDown.value);
        if (Screen.fullScreen)
        {
            PlayerPrefs.SetInt("FullScreen", 1);
        }
        else
        {
            PlayerPrefs.SetInt("FullScreen", 0);
        }

        PlayerPrefs.SetFloat("BGM", musicVolumeSlider.value);
        PlayerPrefs.SetFloat("SE", soundEffectsVolumeSlider.value);
    }

    void OnResetOptions()
    {
        resolutionDropDown.value = PlayerPrefs.GetInt("Resolution");
        if (PlayerPrefs.GetInt("FullScreen") == 1)
        {
            Screen.fullScreen = true;
        }
        else
        {
            Screen.fullScreen = false;
        }
        musicVolumeSlider.value = PlayerPrefs.GetFloat("BGM");
        soundEffectsVolumeSlider.value = PlayerPrefs.GetFloat("SE");
    }

    void OnResolutionSelected(int selection)
    {
        Screen.SetResolution(mainMenuController.supportedRes[selection].width, mainMenuController.supportedRes[selection].height, Screen.fullScreen);

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
}
