using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipManager : MonoBehaviour
{
    #region Singleton
    public static EquipManager instance;
    public CustomCharacter character;
    public Stats playerStats;

    //bool mainHand = false;
    bool offHand = false;
    bool twoHandSword = false;
    bool mainHand = false;

    GameObject rightHand;
    GameObject leftHand;
    [SerializeField] AnimatorOverrider overrider;
    [SerializeField] SetAnimationType animController;
    GameObject[] nu = new GameObject[6];

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
        playerStats = this.GetComponentInChildren<Stats>();
    }

    public void Equip(Equipment newItem)
    {
        int slotIndex = (int)newItem.equipSlot;
        Equipment oldItem = null;
        if (currentEquipment[slotIndex] != null)
        {
            oldItem = currentEquipment[slotIndex];
            //switch (oldItem.type)
            //{
            //    case Ite.ItemType.armor:
            //        Debug.Log("I am in the armor type case");
            //        Inventory.instance.Add(oldItem, Inventory.instance.armorItems);
            //        break;
            //    case Ite.ItemType.weapon:
            //        Inventory.instance.Add(oldItem, Inventory.instance.weaponItems);
            //        break;

            //}
            Unequip(slotIndex);
            //if (nu[slotIndex] != null)
            //{
            //    Destroy(nu[slotIndex]);
            //}
        }
        currentEquipment[slotIndex] = newItem;
        if (newItem.type == Ite.ItemType.weapon)
        {
            nu[slotIndex] = (GameObject)Instantiate(newItem.prefab);
            Destroy(nu[slotIndex].GetComponent<LootLabels.DroppedGear>());
            Destroy(nu[slotIndex].GetComponent<LootLabels.CreateLabel>());
            Destroy(nu[slotIndex].GetComponent<LootLabels.ObjectHighlight>());
            nu[slotIndex].transform.localScale = new Vector3(1, 1, 1);

            if (newItem.equipSlot == EquipmentSlot.OffHand && newItem.name.Contains("Shield"))
            {
                if (twoHandSword)
                    Unequip(0);

                ShieldEquipment shield = (ShieldEquipment)newItem;
                nu[slotIndex].transform.parent = leftHand.transform;
                nu[slotIndex].transform.localPosition = new Vector3(0, 0, 0);
                nu[slotIndex].transform.localRotation = Quaternion.Euler(0, 0, 0);
                playerStats[StatTypes.Armor] += shield.armor;
                playerStats[StatTypes.BlockChance] += shield.blockChance;

                ChangeToMainHandAnimation();

                offHand = true;
            }
            else if (newItem.equipSlot == EquipmentSlot.OffHand)
            {
                if (twoHandSword)
                    Unequip(0);
                WeaponEquipment weapon = (WeaponEquipment)newItem;
                nu[slotIndex].transform.parent = leftHand.transform;
                nu[slotIndex].transform.localPosition = new Vector3(0, 0, 0);
                nu[slotIndex].transform.localRotation = Quaternion.Euler(0, 0, 0);
                playerStats[StatTypes.AttackRange] += weapon.attackRange;
                playerStats[StatTypes.AtkSpeed] += weapon.attackSpeed;
                playerStats[StatTypes.CritChance] += weapon.critChance;
                switch (weapon.typeOfWeapon)
                {
                    case WeaponEquipment.weaponType.dagger:

                        if (currentEquipment[0] != null)
                        {
                            if (currentEquipment[0].name.Contains("Dagger"))
                            {
                                animController.ChangeToDualDaggers();
                            }
                            else if (currentEquipment[0].name.Contains("Sword"))
                            {
                                animController.ChangeToSwordLeftDaggerRight();
                            }
                        }
                        break;
                    case WeaponEquipment.weaponType.righthandsword:
                        if (currentEquipment[0] != null)
                        {
                            if (currentEquipment[0].name.Contains("Dagger"))
                            {
                                animController.ChangeToDaggerLeftSwordRight();
                            }
                            else if (currentEquipment[0].name.Contains("Sword"))
                            {
                                animController.ChangeToDualSwords();
                            }
                        }
                        break;
                }

                offHand = true;
            }
            else
            {
                WeaponEquipment weapon = (WeaponEquipment)newItem;


                // nu.GetComponent<LootLabels.CreateLabel>().enabled = false;
                // nu.GetComponent<LootLabels.ObjectHighlight>().enabled = false;
                switch (weapon.typeOfWeapon)
                {
                    case WeaponEquipment.weaponType.twohandsword:
                        twoHandSword = true;

                        if (offHand)
                            Unequip(1);
                        animController.ChangeToTwoHandedSword();
                        nu[slotIndex].transform.SetParent(rightHand.transform);
                        nu[slotIndex].transform.localPosition = Vector3.zero;
                        nu[slotIndex].transform.localRotation = Quaternion.Euler(0, 0, 0);
                        //playerStat = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Stats>();
                        //UseHealingPotion();
                        break;

                    case WeaponEquipment.weaponType.righthandsword:

                        animController.ChangeToOnlySwordRight();
                        nu[slotIndex].transform.SetParent(rightHand.transform);
                        nu[slotIndex].transform.localPosition = Vector3.zero;
                        nu[slotIndex].transform.localRotation = Quaternion.Euler(0, 0, 0);
                        mainHand = true;
                        if (currentEquipment[1] != null)
                        {
                            if (currentEquipment[1].name.Contains("Dagger"))
                            {
                                animController.ChangeToDaggerLeftSwordRight();
                            }
                            else if (currentEquipment[1].name.Contains("Sword"))
                            {
                                animController.ChangeToDualSwords();
                            }
                        }
                        //playerStat = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Stats>();
                        //UseEnergyPotion();
                        break;

                    case WeaponEquipment.weaponType.dagger:
                        animController.ChangeToOnlyDaggerRight();
                        nu[slotIndex].transform.SetParent(rightHand.transform);
                        nu[slotIndex].transform.localPosition = Vector3.zero;
                        nu[slotIndex].transform.localRotation = Quaternion.Euler(0, 0, 0);

                        if (currentEquipment[1] != null)
                        {
                            if (currentEquipment[1].name.Contains("Dagger"))
                            {
                                animController.ChangeToDualDaggers();
                            }
                            else if (currentEquipment[1].name.Contains("Sword"))
                            {
                                animController.ChangeToSwordLeftDaggerRight();
                            }
                        }
                        break;
                    default:
                        Debug.Log("Don't know what this weapon does");
                        break;
                }
                playerStats[StatTypes.AttackRange] += weapon.attackRange;
                playerStats[StatTypes.AtkSpeed] += weapon.attackSpeed;
                playerStats[StatTypes.CritChance] += weapon.critChance;
            }
        }
        else if (newItem is ArmorEquipment armor && !(newItem.equipSlot == EquipmentSlot.Jewelry))
        {
            playerStats[StatTypes.Armor] += armor.Armor;
            playerStats[StatTypes.Evasion] += armor.Evasion;
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

        foreach (GameObject featureGO in newItem.suffix.FeaturesGOs)
        {
            if (featureGO != null)
            {
                Feature feature = featureGO.GetComponent<Feature>();
                feature.Activate(gameObject);
            }
        }

        // Equipment has been removed so we trigger the callback
        if (onEquipmentChanged != null)
            onEquipmentChanged.Invoke(null, oldItem);
    }

    public void Unequip(int slotIndex)
    {
        Debug.Log("Button click registered.");
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
                    if (!(oldItem.equipSlot == EquipmentSlot.Jewelry))
                    {
                        ArmorEquipment armor = (ArmorEquipment)oldItem;
                        playerStats[StatTypes.Armor] -= armor.Armor;
                        playerStats[StatTypes.Evasion] -= armor.Evasion;
                    }
                    break;
                case Ite.ItemType.weapon:
                    Inventory.instance.Add(oldItem, Inventory.instance.weaponItems);
                    if (nu[slotIndex] != null)
                    {
                        //nu.GetComponent<Animator>().enabled = true;
                        //nu.GetComponent<LootLabels.CreateLabel>().enabled = true;
                        //nu.GetComponent<LootLabels.ObjectHighlight>().enabled = true;
                        Destroy(nu[slotIndex]);
                    }
                    if (oldItem.equipSlot == EquipmentSlot.OffHand && oldItem.name.Contains("Shield"))
                    {
                        ShieldEquipment shield = (ShieldEquipment)oldItem;
                        playerStats[StatTypes.BlockChance] -= shield.blockChance;
                        playerStats[StatTypes.Armor] -= shield.armor;
                        offHand = false;

                    }
                    else if (oldItem.equipSlot == EquipmentSlot.OffHand)
                    {
                        WeaponEquipment weapon = (WeaponEquipment)oldItem;
                        weapon.equipSlot = EquipmentSlot.MainHand;
                        ChangeToMainHandAnimation();
                        playerStats[StatTypes.AttackRange] -= weapon.attackRange;
                        playerStats[StatTypes.AtkSpeed] -= weapon.attackSpeed;
                        playerStats[StatTypes.CritChance] -= weapon.critChance;
                        offHand = false;
                    }
                    else
                    {
                        WeaponEquipment weapon = (WeaponEquipment)oldItem;
                        animController.ChangeToUnarmed();
                        weapon.equipSlot = EquipmentSlot.MainHand;
                        playerStats[StatTypes.AttackRange] -= weapon.attackRange;
                        playerStats[StatTypes.AtkSpeed] -= weapon.attackSpeed;
                        playerStats[StatTypes.CritChance] -= weapon.critChance;

                        if (weapon.typeOfWeapon == WeaponEquipment.weaponType.twohandsword)
                        {
                            twoHandSword = false;
                        }
                        else
                        {
                            mainHand = false;
                        }

                    }
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

            foreach (GameObject featureGO in oldItem.suffix.FeaturesGOs)
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

    private void ChangeToMainHandAnimation()
    {
        if (currentEquipment[0] is WeaponEquipment weapon)
        {
            switch (weapon.typeOfWeapon)
            {
                case WeaponEquipment.weaponType.dagger:
                    animController.ChangeToOnlyDaggerRight();
                    break;
                case WeaponEquipment.weaponType.righthandsword:
                    animController.ChangeToOnlySwordRight();
                    break;
                default:
                    break;
            }
        }
    }
}