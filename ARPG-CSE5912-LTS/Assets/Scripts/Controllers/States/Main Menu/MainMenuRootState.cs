using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using BattleDrakeStudios.ModularCharacters;
using TMPro;

public class MainMenuRootState : BaseMenuState
{
    private SaveSlot selectedSlot;

    public override void Enter()
    {
        base.Enter();
        Debug.Log("entered main menu root state");
        mainMenuController.mainMenuCanvas.enabled = true;

        mainMenuController.displayCharacterObj.SetActive(false);
        mainMenuController.characterNameObj.SetActive(false);

        selectedSlot = null;

        SetUpButtons(); 
    }

    void SetUpButtons()
    {
        startGameButton.onClick.AddListener(() => OnStartGameClicked());
        createCharButton.onClick.AddListener(() => OnCreateCharClicked());
        optionsButton.onClick.AddListener(() => OnOptionsClicked());
        slot1Button.onClick.AddListener(() => OnSlot1Clicked());
    }

    void SetSlotVisibility()
    {

    }

    public override void Exit()
    {
        base.Exit();
        mainMenuController.mainMenuCanvas.enabled = false;
        FindObjectOfType<AudioManager>().Play("MenuClick");
    }

    void OnStartGameClicked()
    {
        if (selectedSlot != null)
        {
            Debug.Log("Start Button Clicked!");
            mainMenuController.charaScriptableObj.CopyCharacterData(selectedSlot.characterData);
            Debug.Log("Menu Ver: " + mainMenuController.charaScriptableObj.skinColor);
            SceneManager.LoadScene("UpdateCharacterGameScene", LoadSceneMode.Single);
            FindObjectOfType<AudioManager>().Play("MenuClick");
        }
        else
        {
            Debug.Log("Error - tried to start game without selecting character");
            //TODO: show error message on screen - "Must Select Character or Create New One")
        }
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

    void OnSlot1Clicked()
    {
        selectedSlot = mainMenuController.slot1Data;

        var manager = mainMenuController.displayCharacterObj.GetComponent<ModularCharacterManager>();
        ConfigureCharacterDisplay(manager, mainMenuController.slot1Data.characterData);
        mainMenuController.displayCharacterObj.SetActive(true);
        mainMenuController.characterNameObj.SetActive(true);
    }

    void ConfigureCharacterDisplay(ModularCharacterManager manager, CustomCharacter customCharacter)
    {
        manager.SwapGender(customCharacter.gender);

        manager.ActivatePart(ModularBodyPart.Hair, customCharacter.hairId);
        manager.SetPartColor(ModularBodyPart.Hair, customCharacter.hairId, "_Color_Hair", customCharacter.hairColor);

        manager.ActivatePart(ModularBodyPart.Eyebrow, customCharacter.eyebrowID);
        manager.SetPartColor(ModularBodyPart.Eyebrow, customCharacter.eyebrowID, "_Color_Hair", customCharacter.eyebrowColor);

        manager.ActivatePart(ModularBodyPart.FacialHair, customCharacter.facialHairID);
        manager.SetPartColor(ModularBodyPart.FacialHair, customCharacter.facialHairID, "_Color_Hair", customCharacter.facialHairColor);

        manager.ActivatePart(ModularBodyPart.Head, customCharacter.faceMarkID);
        manager.SetPartColor(ModularBodyPart.Head, customCharacter.faceMarkID, "_Color_BodyArt", customCharacter.facemarkColor);

        manager.ActivatePart(ModularBodyPart.Head, customCharacter.faceMarkID);
        manager.SetPartColor(ModularBodyPart.Head, customCharacter.faceMarkID, "_Color_Eye", customCharacter.eyeColor);

        manager.ActivatePart(ModularBodyPart.Head, customCharacter.faceMarkID);
        manager.SetPartColor(ModularBodyPart.Head, customCharacter.faceMarkID, "_Color_Skin", customCharacter.skinColor);

        mainMenuController.characterNameObj.GetComponent<TextMeshProUGUI>().text = customCharacter.charName;

        mainMenuController.charaScriptableObj.CopyCharacterData(selectedSlot.characterData);
    }




}
