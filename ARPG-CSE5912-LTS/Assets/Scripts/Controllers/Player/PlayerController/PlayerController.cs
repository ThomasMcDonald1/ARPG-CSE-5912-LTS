using UnityEngine;
using UnityEngine.AI;
using ARPG.Movement;
using ARPG.Combat;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;

/* 
 * 
 * This class is intended to be utilized based on the event fired from within GameplayState.cs
 * The OnClick method (an overrided method) from within GameplayState.cs is triggered, this class is used to
 * respond to the event.
 */


public class PlayerController : MonoBehaviour
    {
    public int DungeonNum;


    public string ClassTypeName
        { get { return classTypeName; } }

        public string WeaponTypeName
        { get { return weaponTypeName; } }

        private string classTypeName;
        private string weaponTypeName;


        public Player player;
    private void Awake()
    {
        player = GetComponent<Player>();
    }

    public void PlayerOnClickEventResponse(int layer, object sender, InfoEventArgs<RaycastHit> e)
        {
            switch (LayerMask.LayerToName(layer))
            {
                case "Walkable":
                    if (!player.abilityQueued)
                    {
                        InteractionManager.GetInstance().StopInteraction();
                        if (GetComponent<Animator>().GetBool("Dead") == false)
                        {
                            player.DialogueCancel();
                            player.AttackCancel();
                            GetComponent<MovementHandler>().MoveToTarget(e.info.point);
                        }

                    }
                    break;
                case "NPC":
                    player.AttackCancel();
                    if (player.NPCTarget != null)
                    { 
                        player.NPCTarget = null; 
                    }
                    NPC npcTarget = e.info.transform.GetComponent<NPC>();
                    Debug.Log(npcTarget);
                    player.NPCTarget = npcTarget;
                    StartCoroutine(player.GoToNPC());
                    break;
                case "Enemy":
                    player.DialogueCancel();
                    player.SetAttackTarget(e.info.transform.GetComponent<EnemyTarget>());
                    player.TargetEnemy();
                    break;
                case "Doodads":
                    if (GetComponent<Animator>().GetBool("Dead") == false)
                    {
                        //Debug.Log("Clicked on " + e.info.collider.gameObject);
                        player.AttackCancel();
                        StartCoroutine(player.RunToDoodad(e.info.collider.gameObject));                     
                    }
                break;
                default:
                    break;
            }
        }

        public void PlayerCancelClickEventResponse(object sender, InfoEventArgs<RaycastHit> e)
        {
            StartCoroutine(StopAttack());
        }

        //Delay in coroutine so that play can attack once with a click
        IEnumerator StopAttack()
        {
            //Print the time of when the function is first called.
            //Debug.Log("Started Coroutine at timestamp : " + Time.time);

            //yield on a new YieldInstruction that waits for 0.5 seconds.
            yield return new WaitForSeconds(0.5f);

            //After we have waited 5 seconds print the time again.
            //Debug.Log("Finished Coroutine at timestamp : " + Time.time);
            player.AttackSignal(false);
            if (player.InCombatTargetRange()) { player.AttackCancel(); }
        }

        private void Start()
        {
            DungeonNum = 0;
            classTypeName = "Knight";
            weaponTypeName = "Unarmed";
        }

        private void Update()
        {
            UpdateAnimator();

            
        }

        private void UpdateAnimator()
        {
            Vector3 velocity = GetComponent<NavMeshAgent>().velocity;
            Vector3 localVelocity = transform.InverseTransformDirection(velocity);
            float speed = localVelocity.z;
            GetComponent<Animator>().SetFloat("ForwardSpeed", speed);
        }
    }



