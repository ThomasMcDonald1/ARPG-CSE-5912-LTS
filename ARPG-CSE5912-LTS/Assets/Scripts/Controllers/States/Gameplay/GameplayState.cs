using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class GameplayState : BaseGameplayState
{
    //[SerializeField] private DialogueUI dialogueUI;
    //public DialogueUI DialogueUI => dialogueUI;
    //public IInteractable Interactable { get; set; }

    int groundLayer, npcLayer, enemyLayer;
    Player player;
    ContextMenuPanel contextMenuPanel;
    ActionBar actionBar;

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
        if (player.agent.enabled)
        {
            if (e.info.collider.gameObject.layer == groundLayer && !player.playerInAOEAbilityTargetSelectionMode)
            {
                player.MoveToLocation(e.info.point);
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
                //Debug.Log("Clicked on enmey");
                if (player.Interactable != null)
                {
                    //Interact with NPC stuff goes here
                    player.Interactable.Interact(player);
                }
                player.MoveToLocation(e.info.point);
            }
        }
    }

<<<<<<< Updated upstream
    protected override void OnClickCanceled(object sender, InfoEventArgs<RaycastHit> e)
    {
        if (player.playerInAOEAbilityTargetSelectionMode)
        {
            player.playerInAOEAbilityTargetSelectionMode = false;
        }
        if (player.playerNeedsToReleaseMouseButton)
        {
            player.playerNeedsToReleaseMouseButton = false;
        }
=======
    protected override void OnClickCanceled(object sender, InfoEventArgs<RaycastHit> r)
    {
   
>>>>>>> Stashed changes
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
        Ability abilityInSlot = actionBar.GetAbilityOnActionButton(actionBar.actionButton1);
        if (abilityInSlot != null)
        {
            player.playerNeedsToReleaseMouseButton = false;
            player.QueueAbilityCast(abilityInSlot);
        }
    }

    protected override void OnActionBar2Pressed(object sender, InfoEventArgs<int> e)
    {
        Ability abilityInSlot = actionBar.GetAbilityOnActionButton(actionBar.actionButton2);
        if (abilityInSlot != null)
        {
            player.playerNeedsToReleaseMouseButton = false;
            player.QueueAbilityCast(abilityInSlot);
        }
    }

    protected override void OnActionBar3Pressed(object sender, InfoEventArgs<int> e)
    {
        Ability abilityInSlot = actionBar.GetAbilityOnActionButton(actionBar.actionButton3);
        if (abilityInSlot != null)
        {
            player.playerNeedsToReleaseMouseButton = false;
            player.QueueAbilityCast(abilityInSlot);
        }
    }

    protected override void OnActionBar4Pressed(object sender, InfoEventArgs<int> e)
    {
        Ability abilityInSlot = actionBar.GetAbilityOnActionButton(actionBar.actionButton4);
        if (abilityInSlot != null)
        {
            player.playerNeedsToReleaseMouseButton = false;
            player.QueueAbilityCast(abilityInSlot);
        }
    }

    protected override void OnActionBar5Pressed(object sender, InfoEventArgs<int> e)
    {
        Ability abilityInSlot = actionBar.GetAbilityOnActionButton(actionBar.actionButton5);
        if (abilityInSlot != null)
        {
            player.playerNeedsToReleaseMouseButton = false;
            player.QueueAbilityCast(abilityInSlot);
        }
    }

    protected override void OnActionBar6Pressed(object sender, InfoEventArgs<int> e)
    {
        Ability abilityInSlot = actionBar.GetAbilityOnActionButton(actionBar.actionButton6);
        if (abilityInSlot != null)
        {
            player.playerNeedsToReleaseMouseButton = false;
            player.QueueAbilityCast(abilityInSlot);
        }
    }

    protected override void OnActionBar7Pressed(object sender, InfoEventArgs<int> e)
    {
        Ability abilityInSlot = actionBar.GetAbilityOnActionButton(actionBar.actionButton7);
        if (abilityInSlot != null)
        {
            player.playerNeedsToReleaseMouseButton = false;
            player.QueueAbilityCast(abilityInSlot);
        }
    }

    protected override void OnActionBar8Pressed(object sender, InfoEventArgs<int> e)
    {
        Ability abilityInSlot = actionBar.GetAbilityOnActionButton(actionBar.actionButton8);
        if (abilityInSlot != null)
        {
            player.playerNeedsToReleaseMouseButton = false;
            player.QueueAbilityCast(abilityInSlot);
        }
    }

    protected override void OnActionBar9Pressed(object sender, InfoEventArgs<int> e)
    {
        Ability abilityInSlot = actionBar.GetAbilityOnActionButton(actionBar.actionButton9);
        if (abilityInSlot != null)
        {
            player.playerNeedsToReleaseMouseButton = false;
            player.QueueAbilityCast(abilityInSlot);
        }
    }

    protected override void OnActionBar10Pressed(object sender, InfoEventArgs<int> e)
    {
        Ability abilityInSlot = actionBar.GetAbilityOnActionButton(actionBar.actionButton10);
        if (abilityInSlot != null)
        {
            player.playerNeedsToReleaseMouseButton = false;
            player.QueueAbilityCast(abilityInSlot);
        }
    }

    protected override void OnActionBar11Pressed(object sender, InfoEventArgs<int> e)
    {
        Ability abilityInSlot = actionBar.GetAbilityOnActionButton(actionBar.actionButton11);
        if (abilityInSlot != null)
        {
            player.playerNeedsToReleaseMouseButton = false;
            player.QueueAbilityCast(abilityInSlot);
        }
    }

    protected override void OnActionBar12Pressed(object sender, InfoEventArgs<int> e)
    {
        Ability abilityInSlot = actionBar.GetAbilityOnActionButton(actionBar.actionButton12);
        if (abilityInSlot != null)
        {
            player.playerNeedsToReleaseMouseButton = false;
            player.QueueAbilityCast(abilityInSlot);
        }
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
                Ability abilityInSlot = actionBar.GetAbilityOnActionButton(actionButton);
                if (abilityInSlot != null)
                {
                    player.playerNeedsToReleaseMouseButton = true;
                    player.QueueAbilityCast(abilityInSlot);
                }
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

    
}
