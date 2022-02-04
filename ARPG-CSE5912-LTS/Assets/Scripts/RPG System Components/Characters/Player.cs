using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using ARPG.Movement;

    public abstract class Player : Character
    {
        //Dialogue
        [SerializeField] private DialogueUI dialogueUI;
        public DialogueUI DialogueUI => dialogueUI;
        public IInteractable Interactable { get; set; }

        //Sound
        private Vector3 playerVelocity;
        private bool isMoving;
        private bool soundPlaying = false;

        public List<Ability> abilitiesKnown;
        [SerializeField] Ability basicAttack;
        [SerializeField] Ability fireballTest;

        //Combat
        private GameObject GeneralClass;
        public virtual float AttackRange { get; set; }
        private bool signalAttack;

        protected override void Start()
        {
            base.Start();

            GeneralClass = GameObject.Find("Class");

            //StopAttack is true when Knight is not in attacking state, basicaly allows Knight to stop attacking when click is released
            GetComponent<Animator>().SetBool("StopAttack", true);
            GetComponent<Animator>().SetBool("Dead", false);

        }
        void Awake()
        {
            abilitiesKnown = new List<Ability>();
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




