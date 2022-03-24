using UnityEngine;
using UnityEngine.AI;
using ARPG.Combat;

namespace ARPG.Core
{
    public class EnemyController : MonoBehaviour
    {
        public EnemyTarget target;
        private string classTypeName;
        private string weaponTypeName;
        private Enemy enemyClass;

        public string ClassTypeName
        { get { return classTypeName; } }
        public string WeaponTypeName
        { get { return weaponTypeName; } }

        private void Start()
        {
            if (GetComponent<Transform>().GetChild(0).gameObject.name == "EnemyKnight")
            {
                classTypeName = "EnemyKnight";
                this.gameObject.AddComponent<EnemyKnight>();
                enemyClass = this.gameObject.GetComponent<EnemyKnight>();
            }
            if (GetComponent<Transform>().GetChild(0).gameObject.name == "EliteWarrior")
            {
                classTypeName = "EliteWarrior";
                this.gameObject.AddComponent<EliteWarrior>();
                enemyClass = this.gameObject.GetComponent<EliteWarrior>();

            }
            if (GetComponent<Transform>().GetChild(0).gameObject.name == "EnemyPath")
            {
                classTypeName = "EnemyPath";
                this.gameObject.AddComponent<Paths>();
                enemyClass = this.gameObject.GetComponent<Paths>();

            }
            if (GetComponent<Transform>().GetChild(0).gameObject.name == "EnemySage")
            {
                classTypeName = "EnemySage";
                this.gameObject.AddComponent<SageOfSixPaths>();
                enemyClass = this.gameObject.GetComponent<SageOfSixPaths>();

            }
            weaponTypeName = "Unarmed";
            target = FindObjectOfType<Player>().GetComponent<EnemyTarget>();
            enemyClass.AttackTarget = target.transform;
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

    //private void AttachClassScript()
    //{
    //    switch (classTypeName)
    //    {
    //        case "EnemyKnight":
    //            this.gameObject.AddComponent<EnemyKnight>();
    //            enemyClass = this.gameObject.GetComponent<EnemyKnight>();

    //            break;
    //        case "EliteWarrior":
    //            this.gameObject.AddComponent<EliteWarrior>();
    //            enemyClass = this.gameObject.GetComponent<EliteWarrior>();

    //            break;
    //        default:
    //            break;
    //    }
    //}

    //private Enemy enemyClass;

    //private void Update()
    //{
    //    enemyClass.Attack(target);
    //    UpdateAnimator();
    //}
    //private Enemy AttachClassScript()
    //{
    //    Enemy enemyClass;
    //    switch (classTypeName)
    //    {
    //        case "EnemyKnight":
    //            this.gameObject.AddComponent<EnemyKnight>();
    //            enemyClass = this.gameObject.GetComponent<EnemyKnight>();
    //            break;
    //        case "EliteWarrior":
    //            this.gameObject.AddComponent<EliteWarrior>();
    //            enemyClass = this.gameObject.GetComponent<EliteWarrior>();
    //            break;
    //        default:
    //            enemyClass = null;
    //            break;
    //    }
    //    return enemyClass;
}



