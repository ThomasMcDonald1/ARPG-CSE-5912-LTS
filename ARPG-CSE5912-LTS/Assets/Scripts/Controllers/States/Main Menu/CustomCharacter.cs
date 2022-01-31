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

}
