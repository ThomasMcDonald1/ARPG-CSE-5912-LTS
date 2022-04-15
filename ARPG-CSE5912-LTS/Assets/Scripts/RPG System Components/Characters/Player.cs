using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
//using UnityEngine.InputSystem;
using ARPG.Movement;

public class Player : Character
{
    [SerializeField] private DialogueUI dialogueUI;
    [SerializeField] private InventoryUI uiInventory;
    public MouseCursorChanger cursorChanger;

    public static event EventHandler InteractNPC;

    public List<Quest> questList;

    private Inventory inventory;
    public DialogueUI DialogueUI => dialogueUI;
    public IInteractable Interactable { get; set; }

    private Vector3 playerVelocity;
    private bool isMoving;
    private bool soundPlaying = false;

    //Combat
    //private GameObject GeneralClass;
    //public virtual float AttackRange { get; set; }
    private bool signalAttack;
    Animator animator;
    AudioManager audioManager;
    MovementHandler movementHandler;

    void Awake()
    {
        //Debug.Log(stats);
        animator = GetComponent<Animator>();
        audioManager = FindObjectOfType<AudioManager>();
        movementHandler = GetComponent<MovementHandler>();
        // inventory = new Inventory();
        // uiInventory.SetInventory(inventory);
        // ItemWorld.SpawnItemWorld(new Vector3(-4.83f, 1.13f, 14.05f), new InventoryItems { itemType = InventoryItems.ItemType.HealthPotion, amount = 1 });
    }

    protected override void Start()
    {
        base.Start();

        //GeneralClass = GameObject.Find("Class");

        //StopAttack is true when Knight is not in attacking state, basicaly allows Knight to stop attacking when click is released
        animator.SetBool("StopAttack", true);
        animator.SetBool("Dead", false);

    }

    protected override void Update()
    {
        //inventory system
        // playUpItem();
        //Sound
        float attackSpeed = 1 + (stats[StatTypes.AtkSpeed] * 0.01f);
        animator.SetFloat("AttackSpeed", attackSpeed);
        agent.speed = baseRunSpeed * (1 + stats[StatTypes.RunSpeed] * 0.01f);

        playerVelocity = agent.velocity;
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
            audioManager.Play("Footsteps");
            soundPlaying = true;
        }
        else if (!isMoving)
        {
            audioManager.Stop("Footsteps");
            soundPlaying = false;
        }

        //Combat
        if (stats[StatTypes.HP] <= 0)
        {
            animator.SetBool("Dead", true);
        }

        // Enemy target, always try to look at
        if (AttackTarget != null)
        {
            //rotation toward enemy
            Quaternion rotationToLookAt = Quaternion.LookRotation(AttackTarget.transform.position - transform.position);
            float rotationY = Mathf.SmoothDampAngle(transform.eulerAngles.y,
            rotationToLookAt.eulerAngles.y, ref yVelocity, smooth);
            transform.eulerAngles = new Vector3(0, rotationY, 0);
        }
    }

    public void TargetEnemy()
    {
        if (AttackTarget != null)
        {
            animator.SetBool("StopAttack", false);
            if (!InCombatTargetRange())
            {
                StartCoroutine(MoveToEnemy());
            }
            else if (InCombatTargetRange())
            {
                //GetComponent<Animator>().SetBool("StopAttack", false);
                movementHandler.Cancel();

                if (animator.GetBool("AttackingMainHand"))
                {

                    animator.SetTrigger("AttackMainHandTrigger");
                    //Debug.Log(GetComponent<Animator>().GetBool("AttackingMainHand"));
                }
                else
                {

                    animator.SetTrigger("AttackOffHandTrigger");
                    //Debug.Log(GetComponent<Animator>().GetBool("AttackingMainHand"));
                }
            }
        }
    }

    public IEnumerator MoveToEnemy()
    {
        while (AttackTarget != null && !InCombatTargetRange())
        {
            movementHandler.NavMeshAgent.isStopped = false;
            movementHandler.MoveToTarget(AttackTarget.transform.position);
            yield return null;
        }
        if (AttackTarget != null)
        {
            movementHandler.Cancel();

            if (animator.GetBool("AttackingMainHand"))
            {
                animator.SetTrigger("AttackMainHandTrigger");
                //Debug.Log(GetComponent<Animator>().GetBool("AttackingMainHand"));
            }
            else
            {
                animator.SetTrigger("AttackOffHandTrigger");
                //Debug.Log(GetComponent<Animator>().GetBool("AttackingMainHand"));
            }
        }
    }

    public IEnumerator GoToNPC()
    {
        while (NPCTarget != null && !InInteractNPCRange())
        {
            movementHandler.NavMeshAgent.isStopped = false;
            movementHandler.MoveToTarget(NPCTarget.transform.position);
            yield return null;
        }
        InteractNPC?.Invoke(this, EventArgs.Empty);
        StartCoroutine(LookAtNPCTarget());
    }

    public IEnumerator LookAtNPCTarget()
    {
        float time = 0.0f;
        float speed = 1.0f;
        movementHandler.Cancel();

        while (NPCTarget != null && time < 1.0f)
        {
            Quaternion rotationToLookAt = Quaternion.LookRotation(NPCTarget.transform.position - transform.position);
            float rotationY = Mathf.SmoothDampAngle(transform.eulerAngles.y,
            rotationToLookAt.eulerAngles.y, ref yVelocity, smooth);
            transform.eulerAngles = new Vector3(0, rotationY, 0);
            time += Time.deltaTime * speed;
            yield return null;
        }
    }

    public void SetAttackTarget(EnemyTarget target)
    {
        AttackTarget = target.transform;
    }

    public void AttackCancel()
    {
        AttackTarget = null;
        animator.SetBool("StopAttack", true);
    }

    public void DialogueCancel()
    {
        NPCTarget = null;
    }

    public bool InCombatTargetRange()
    {
        if (AttackTarget == null) return false;
        //return Vector3.Distance(GeneralClass.transform.position, AttackTarget.position) < AttackRange;
        return Vector3.Distance(this.transform.position, AttackTarget.position) < stats[StatTypes.AttackRange];

    }

    public bool InInteractNPCRange()
    {
        if (NPCTarget == null) return false;
        return Vector3.Distance(transform.position, NPCTarget.transform.position) < 1.5f;
    }

    public void AttackSignal(bool signal)
    {
        signalAttack = signal;
    }

    // From animation event
    public void Hit()
    {
        if (AttackTarget != null)
        {
            //Debug.Log("AttackTarget: " + AttackTarget.name);
            QueueBasicAttack(basicAttackAbility, AttackTarget.GetComponent<Character>(), this);
        }
        animator.SetBool("StopAttack", true);
    }

    // From animation event
    public void EndMainHandAttack()
    {
        if (animator.GetBool("CanDualWield")) { animator.SetBool("AttackingMainHand", false); }
    }

    // From animation event
    public void EndOffHandAttack()
    {
        animator.SetBool("AttackingMainHand", true);
    }
    

    public void ProduceItem()
    {
        Debug.Log("Item or experience off");
    }

    public override Type GetCharacterType()
    {
        return typeof(Player);
    }
}



