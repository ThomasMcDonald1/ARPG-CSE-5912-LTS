using UnityEngine;
using BattleDrakeStudios.ModularCharacters;

using System.Linq;

public class EquipmentManager : MonoBehaviour {
    #region Singleton
    public static EquipmentManager instance;
    #endregion

    [SerializeField] private Item[] equipmentSlots;

    private ModularCharacterManager characterManager;

    private void Awake() {
        characterManager = GetComponent<ModularCharacterManager>();
        instance = this;
    }

    private void Start() {
        foreach (var item in equipmentSlots) {
            EquipItem(item);
        }
    }

    public void EquipItem(Item itemToEquip) {

        foreach (var part in itemToEquip.modularArmor.armorParts) {
            if (part.partID > -1) {
                Debug.Log("part.body Type is " + part.bodyType);
                characterManager.ActivatePart(part.bodyType, part.partID);
                if (part.bodyType.Equals(ModularBodyPart.Helmet))
                {
                   characterManager.DeactivatePart(ModularBodyPart.Hair);
                    characterManager.DeactivatePart(ModularBodyPart.FacialHair);
                }
                ColorPropertyLinker[] armorColors = itemToEquip.modularArmor.armorColors;
                for (int i = 0; i < armorColors.Length; i++) {
                    characterManager.SetPartColor(part.bodyType, part.partID, armorColors[i].property, armorColors[i].color);
                }
            } else {
                characterManager.DeactivatePart(part.bodyType);
            }
        }
    }

    public void UnequipItem(Item itemToEquip, CustomCharacter character)
    {
        foreach (var part in itemToEquip.modularArmor.armorParts)
        {
            if (part.partID > -1)
            {
                Debug.Log("part.body Type is " + part.bodyType);
                characterManager.DeactivatePart(part.bodyType);
                if (part.bodyType.Equals(ModularBodyPart.Helmet))
                {
                    characterManager.ActivatePart(ModularBodyPart.FacialHair, character.facialHairID);
                   // characterManager.SetPartColor(part.bodyType, character.facialHairID, "_Color_Hair", character.facialHairColor);

                    characterManager.ActivatePart(ModularBodyPart.Hair, character.hairId);
                    //characterManager.SetPartColor(part.bodyType, character.hairId, "_Color_Hair", character.hairColor);

                }


            }
            else
            {
                characterManager.DeactivatePart(part.bodyType);
            }
        }
    }
}
