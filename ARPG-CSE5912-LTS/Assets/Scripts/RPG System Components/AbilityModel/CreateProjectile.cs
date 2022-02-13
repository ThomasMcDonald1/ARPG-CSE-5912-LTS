using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateProjectile : MonoBehaviour {

    public Rigidbody ProjectileRigidbody;
    public float Time = 1.0f;

    private bool CreateInstances = true;
    [HideInInspector] public Projectile Instance;

    //void Start()
    //{
    //   InvokeRepeating("Create", Time, Time);
    //}
    //void Update()
    //{
    //    if (Instance == null)
    //    {
    //        CreateInstances = true;
    //    }
    //}

    public Projectile Create(Character caster, Vector3 spawnPos, Quaternion casterRot)
    {
        Instance = new Projectile(ProjectileRigidbody, spawnPos, casterRot);
        return Instance;
    }
}
