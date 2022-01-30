using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BattleDrakeStudios.ModularCharacters;

public enum SelectionDirection { Forward, Backward }
public enum BodyPartNames { Hair, Eyes, Skin, Eyebrows, FaceMark, FacialHair}

public class CharacterCustomizer //: ScriptableObject
{
    private class NonBodyPart
    {
        private Color[] colors;
        private int currColor;
        private string colorProperty;
        public Color PartColor { get { return colors[currColor]; } }
        public string ColorProperty { get { return colorProperty; } }

        public NonBodyPart(string cP, Color[] c)
        {
            colors = c;
            colorProperty = cP;
            currColor = 0;
        }
        public void Reset()
        {
            currColor = 0;
        }
        public void NextColor()
        {
            currColor++;
            if (currColor >= colors.Length) { currColor = 0; }
        }

    }

    private class BodyPart
    {
        private int minIndex;
        private int maxIndex;
        private int initialIndex;
        private int currentIndex;
        private ModularBodyPart type;
        private Color[] colors;
        private int currColor;
        private string colorProperty;

        public int IDvalue { get { return currentIndex; } }
        public ModularBodyPart PartType { get { return type; } }
        public Color PartColor { get { return colors[currColor]; } }
        public string ColorProperty { get { return colorProperty; } }

        public BodyPart(int min, int max, int initial, ModularBodyPart t, Color[] c, string colorProperty)
        {
            minIndex = min;
            maxIndex = max;
            currentIndex = initial;
            initialIndex = initial;
            type = t;
            colors = c;
            currColor = 0;
            this.colorProperty = colorProperty;
        }

        public void Reset()
        {
            currentIndex = initialIndex;
            currColor = 0;
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

        public void NextColor()
        {
            currColor++;
            if (currColor >= colors.Length) { currColor = 0; }
        }

    }

    private ModularCharacterManager character;
    private BodyPart hair, eyebrow, faceMark, facialHair;
    private NonBodyPart eyes, skin;
    private string charName;

    public string CharacterName { get { return charName; } }

    public CharacterCustomizer(ModularCharacterManager character)
    {
        this.character = character;
        this.character.SwapGender(Gender.Male);
        this.charName = "";
        InitializeParts();
    }

    private void InitializeParts()
    {
        Color[] colors = new Color[] { (ColorConverter(118, 13, 17, 121)), (ColorConverter(219, 170, 43, 121)), (ColorConverter(65, 31, 14, 121)), (ColorConverter(22, 22, 22, 121)), (ColorConverter(6, 133, 181, 121)) };

        hair = new BodyPart(-1, 37, 0, ModularBodyPart.Hair, colors, "_Color_Hair");
        ActivateBodyPart(hair);

        eyebrow = new BodyPart(-1, 6, -1, ModularBodyPart.Eyebrow, colors, "_Color_Hair");
        ActivateBodyPart(eyebrow);

        facialHair = new BodyPart(-1, 17, -1, ModularBodyPart.FacialHair, colors, "_Color_Hair");
        ActivateBodyPart(facialHair);

        colors = new Color[] { (ColorConverter(162, 100, 195, 225)), (ColorConverter(231, 42, 0, 225)), (ColorConverter(3, 87, 13, 225)), (ColorConverter(229, 229, 229, 225))};

        faceMark = new BodyPart(0, 22, 0, ModularBodyPart.Head, colors, "_Color_BodyArt");
        ActivateBodyPart(faceMark);

        colors = new Color[] { (ColorConverter(236, 197, 175, 225)), (ColorConverter(183, 116, 77, 225)), (ColorConverter(111, 63, 37, 225)) };
        skin = new NonBodyPart("_Color_Skin", colors);

        colors = new Color[] { (ColorConverter(0, 0, 0, 225)), (ColorConverter(32, 128, 162, 255)), (ColorConverter(43, 162, 30, 255)), (ColorConverter(123, 57, 14, 255)) };
        eyes = new NonBodyPart("_Color_Eyes", colors);
    }

    private Color ColorConverter(float r, float g, float b, float o)
    {
        return new Color(r / 255.0f, g / 255.0f, b / 255.0f, o/1f);
    }

    private void ActivateBodyPart(BodyPart bp)
    {
        if (bp.IDvalue < 0)
        {
            character.DeactivatePart(bp.PartType);
        }
        else
        {
            UpdateBodyPartColor(bp);
            character.ActivatePart(bp.PartType, bp.IDvalue);
        }
    }

    private void UpdateBodyPartColor(BodyPart bp)
    {
        if (bp.IDvalue >= 0)
        { 
            character.SetPartColor(bp.PartType, bp.IDvalue, bp.ColorProperty, bp.PartColor);
        }
    }

    public Color GetPartColor(BodyPartNames bp)
    {
        switch(bp)
        {
            case BodyPartNames.Eyebrows:
                return eyebrow.PartColor;
            case BodyPartNames.Eyes:
                return eyes.PartColor;
            case BodyPartNames.Hair:
                return hair.PartColor;
            case BodyPartNames.FaceMark:
                return faceMark.PartColor;
            case BodyPartNames.FacialHair:
                return facialHair.PartColor;
            default:
                return skin.PartColor;
        }
    }

    public void ResetParts()
    {
        hair.Reset();
        ActivateBodyPart(hair);
        SetHairColor();

        eyebrow.Reset();
        ActivateBodyPart(eyebrow);

        faceMark.Reset();
        SetFaceMarkColor();
        ActivateBodyPart(faceMark);

        facialHair.Reset();
        ActivateBodyPart(facialHair);

        eyes.Reset();
        SetEyeColor();

        skin.Reset();
        SetSkinColor();
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

    public void SetHairColor()
    {
        hair.NextColor();
        UpdateBodyPartColor(hair);
    }

    public void SetFaceMarkColor()
    {
        faceMark.NextColor();
        UpdateBodyPartColor(faceMark);
    }

    public void SetFacialHairColor()
    {
        facialHair.NextColor();
        UpdateBodyPartColor(facialHair);
    }

    public void SetEyebrowColor()
    {
        eyebrow.NextColor();
        UpdateBodyPartColor(eyebrow);
    }

    public void SetEyeColor()
    {
        eyes.NextColor();
        character.SetPartColor(faceMark.PartType, faceMark.IDvalue, eyes.ColorProperty, eyes.PartColor);
    }

    public void SetSkinColor()
    {
        skin.NextColor();
        character.SetPartColor(faceMark.PartType, faceMark.IDvalue, skin.ColorProperty, skin.PartColor);
    }

    public void SetCharacterName(string n)
    {
        if (!(string.IsNullOrEmpty(n) || string.IsNullOrWhiteSpace(n)))
        {
            charName = n;
        }
    }

    public bool NameIsValid()
    {
        if (string.IsNullOrEmpty(charName) || string.IsNullOrWhiteSpace(charName))
        {
            return false;
        }
        else
        {
            return true;
        }
    }

}




