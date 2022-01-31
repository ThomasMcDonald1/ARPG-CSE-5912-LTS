using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuRootState : BaseMenuState
{
    public override void Enter()
    {
        base.Enter();
        Debug.Log("entered main menu root state");
        mainMenuController.mainMenuCanvas.enabled = true;
        startGameButton.onClick.AddListener(() => OnStartGameClicked());
        createCharButton.onClick.AddListener(() => OnCreateCharClicked());
        optionsButton.onClick.AddListener(() => OnOptionsClicked());
    }

    public override void Exit()
    {
        base.Exit();
        mainMenuController.mainMenuCanvas.enabled = false;
        FindObjectOfType<AudioManager>().Play("MenuClick");
    }

    void OnStartGameClicked()
    {
        Debug.Log("Start Button Clicked!");
        SceneManager.LoadScene("GameScene", LoadSceneMode.Single);
        FindObjectOfType<AudioManager>().Play("MenuClick");
    }

    void OnCreateCharClicked()
    {
        mainMenuController.ChangeState<CharacterCreationState>();
        FindObjectOfType<AudioManager>().Play("MenuClick");
    }

    void OnOptionsClicked()
    {
        mainMenuController.ChangeState<OptionsState>();
        FindObjectOfType<AudioManager>().Play("MenuClick");
    }
}
