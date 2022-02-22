using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayStateController : StateMachine
{
    // References to things that will need to be controlled and known by the state controller go here
    public GameObject pauseMenuCanvasObj;
    public GameObject gameplayUICanvasObj;
    public GameObject optionsMenuCanvasObj;
    public GameObject characterPanelCanvasObj;
    public GameObject inventoryCanvasObj;
    public GameObject gameoverCanvasObj;

    [HideInInspector] public Canvas pauseMenuCanvas;
    [HideInInspector] public Canvas gameplayUICanvas;
    [HideInInspector] public Canvas optionsMenuCanvas;
    [HideInInspector] public Canvas characterPanelCanvas;
    [HideInInspector] public Canvas gameoverCanvas;
    [HideInInspector] public Canvas inventoryCanvas;
    // Button to pause game and bring up pause menu
    public GameObject pauseMenuButtonObj;

    // Pause menu buttons
    public GameObject resumeGameButtonObj;
    public GameObject inGameOptionsButtonObj;
    public GameObject exitToMainMenuButtonObj;
    public GameObject exitGameButtonObj;
    public GameObject exitOptionsToPauseButtonObj;

    // Character panel buttons
    public GameObject charaPanelButtonObj;
    public GameObject exitPanelToGameButtonObj;

    public GameObject openInventoryMenuObj;
    public GameObject exitInventoryMenuObj;

    public GameObject aoeReticleSphere;
    public GameObject aoeReticleCylinder;

    // Casting bar
    public CastingBar castingBar;

    // Character model things
    public GameObject playerModel;
    public CustomCharacter customCharacter; //scriptable object


    //Gameover panel button
    public GameObject yesRespawnButtonObj;
    public GameObject noRespawnButtonObj;

    public HealthBarController healthBarController;
    public EnergyBarController energyBarController;
    public GameObject passiveTreeUI;
    public GameObject confirmPassiveTreeButton;
    public GameObject closePassiveTreeButton;

    //TODO: Maybe a keybinds button, if we have time to add

    private void Awake()
    {

        pauseMenuCanvas = pauseMenuCanvasObj.GetComponent<Canvas>();
        gameplayUICanvas = gameplayUICanvasObj.GetComponent<Canvas>();
        optionsMenuCanvas = optionsMenuCanvasObj.GetComponent<Canvas>();
        characterPanelCanvas = characterPanelCanvasObj.GetComponent<Canvas>();
        gameoverCanvas = gameoverCanvasObj.GetComponent<Canvas>();
        inventoryCanvas = inventoryCanvasObj.GetComponent<Canvas>();

        pauseMenuCanvas.enabled = false;
        gameplayUICanvas.enabled = false;
        optionsMenuCanvas.enabled = false;
        characterPanelCanvas.enabled = false;
        inventoryCanvas.enabled = false;
        aoeReticleSphere.SetActive(false);
        aoeReticleCylinder.SetActive(false);
        gameoverCanvas.enabled = false;

        ChangeState<GameplayState>();
    }
}
