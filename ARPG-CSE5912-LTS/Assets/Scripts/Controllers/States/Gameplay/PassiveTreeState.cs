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
        foreach (Sound s in FindObjectOfType<AudioManager>().sounds)
        {
            if (!s.name.Contains("BGM")) s.source.Stop();
        }
        Time.timeScale = 0;
        gameplayStateController.passiveTreeUI.SetActive(true);
        SetupButtonListeners();
        passiveSkills = gameplayStateController.passiveTreeUI.GetComponentInChildren<PassiveTreeUI>().passiveSkills;
    }

    void SetupButtonListeners()
    {
        confirmPassiveTreeButton.onClick.AddListener(() => OnConfirmButtonClicked());
        closePassiveTreeButton.onClick.AddListener(() => OnCloseButtonClicked());
    }
    void RemoveButtonListners()
    {
        confirmPassiveTreeButton.onClick.RemoveAllListeners();
        closePassiveTreeButton.onClick.RemoveAllListeners();
    }
    // Update is called once per frame
    public override void Exit()
    {
        base.Exit();
        Time.timeScale = 1;
        gameplayStateController.passiveTreeUI.SetActive(false);
        RemoveButtonListners();
    }
    void OnCloseButtonClicked()
    {
        FindObjectOfType<AudioManager>().Play("MenuClick");
        passiveSkills.ResetVisualCloseButton();
        gameplayStateController.ChangeState<GameplayState>();
    }
    void OnConfirmButtonClicked()
    {
        FindObjectOfType<AudioManager>().Play("MenuClick");
        passiveSkills.UnlockPassives();
    }
}
