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
    public MouseCursorChanger cursorChanger;

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


    void Awake()
    {
        //Debug.Log(stats);
        agent = GetComponent<NavMeshAgent>();
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
        else
        {

            if (AttackTarget != null)
            {
                GetComponent<Animator>().SetBool("StopAttack", false);
                if (!InCombatTargetRange())
                {
                    //GeneralClass.GetComponent<MovementHandler>().NavMeshAgent.isStopped = false;
                    //GeneralClass.GetComponent<MovementHandler>().MoveToTarget(AttackTarget.position);
                    this.GetComponent<MovementHandler>().NavMeshAgent.isStopped = false;
                    this.GetComponent<MovementHandler>().MoveToTarget(AttackTarget.position);
                }
                else if (InCombatTargetRange() && signalAttack)
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
            }
        }

        // NPCInteraction
        if (NPCTarget != null)
        {
            if (!InInteractNPCRange())
            {
                GetComponent<MovementHandler>().NavMeshAgent.isStopped = false;
                GetComponent<MovementHandler>().MoveToTarget(NPCTarget.transform.position);
            }
            else if (InInteractNPCRange())
            {
                this.GetComponent<MovementHandler>().Cancel();
                Quaternion rotationToLookAt = Quaternion.LookRotation(NPCTarget.transform.position - transform.position);
                float rotationY = Mathf.SmoothDampAngle(transform.eulerAngles.y,
                rotationToLookAt.eulerAngles.y, ref yVelocity, smooth);
                transform.eulerAngles = new Vector3(0, rotationY, 0);
                NPCTarget.Interact();
            }
        }
    }

    //private void playUpItem()
    //{

    //    Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
    //    RaycastHit hit;
    //    if (Mouse.current.leftButton.wasReleasedThisFrame)
    //    {
    //        if (Physics.Raycast(ray, out hit))
    //        {
    //            if (hit.collider != null)
    //            {
    //                //Debug.Log("hit: " + hit.transform.gameObject.tag);
    //                if (hit.transform.gameObject.tag == "InventoryItem")
    //                {
    //                    InventoryItems item = hit.transform.gameObject.GetComponent<ItemWorld>().getItem();
    //                    inventory.AddItem(item);
    //                    hit.transform.gameObject.GetComponent<ItemWorld>().DestroySelf();

    //                }

    //            }
    //        }
    //    }
    //}


    public void Attack(EnemyTarget target)
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

    // Needed for the animation event
    public void Hit()
    {
        if (AttackTarget != null)
        {
            AttackTarget.GetComponent<HealthBarController>().SubtractHealth(stats[StatTypes.PHYATK]);

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



