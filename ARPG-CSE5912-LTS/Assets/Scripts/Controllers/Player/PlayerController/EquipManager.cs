using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipManager : MonoBehaviour
{
    #region Singleton
    public static EquipManager instance;
    public CustomCharacter character;

    GameObject rightHand;
    GameObject leftHand;
    [SerializeField] AnimatorOverrider overrider;
    [SerializeField] SetAnimationType animController;
    GameObject nu;

    public delegate void OnEquipmentChanged(Equipment newItem, Equipment oldItem);
    public event OnEquipmentChanged onEquipmentChanged;
    private void Awake()
    {
        leftHand = GameObject.Find("EquippedItemLeft");
        rightHand = GameObject.Find("EquippedItemRight");
        instance = this;
        character = GameObject.Find("GameplayController").GetComponent<GameplayStateController>().customCharacter;
        Debug.Log("character is " + character);
    }
    #endregion
    public Equipment[] currentEquipment;
    Inventory inventory;
    private void Start()
    {
        inventory = Inventory.instance;
        int numSlots = System.Enum.GetNames(typeof(EquipmentSlot)).Length;
        Debug.Log("numSlot is " + numSlots);
        currentEquipment = new Equipment[numSlots];
    }

    public void Equip(Equipment newItem)
    {
        int slotIndex = (int)newItem.equipSlot;
        Equipment oldItem = null;
        if (currentEquipment[slotIndex] != null)
        {
            oldItem = currentEquipment[slotIndex];
            switch (oldItem.type)
            {
                case Ite.ItemType.armor:
                    Debug.Log("I am in the armor type case");
                    Inventory.instance.Add(oldItem, Inventory.instance.armorItems);
                    break;
                case Ite.ItemType.weapon:
                    Inventory.instance.Add(oldItem, Inventory.instance.weaponItems);
                    break;

            }
        }
        currentEquipment[slotIndex] = newItem;
        if(newItem.type == Ite.ItemType.weapon)
        {
            WeaponEquipment weapon = (WeaponEquipment)newItem;
            nu = (GameObject)Instantiate(weapon.prefab);
            nu.transform.parent = rightHand.transform;
            nu.transform.localPosition = new Vector3(0, 0, 0);
            nu.transform.localRotation = Quaternion.Euler(0, 0, 0);
            switch (weapon.typeOfWeapon)
            {
                case WeaponEquipment.weaponType.twohandsword:
                    animController.ChangeToTwoHandedSword();
                    //playerStat = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Stats>();
                    //UseHealingPotion();
                    break;
                case WeaponEquipment.weaponType.righthandsword:

                    animController.ChangeToOnlySwordRight();
                    //playerStat = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Stats>();
                    //UseEnergyPotion();
                    break;
                case WeaponEquipment.weaponType.lefthandsword:
                    animController.ChangeToOnlySwordLeft();
                    nu.transform.parent = leftHand.transform;
                    nu.transform.localPosition = new Vector3(0, 0, 0);
                    nu.transform.localRotation = Quaternion.Euler(0, 0, 0);
                    break;
                default:
                    Debug.Log("Don't know what this weapon does");
                    break;
            }
        }

        // Activate the equipment's features
        foreach (GameObject featureGO in newItem.prefix.FeaturesGOs)
        {
            Feature feature = featureGO.GetComponent<Feature>();
            Debug.Log(feature.name);
            feature.Activate(gameObject);
        }

        // Equipment has been removed so we trigger the callback
        if (onEquipmentChanged != null)
            onEquipmentChanged.Invoke(null, oldItem);
    }

    public void Unequip(int slotIndex)
    {
        if (currentEquipment[slotIndex] != null)
        {
            Equipment oldItem = currentEquipment[slotIndex];
            GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Stats>()[StatTypes.PHYATK] -= oldItem.damage;
            GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Stats>()[StatTypes.AttackRange] -= oldItem.attackRange;
            switch (oldItem.type)
            {
                case Ite.ItemType.armor:
                    Inventory.instance.Add(oldItem, Inventory.instance.armorItems);
                    EquipmentManager.instance.UnequipItem(oldItem.equipment, character);
                    break;
                case Ite.ItemType.weapon:
                    Inventory.instance.Add(oldItem, Inventory.instance.weaponItems);
                    if (nu != null)
                    {
                        Destroy(nu);
                    }
                    animController.ChangeToUnarmed();
                    break;

            }

            currentEquipment[slotIndex] = null;

            // Dectivate the equipment's features
            foreach (GameObject featureGO in oldItem.prefix.FeaturesGOs)
            {
                Feature feature = featureGO.GetComponent<Feature>();
                feature.Deactivate();
            }

            // Equipment has been removed so we trigger the callback
            if (onEquipmentChanged != null)
                onEquipmentChanged.Invoke(null, oldItem);

        }


    }
}