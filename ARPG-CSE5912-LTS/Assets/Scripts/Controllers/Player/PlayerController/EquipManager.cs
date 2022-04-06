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
            if (nu != null)
            {
                Destroy(nu);
            }
        }
        currentEquipment[slotIndex] = newItem;
        if(newItem.type == Ite.ItemType.weapon)
        {
            nu = (GameObject)Instantiate(newItem.prefab);
            nu.GetComponent<Animator>().enabled = false;
            Destroy(nu.GetComponent<LootLabels.DroppedGear>());
            Destroy(nu.GetComponent<LootLabels.CreateLabel>());
            Destroy(nu.GetComponent<LootLabels.ObjectHighlight>());
            if (newItem.equipSlot == EquipmentSlot.OffHand)
            {
                nu.transform.parent = leftHand.transform;
                nu.transform.localPosition = new Vector3(0, 0, 0);
                nu.transform.localRotation = Quaternion.Euler(0, 0, 0);
            }
            else
            {
                WeaponEquipment weapon = (WeaponEquipment)newItem;


                // nu.GetComponent<LootLabels.CreateLabel>().enabled = false;
                // nu.GetComponent<LootLabels.ObjectHighlight>().enabled = false;
                nu.transform.SetParent(rightHand.transform);
                nu.transform.localPosition = Vector3.zero;
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
                    case WeaponEquipment.weaponType.dagger:
                        animController.ChangeToOnlyDaggerRight();
                        break;
                    default:
                        Debug.Log("Don't know what this weapon does");
                        break;
                }
            }
        }

        Debug.Log("newItem is " + newItem);
        Debug.Log("prefix is " + newItem.prefix);
        Debug.Log("FeaturesGo is " + newItem.prefix.FeaturesGOs);
        // Activate the equipment's features
        foreach (GameObject featureGO in newItem.prefix.FeaturesGOs)
        {
            if (featureGO != null)
            {
                Feature feature = featureGO.GetComponent<Feature>();
                Debug.Log(feature.name);
                feature.Activate(gameObject);
            }
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
            //NOTE: Weapons don't add to PHYATK stat. Instead, PHYATK will come from putting points in it via the Passive Tree and/or by leveling up
            //      Instead, the BasicAttackDamageAbilityEffect will read the weapon's minimum and maximum damage numbers and make calculations with them
            //GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Stats>()[StatTypes.PHYATK] -= oldItem.damage;
            //GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Stats>()[StatTypes.AttackRange] -= oldItem.attackRange;
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
                        //nu.GetComponent<Animator>().enabled = true;
                        //nu.GetComponent<LootLabels.CreateLabel>().enabled = true;
                        //nu.GetComponent<LootLabels.ObjectHighlight>().enabled = true;
                        Destroy(nu);
                    }
                    animController.ChangeToUnarmed();
                    break;

            }

            currentEquipment[slotIndex] = null;

            // Dectivate the equipment's features
            foreach (GameObject featureGO in oldItem.prefix.FeaturesGOs)
            {
                if (featureGO != null)
                {
                    Feature feature = featureGO.GetComponent<Feature>();
                    feature.Deactivate();
                }
            }

            // Equipment has been removed so we trigger the callback
            if (onEquipmentChanged != null)
                onEquipmentChanged.Invoke(null, oldItem);

        }


    }
}