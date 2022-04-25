using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetAnimationType : MonoBehaviour
{
    [SerializeField] private AnimatorOverrideController TwoHandedSwordOverrider;
    [SerializeField] private AnimatorOverrideController BowOverrider;
    [SerializeField] private AnimatorOverrideController StaffOverrider;

    [SerializeField] private AnimatorOverrideController SwordRightOnlyOverrider;
    [SerializeField] private AnimatorOverrideController SwordLeftOnlyOverrider;

    [SerializeField] private AnimatorOverrideController DaggerRightOnlyOverrider;
    [SerializeField] private AnimatorOverrideController DaggerLeftOnlyOverrider;

    [SerializeField] private AnimatorOverrideController SwordLeftDaggerRightOverrider;
    [SerializeField] private AnimatorOverrideController DaggerLeftSwordRightOverrider;

    [SerializeField] private AnimatorOverrideController DualSwordOverrider;
    [SerializeField] private AnimatorOverrideController DualDaggerOverrider;

    [SerializeField] private AnimatorOverrideController UnarmedOverrider;

    [SerializeField] private AnimatorOverrideController SummonOverrider;
    [SerializeField] private AnimatorOverrideController MageOverrider;

    [SerializeField] private AnimatorOverrider overrider;

    private const float DoubleAttackSpeed = 2.0f;
    private const float NormalAttackSpeed = 1.0f;
    private bool hasSecondWeapon = false;

    public void ChangeToUnarmed()
    {
        if (hasSecondWeapon)
        {

            GetComponent<Animator>().SetFloat("AttackSpeed", GetComponent<Animator>().GetFloat("AttackSpeed") / DoubleAttackSpeed);
            hasSecondWeapon = false;
        }
        overrider.GetComponent<Animator>().SetBool("CanDualWield", true);
        overrider.GetComponent<Animator>().SetBool("AttackingMainHand", true);
        overrider.SetAnimations(UnarmedOverrider);
    }

    public void ChangeToTwoHandedSword()
    {
        if (hasSecondWeapon)
        {
            GetComponent<Animator>().SetFloat("AttackSpeed", GetComponent<Animator>().GetFloat("AttackSpeed") / DoubleAttackSpeed);
            hasSecondWeapon = false;
        }
        overrider.GetComponent<Animator>().SetBool("CanDualWield", false);
        overrider.GetComponent<Animator>().SetBool("AttackingMainHand", true);
        overrider.SetAnimations(TwoHandedSwordOverrider);
    }

    public void ChangeToBow()
    {
        if (hasSecondWeapon)
        {
            GetComponent<Animator>().SetFloat("AttackSpeed", GetComponent<Animator>().GetFloat("AttackSpeed") / DoubleAttackSpeed);
            hasSecondWeapon = false;
        }
        overrider.GetComponent<Animator>().SetBool("CanDualWield", false);
        overrider.GetComponent<Animator>().SetBool("AttackingMainHand", true);
        overrider.SetAnimations(BowOverrider);
    }

    public void ChangeToStaff()
    {
        if (hasSecondWeapon)
        {
            GetComponent<Animator>().SetFloat("AttackSpeed", GetComponent<Animator>().GetFloat("AttackSpeed") / DoubleAttackSpeed);
            hasSecondWeapon = false;
        }
        overrider.GetComponent<Animator>().SetBool("CanDualWield", false);
        overrider.GetComponent<Animator>().SetBool("AttackingMainHand", true);
        overrider.SetAnimations(StaffOverrider);
    }

    public void ChangeToOnlySwordRight()
    {
        if (hasSecondWeapon)
        {
            GetComponent<Animator>().SetFloat("AttackSpeed", GetComponent<Animator>().GetFloat("AttackSpeed") / DoubleAttackSpeed);
            hasSecondWeapon = false;
        }
        overrider.GetComponent<Animator>().SetBool("CanDualWield", false);
        overrider.GetComponent<Animator>().SetBool("AttackingMainHand", true);
        overrider.SetAnimations(SwordRightOnlyOverrider);
    }

    public void ChangeToOnlySwordLeft()
    {
        if (!hasSecondWeapon)
        {
            GetComponent<Animator>().SetFloat("AttackSpeed", GetComponent<Animator>().GetFloat("AttackSpeed") * DoubleAttackSpeed);
            hasSecondWeapon = true;
        }
        overrider.GetComponent<Animator>().SetBool("CanDualWield", true);
        overrider.GetComponent<Animator>().SetBool("AttackingMainHand", true);
        overrider.SetAnimations(SwordLeftOnlyOverrider);
    }

    public void ChangeToOnlyDaggerRight()
    {
        if (hasSecondWeapon)
        {
            GetComponent<Animator>().SetFloat("AttackSpeed", GetComponent<Animator>().GetFloat("AttackSpeed") / DoubleAttackSpeed);
            hasSecondWeapon = false;
        }
        overrider.GetComponent<Animator>().SetBool("CanDualWield", false);
        overrider.GetComponent<Animator>().SetBool("AttackingMainHand", true);
        overrider.SetAnimations(DaggerRightOnlyOverrider);
    }

    public void ChangeToOnlyDaggerLeft()
    {
        if (!hasSecondWeapon)
        {
            GetComponent<Animator>().SetFloat("AttackSpeed", GetComponent<Animator>().GetFloat("AttackSpeed") * DoubleAttackSpeed);
            hasSecondWeapon = true;
        }
        overrider.GetComponent<Animator>().SetBool("CanDualWield", true);
        overrider.GetComponent<Animator>().SetBool("AttackingMainHand", true);
        overrider.SetAnimations(DaggerLeftOnlyOverrider);
    }

    public void ChangeToDaggerLeftSwordRight()
    {
        if (!hasSecondWeapon)
        {
            GetComponent<Animator>().SetFloat("AttackSpeed", GetComponent<Animator>().GetFloat("AttackSpeed") * DoubleAttackSpeed);
            hasSecondWeapon = true;
        }
        overrider.GetComponent<Animator>().SetBool("CanDualWield", true);
        overrider.GetComponent<Animator>().SetBool("AttackingMainHand", true);
        overrider.SetAnimations(DaggerLeftSwordRightOverrider);
    }

    public void ChangeToSwordLeftDaggerRight()
    {
        if (!hasSecondWeapon)
        {
            GetComponent<Animator>().SetFloat("AttackSpeed", GetComponent<Animator>().GetFloat("AttackSpeed") * DoubleAttackSpeed);
            hasSecondWeapon = true;
        }
        overrider.GetComponent<Animator>().SetBool("CanDualWield", true);
        overrider.GetComponent<Animator>().SetBool("AttackingMainHand", true);
        overrider.SetAnimations(SwordLeftDaggerRightOverrider);
    }

    public void ChangeToDualSwords()
    {
        if (!hasSecondWeapon)
        {
            GetComponent<Animator>().SetFloat("AttackSpeed", GetComponent<Animator>().GetFloat("AttackSpeed") * DoubleAttackSpeed);
            hasSecondWeapon = true;
        }
        overrider.GetComponent<Animator>().SetBool("CanDualWield", true);
        overrider.GetComponent<Animator>().SetBool("AttackingMainHand", true);
        overrider.SetAnimations(DualSwordOverrider);
    }

    public void ChangeToDualDaggers()
    {
        if (!hasSecondWeapon)
        {
            GetComponent<Animator>().SetFloat("AttackSpeed", GetComponent<Animator>().GetFloat("AttackSpeed") * DoubleAttackSpeed);
            hasSecondWeapon = true;
        }
        overrider.GetComponent<Animator>().SetBool("CanDualWield", true);
        overrider.GetComponent<Animator>().SetBool("AttackingMainHand", true);
        overrider.SetAnimations(DualDaggerOverrider);
    }

    public void ChangeToSummon()
    {
        overrider.GetComponent<Animator>().SetBool("CanDualWield", false);
        overrider.GetComponent<Animator>().SetBool("AttackingMainHand", true);

        overrider.SetAnimations(SummonOverrider);

    }
    public void ChangeToMage()
    {
        overrider.GetComponent<Animator>().SetBool("CanDualWield", false);
        overrider.GetComponent<Animator>().SetBool("AttackingMainHand", true);

        overrider.SetAnimations(MageOverrider);

    }
}
