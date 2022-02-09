using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class ActionBar : MonoBehaviour
{
    public ActionButton actionButton1;
    public ActionButton actionButton2;
    public ActionButton actionButton3;
    public ActionButton actionButton4;
    public ActionButton actionButton5;
    public ActionButton actionButton6;
    public ActionButton actionButton7;
    public ActionButton actionButton8;
    public ActionButton actionButton9;
    public ActionButton actionButton10;
    public ActionButton actionButton11;
    public ActionButton actionButton12;
    public ActionButton actionButtonLeft;
    public ActionButton actionButtonRight;
    [SerializeField] Ability defaultLeft;
    List<ActionButton> actionButtons;

    void Awake()
    {
        actionButtons = new List<ActionButton> { actionButton1, actionButton2, actionButton3, actionButton4, actionButton5, actionButton6, actionButton7,
        actionButton8, actionButton9, actionButton10, actionButton11, actionButton12, actionButtonLeft, actionButtonRight};
    }

    private void Start()
    {
        SetDefaults();
    }

    void Update()
    {
        foreach (ActionButton actionButton in actionButtons)
        {
            actionButton.LockSlotFromCostDeficit();
        }
    }

    public Ability GetAbilityOnActionButton(ActionButton actionButton)
    {
        return actionButton.abilityAssigned;
    }

    void SetDefaults()
    {
        actionButtonLeft.abilityAssigned = defaultLeft;
        actionButtonLeft.SetIcon();
    }
}
