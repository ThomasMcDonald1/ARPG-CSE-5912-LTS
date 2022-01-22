using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseMenuState : State
{
    protected MainMenuController mainMenuController;

    public Button startGameButton;
    public Button createCharButton;
    public Button deleteCharButton;
    public Button optionsButton;
    public Button exitGameButton;
    public Button backFromCharCreateToMainButton;

    protected virtual void Awake()
    {
        mainMenuController = GetComponent<MainMenuController>();
        startGameButton = mainMenuController.startGameButtonObj.GetComponent<Button>();
        createCharButton = mainMenuController.createCharButtonObj.GetComponent<Button>();
        deleteCharButton = mainMenuController.deleteCharButtonObj.GetComponent<Button>();
        optionsButton = mainMenuController.optionsButtonObj.GetComponent<Button>();
        exitGameButton = mainMenuController.exitGameButtonObj.GetComponent<Button>();
        backFromCharCreateToMainButton = mainMenuController.backFromCharCreateToMainButtonObj.GetComponent<Button>();
    }
}
