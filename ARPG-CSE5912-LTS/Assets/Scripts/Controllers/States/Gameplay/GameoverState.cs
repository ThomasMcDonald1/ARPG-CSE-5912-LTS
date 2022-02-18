using System.Collections;
using System.Collections.Generic;
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

    public override void Enter()
    {
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
        Debug.Log(playerController.player.AttackTarget);
        FindObjectOfType<AudioManager>().Play("MenuClick");
    }

    void OnNoButtonClicked()
    {
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
        FindObjectOfType<AudioManager>().Play("MenuClick");
    }
}
