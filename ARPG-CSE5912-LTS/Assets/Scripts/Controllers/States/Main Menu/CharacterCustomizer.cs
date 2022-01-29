using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BattleDrakeStudios.ModularCharacters;

public enum SelectionDirection { Forward, Backward }

public class CharacterCustomizer
{
    //helper class to store character details
    private class BodyPart
    {
        private int minIndex;
        private int maxIndex;
        private int initialIndex;
        private int currentIndex;
        private ModularBodyPart type;

        public int IDvalue { get { return currentIndex; } }
        public ModularBodyPart PartType { get { return type; } }

        public BodyPart(int min, int max, int initial, ModularBodyPart t)
        {
            minIndex = min;
            maxIndex = max;
            currentIndex = initial;
            initialIndex = initial;
            type = t;
        }

        public void Reset()
        {
            currentIndex = initialIndex;
        }

        public void IncrementIndex()
        {
            currentIndex++;
            if (currentIndex > maxIndex) currentIndex = minIndex;
        }

        public void DecrementIndex()
        {
            currentIndex--;
            if (currentIndex < minIndex) currentIndex = maxIndex;
        }

    }

    private ModularCharacterManager character;
    private BodyPart hair, eyebrow, faceMark, facialHair, eyes, skin;
    private string name;

    public string CharacterName { get { return name; } }

    public CharacterCustomizer(ModularCharacterManager character)
    {
        this.character = character;
        this.character.SwapGender(Gender.Male);
        this.name = "";
        InitializeParts();
    }

    private void InitializeParts()
    {
        hair = new BodyPart(-1, 37, 0, ModularBodyPart.Hair);
        ActivateBodyPart(hair);

        eyebrow = new BodyPart(-1, 6, -1, ModularBodyPart.Eyebrow);
        ActivateBodyPart(eyebrow);

        faceMark = new BodyPart(0, 22, 0, ModularBodyPart.Head);
        ActivateBodyPart(faceMark);

        facialHair = new BodyPart(-1, 17, -1, ModularBodyPart.FacialHair);
        ActivateBodyPart(facialHair);
    }

    private void ActivateBodyPart(BodyPart bp)
    {
        if (bp.IDvalue < 0)
        {
            character.DeactivatePart(bp.PartType);
        }
        else
        {
            character.ActivatePart(bp.PartType, bp.IDvalue);
        }
    }

    public void ResetParts()
    {
        hair.Reset();
        ActivateBodyPart(hair);

        eyebrow.Reset();
        ActivateBodyPart(eyebrow);

        faceMark.Reset();
        ActivateBodyPart(faceMark);

        facialHair.Reset();
        ActivateBodyPart(facialHair);
    }


    public void SetGender(Gender g)
    {
        character.SwapGender(g);
    }

    public void SetHairStyle(SelectionDirection d)
    {
        if (d == SelectionDirection.Forward)
        {
            hair.IncrementIndex();
        }
        else
        {
            hair.DecrementIndex();
        }

        ActivateBodyPart(hair);
    }

    public void SetHairColor()
    {
        character.SetPartColor(hair.PartType, hair.IDvalue, "Red", Color.red);
    }

    public void SetEyebrowStyle(SelectionDirection d)
    {
        if (d == SelectionDirection.Forward)
        {
            eyebrow.IncrementIndex();
        }
        else
        {
            eyebrow.DecrementIndex();
        }

        ActivateBodyPart(eyebrow);
    }

    public void SetEyebrowColor()
    {

    }

    public void SetFaceMarkStyle(SelectionDirection d)
    {
        if (d == SelectionDirection.Forward)
        {
            faceMark.IncrementIndex();
        }
        else
        {
            faceMark.DecrementIndex();
        }

        ActivateBodyPart(faceMark);
    }

    public void SetFaceMarkColor()
    {

    }

    public void SetFacialHairStyle(SelectionDirection d)
    {
        if (d == SelectionDirection.Forward)
        {
            facialHair.IncrementIndex();
        }
        else
        {
            facialHair.DecrementIndex();
        }

        ActivateBodyPart(facialHair);
    }

    public void SetFacialHairColor()
    {

    }

    public void SetEyeColor()
    {

    }
    public void SetSkinColor()
    {

    }

    public void SetCharacterName(string n)
    {
        if (!(string.IsNullOrEmpty(n) || string.IsNullOrWhiteSpace(n)))
        {
            name = n;
        }
    }

    public bool NameIsValid()
    {
        if (string.IsNullOrEmpty(name) || string.IsNullOrWhiteSpace(name))
        {
            return false;
        }
        else
        {
            return true;
        }
    }

}




