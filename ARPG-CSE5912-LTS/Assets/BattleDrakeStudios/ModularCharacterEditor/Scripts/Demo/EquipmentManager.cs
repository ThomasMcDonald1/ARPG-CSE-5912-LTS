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
                characterManager.DeactivatePart(part.bodyType);
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
                Debug.Log("part.bodyType is " + part.bodyType);
                switch (part.bodyType)
                {
                    case (ModularBodyPart.BackAttachment):
                        characterManager.ActivatePart(part.bodyType, character.backAttach);
                        break;
                    case (ModularBodyPart.ShoulderAttachmentLeft):
                        characterManager.ActivatePart(part.bodyType, character.shoulderAttach);
                        break;
                    case (ModularBodyPart.ShoulderAttachmentRight):
                        characterManager.ActivatePart(part.bodyType, character.shoulderAttach);
                        break;
                    case (ModularBodyPart.ArmUpperLeft):
                        characterManager.ActivatePart(part.bodyType, character.armUpper);
                        break;
                    case (ModularBodyPart.ArmUpperRight):
                        characterManager.ActivatePart(part.bodyType, character.armUpper);
                        break;
                    case (ModularBodyPart.ElbowAttachmentLeft):
                        characterManager.ActivatePart(part.bodyType, character.elbowAttach);
                        break;
                    case (ModularBodyPart.ElbowAttachmentRight):
                        characterManager.ActivatePart(part.bodyType, character.elbowAttach);
                        break;
                    case (ModularBodyPart.ArmLowerLeft):
                        characterManager.ActivatePart(part.bodyType, character.armLower);
                        break;
                    case (ModularBodyPart.ArmLowerRight):
                        characterManager.ActivatePart(part.bodyType, character.armLower);
                        break;
                    case (ModularBodyPart.HandLeft):
                        characterManager.ActivatePart(part.bodyType, character.hands);
                        break;
                    case (ModularBodyPart.HandRight):
                        characterManager.ActivatePart(part.bodyType, character.hands);
                        break;
                    case (ModularBodyPart.HipsAttachment):
                        characterManager.ActivatePart(part.bodyType, character.hipsAttach);
                        break;
                    case (ModularBodyPart.Hips):
                        characterManager.ActivatePart(part.bodyType, character.hips);
                        break;
                    case (ModularBodyPart.KneeAttachmentLeft):
                        characterManager.ActivatePart(part.bodyType, character.kneeAttach);
                        break;
                    case (ModularBodyPart.KneeAttachmentRight):
                        characterManager.ActivatePart(part.bodyType, character.kneeAttach);
                        break;
                    case (ModularBodyPart.LegLeft):
                        characterManager.ActivatePart(part.bodyType, character.legs);
                        break;
                    case (ModularBodyPart.LegRight):
                        characterManager.ActivatePart(part.bodyType, character.legs);
                        break;
                    case (ModularBodyPart.Torso):
                        Debug.Log("I am in torso and torso id is " + character.torso);
                        characterManager.ActivatePart(part.bodyType, character.torso);
                        break;
                    default:
                        return;

                }


            }
            else
            {
                characterManager.DeactivatePart(part.bodyType);
                
                //characterManager.ActivatePart(part.bodyType, character.;
            }
        }
    }
}
