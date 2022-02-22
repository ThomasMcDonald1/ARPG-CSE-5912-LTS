using UnityEngine;
using System.Collections;

[AddComponentMenu("Camera-Control/Mouse Orbit with zoom")]
public class SCR_cameraControl : MonoBehaviour {

    public Transform target;
    public float distance = 5.0f;
    public float xSpeed = 120.0f;
    public float ySpeed = 120.0f;

    public float yMinLimit = -20f;
    public float yMaxLimit = 80f;

    public float distanceMin = .5f;
    public float distanceMax = 15f;

    public bool lockCameraRotation = false;

    float x = 0.0f;
    float y = 0.0f;

    Transform thisTransform;
    Quaternion rotation;
    Vector3 negDistance;
    Vector3 position;

    //private int direction;  //random camera direction

    // Use this for initialization
    void Start() {
        thisTransform = transform;

        Vector3 angles = transform.eulerAngles;
        x = angles.y;
        y = angles.x;
    }

    void LateUpdate() {
        UpdateCameraPosition();
    }

    public static float ClampAngle(float angle, float min, float max) {
        if (angle < -360F)
            angle += 360F;
        if (angle > 360F)
            angle -= 360F;
        return Mathf.Clamp(angle, min, max);
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Alpha0)) {
            lockCameraRotation = !lockCameraRotation;
        }

        //UpdateCameraPosition();
    }

    void UpdateCameraPosition() {
        if (target) {

            //turning camera around, disable later
            if (Input.GetMouseButton(1) && !lockCameraRotation) {
                x += Input.GetAxis("Mouse X") * xSpeed * distance * 0.02f;
                y -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f;
            }

            y = ClampAngle(y, yMinLimit, yMaxLimit);

            rotation = Quaternion.Euler(y, x, 0);

            distance = Mathf.Clamp(distance - Input.GetAxis("Mouse ScrollWheel") * 5, distanceMin, distanceMax);

            //RaycastHit hit;
            //if (Physics.Linecast (target.position, transform.position, out hit)) {
            //        distance -=  hit.distance;
            //}
            negDistance = new Vector3(0.0f, 0.0f, -distance);
            position = rotation * negDistance + target.position;

            thisTransform.rotation = rotation;
            thisTransform.position = position;

        }
        else {
            Debug.Log("Assign camera target to follow");
        }
    }
}