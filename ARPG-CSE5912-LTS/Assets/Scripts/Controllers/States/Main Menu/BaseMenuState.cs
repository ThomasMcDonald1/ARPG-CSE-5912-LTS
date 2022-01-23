using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseMenuState : State
{
    protected MainMenuController mainMenuController;

    // main menu buttons
    public Button startGameButton;
    public Button createCharButton;
    public Button deleteCharButton;
    public Button optionsButton;
    public Button exitGameButton;

    // options menu buttons
    public Button backFromOptionsToMainButton;

    // character creation buttons
    public Button backFromCharCreateToMainButton;

    protected virtual void Awake()
    {
        //main menu
        mainMenuController = GetComponent<MainMenuController>();
        startGameButton = mainMenuController.startGameButtonObj.GetComponent<Button>();
        createCharButton = mainMenuController.createCharButtonObj.GetComponent<Button>();
        deleteCharButton = mainMenuController.deleteCharButtonObj.GetComponent<Button>();
        optionsButton = mainMenuController.optionsButtonObj.GetComponent<Button>();
        exitGameButton = mainMenuController.exitGameButtonObj.GetComponent<Button>();

        //options
        backFromOptionsToMainButton = mainMenuController.backFromOptionsToMainButtonObj.GetComponent<Button>();
        
        //character creation
        backFromCharCreateToMainButton = mainMenuController.backFromCharCreateToMainButtonObj.GetComponent<Button>();
    }
}
