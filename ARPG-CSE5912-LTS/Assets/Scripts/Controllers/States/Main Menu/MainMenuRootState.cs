using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using BattleDrakeStudios.ModularCharacters;
using TMPro;

public class MainMenuRootState : BaseMenuState
{
    public override void Enter()
    {
        base.Enter();
        Debug.Log("entered main menu root state");
        mainMenuController.mainMenuCanvas.enabled = true;
        mainMenuController.deleteCharacterCanvas.enabled = false;

        mainMenuController.displayCharacterObj.SetActive(false);
        mainMenuController.characterNameObj.SetActive(false);

        ClearErrorMessages();

        selectedSlot = null;

        SetUpButtons();
        SetSlotVisibility();
    }

    void SetUpButtons()
    {
        startGameButton.onClick.AddListener(() => OnStartGameClicked());
        createCharButton.onClick.AddListener(() => OnCreateCharClicked());
        optionsButton.onClick.AddListener(() => OnOptionsClicked());
        slot1Button.onClick.AddListener(() => OnSlotClicked(1));
        slot2Button.onClick.AddListener(() => OnSlotClicked(2));
        slot3Button.onClick.AddListener(() => OnSlotClicked(3));
        slot4Button.onClick.AddListener(() => OnSlotClicked(4));
        slot5Button.onClick.AddListener(() => OnSlotClicked(5));
        slot6Button.onClick.AddListener(() => OnSlotClicked(6));
        deleteCharButton.onClick.AddListener(() => OnDeleteCharacterSelected());
        yesDeleteButton.onClick.AddListener(() => OnYesDeleteClicked());
        noDeleteButton.onClick.AddListener(() => OnNoDeleteClicked());
    }

    void SetSlotVisibility()
    {
        for (int slotNum = 0; slotNum < mainMenuController.saveSlotDataObjs.Count; slotNum++)
        {
            if (mainMenuController.saveSlotDataObjs[slotNum].containsData)
            {
                mainMenuController.saveSlotButtonObjs[slotNum].SetActive(true);
                var buttonText = mainMenuController.saveSlotButtonObjs[slotNum].GetComponentInChildren<TextMeshProUGUI>();
                buttonText.text = mainMenuController.saveSlotDataObjs[slotNum].characterData.charName;
            }
            else
            {
                mainMenuController.saveSlotButtonObjs[slotNum].SetActive(false);
            }
        }
    }

    public override void Exit()
    {
        base.Exit();
        mainMenuController.mainMenuCanvas.enabled = false;
        FindObjectOfType<AudioManager>().Play("MenuClick");
    }

    void OnStartGameClicked()
    {
        ClearErrorMessages();
        if (selectedSlot != null)
        {
            Debug.Log("Start Button Clicked!");
            mainMenuController.charaScriptableObj.CopyCharacterData(selectedSlot.characterData);
            SceneManager.LoadScene("GameScene", LoadSceneMode.Single);
            FindObjectOfType<AudioManager>().Play("MenuClick");
        }
        else
        {
            Debug.Log("Error - tried to start game without selecting character");
            mainMenuController.startErrorObj.SetActive(true);
        }
    }

    void OnCreateCharClicked()
    {
        ClearErrorMessages();
        if (!CheckIfSlotsFull())
        {
            mainMenuController.ChangeState<CharacterCreationState>();
            FindObjectOfType<AudioManager>().Play("MenuClick");
        }
        else
        {
            Debug.Log("Error: create character when all slots filled");
            mainMenuController.slotsFullErrorObj.SetActive(true);
        }
    }

    bool CheckIfSlotsFull()
    {
        for (int i = 0; i < mainMenuController.saveSlotDataObjs.Count; i++)
        {
            if (!mainMenuController.saveSlotDataObjs[i].containsData)
            {
                return false;
            }
        }
        return true;
    }

    void OnOptionsClicked()
    {
        ClearErrorMessages();
        mainMenuController.ChangeState<OptionsState>();
        FindObjectOfType<AudioManager>().Play("MenuClick");
    }

    void OnSlotClicked(int slotNumber)
    {
        ClearErrorMessages();
        var manager = mainMenuController.displayCharacterObj.GetComponent<ModularCharacterManager>();
        selectedSlot = mainMenuController.saveSlotDataObjs[slotNumber - 1];

        if (selectedSlot != null && selectedSlot.slotNumber == slotNumber)
        {
            ConfigureCharacterDisplay(manager, selectedSlot.characterData);
            mainMenuController.displayCharacterObj.SetActive(true);
            mainMenuController.characterNameObj.SetActive(true);
        }
    }

    void ConfigureCharacterDisplay(ModularCharacterManager manager, CustomCharacter customCharacter)
    {
        manager.SwapGender(customCharacter.gender);

        if (customCharacter.hairId < 0)
        {
            manager.DeactivatePart(ModularBodyPart.Hair);
        }
        else
        {
            manager.ActivatePart(ModularBodyPart.Hair, customCharacter.hairId);
            manager.SetPartColor(ModularBodyPart.Hair, customCharacter.hairId, "_Color_Hair", customCharacter.hairColor);
        }

        if (customCharacter.eyebrowID < 0)
        {
            manager.DeactivatePart(ModularBodyPart.Eyebrow);
        }
        else
        {
            manager.ActivatePart(ModularBodyPart.Eyebrow, customCharacter.eyebrowID);
            manager.SetPartColor(ModularBodyPart.Eyebrow, customCharacter.eyebrowID, "_Color_Hair", customCharacter.eyebrowColor);
        }

        if (customCharacter.facialHairID < 0)
        {
            manager.DeactivatePart(ModularBodyPart.FacialHair);
        }
        else
        {
            manager.ActivatePart(ModularBodyPart.FacialHair, customCharacter.facialHairID);
            manager.SetPartColor(ModularBodyPart.FacialHair, customCharacter.facialHairID, "_Color_Hair", customCharacter.facialHairColor);
        }

        manager.ActivatePart(ModularBodyPart.Head, customCharacter.faceMarkID);
        manager.SetPartColor(ModularBodyPart.Head, customCharacter.faceMarkID, "_Color_BodyArt", customCharacter.facemarkColor);

        manager.ActivatePart(ModularBodyPart.Head, customCharacter.faceMarkID);
        manager.SetPartColor(ModularBodyPart.Head, customCharacter.faceMarkID, "_Color_Eye", customCharacter.eyeColor);

        manager.ActivatePart(ModularBodyPart.Head, customCharacter.faceMarkID);
        manager.SetPartColor(ModularBodyPart.Head, customCharacter.faceMarkID, "_Color_Skin", customCharacter.skinColor);

        mainMenuController.characterNameObj.GetComponent<TextMeshProUGUI>().text = customCharacter.charName;

        mainMenuController.charaScriptableObj.CopyCharacterData(selectedSlot.characterData);
    }

    void OnDeleteCharacterSelected()
    {
        ClearErrorMessages();
        if (selectedSlot == null)
        {
            Debug.Log("Error - must select character to delete first.");
            mainMenuController.deleteCharaErrorObj.SetActive(true);
        }
        else
        {
            mainMenuController.deleteCharacterCanvas.enabled = true;
        }
        SetSlotVisibility();
    }

    void OnYesDeleteClicked()
    {
        Debug.Log("Character deletion confirmed. Deleting character in slot " + selectedSlot.slotNumber);

        mainMenuController.saveSlotDataObjs[selectedSlot.slotNumber - 1].containsData = false;
        selectedSlot = null;
        mainMenuController.displayCharacterObj.SetActive(false);
        mainMenuController.characterNameObj.SetActive(false);

        mainMenuController.deleteCharacterCanvas.enabled = false;

        SetSlotVisibility();
    }

    void OnNoDeleteClicked()
    {
        Debug.Log("Character deletion was cancelled");
        mainMenuController.deleteCharacterCanvas.enabled = false;

        SetSlotVisibility();
    }

    void ClearErrorMessages()
    {
        mainMenuController.startErrorObj.SetActive(false);
        mainMenuController.deleteCharaErrorObj.SetActive(false);
        mainMenuController.slotsFullErrorObj.SetActive(false);
    }

}
