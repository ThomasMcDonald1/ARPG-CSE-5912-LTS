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

        protected override void Update()
        {
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
            if (statScript[StatTypes.HEALTH] <= 0)
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
                        GeneralClass.GetComponent<MovementHandler>().NavMeshAgent.isStopped = false;
                        GeneralClass.GetComponent<MovementHandler>().MoveToTarget(AttackTarget.position);
                    }
                    else if (InTargetRange() && signalAttack)
                    {
                        GeneralClass.GetComponent<MovementHandler>().Cancel();
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
            return Vector3.Distance(GeneralClass.transform.position, AttackTarget.position) < AttackRange;
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

                AttackTarget.GetComponent<Stats>()[StatTypes.HEALTH] -= statScript[StatTypes.PHYATK];
            }
        }

        public void Dead()
        {

        }

        public void ProduceItem()
        {
            Debug.Log("Item or experience off");
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



