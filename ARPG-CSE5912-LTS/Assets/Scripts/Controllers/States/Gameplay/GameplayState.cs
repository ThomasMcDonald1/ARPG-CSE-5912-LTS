using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameplayState : BaseGameplayState
{
    //[SerializeField] private DialogueUI dialogueUI;
    //public DialogueUI DialogueUI => dialogueUI;
    //public IInteractable Interactable { get; set; }

    int groundLayer, npcLayer, enemyLayer;
    Player player;
    NavMeshAgent agent;

    public override void Enter()
    {
        base.Enter();
        Debug.Log("entered GameplayState");
        gameplayStateController.gameplayUICanvas.enabled = true;
        pauseMenuButton.onClick.AddListener(() => OnPauseMenuClicked());
        exitToMainMenuButton.onClick.AddListener(() => OnExitToMenuClicked());
        exitGameButton.onClick.AddListener(() => OnExitGameClicked());
        charaPanelButton.onClick.AddListener(() => OnCharaPanelClicked());
        groundLayer = LayerMask.NameToLayer("Walkable");
        npcLayer = LayerMask.NameToLayer("NPC");
        enemyLayer = LayerMask.NameToLayer("Enemy");
        player = GetComponentInChildren<Player>();
        agent = player.GetComponent<NavMeshAgent>();
    }

    public override void Exit()
    {
        base.Exit();
        // Can remove this line to keep gameplay HUD visible while game is paused.
        gameplayStateController.gameplayUICanvas.enabled = false;
    }

    void OnExitToMenuClicked()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
        FindObjectOfType<AudioManager>().Play("MenuClick");
    }

    void OnExitGameClicked()
    {
        Application.Quit();
    }

    void OnPauseMenuClicked()
    {
        PauseGame();
        FindObjectOfType<AudioManager>().Play("MenuClick");
    }

    void OnCharaPanelClicked()
    {
        OpenCharacterPanel();
        FindObjectOfType<AudioManager>().Play("MenuClick");
    }

    protected override void OnClick(object sender, InfoEventArgs<RaycastHit> e)
    {
        if (agent.enabled)
        {
            if (e.info.collider.gameObject.layer == groundLayer)
            {
                agent.destination = e.info.point;
            }
            //else if (e.info.collider.gameObject.layer == npcLayer)
            //{
            //    Debug.Log("Clicked on npc");
            //    if (player.Interactable != null)
            //    {
            //        //Interact with NPC stuff goes here
            //        player.Interactable.Interact(player);
            //    }
            //    agent.destination = e.info.point;



            //}
            else if (e.info.collider.gameObject.layer == enemyLayer)
            {
                //fight enemy
                Debug.Log("Clicked on enmey");
                if (player.Interactable != null)
                {
                    //Interact with NPC stuff goes here
                    player.Interactable.Interact(player);
                }
                agent.destination = e.info.point;
            }
        }
    }

    protected override void OnClickCanceled(object sender, InfoEventArgs<bool> e)
    {

    }

    protected override void OnCancelPressed(object sender, InfoEventArgs<int> e)
    {
        PauseGame();
    }

    void PauseGame()
    {
        gameplayStateController.ChangeState<PauseGameState>();
    }

    void OpenCharacterPanel()
    {
        gameplayStateController.ChangeState<CharacterPanelState>();
    }
}
