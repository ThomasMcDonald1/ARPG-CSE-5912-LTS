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
    Coroutine aoeAbilitySelectionMode;
    Coroutine singleTargetSelectionMode;
    public bool playerInAOEAbilityTargetSelectionMode;
    public bool playerInSingleTargetAbilitySelectionMode;
    public bool playerNeedsToReleaseMouseButton;

    [SerializeField] MouseCursorChanger cursorChanger;

    [SerializeField] Ability basicAttack;
    [SerializeField] Ability fireballTest;
    [SerializeField] Ability bowAbilityTest;

    public HashSet<Vector3> unlockedWaypoints;

    public static event EventHandler<InfoEventArgs<(RaycastHit, Ability)>> AgentMadeItWithinRangeToPerformAbilityWithoutCancelingEvent;
    public static event EventHandler<InfoEventArgs<(RaycastHit, Ability)>> PlayerSelectedGroundTargetLocationEvent;
    public static event EventHandler<InfoEventArgs<(RaycastHit, Ability)>> PlayerSelectedSingleTargetEvent;
    public static event EventHandler<InfoEventArgs<int>> PlayerBeganMovingEvent;
    public static event EventHandler<InfoEventArgs<Ability>> AbilityIsReadyToBeCastEvent;


    int groundLayerMask = 1 << 6;

    //[SerializeField] Camera mainCamera;
    private Camera mainCamera;
    void Awake()
    {
<<<<<<< Updated upstream
        agent = GetComponent<NavMeshAgent>();
        abilitiesKnown.Add(basicAttack);
        abilitiesKnown.Add(fireballTest);
        abilitiesKnown.Add(bowAbilityTest);
        unlockedWaypoints = new HashSet<Vector3>();
    }

    private void OnEnable()
    {
        InputController.CancelPressedEvent += OnCancelPressed;
        AgentMadeItWithinRangeToPerformAbilityWithoutCancelingEvent += OnAgentMadeItWithinRangeWithoutCanceling;
        PlayerSelectedGroundTargetLocationEvent += OnPlayerSelectedGroundTargetLocation;
        PlayerSelectedSingleTargetEvent += OnPlayerSelectedSingleTarget;
    }

    private void OnCancelPressed(object sender, InfoEventArgs<int> e)
    {
        playerInAOEAbilityTargetSelectionMode = false;
        playerInSingleTargetAbilitySelectionMode = false;
        StopCoroutine(singleTargetSelectionMode);
        StopCoroutine(aoeAbilitySelectionMode);
    }

    void OnAgentMadeItWithinRangeWithoutCanceling(object sender, InfoEventArgs<(RaycastHit, Ability)> e)
    {
        BaseAbilityArea abilityArea = e.info.Item2.GetComponent<BaseAbilityArea>();
        abilityQueued = false;
        AbilityIsReadyToBeCastEvent?.Invoke(this, new InfoEventArgs<Ability>(e.info.Item2));
        abilityArea.PerformAOE(e.info.Item1);
    }

    void OnPlayerSelectedGroundTargetLocation(object sender, InfoEventArgs<(RaycastHit, Ability)> e)
    {
        BaseAbilityArea abilityArea = e.info.Item2.GetComponent<BaseAbilityArea>();
        BaseAbilityRange abilityRange = e.info.Item2.GetComponent<BaseAbilityRange>();
        //TODO: make player run to max range of the ability
        float distFromCharacter = Vector3.Distance(e.info.Item1.point, transform.position);
        float distToTravel = distFromCharacter - abilityRange.range;
        if (GetComponent<Player>() != null && distFromCharacter > abilityRange.range)
        {
            Debug.Log("Too far away");
            abilityQueued = true;
            StartCoroutine(RunWithinRange(e.info.Item1, abilityRange.range, distToTravel, e.info.Item2));
        }
        else if (GetComponent<Player>() != null)
        {
            AbilityIsReadyToBeCastEvent?.Invoke(this, new InfoEventArgs<Ability>(e.info.Item2));
            abilityArea.PerformAOE(e.info.Item1);
        }
    }

    void OnPlayerSelectedSingleTarget(object sender, InfoEventArgs<(RaycastHit, Ability)> e)
    {
        //abilityQueued = true;
        //do a coroutine for running within range of enemy
=======
        inventory = new Inventory();
       
        abilitiesKnown = new List<Ability>();
        abilitiesKnown.Add(basicAttack);
        abilitiesKnown.Add(fireballTest);
        uiInventory.SetInventory(inventory);
        mainCamera = Camera.main;

>>>>>>> Stashed changes
    }
  
    private void detectObj()
    {

        Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
        RaycastHit hit;
        if (Mouse.current.leftButton.wasReleasedThisFrame)
        {
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider != null)
                {
                    Debug.Log("hit: " + hit.transform.gameObject.tag);
                    
                }
            }
        }
        //Debug.Log("In detect obj func");
        
    }
    void Update()
    {
        //if (dialogueUI.IsOpen) return;
        detectObj();
        //onTriggerEnter();
        //Debug.Log("In detect obj func");
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
        if (!playerInAOEAbilityTargetSelectionMode && !playerInSingleTargetAbilitySelectionMode)
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
            playerInAOEAbilityTargetSelectionMode = true;
            singleTargetSelectionMode = StartCoroutine(WaitForPlayerClick(ability));
        }
        else
        {
            playerInAOEAbilityTargetSelectionMode = true;
            aoeAbilitySelectionMode = StartCoroutine(WaitForPlayerClickAOE(ability));
        }
    }

    private IEnumerator RunWithinRange(RaycastHit hit, float range, float distToTravel, Ability ability)
    {
        Debug.Log("Running to within range of point.");
        BaseAbilityArea abilityArea = ability.GetComponent<BaseAbilityArea>();
        Vector3 dir = hit.point - transform.position;
        Vector3 normalizedDir = dir.normalized;
        Vector3 endPoint = transform.position + (normalizedDir * (distToTravel + 0.1f));

        while (abilityQueued)
        {
            agent.destination = endPoint;
            float distFromPlayer = Vector3.Distance(hit.point, transform.position);
            if (distFromPlayer <= range)
            {
                AgentMadeItWithinRangeToPerformAbilityWithoutCancelingEvent?.Invoke(this, new InfoEventArgs<(RaycastHit, Ability)>((hit, ability)));
            }

            yield return null;
        }
    }

    private IEnumerator WaitForPlayerClick(Ability ability)
    {
        playerInSingleTargetAbilitySelectionMode = true;
        while (playerInSingleTargetAbilitySelectionMode)
        {
            Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                GameObject go = hit.collider.gameObject;
                Character target = go.GetComponent<Character>();
                //TODO: If ability requires enemy or ally to be clicked, excluding the other, check that first
                if (target != null && Mouse.current.leftButton.wasReleasedThisFrame)
                {
                    playerInSingleTargetAbilitySelectionMode = false;
                    cursorChanger.ChangeCursorToDefaultGraphic();
                    Debug.Log("Clicked on: " + target.name + " as ability selection target.");
                    bool targetInRange = CheckCharacterInRange(target);
                    if (!targetInRange)
                    {
                        PlayerSelectedSingleTargetEvent?.Invoke(this, new InfoEventArgs<(RaycastHit, Ability)>((hit, ability)));
                        //do more stuff
                    }
                }
            }

            yield return null;
        }
    }

    private IEnumerator WaitForPlayerClickAOE(Ability ability)
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
            if (!playerNeedsToReleaseMouseButton && Mouse.current.leftButton.wasReleasedThisFrame)
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
