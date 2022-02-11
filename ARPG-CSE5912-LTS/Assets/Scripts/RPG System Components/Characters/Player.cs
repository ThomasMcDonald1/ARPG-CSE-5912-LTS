using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using ARPG.Movement;

public class Player : Character
{
    [SerializeField] private DialogueUI dialogueUI;
    [SerializeField] private InventoryUI uiInventory;
    private Inventory inventory;
    private Camera mainCamera;
    public DialogueUI DialogueUI => dialogueUI;
    public IInteractable Interactable { get; set; }

    private Vector3 playerVelocity;
    private bool isMoving;
    private bool soundPlaying = false;

    Coroutine aoeAbilitySelectionMode;
    Coroutine singleTargetSelectionMode;
    [HideInInspector] public bool playerInAOEAbilityTargetSelectionMode;
    [HideInInspector] public bool playerInSingleTargetAbilitySelectionMode;
    [HideInInspector] public bool playerNeedsToReleaseMouseButton;

    MouseCursorChanger cursorChanger;

    [SerializeField] Ability basicAttack;
    [SerializeField] Ability fireballTest;
    [SerializeField] Ability bowAbilityTest;

    public static event EventHandler<InfoEventArgs<(RaycastHit, Ability)>> AgentMadeItWithinRangeToPerformAbilityWithoutCancelingEvent;
    public static event EventHandler<InfoEventArgs<(RaycastHit, Ability)>> PlayerSelectedGroundTargetLocationEvent;
    public static event EventHandler<InfoEventArgs<(RaycastHit, Ability)>> PlayerSelectedSingleTargetEvent;
    public static event EventHandler<InfoEventArgs<Ability>> AbilityIsReadyToBeCastEvent;

    int groundLayerMask = 1 << 6;

    //Combat
    //private GameObject GeneralClass;
    public virtual float AttackRange { get; set; }
    private bool signalAttack;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        cursorChanger = gameplayStateController.GetComponent<MouseCursorChanger>();
        abilitiesKnown.Add(basicAttack);
        abilitiesKnown.Add(fireballTest);
        abilitiesKnown.Add(bowAbilityTest);

        inventory = new Inventory();
        uiInventory.SetInventory(inventory);
        mainCamera = Camera.main;
        // ItemWorld.SpawnItemWorld(new Vector3(-4.83f, 1.13f, 14.05f), new InventoryItems { itemType = InventoryItems.ItemType.HealthPotion, amount = 1 });
    }

    protected override void Start()
    {
        base.Start();

        //GeneralClass = GameObject.Find("Class");

        //StopAttack is true when Knight is not in attacking state, basicaly allows Knight to stop attacking when click is released
        GetComponent<Animator>().SetBool("StopAttack", true);
        GetComponent<Animator>().SetBool("Dead", false);

    }

    protected override void Update()
    {
        //inventory system
        playUpItem();
        //Sound
        playerVelocity = GetComponent<NavMeshAgent>().velocity;
        if (playerVelocity.magnitude > 0)
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

        //Combat
        if (stats[StatTypes.HEALTH] <= 0)
        {
            GetComponent<Animator>().SetBool("Dead", true);
        }
        else
        {

            if (AttackTarget != null)
            {
                GetComponent<Animator>().SetBool("StopAttack", false);
                if (!InTargetRange())
                {
                    //GeneralClass.GetComponent<MovementHandler>().NavMeshAgent.isStopped = false;
                    //GeneralClass.GetComponent<MovementHandler>().MoveToTarget(AttackTarget.position);
                    this.GetComponent<MovementHandler>().NavMeshAgent.isStopped = false;
                    this.GetComponent<MovementHandler>().MoveToTarget(AttackTarget.position);
                }
                else if (InTargetRange() && signalAttack)
                {
                    //GeneralClass.GetComponent<MovementHandler>().Cancel();
                    this.GetComponent<MovementHandler>().Cancel();
                    GetComponent<Animator>().SetTrigger("AttackTrigger");

                    //rotation toward enemy
                    Quaternion rotationToLookAt = Quaternion.LookRotation(AttackTarget.transform.position - transform.position);
                    float rotationY = Mathf.SmoothDampAngle(transform.eulerAngles.y,
                    rotationToLookAt.eulerAngles.y, ref yVelocity, smooth);
                    transform.eulerAngles = new Vector3(0, rotationY, 0);
                }
                // doesnt get triggered?
                //else if (InTargetRange() && !signalAttack)/
                //{
                //    GeneralClass.GetComponent<MovementHandler>().Cancel();
                //    GetComponent<Animator>().SetTrigger("AttackTrigger");
                //    Debug.Log("triggered");
                //    Cancel();
                //}
            }
        }
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
    }


    private void playUpItem()
    {

        Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
        RaycastHit hit;
        if (Mouse.current.leftButton.wasReleasedThisFrame)
        {
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider != null)
                {
                    //Debug.Log("hit: " + hit.transform.gameObject.tag);
                    if (hit.transform.gameObject.tag == "InventoryItem")
                    {
                        InventoryItems item = hit.transform.gameObject.GetComponent<ItemWorld>().getItem();
                        inventory.AddItem(item);
                        hit.transform.gameObject.GetComponent<ItemWorld>().DestroySelf();

                    }

                }
            }
        }
    }


    public void Attack(EnemyTarget target)
    {
        AttackTarget = target.transform;
    }

    public void Cancel()
    {
        AttackTarget = null;
        GetComponent<Animator>().SetBool("StopAttack", true);
    }

    public bool InTargetRange()
    {
        if (AttackTarget == null) return false;
        //return Vector3.Distance(GeneralClass.transform.position, AttackTarget.position) < AttackRange;
        return Vector3.Distance(this.transform.position, AttackTarget.position) < AttackRange;

    }

    public void AttackSignal(bool signal)
    {
        signalAttack = signal;
    }

    // Needed for the animation event
    public void Hit()
    {
        if (AttackTarget != null)
        {

            AttackTarget.GetComponent<Stats>()[StatTypes.HEALTH] -= stats[StatTypes.PHYATK];
        }
    }

    public void Dead()
    {

    }

    public void ProduceItem()
    {
        Debug.Log("Item or experience off");
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



