using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayStateController : StateMachine
{
    // References to things that will need to be controlled and known by the state controller go here
    public GameObject pauseMenuCanvasObj;
    public GameObject gameplayUICanvasObj;

    [HideInInspector] public Canvas pauseMenuCanvas;
    [HideInInspector] public Canvas gameplayUICanvas;

    // Button to pause game and bring up pause menu
    public GameObject pauseMenuButtonObj;

    // Pause menu buttons
    public GameObject resumeGameButtonObj;
    public GameObject inGameOptionsButtonObj;
    public GameObject exitToMainMenuButtonObj;
    public GameObject exitGameButtonObj;
    //TODO: Maybe a keybinds button, if we have time to add

    private void Awake()
    {
        pauseMenuCanvas = pauseMenuCanvasObj.GetComponent<Canvas>();
        gameplayUICanvas = gameplayUICanvasObj.GetComponent<Canvas>();

        pauseMenuCanvas.enabled = false;
        gameplayUICanvas.enabled = false;
        ChangeState<GameplayState>();
    }
}
