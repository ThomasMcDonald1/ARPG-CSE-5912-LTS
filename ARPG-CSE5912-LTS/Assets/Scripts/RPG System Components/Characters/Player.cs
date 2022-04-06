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


    void Awake()
    {
        //Debug.Log(stats);
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        // inventory = new Inventory();
        // uiInventory.SetInventory(inventory);
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
        // playUpItem();
        //Sound
        float attackSpeed = 1 + (stats[StatTypes.AtkSpeed] * 0.01f);
        animator.SetFloat("AttackSpeed", attackSpeed);
        agent.speed = baseRunSpeed * (1 + stats[StatTypes.RunSpeed] * 0.01f);

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
        if (stats[StatTypes.HP] <= 0)
        {
            GetComponent<Animator>().SetBool("Dead", true);
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
            GetComponent<Animator>().SetBool("StopAttack", false);
            if (!InCombatTargetRange())
            {
                StartCoroutine(MoveToEnemy());
            }
            else if (InCombatTargetRange())
            {
                //GetComponent<Animator>().SetBool("StopAttack", false);
                GetComponent<MovementHandler>().Cancel();

                if (GetComponent<Animator>().GetBool("AttackingMainHand"))
                {

                    GetComponent<Animator>().SetTrigger("AttackMainHandTrigger");
                    //Debug.Log(GetComponent<Animator>().GetBool("AttackingMainHand"));
                }
                else
                {

                    GetComponent<Animator>().SetTrigger("AttackOffHandTrigger");
                    //Debug.Log(GetComponent<Animator>().GetBool("AttackingMainHand"));
                }
            }
        }
    }

    public IEnumerator MoveToEnemy()
    {
        while (AttackTarget != null && !InCombatTargetRange())
        {
            this.GetComponent<MovementHandler>().NavMeshAgent.isStopped = false;
            this.GetComponent<MovementHandler>().MoveToTarget(AttackTarget.transform.position);
            yield return null;
        }
        if (AttackTarget != null)
        {
            GetComponent<MovementHandler>().Cancel();

            if (GetComponent<Animator>().GetBool("AttackingMainHand"))
            {
                GetComponent<Animator>().SetTrigger("AttackMainHandTrigger");
                //Debug.Log(GetComponent<Animator>().GetBool("AttackingMainHand"));
            }
            else
            {
                GetComponent<Animator>().SetTrigger("AttackOffHandTrigger");
                //Debug.Log(GetComponent<Animator>().GetBool("AttackingMainHand"));
            }
        }
    }

    public IEnumerator GoToNPC()
    {
        while (NPCTarget != null && !InInteractNPCRange())
        {
            GetComponent<MovementHandler>().NavMeshAgent.isStopped = false;
            GetComponent<MovementHandler>().MoveToTarget(NPCTarget.transform.position);
            yield return null;
        }
        InteractNPC?.Invoke(this, EventArgs.Empty);
        StartCoroutine(LookAtNPCTarget());
    }

    public IEnumerator LookAtNPCTarget()
    {
        float time = 0.0f;
        float speed = 1.0f;
        GetComponent<MovementHandler>().Cancel();

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
        GetComponent<Animator>().SetBool("StopAttack", true);
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
        GetComponent<Animator>().SetBool("StopAttack", true);
    }

    // From animation event
    public void EndMainHandAttack()
    {
        if (GetComponent<Animator>().GetBool("CanDualWield")) { GetComponent<Animator>().SetBool("AttackingMainHand", false); }
    }

    // From animation event
    public void EndOffHandAttack()
    {
        GetComponent<Animator>().SetBool("AttackingMainHand", true);
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



