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
        if (overrider.GetComponent<Animator>().GetBool("CanDualWield") && hasSecondWeapon)
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
        if (overrider.GetComponent<Animator>().GetBool("CanDualWield") && hasSecondWeapon)
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
        if (overrider.GetComponent<Animator>().GetBool("CanDualWield") && hasSecondWeapon)
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
        if (overrider.GetComponent<Animator>().GetBool("CanDualWield") && hasSecondWeapon)
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
        if (overrider.GetComponent<Animator>().GetBool("CanDualWield") && hasSecondWeapon)
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
        if (overrider.GetComponent<Animator>().GetBool("CanDualWield") && !hasSecondWeapon)
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
        if (overrider.GetComponent<Animator>().GetBool("CanDualWield") && hasSecondWeapon)
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
        if (overrider.GetComponent<Animator>().GetBool("CanDualWield") && !hasSecondWeapon)
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
        if (overrider.GetComponent<Animator>().GetBool("CanDualWield") && !hasSecondWeapon)
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
        if (overrider.GetComponent<Animator>().GetBool("CanDualWield") && !hasSecondWeapon)
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
        if (overrider.GetComponent<Animator>().GetBool("CanDualWield") && !hasSecondWeapon)
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
        if (overrider.GetComponent<Animator>().GetBool("CanDualWield") && !hasSecondWeapon)
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
