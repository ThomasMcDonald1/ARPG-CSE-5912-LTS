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
    }

    public override void Exit()
    {
        base.Exit();
        mainMenuController.mainMenuCanvas.enabled = false;
    }

    void OnStartGameClicked()
    {
        Debug.Log("Start Button Clicked!");
        //TODO: Add a loading screen here somehow
        SceneManager.LoadScene("GameScene", LoadSceneMode.Single);
    }

    void OnCreateCharClicked()
    {
        mainMenuController.ChangeState<CharacterCreationState>();
    }
}
