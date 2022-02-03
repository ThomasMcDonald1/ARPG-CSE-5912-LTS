using UnityEngine;
using UnityEngine.AI;
using ARPG.Movement;
using ARPG.Combat;
using System.Collections;

/* 
 * 
 * This class is intended to be utilized based on the event fired from within GameplayState.cs
 * The OnClick method (an overrided method) from within GameplayState.cs is triggered, this class is used to
 * respond to the event.
 */

namespace ARPG.Core
{
    public class PlayerController : MonoBehaviour
    {

        public string ClassTypeName
        { get { return classTypeName; } }

        public string WeaponTypeName
        { get { return weaponTypeName; } }

        private string classTypeName;
        private string weaponTypeName;


        public IPlayerClass playerClass;

        public void PlayerOnClickEventResponse(int layer, object sender, InfoEventArgs<RaycastHit> e)
        {
            switch (LayerMask.LayerToName(layer))
            {
                case "Walkable":
                    if (GetComponent<Animator>().GetBool("Dead") == false)
                    {
                        playerClass.Cancel();
                        GetComponentInChildren<MovementHandler>().MoveToTarget(e.info.point);
                    }
                    break;

                case "NPC":
                    break;

                case "Enemy":
                    playerClass.AttackSignal(true);
                    EnemyTarget target = e.info.transform.GetComponent<EnemyTarget>();
                    playerClass.Attack(target);
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
            Debug.Log("Started Coroutine at timestamp : " + Time.time);

            //yield on a new YieldInstruction that waits for 0.5 seconds.
            yield return new WaitForSeconds(0.5f);

            //After we have waited 5 seconds print the time again.
            Debug.Log("Finished Coroutine at timestamp : " + Time.time);
            playerClass.AttackSignal(false);
            if (playerClass.InTargetRange()) { playerClass.Cancel(); }
        }

        private void Start()
        {
            /* 
                * classTypeName and weaponTypeName are default for now. We need to:
                *          1.] Find another way to change the class type based on a choice in character selection from the main menu]
                *          2.] Also need to find a way to change weaponTypeName based on what the player has equipped
                */
            classTypeName = "Knight";
            weaponTypeName = "Unarmed";

            /*foreach (Transform child in transform)
            {
                if (child.gameObject.name == classTypeName)
                {
                    ClassType = child.gameObject;
                }
            }*/

            playerClass = AttachClassScript();
        }

        private IPlayerClass AttachClassScript()
        {
            IPlayerClass playerClass;
            switch(classTypeName)
            {
                case "Knight":
                    this.gameObject.AddComponent<Knight>();
                    playerClass = this.gameObject.GetComponent<Knight>();
                    break;
                default:
                    playerClass = null;
                    break;
            }
            return playerClass;
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
}


