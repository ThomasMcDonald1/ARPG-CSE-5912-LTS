using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveTreeState : BaseGameplayState
{
    // Start is called before the first frame update
    public override void Enter()
    {
        base.Enter();
        Debug.Log("entered passive tree state");
        Time.timeScale = 0;
        gameplayStateController.passiveTreeUI.SetActive(true);
        confirmPassiveTreeButton.onClick.AddListener(() => OnConfirmButtonClicked());
    }

    // Update is called once per frame
    public override void Exit()
    {
        base.Exit();
        Time.timeScale = 1;
        gameplayStateController.passiveTreeUI.SetActive(false);
    }
    void OnConfirmButtonClicked()
    {
        gameplayStateController.ChangeState<GameplayState>();
    }
}
