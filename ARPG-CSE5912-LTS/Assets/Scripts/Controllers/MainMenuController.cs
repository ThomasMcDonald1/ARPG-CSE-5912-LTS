using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController : StateMachine
{
    // References to things that will need to be controlled and known by the state controller go here
    public GameObject mainMenuCanvasObj;
    public GameObject deleteCharacterCanvasObj;
    public GameObject createCharMenuCanvasObj;
    public GameObject optionsMenuCanvasObj;

    [HideInInspector] public Canvas mainMenuCanvas;
    [HideInInspector] public Canvas deleteCharacterCanvas;
    [HideInInspector] public Canvas createCharMenuCanvas;
    [HideInInspector] public Canvas optionsMenuCanvas;

    // Main menu buttons
    public GameObject startGameButtonObj;
    public GameObject createCharButtonObj;
    public GameObject deleteCharButtonObj;
    public GameObject optionsButtonObj;
    public GameObject exitGameButtonObj;

    // Character slots
    public GameObject startErrorObj, slotsFullErrorObj, deleteCharaErrorObj;
    public GameObject displayCharacterObj;
    public GameObject characterNameObj;
    public GameObject yesDeleteObj, noDeleteObj;
    public List<GameObject> saveSlotButtonObjs;
    public List<SaveSlot> saveSlotDataObjs;
    public GameObject slotContainerObj;

    // Options menu buttons
    public GameObject backFromOptionsToMainButtonObj;
    public GameObject resolutionDropDownObj;
    public GameObject fullScreenButtonObj, noFullScreenButtonObj;
    public GameObject musicVolumeSliderObj, soundEffectsVolumeSliderObj;

    // Create character menu obj
    public Camera characterCreationCameraObj;
    [HideInInspector] public Camera characterCreationCamera;
    public GameObject customCharacterObj;

    public GameObject backFromCharCreateToMainButtonObj, resetCharaButtonObj, confirmButtonObj;
    public GameObject maleButtonObj, femaleButtonObj;
    public GameObject hairForwardButtonObj, hairBackwardButtonObj, hairColorButtonObj;
    public GameObject eyebrowsForwardButtonObj, eyebrowsBackwardButtonObj, eyebrowsColorButtonObj;
    public GameObject faceForwardButtonObj, faceBackwardButtonObj, faceColorButtonObj;
    public GameObject facialHairForwardButtonObj, facialHairBackwardButtonObj, facialHairColorButtonObj;
    public GameObject eyesColorButtonObj, skinColorButtonObj;
    public GameObject nameFieldObj, nameErrorObj;
    public CustomCharacter charaScriptableObj;

    //dungeon game objects - to reset upon new play
    public SaveDungeon dungeon1, dungeon2, dungeon3;

    //screen resolutions
    public Resolution[] supportedRes;

    private void Awake()
    {
        mainMenuCanvas = mainMenuCanvasObj.GetComponent<Canvas>();
        deleteCharacterCanvas = deleteCharacterCanvasObj.GetComponent<Canvas>();
        createCharMenuCanvas = createCharMenuCanvasObj.GetComponent<Canvas>();
        optionsMenuCanvas = optionsMenuCanvasObj.GetComponent<Canvas>();
        characterCreationCamera = characterCreationCameraObj.GetComponent<Camera>();

        mainMenuCanvas.enabled = false;
        createCharMenuCanvas.enabled = false;
        optionsMenuCanvas.enabled = false;
        characterCreationCamera.enabled = false;

        //get supported resolutions
        supportedRes = Screen.resolutions;

        ChangeState<MainMenuRootState>();
    }
}