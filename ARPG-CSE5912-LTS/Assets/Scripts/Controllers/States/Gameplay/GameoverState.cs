using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using ARPG.Core;
using UnityEngine.AI;

public class GameoverState : BaseGameplayState
{
    Player player;
    NavMeshAgent agent;
    PlayerController playerController;
    Stats stats;
    Animator animator;
    Transform playerTransform;
    InteractionManager interactionManager;
    LevelController levelController;
    public static event EventHandler<InfoEventArgs<(int, int)>> SaveExpEvent; //Saved exp
    //public static event EventHandler<InfoEventArgs<(int, int)>> GetBackExpEvent; //Get exp back
    //public static event EventHandler<InfoEventArgs<(int, int)>> TemporaryExpLossEvent;
    public static event EventHandler<InfoEventArgs<(int, int)>> EmptyExpEvent; //Empty the saved exp

    public override void Enter()
    {
        SaveExpEvent?.Invoke(this, new InfoEventArgs<(int, int)>((0, 0)));
        base.Enter();
        
        Debug.Log("Game over");
        
        Time.timeScale = 0;
        gameplayStateController.gameoverCanvas.enabled = true;
        player = GetComponentInChildren<Player>();
        agent = player.GetComponent<NavMeshAgent>();
        stats = player.GetComponent<Stats>();
        animator = player.GetComponent<Animator>();
        playerTransform = player.GetComponent<Transform>();
        playerController = player.GetComponent<PlayerController>();
        interactionManager = GetComponentInChildren<InteractionManager>();
        levelController = GetComponentInChildren<LevelController>();
        yesRespawnButton.onClick.AddListener(() => OnYesButtonClicked());
        noRespawnButton.onClick.AddListener(() => OnNoButtonClicked());
        agent.isStopped = true;
    }

    public override void Exit()
    {
        base.Exit();
        gameplayStateController.gameoverCanvas.enabled = false;
        Time.timeScale = 1;
        yesRespawnButton.onClick.RemoveAllListeners();
        noRespawnButton.onClick.RemoveAllListeners();
    }

    void OnYesButtonClicked()
    {
        int deductedExp = CalculateExpLoss(stats);
        Tombstone.Instance.HoldTempExpLoss(deductedExp);
        stats[StatTypes.EXP] -= deductedExp;
        Tombstone.Instance.RememberPlayerDeathPosition(playerTransform.position);
        Tombstone.Instance.RememberDungeonNumOfPlayerDeath(playerController.DungeonNum); //Should I remember this or something else?
        interactionManager.EnterTown();
        agent.isStopped = false;
        //playerTransform.position = new Vector3(0f, 1.5f, 0f);//could add some other position later(like savepoint??)
        stats[StatTypes.HP] = stats[StatTypes.MaxHP];
        animator.SetBool("Dead", false);
        FindObjectOfType<AudioManager>().Play("MenuClick");
        gameplayStateController.ChangeState<GameplayState>();
        //GetBackExpEvent?.Invoke(this, new InfoEventArgs<(int, int)>((0, 0)));
        //Debug.Log(playerController.player.AttackTarget);
    }

    void OnNoButtonClicked()
    {
        EmptyExpEvent?.Invoke(this, new InfoEventArgs<(int, int)>((0, 0)));
        FindObjectOfType<AudioManager>().Play("MenuClick");
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }

    private int CalculateExpLoss(Stats stats)
    {
        int currentLevelMinExp = LevelController.currentLevelExp(levelController.EXP, levelController.LVL);
        int maxLevelExp = LevelController.currentLevelExp(levelController.EXP, levelController.LVL) + LevelController.currentLevelExpToNext(levelController.LVL);
        float expToLose = (maxLevelExp - currentLevelMinExp) * 0.3f;
        expToLose = Mathf.Clamp(expToLose, currentLevelMinExp, maxLevelExp);
        return Mathf.CeilToInt(expToLose);
    }
}
