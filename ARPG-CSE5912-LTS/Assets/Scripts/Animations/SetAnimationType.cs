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

    public void ChangeToUnarmed()
    {
        overrider.GetComponent<Animator>().SetBool("CanDualWield", true);
        overrider.GetComponent<Animator>().SetBool("AttackingMainHand", true);
        overrider.SetAnimations(UnarmedOverrider);
    }

    public void ChangeToTwoHandedSword()
    {
        overrider.GetComponent<Animator>().SetBool("CanDualWield", false);
        overrider.GetComponent<Animator>().SetBool("AttackingMainHand", true);
        overrider.SetAnimations(TwoHandedSwordOverrider);
    }

    public void ChangeToBow()
    {
        overrider.GetComponent<Animator>().SetBool("CanDualWield", false);
        overrider.GetComponent<Animator>().SetBool("AttackingMainHand", true);
        overrider.SetAnimations(BowOverrider);
    }

    public void ChangeToStaff()
    {
        overrider.GetComponent<Animator>().SetBool("CanDualWield", false);
        overrider.GetComponent<Animator>().SetBool("AttackingMainHand", true);
        overrider.SetAnimations(StaffOverrider);
    }

    public void ChangeToOnlySwordRight()
    {
        overrider.GetComponent<Animator>().SetBool("CanDualWield", false);
        overrider.GetComponent<Animator>().SetBool("AttackingMainHand", true);
        overrider.SetAnimations(SwordRightOnlyOverrider);
    }

    public void ChangeToOnlySwordLeft()
    {
        overrider.GetComponent<Animator>().SetBool("CanDualWield", true);
        overrider.GetComponent<Animator>().SetBool("AttackingMainHand", true);
        overrider.SetAnimations(SwordLeftOnlyOverrider);
    }

    public void ChangeToOnlyDaggerRight()
    {
        overrider.GetComponent<Animator>().SetBool("CanDualWield", false);
        overrider.GetComponent<Animator>().SetBool("AttackingMainHand", true);
        overrider.SetAnimations(DaggerRightOnlyOverrider);
    }

    public void ChangeToOnlyDaggerLeft()
    {
        overrider.GetComponent<Animator>().SetBool("CanDualWield", true);
        overrider.GetComponent<Animator>().SetBool("AttackingMainHand", true);
        overrider.SetAnimations(DaggerLeftOnlyOverrider);
    }

    public void ChangeToDaggerLeftSwordRight()
    {
        overrider.GetComponent<Animator>().SetBool("CanDualWield", true);
        overrider.GetComponent<Animator>().SetBool("AttackingMainHand", true);
        overrider.SetAnimations(DaggerLeftSwordRightOverrider);
    }

    public void ChangeToSwordLeftDaggerRight()
    {
        overrider.GetComponent<Animator>().SetBool("CanDualWield", true);
        overrider.GetComponent<Animator>().SetBool("AttackingMainHand", true);
        overrider.SetAnimations(SwordLeftDaggerRightOverrider);
    }

    public void ChangeToDualSwords()
    {
        overrider.GetComponent<Animator>().SetBool("CanDualWield", true);
        overrider.GetComponent<Animator>().SetBool("AttackingMainHand", true);
        overrider.SetAnimations(DualSwordOverrider);
    }

    public void ChangeToDualDaggers()
    {
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
