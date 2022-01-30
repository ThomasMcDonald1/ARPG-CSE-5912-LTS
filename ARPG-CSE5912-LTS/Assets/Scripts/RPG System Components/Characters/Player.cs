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

    [SerializeField] MouseCursorChanger cursorChanger;

    [SerializeField] Ability basicAttack;
    [SerializeField] Ability fireballTest;

    void Awake()
    {
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

    public void PlayerCastAbility(Ability abilityToCast)
    {
        bool requiresCharacter = CheckForCharacterRequiredUnderCursor(abilityToCast);

        cursorChanger.ChangeCursorToSelectionGraphic();
        StartCoroutine(WaitForPlayerClick());

        if (requiresCharacter)
        {
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
        BaseAbilityArea abilityArea = abilityToCast.GetComponent<BaseAbilityArea>();
        abilityArea.DisplayAOEArea();
    }

    bool CheckForCharacterRequiredUnderCursor(Ability abilityToCast)
    {
        BaseAbilityConditional[] conditionals = abilityToCast.GetComponentsInChildren<BaseAbilityConditional>();
        foreach (BaseAbilityConditional conditional in conditionals)
        {
            if (conditional is AbilityRequiresCharacterUnderCursor)
                return true;
        }
        return false;
    }

    private IEnumerator WaitForPlayerClick()
    {
        bool playerHasNotClicked = true;
        while (playerHasNotClicked)
        {
            if (Mouse.current.leftButton.wasReleasedThisFrame)
            {
                playerHasNotClicked = false;
                cursorChanger.ChangeCursorToDefaultGraphic();
            }

            //TODO: Check for other input that would stop this current ability cast, like queueing up a different ability instead, or pressing escape
            yield return null;
        }
    }
}
