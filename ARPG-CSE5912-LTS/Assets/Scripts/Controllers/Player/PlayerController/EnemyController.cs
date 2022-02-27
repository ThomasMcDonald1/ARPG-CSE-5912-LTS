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

        private Enemy enemyClass;


        private void Start()
        {
            if (GetComponent<Transform>().GetChild(0).gameObject.name == "EnemyKnight")
            {
                classTypeName = "EnemyKnight";

            }
            if (GetComponent<Transform>().GetChild(0).gameObject.name == "EliteWarrior")
            {
                classTypeName = "EliteWarrior";

            }
            weaponTypeName = "Unarmed";
            target = FindObjectOfType<Player>().GetComponent<EnemyTarget>();
            enemyClass = AttachClassScript();

           
        }

        private Enemy AttachClassScript()
        {
            Enemy enemyClass;
            switch(classTypeName)
            {
                case "EnemyKnight":
                    this.gameObject.AddComponent<EnemyKnight>();
                    enemyClass = this.gameObject.GetComponent<EnemyKnight>();
                    break;
                case "EliteWarrior":
                    this.gameObject.AddComponent<EliteWarrior>();
                    enemyClass = this.gameObject.GetComponent<EliteWarrior>();
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


