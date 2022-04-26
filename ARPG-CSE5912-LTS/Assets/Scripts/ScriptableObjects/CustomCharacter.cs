using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BattleDrakeStudios.ModularCharacters;
using System;

[CreateAssetMenu(fileName = "CustomCharacter", menuName = "ScriptableObjects/CustomCharacter", order = 1)]

public class CustomCharacter : ScriptableObject
{

    private void OnEnable()
    {
        hideFlags = HideFlags.DontUnloadUnusedAsset;
    }

    public int slotNum;
    public string charName;
    public int hairId, eyebrowID, faceMarkID, facialHairID;
    public Color hairColor, eyebrowColor, facemarkColor, facialHairColor, eyeColor, skinColor;
    public Gender gender;

    //THE FOLLOWING ARE ALL FOR DEFAULT OUTFIT PURPOSES
    public int backAttach, shoulderAttach, armUpper, elbowAttach, armLower, hands, hipsAttach, hips, kneeAttach, legs, torso;

    private Dictionary<BodyPartNames, string> colorProperties = new Dictionary<BodyPartNames, string>()
        { { BodyPartNames.Hair, "_Color_Hair" },  {BodyPartNames.Eyebrows, "_Color_Hair" },  {BodyPartNames.FaceMark, "_Color_BodyArt" },
        {BodyPartNames.FacialHair, "_Color_Hair" },  {BodyPartNames.Eyes, "_Color_Eye" },  {BodyPartNames.Skin, "_Color_Skin" } };

    public void SetDefaultOutfit(List<int> outfitIds)
    {
        if (outfitIds.Count == 10)
        {
            this.backAttach = outfitIds[0];
            this.shoulderAttach = outfitIds[1];
            this.armUpper = outfitIds[2];
            this.elbowAttach = outfitIds[3];
            this.armLower = outfitIds[4];
            this.hands = outfitIds[5];
            this.hipsAttach = outfitIds[6];
            this.hips = outfitIds[7];
            this.kneeAttach = outfitIds[8];
            this.legs = outfitIds[9];
            this.torso = outfitIds[10];
        }
        else
        {
            this.backAttach = 1;
            this.shoulderAttach = 1;
            this.armUpper = 1;
            this.elbowAttach = 1;
            this.armLower = 1;
            this.hands = 1;
            this.hipsAttach = 1;
            this.hips = 1;
            this.kneeAttach = 1;
            this.legs = 1;
            this.torso = 1;
        }
    }

    public void UpdateOutfit(int backAttach, int shoulderAttach, int armUpper, int elbowAttach, int armLower, int hands, int hipsAttach, int hips, int kneeAttach, int legs, int torso)
    {
        if(backAttach > -1)
            this.backAttach = backAttach;
        if(shoulderAttach > -1)
            this.shoulderAttach = shoulderAttach;
        if(armUpper > -1)
            this.armUpper = armUpper;
        if(elbowAttach > -1)
            this.elbowAttach = elbowAttach;
        if(armLower > -1)
            this.armLower = armLower;
        if(hands > -1)
            this.hands = hands;
        if(hipsAttach > -1)
            this.hipsAttach = hipsAttach;
        if(hips > -1)
            this.hips = hips;
        if(kneeAttach > -1)
            this.kneeAttach = kneeAttach;
        if(legs > -1)
            this.legs = legs;
        if(torso > -1)
            this.torso = torso;
    }

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
        customizer.SwapGender(gender);
        ActivatePart(customizer, ModularBodyPart.Hair, BodyPartNames.Hair, hairId, hairColor);
        ActivatePart(customizer, ModularBodyPart.Eyebrow, BodyPartNames.Eyebrows, eyebrowID, eyebrowColor);
        ActivatePart(customizer, ModularBodyPart.FacialHair, BodyPartNames.FacialHair, facialHairID, facialHairColor);
        ActivatePart(customizer, ModularBodyPart.Head, BodyPartNames.FaceMark, faceMarkID, facemarkColor);
        ActivatePart(customizer, ModularBodyPart.Head, BodyPartNames.Skin, faceMarkID, skinColor);
        ActivatePart(customizer, ModularBodyPart.Head, BodyPartNames.Eyes, faceMarkID, eyeColor);
        ChangeToPlayerDefaultOutfit(customizer);
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

    public void ChangeToPlayerDefaultOutfit(ModularCharacterManager customizer)
    {
        customizer.ActivatePart(ModularBodyPart.BackAttachment, backAttach);
        customizer.ActivatePart(ModularBodyPart.ShoulderAttachmentLeft, shoulderAttach);
        customizer.ActivatePart(ModularBodyPart.ShoulderAttachmentRight, shoulderAttach);
        customizer.ActivatePart(ModularBodyPart.ArmUpperLeft, armUpper);
        customizer.ActivatePart(ModularBodyPart.ArmUpperRight, armUpper);
        customizer.ActivatePart(ModularBodyPart.ElbowAttachmentLeft, elbowAttach);
        customizer.ActivatePart(ModularBodyPart.ElbowAttachmentRight, elbowAttach);
        customizer.ActivatePart(ModularBodyPart.ArmLowerLeft, armLower);
        customizer.ActivatePart(ModularBodyPart.ArmLowerRight, armLower);
        customizer.ActivatePart(ModularBodyPart.HandLeft, hands);
        customizer.ActivatePart(ModularBodyPart.HandRight, hands);
        customizer.ActivatePart(ModularBodyPart.HipsAttachment, hipsAttach);
        customizer.ActivatePart(ModularBodyPart.Hips, hips);
        customizer.ActivatePart(ModularBodyPart.KneeAttachmentLeft, kneeAttach);
        customizer.ActivatePart(ModularBodyPart.KneeAttachmentRight, kneeAttach);
        customizer.ActivatePart(ModularBodyPart.LegLeft, legs);
        customizer.ActivatePart(ModularBodyPart.LegRight, legs);
        customizer.ActivatePart(ModularBodyPart.Torso, torso);
    }


    public void CopyCharacterData(CustomCharacter toCopy)
    {
        UpdateGender(toCopy.gender);
        UpdateIds(toCopy.hairId, toCopy.eyebrowID, toCopy.faceMarkID, toCopy.facialHairID);
        UpdateColors(toCopy.hairColor, toCopy.eyebrowColor, toCopy.facemarkColor, toCopy.facialHairColor, toCopy.eyeColor, toCopy.skinColor);
        UpdateName(toCopy.charName);
        UpdateOutfit(toCopy.backAttach, toCopy.shoulderAttach, toCopy.armUpper, toCopy.elbowAttach, toCopy.armLower, toCopy.hands, toCopy.hipsAttach, toCopy.hips, toCopy.kneeAttach, toCopy.legs, toCopy.torso);
    }

}
