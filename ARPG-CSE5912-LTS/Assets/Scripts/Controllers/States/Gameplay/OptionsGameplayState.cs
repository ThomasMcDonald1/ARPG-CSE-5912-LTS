using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsGameplayState : BaseGameplayState
{
    private Resolution[] supportedRes;

    public override void Enter()
    {
        base.Enter();
        Debug.Log("Game options selected");
        Time.timeScale = 0;
        GetResolutionsSupported();
        gameplayStateController.pauseMenuCanvas.enabled = false;
        gameplayStateController.optionsMenuCanvas.enabled = true;
        gameplayStateController.npcInterfaceObj.SetActive(false);
        gameplayStateController.gameplayUICanvasObj.SetActive(false);
        exitOptionsToPauseButton.onClick.AddListener(() => OnBackToPauseClicked());
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
        gameplayStateController.pauseMenuCanvas.enabled = true;
        gameplayStateController.optionsMenuCanvas.enabled = false;
        gameplayStateController.gameplayUICanvasObj.SetActive(true);
        gameplayStateController.npcInterfaceObj.SetActive(true);
        Time.timeScale = 1;
    }

    void GetResolutionsSupported()
    {
        supportedRes = Screen.resolutions;
        resolutionDropDown.options.Clear();
        foreach (var res in supportedRes)
        {
            string str = "" + res.width + " x " + res.height;
            Debug.Log(str);
            TMPro.TMP_Dropdown.OptionData option = new TMPro.TMP_Dropdown.OptionData(str);
            resolutionDropDown.options.Add(option);
        }
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
        if (PlayerPrefs.GetInt("FullScreen")==1)
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
        Screen.SetResolution(supportedRes[selection].width, supportedRes[selection].height, Screen.fullScreen);

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