using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateProjectile : MonoBehaviour {

    public Rigidbody ProjectileRigidbody;
    public float Time = 1.0f;

    [HideInInspector] public Projectile Instance;


    public void Create(Vector3 spawnPos, Quaternion casterRot, RaycastHit hit)
    {
        Instance = new Projectile(ProjectileRigidbody, spawnPos, casterRot, hit);
    }
}
