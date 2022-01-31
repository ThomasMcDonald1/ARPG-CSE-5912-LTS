using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthBar : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
        transform.SetPositionAndRotation(transform.position, Quaternion.Euler(new Vector3(30, 45, 0)));//same rotation as camera

    }
    private void LateUpdate()
    {
        //transform.LookAt(Camera.main.transform.position);
        //transform.Rotate(0, 180, 0);
    }
}
