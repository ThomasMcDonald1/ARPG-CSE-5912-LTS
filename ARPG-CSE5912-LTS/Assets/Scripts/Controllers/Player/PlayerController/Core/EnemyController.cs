using UnityEngine;
using UnityEngine.AI;
using ARPG.Combat;



namespace ARPG.Core
{
    public class EnemyController : MonoBehaviour
    {

        public string ClassTypeName
        { get { return classTypeName; } }

        public string WeaponTypeName
        { get { return weaponTypeName; } }

        public EnemyTarget target;

        private string classTypeName;
        private string weaponTypeName;

        private EnemyClass enemyClass;


        private void Start()
        {
            classTypeName = "HumanEnemy";
            weaponTypeName = "Unarmed";

            enemyClass = AttachClassScript();

           
        }

        private EnemyClass AttachClassScript()
        {
            EnemyClass enemyClass;
            switch(classTypeName)
            {
                case "HumanEnemy":
                    this.gameObject.AddComponent<HumanEnemy>();
                    enemyClass = this.gameObject.GetComponent<HumanEnemy>();
                    break;
                default:
                    enemyClass = null;
                    break;
            }
            return enemyClass;
        }


        private void Update()
        {
            enemyClass.Attack(target);
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


