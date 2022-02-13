using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {
    public GameObject ExplosionPrefab;
    public float DestroyExplosion = 4.0f;
    public float DestroyChildren = 2.0f;
    public Vector3 Velocity;

    public Rigidbody rb;
    Rigidbody ProjectileRigidbody;

    public Projectile(Rigidbody ProjectileRigidBody, Vector3 spawnPos, Quaternion casterRot)
    {
        this.ProjectileRigidbody = ProjectileRigidBody;
        rb = Instantiate(this.ProjectileRigidbody, spawnPos, casterRot);
    }

    //void Start () {
    //    rb = gameObject.GetComponent<Rigidbody>();
    //    rb.velocity = Velocity;
    //}

    void OnCollisionEnter(Collision col)
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
