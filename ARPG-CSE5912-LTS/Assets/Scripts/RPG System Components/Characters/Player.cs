using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class Player : Character
{
    [SerializeField] private DialogueUI dialogueUI;

    public DialogueUI DialogueUI => dialogueUI;

    public IInteractable Interactable { get; set; }
    private Vector3 playerVelocity;
    private bool isMoving;
    private bool soundPlaying = false;
    public bool playerInAbilityTargetSelectionMode;

    [SerializeField] MouseCursorChanger cursorChanger;

    [SerializeField] Ability basicAttack;
    [SerializeField] Ability fireballTest;

    public static event EventHandler<InfoEventArgs<(RaycastHit, Ability)>> PlayerSelectedGroundTargetLocationEvent;
    public static event EventHandler<InfoEventArgs<int>> PlayerBeganMovingEvent;
    int groundLayerMask = 1 << 6;


    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        abilitiesKnown.Add(basicAttack);
        abilitiesKnown.Add(fireballTest);
    }

    void Update()
    {
        //if (dialogueUI.IsOpen) return;
        playerVelocity = GetComponent<NavMeshAgent>().velocity;
        if(playerVelocity.magnitude > 0)
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }
        if (isMoving && !soundPlaying)
        {
            FindObjectOfType<AudioManager>().Play("Footsteps");
            soundPlaying = true;
        }
        else if (!isMoving)
        {
            FindObjectOfType<AudioManager>().Stop("Footsteps");
            soundPlaying = false;
        }

    }

    public void MoveToLocation(Vector3 location)
    {
        if (!playerInAbilityTargetSelectionMode)
        {
            PlayerBeganMovingEvent?.Invoke(this, new InfoEventArgs<int>(0));
            agent.destination = location;
            abilityQueued = false;
        }
    }

    public void PlayerQueueAbilityCastSelectionRequired(Ability ability, bool requiresCharacter)
    {
        cursorChanger.ChangeCursorToSelectionGraphic();

        if (requiresCharacter)
        {
            playerInAbilityTargetSelectionMode = true;
            StartCoroutine(WaitForPlayerClick(ability));

            Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                GameObject go = hit.collider.gameObject;
                Character target = go.GetComponent<Character>();

                if (target != null)
                {
                    bool targetInRange = CheckCharacterInRange(target);
                    if (targetInRange)
                    {
                        //do more stuff
                    }
                }
            }
        }
        else
        {
            playerInAbilityTargetSelectionMode = true;
            StartCoroutine(WaitForPlayerClick(ability));
        }
    }

    private IEnumerator WaitForPlayerClick(Ability ability)
    {
        BaseAbilityArea abilityArea = ability.GetComponent<BaseAbilityArea>();
        BaseAbilityRange abilityRange = ability.GetComponent<BaseAbilityRange>();
        bool playerHasNotClicked = true;
        while (playerHasNotClicked)
        {
            if (abilityArea != null)
            {
                abilityArea.DisplayAOEArea();
            }
            if (Mouse.current.leftButton.wasReleasedThisFrame)
            {
                playerHasNotClicked = false;
                cursorChanger.ChangeCursorToDefaultGraphic();
                if (abilityArea != null)
                {
                    Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
                    RaycastHit hit;

                    if (Physics.Raycast(ray, out hit, groundLayerMask))
                    {
                        PlayerSelectedGroundTargetLocationEvent?.Invoke(this, new InfoEventArgs<(RaycastHit, Ability)>((hit, ability)));
                    }
                    abilityArea.abilityAreaNeedsShown = false;
                    gameplayStateController.aoeReticleCylinder.SetActive(false);
                }
            }
            //TODO: Check for other input that would stop this current ability cast, like queueing up a different ability instead, or pressing escape
            yield return null;
        }
    }
}
