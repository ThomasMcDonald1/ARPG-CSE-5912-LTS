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
    Stats statScript;
    Animator animator;
    Transform transform;
    public static event EventHandler<InfoEventArgs<(int, int)>> SaveExpEvent; //Saved exp
    public static event EventHandler<InfoEventArgs<(int, int)>> GetBackExpEvent; //Get exp back
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
        statScript = player.GetComponent<Stats>();
        animator = player.GetComponent<Animator>();
        transform = player.GetComponent<Transform>();
        playerController = player.GetComponent<PlayerController>();
        yesRespawnButton.onClick.AddListener(() => OnYesButtonClicked());
        noRespawnButton.onClick.AddListener(() => OnNoButtonClicked());
    }

    public override void Exit()
    {
        base.Exit();
        gameplayStateController.gameoverCanvas.enabled = false;
        Time.timeScale = 1;
    }

    void OnYesButtonClicked()
    {
        agent.isStopped = true;
        transform.position = new Vector3(0f, 1.5f, 0f);//could add some other position later(like savepoint??)
        statScript[StatTypes.HP] = statScript[StatTypes.MaxHP];
        animator.SetBool("Dead", false);
        gameplayStateController.ChangeState<GameplayState>();
        GetBackExpEvent?.Invoke(this, new InfoEventArgs<(int, int)>((0, 0)));
        Debug.Log(playerController.player.AttackTarget);
        FindObjectOfType<AudioManager>().Play("MenuClick");
    }

    void OnNoButtonClicked()
    {
        EmptyExpEvent?.Invoke(this, new InfoEventArgs<(int, int)>((0, 0)));
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
        FindObjectOfType<AudioManager>().Play("MenuClick");
    }
}
