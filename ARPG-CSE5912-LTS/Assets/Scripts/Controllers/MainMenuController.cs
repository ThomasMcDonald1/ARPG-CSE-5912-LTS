using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController : StateMachine
{
    // References to things that will need to be controlled and known by the state controller go here
    public GameObject mainMenuCanvasObj;
    public GameObject createCharMenuCanvasObj;
    public GameObject optionsMenuCanvasObj;

    [HideInInspector] public Canvas mainMenuCanvas;
    [HideInInspector] public Canvas createCharMenuCanvas;
    [HideInInspector] public Canvas optionsMenuCanvas;

    // Main menu buttons
    public GameObject startGameButtonObj;
    public GameObject createCharButtonObj;
    public GameObject deleteCharButtonObj;
    public GameObject optionsButtonObj;
    public GameObject exitGameButtonObj;

    // Options menu buttons
    public GameObject backFromOptionsToMainButtonObj;

    // Create character menu buttons
    public GameObject backFromCharCreateToMainButtonObj;

    private void Awake()
    {
        mainMenuCanvas = mainMenuCanvasObj.GetComponent<Canvas>();
        createCharMenuCanvas = createCharMenuCanvasObj.GetComponent<Canvas>();
        optionsMenuCanvas = optionsMenuCanvasObj.GetComponent<Canvas>();

        mainMenuCanvas.enabled = false;
        createCharMenuCanvas.enabled = false;
        optionsMenuCanvas.enabled = false;
        ChangeState<MainMenuRootState>();
    }
}
