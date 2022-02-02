using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using BattleDrakeStudios.ModularCharacters;

public class CharacterCreationState : BaseMenuState
{
    private CharacterCustomizer characterManager;
    public CustomCharacter customCharacter;

    public override void Enter()
    {
        base.Enter();
        Debug.Log("entered character creation menu state");
        mainMenuController.createCharMenuCanvas.enabled = true;
        mainMenuController.characterCreationCamera.enabled = true;
        if (characterManager ==  null)
        {
            characterManager = new CharacterCustomizer(mainMenuController.customCharacterObj.GetComponent<ModularCharacterManager>());
        }
        customCharacter = mainMenuController.charaScriptableObj;
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

        ChangeEyebrowButton();
        ChangeEyeButton();
        ChangeFacialHairButton();
        ChangeFacialMarkButton();
        ChangeHairButton();
        ChangeSkinButton();
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
            GetCharacterDetails();
            nameError.SetActive(false);
            Debug.Log("Confirm Button Clicked!");
            SceneManager.LoadScene("AbilitySystemTestScene", LoadSceneMode.Single);
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
        ChangeHairButton();
        PlayAudio();
    }

    void ChangeHairButton()
    {
        var colors = hairColorButton.colors;
        colors.normalColor = characterManager.GetPartColor(BodyPartNames.Hair);
        colors.selectedColor = characterManager.GetPartColor(BodyPartNames.Hair);
        hairColorButton.colors = colors;
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
        ChangeEyebrowButton();
        PlayAudio();
    }

    void ChangeEyebrowButton()
    {
        var colors = eyebrowColorButton.colors;
        colors.normalColor = characterManager.GetPartColor(BodyPartNames.Eyebrows);
        colors.selectedColor = characterManager.GetPartColor(BodyPartNames.Eyebrows);
        eyebrowColorButton.colors = colors;
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
        ChangeFacialMarkButton();
    }

    void ChangeFacialMarkButton()
    {
        var colors = faceMarkColorButton.colors;
        colors.normalColor = characterManager.GetPartColor(BodyPartNames.FaceMark);
        colors.selectedColor = characterManager.GetPartColor(BodyPartNames.FaceMark);
        faceMarkColorButton.colors = colors;
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
        ChangeFacialHairButton();
        PlayAudio();        
    }

    void ChangeFacialHairButton()
    {
        var colors = facialHairColorButton.colors;
        colors.normalColor = characterManager.GetPartColor(BodyPartNames.FacialHair);
        colors.selectedColor = characterManager.GetPartColor(BodyPartNames.FacialHair);
        facialHairColorButton.colors = colors;
    }

    void SetEyeColor()
    {
        Debug.Log("Player eye color changed");
        characterManager.SetEyeColor();
        ChangeEyeButton();
        PlayAudio();
    }

    void ChangeEyeButton()
    {
        var colors = eyeColorButton.colors;
        colors.normalColor = characterManager.GetPartColor(BodyPartNames.Eyes);
        colors.selectedColor = characterManager.GetPartColor(BodyPartNames.Eyes);
        eyeColorButton.colors = colors;
    }
   
    void SetSkinColor()
    {
        Debug.Log("Player skin color changed");
        characterManager.SetSkinColor();
        ChangeSkinButton();
        PlayAudio();
    }

    void ChangeSkinButton()
    {
        var colors = skinColorButton.colors;
        colors.normalColor = characterManager.GetPartColor(BodyPartNames.Skin);
        colors.selectedColor = characterManager.GetPartColor(BodyPartNames.Skin);
        skinColorButton.colors = colors;
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

        ChangeEyebrowButton();
        ChangeEyeButton();
        ChangeFacialHairButton();
        ChangeFacialMarkButton();
        ChangeHairButton();
        ChangeSkinButton();
    }

    void PlayAudio()
    {
        FindObjectOfType<AudioManager>().Play("MenuClick");
    }

    void GetCharacterDetails()
    {
        var hairId = characterManager.GetPartID(BodyPartNames.Hair);
        var hairColor = characterManager.GetPartColor(BodyPartNames.Hair);

        var faceMarkID = characterManager.GetPartID(BodyPartNames.FaceMark);
        var facemarkColor = characterManager.GetPartColor(BodyPartNames.FaceMark);

        var facialHairID = characterManager.GetPartID(BodyPartNames.FacialHair);
        var facialHairColor = characterManager.GetPartColor(BodyPartNames.FacialHair);

        var eyebrowID = characterManager.GetPartID(BodyPartNames.Eyebrows);
        var eyebrowColor = characterManager.GetPartColor(BodyPartNames.Eyebrows);

        var eyeColor = characterManager.GetPartColor(BodyPartNames.Eyes);
        var skinColor = characterManager.GetPartColor(BodyPartNames.Skin);

        var gender = characterManager.CharacterGender;
        var charName = characterManager.CharacterName;

        customCharacter.UpdateIds(hairId, eyebrowID, faceMarkID, facialHairID);
        customCharacter.UpdateColors(hairColor, eyebrowColor, facemarkColor, facialHairColor, eyeColor, skinColor);
        customCharacter.UpdateGender(gender);
        customCharacter.UpdateName(charName);
    }

}