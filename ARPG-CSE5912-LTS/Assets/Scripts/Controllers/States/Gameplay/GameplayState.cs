using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using ARPG.Core;
public class GameplayState : BaseGameplayState
{
    //[SerializeField] private DialogueUI dialogueUI;
    //public DialogueUI DialogueUI => dialogueUI;
    //public IInteractable Interactable { get; set; }   

    int groundLayer, npcLayer, enemyLayer;
    Player player;
    NavMeshAgent agent;
    Animator animator;
    ContextMenuPanel contextMenuPanel;
    ActionBar actionBar;

    // Test inventory system

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
        animator = player.GetComponent<Animator>();
        contextMenuPanel = gameplayStateController.GetComponentInChildren<ContextMenuPanel>();
        if (contextMenuPanel != null)
        {
            contextMenuPanel.contextMenuPanelCanvas.SetActive(false);
        }
        actionBar = gameplayStateController.GetComponentInChildren<ActionBar>();
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
        SceneManager.LoadScene("MenuCharacterDisplay", LoadSceneMode.Single);
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
            player.GetComponent<PlayerController>().PlayerOnClickEventResponse(e.info.collider.gameObject.layer, sender, e);
        }
    }

    protected override void OnClickCanceled(object sender, InfoEventArgs<RaycastHit> e)
    {
        if (agent.enabled)
        {
            player.GetComponent<PlayerController>().PlayerCancelClickEventResponse(sender, e);
        }
    }

    protected override void OnCancelPressed(object sender, InfoEventArgs<int> e)
    {
        PauseGame();
    }

    protected override void OnSecondaryClickPressed(object sender, InfoEventArgs<int> e)
    {
        
    }

    protected override void OnCharacterMenuPressed(object sender, InfoEventArgs<int> e)
    {

    }

    protected override void OnStationaryButtonPressed(object sender, InfoEventArgs<int> e)
    {

    }

    protected override void OnStationaryButtonCanceled(object sender, InfoEventArgs<bool> e)
    {

    }

    protected override void OnPotion1Pressed(object sender, InfoEventArgs<int> e)
    {

    }

    protected override void OnPotion2Pressed(object sender, InfoEventArgs<int> e)
    {

    }

    protected override void OnPotion3Pressed(object sender, InfoEventArgs<int> e)
    {

    }

    protected override void OnPotion4Pressed(object sender, InfoEventArgs<int> e)
    {

    }

    protected override void OnActionBar1Pressed(object sender, InfoEventArgs<int> e)
    {

    }

    protected override void OnActionBar2Pressed(object sender, InfoEventArgs<int> e)
    {

    }

    protected override void OnActionBar3Pressed(object sender, InfoEventArgs<int> e)
    {

    }

    protected override void OnActionBar4Pressed(object sender, InfoEventArgs<int> e)
    {

    }

    protected override void OnActionBar5Pressed(object sender, InfoEventArgs<int> e)
    {

    }

    protected override void OnActionBar6Pressed(object sender, InfoEventArgs<int> e)
    {

    }

    protected override void OnActionBar7Pressed(object sender, InfoEventArgs<int> e)
    {

    }

    protected override void OnActionBar8Pressed(object sender, InfoEventArgs<int> e)
    {

    }

    protected override void OnActionBar9Pressed(object sender, InfoEventArgs<int> e)
    {

    }

    protected override void OnActionBar10Pressed(object sender, InfoEventArgs<int> e)
    {

    }

    protected override void OnActionBar11Pressed(object sender, InfoEventArgs<int> e)
    {

    }

    protected override void OnActionBar12Pressed(object sender, InfoEventArgs<int> e)
    {

    }

    protected override void OnUIElementLeftClicked(object sender, InfoEventArgs<List<RaycastResult>> e)
    {
        //figure out if the results contain an action button
        foreach (RaycastResult result in e.info)
        {
            GameObject go = result.gameObject;
            ActionButton actionButton = go.GetComponent<ActionButton>();
            if (actionButton != null)
            {
                Debug.Log("Left Clicked: " + go.name);
            }           
        }
    }

    protected override void OnUIElementRightClicked(object sender, InfoEventArgs<List<RaycastResult>> e)
    {
        //figure out if the results contain an action button
        foreach (RaycastResult result in e.info)
        {
            GameObject go = result.gameObject;
            ActionButton actionButton = go.GetComponent<ActionButton>();
            if (actionButton != null)
            {
                Debug.Log("Action Button clicked on: " + actionButton.name);
                contextMenuPanel.transform.position = Mouse.current.position.ReadValue();
                contextMenuPanel.transform.position = new Vector3(contextMenuPanel.transform.position.x, 400, contextMenuPanel.transform.position.z);
                contextMenuPanel.contextMenuPanelCanvas.SetActive(true);
                contextMenuPanel.PopulateContextMenu(actionButton);
            }
        }
    }

    void PauseGame()
    {
        gameplayStateController.ChangeState<PauseGameState>();
    }

    void OpenCharacterPanel()
    {
        gameplayStateController.ChangeState<CharacterPanelState>();
    }

    void GameOver()
    {
        gameplayStateController.ChangeState<GameoverState>();
    }

    void Update()
    {
        //Debug.Log(animator.GetCurrentAnimatorStateInfo(0).normalizedTime);
        if (animator.GetBool("Dead") == true && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 2f && animator.GetCurrentAnimatorStateInfo(0).IsName("Dead"))
        {
            GameOver();
        }
    }
}
