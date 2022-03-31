using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveTreeState : BaseGameplayState
{
    private PassiveSkills passiveSkills;
    public override void Enter()
    {
        base.Enter();
        Debug.Log("entered passive tree state");
        Time.timeScale = 0;
        gameplayStateController.passiveTreeUI.SetActive(true);
        confirmPassiveTreeButton.onClick.AddListener(() => OnConfirmButtonClicked());
        closePassiveTreeButton.onClick.AddListener(() => OnCloseButtonClicked());
        passiveSkills = gameplayStateController.passiveTreeUI.GetComponentInChildren<PassiveTreeUI>().passiveSkills;
    }

    // Update is called once per frame
    public override void Exit()
    {
        base.Exit();
        Time.timeScale = 1;
        gameplayStateController.passiveTreeUI.SetActive(false);
    }
    void OnCloseButtonClicked()
    {
        passiveSkills.ResetVisualCloseButton();
        gameplayStateController.ChangeState<GameplayState>();
    }
    void OnConfirmButtonClicked()
    {
        passiveSkills.UnlockPassives();
    }
}
