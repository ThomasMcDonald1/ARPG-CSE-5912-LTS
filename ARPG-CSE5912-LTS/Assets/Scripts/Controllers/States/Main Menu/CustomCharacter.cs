using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BattleDrakeStudios.ModularCharacters;

[CreateAssetMenu(fileName = "CustomCharacter", menuName = "ScriptableObjects/CustomCharacter", order = 1)]

public class CustomCharacter : ScriptableObject
{
    public string charName;
    public int hairId, eyebrowID, faceMarkID, facialHairID;
    public Color hairColor, eyebrowColor, facemarkColor, facialHairColor, eyeColor, skinColor;
    public Gender gender;

    private Dictionary<BodyPartNames, string> colorProperties = new Dictionary<BodyPartNames, string>()
        { { BodyPartNames.Hair, "_Color_Hair" },  {BodyPartNames.Eyebrows, "_Color_Hair" },  {BodyPartNames.FaceMark, "_Color_BodyArt" },
        {BodyPartNames.FacialHair, "_Color_Hair" },  {BodyPartNames.Eyes, "_Color_Eye" },  {BodyPartNames.Skin, "_Color_Skin" } };

    public void UpdateIds(int h, int eye, int mark, int faci)
    {
        hairId = h;
        eyebrowID = eye;
        faceMarkID = mark;
        facialHairID = faci;
    }

    public void UpdateColors(Color h, Color eb, Color fm, Color fh, Color e, Color s)
    {
        hairColor = h;
        eyebrowColor = eb;
        facemarkColor = fm;
        facialHairColor = fh;
        eyeColor = e;
        skinColor = s;
    }

    public void UpdateGender(Gender g)
    {
        gender = g;
    }

    public void UpdateName(string n)
    {
        charName = n;
    }

    public void UpdatePlayerModel(GameObject player)
    {
        Debug.Log("Setting up player character");
        var customizer = player.GetComponent<ModularCharacterManager>();
        ActivatePart(customizer, ModularBodyPart.Hair, BodyPartNames.Hair, hairId, hairColor);
        ActivatePart(customizer, ModularBodyPart.Eyebrow, BodyPartNames.Eyebrows, eyebrowID, eyebrowColor);
        ActivatePart(customizer, ModularBodyPart.FacialHair, BodyPartNames.FacialHair, facialHairID, facialHairColor);
        ActivatePart(customizer, ModularBodyPart.Head, BodyPartNames.FaceMark, faceMarkID, facemarkColor);
        ActivatePart(customizer, ModularBodyPart.Head, BodyPartNames.Skin, faceMarkID, skinColor);
        ActivatePart(customizer, ModularBodyPart.Head, BodyPartNames.Eyes, faceMarkID, eyeColor);
        customizer.SwapGender(gender);
    }

    private void ActivatePart(ModularCharacterManager man, ModularBodyPart part, BodyPartNames bP, int partID, Color partColor)
    {
        if (partID < 0)
        {
            man.DeactivatePart(part);
        }
        else
        {
            man.ActivatePart(part, partID);
            man.SetPartColor(part, partID, colorProperties[bP], partColor);
        }
    }

}
