using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using BattleDrakeStudios.ModularCharacters;

public class CharacterCreationState : BaseMenuState
{
    private CharacterCustomizer characterManager;

    public override void Enter()
    {
        base.Enter();
        Debug.Log("entered character creation menu state");
        mainMenuController.createCharMenuCanvas.enabled = true;
        mainMenuController.characterCreationCamera.enabled = true;
        if (characterManager ==  null)
        {
            characterManager = new CharacterCustomizer(mainMenuController.characterObj.GetComponent<ModularCharacterManager>());
        }
        nameError.SetActive(false);
        SetUpButtons();
    }

    void SetUpButtons()
    {
        backFromCharCreateToMainButton.onClick.AddListener(() => OnBackButtonClicked());
        resetCharaButton.onClick.AddListener(() => ResetCharacter());
        confirmButton.onClick.AddListener(() => OnConfirmButtonClicked());
        maleButton.onClick.AddListener(() => SetGenderMale());
        femaleButton.onClick.AddListener(() => SetGenderFemale());
        hairForwardButton.onClick.AddListener(() => SetHairStyle(SelectionDirection.Forward));
        hairBackwardButton.onClick.AddListener(() => SetHairStyle(SelectionDirection.Backward));
        hairColorButton.onClick.AddListener(() => SetHairColor());
        eyebrowForwardButton.onClick.AddListener(() => SetEyebrowsStyle(SelectionDirection.Forward));
        eyebrowBackwardButton.onClick.AddListener(() => SetEyebrowsStyle(SelectionDirection.Backward));
        eyebrowColorButton.onClick.AddListener(() => SetEyebrowsColor());
        faceMarkForwardButton.onClick.AddListener(() => SetFacialMarkStyle(SelectionDirection.Forward));
        faceMarkBackwardButton.onClick.AddListener(() => SetFacialMarkStyle(SelectionDirection.Backward));
        faceMarkColorButton.onClick.AddListener(() => SetFacialMarkColor());
        facialHairForwardButton.onClick.AddListener(() => SetFacialHairStyle(SelectionDirection.Forward));
        facialHairBackwardButton.onClick.AddListener(() => SetFacialHairStyle(SelectionDirection.Backward));
        facialHairColorButton.onClick.AddListener(() => SetFacialHairColor());
        eyeColorButton.onClick.AddListener(() => SetEyeColor());
        skinColorButton.onClick.AddListener(() => SetSkinColor());
        nameField.onEndEdit.AddListener(s => SetName(s));
    }

    public override void Exit()
    {
        base.Exit();
        mainMenuController.createCharMenuCanvas.enabled = false;
        mainMenuController.characterCreationCamera.enabled = false;        
    }

    void OnBackButtonClicked()
    {
        mainMenuController.ChangeState<MainMenuRootState>();
    }

    void OnConfirmButtonClicked()
    {
        if (characterManager.NameIsValid())
        {
            nameError.SetActive(false);
            Debug.Log("Confirm Button Clicked!");
            mainMenuController.characterPrefab = mainMenuController.characterObj;
            SceneManager.LoadScene("GameScene", LoadSceneMode.Single);
        }
        else
        {
            nameError.SetActive(true);
        }
        PlayAudio();
    }

    void SetGenderMale()
    {
        characterManager.SetGender(Gender.Male);
        Debug.Log("Player gender set to Male");
        PlayAudio();
    }

    void SetGenderFemale()
    {
        characterManager.SetGender(Gender.Female);
        Debug.Log("Player gender set to Female");
        PlayAudio();
    }

    void SetHairStyle(SelectionDirection d)
    {
        characterManager.SetHairStyle(d);
        Debug.Log("Player hair style changed");
        PlayAudio();
    }

    void SetHairColor()
    {
        Debug.Log("Player hair color changed");
        characterManager.SetHairColor();
        PlayAudio();
    }

    void SetEyebrowsStyle(SelectionDirection d)
    {
        Debug.Log("Player eyebrow style changed");
        characterManager.SetEyebrowStyle(d);
        PlayAudio();
    }

    void SetEyebrowsColor()
    {
        Debug.Log("Player eyebrow color changed");
        characterManager.SetEyebrowColor();
        PlayAudio();
    }

    void SetFacialMarkStyle(SelectionDirection d)
    {
        Debug.Log("Player face mark color changed");
        characterManager.SetFaceMarkStyle(d);
        PlayAudio();
    }

    void SetFacialMarkColor()
    {
        Debug.Log("Player face mark color changed");
        characterManager.SetFaceMarkColor();
        PlayAudio();
    }

    void SetFacialHairStyle(SelectionDirection d)
    {
        Debug.Log("Player facial hair style changed");
        characterManager.SetFacialHairStyle(d);
        PlayAudio();
    }

    void SetFacialHairColor()
    {
        Debug.Log("Player facial hair color changed");
        characterManager.SetFacialHairColor();
        PlayAudio();
    }

    void SetEyeColor()
    {
        Debug.Log("Player eye color changed");
        characterManager.SetEyeColor();
        PlayAudio();
    }
   
    void SetSkinColor()
    {
        Debug.Log("Player skin color changed");
        characterManager.SetSkinColor();
        PlayAudio();
    }

    void SetName(string n)
    {
        Debug.Log("Player name changed to " + n);
        characterManager.SetCharacterName(n);
    }

    void ResetCharacter()
    {
        Debug.Log("Player character reset");
        characterManager.ResetParts();
    }

    void PlayAudio()
    {
        FindObjectOfType<AudioManager>().Play("MenuClick");
    }

}