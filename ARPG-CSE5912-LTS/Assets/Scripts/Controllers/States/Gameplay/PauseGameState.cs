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
        gameplayStateController.npcNamesCanvas.enabled = false;
        gameplayStateController.pauseMenuCanvas.enabled = true;
        gameplayStateController.gameplayUICanvas.enabled = false;
        foreach (Sound s in FindObjectOfType<AudioManager>().sounds)
        {
            if (s.name != "Theme") s.source.Stop();
        }
        resumeGameButton.onClick.AddListener(() => OnResumeGameClicked());
        inGameOptionsButton.onClick.AddListener(() => OnOptionsClicked());
    }

    public override void Exit()
    {
        base.Exit();
        gameplayStateController.npcNamesCanvas.enabled = true;
        gameplayStateController.pauseMenuCanvas.enabled = false;
        gameplayStateController.gameplayUICanvas.enabled = true;
        Time.timeScale = 1;
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
