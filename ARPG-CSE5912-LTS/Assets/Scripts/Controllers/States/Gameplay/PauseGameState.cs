using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseGameState : BaseGameplayState
{
    public override void Enter()
    {
        base.Enter();
        Debug.Log("Game paused");
        Time.timeScale = 0;
        gameplayStateController.pauseMenuCanvas.enabled = true;
        gameplayStateController.npcInterfaceObj.SetActive(false);
        gameplayStateController.equipmentObj.SetActive(false);
        foreach (Sound s in FindObjectOfType<AudioManager>().sounds)
        {
            if (s.name != "Theme") s.source.Stop();
        }

        AddButtonListeners();
    }

    void AddButtonListeners()
    {
        resumeGameButton.onClick.AddListener(() => OnResumeGameClicked());
        inGameOptionsButton.onClick.AddListener(() => OnOptionsClicked());
        exitToMainMenuButton.onClick.AddListener(() => OnExitToMenuClicked());
        exitGameButton.onClick.AddListener(() => OnExitGameClicked());
    }

    public override void Exit()
    {
        base.Exit();
        RemoveButtonListeners();
        gameplayStateController.pauseMenuCanvas.enabled = false;
        gameplayStateController.npcInterfaceObj.SetActive(true);
        gameplayStateController.equipmentObj.SetActive(true);
        Time.timeScale = 1;
    }

    void RemoveButtonListeners()
    {
        resumeGameButton.onClick.RemoveAllListeners();
        inGameOptionsButton.onClick.RemoveAllListeners();
        exitToMainMenuButton.onClick.RemoveAllListeners();
        exitGameButton.onClick.RemoveAllListeners();
    }

    void OnResumeGameClicked()
    {
        ResumeGame();
        FindObjectOfType<AudioManager>().Play("MenuClick");
    }

    void OnOptionsClicked()
    {
        gameplayStateController.ChangeState<OptionsGameplayState>();
        FindObjectOfType<AudioManager>().Play("MenuClick");
    }

    void OnExitToMenuClicked()
    {
        gameplayStateController.ChangeState<GameplayState>();
        gameplayStateController.gameplayUICanvas.enabled = false;

        gameplayStateController.gameplayUICanvasObj.SetActive(false);
        gameplayStateController.npcNamesObj.SetActive(false);
        gameplayStateController.equipmentObj.SetActive(false);

        Time.timeScale = 1;
        LoadingStateController.Instance.LoadScene("MainMenu");
        FindObjectOfType<AudioManager>().Play("MenuClick");
    }

    void OnExitGameClicked()
    {
        Application.Quit();
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
        ResumeGame();
    }

    void ResumeGame()
    {
        gameplayStateController.ChangeState<GameplayState>();
    }
}
