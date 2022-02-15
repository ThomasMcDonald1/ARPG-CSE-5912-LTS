using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {
    public GameObject ExplosionPrefab;
    public float DestroyExplosion = 4.0f;
    public float DestroyChildren = 2.0f;

    public Rigidbody rb;
    Rigidbody ProjectileRigidbody;
    RaycastHit hit;

    public Projectile(Rigidbody ProjectileRigidBody, Vector3 spawnPos, Quaternion casterRot, RaycastHit hit)
    {
        //Instantiate(GameObject.CreatePrimitive(PrimitiveType.Sphere), hit.point, Quaternion.identity);
        this.ProjectileRigidbody = ProjectileRigidBody;
        this.hit = hit;
        rb = Instantiate(this.ProjectileRigidbody, spawnPos, casterRot);
        //if (hit.point != null)
        //{
        //    rb.constraints = RigidbodyConstraints.None;
        //}
        DetermineVelocity(hit, spawnPos);
    }

    //void Start () {
    //    rb = gameObject.GetComponent<Rigidbody>();
    //    rb.velocity = Velocity;
    //}

    void OnCollisionEnter(Collision col)
    {
        Explode();
    }


    private void DetermineVelocity(RaycastHit hit, Vector3 spawnPos)
    {
        float height = 1;
        float gravity = -18;

        float displacementY = hit.point.y - rb.transform.position.y;
        Vector3 displacementXZ = new Vector3(hit.point.x - spawnPos.x, 0, hit.point.z - spawnPos.z);

        Vector3 velocityY = Vector3.up * Mathf.Sqrt(-2 * gravity * height);
        Vector3 velocityXZ = displacementXZ / (Mathf.Sqrt(-2 * height / gravity) + Mathf.Sqrt(2 * (displacementY - height) / gravity));
        
        rb.velocity = velocityXZ + velocityY;
    }

    private void Explode()
    {
        var exp = Instantiate(ExplosionPrefab, transform.position, ExplosionPrefab.transform.rotation);
        Destroy(exp, DestroyExplosion);
        Transform child;
        child = transform.GetChild(0);
        transform.DetachChildren();
        Destroy(child.gameObject, DestroyChildren);
        Destroy(gameObject);
    }
}
