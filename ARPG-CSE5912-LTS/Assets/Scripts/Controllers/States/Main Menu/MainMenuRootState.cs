using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
    }

    void OnStartGameClicked()
    {
        Debug.Log("Start Button Clicked!");
        SceneManager.LoadScene("NatureScene", LoadSceneMode.Single);
    }

    void OnCreateCharClicked()
    {
        mainMenuController.ChangeState<CharacterCreationState>();
    }

    void OnOptionsClicked()
    {
        mainMenuController.ChangeState<OptionsState>();
    }
}
