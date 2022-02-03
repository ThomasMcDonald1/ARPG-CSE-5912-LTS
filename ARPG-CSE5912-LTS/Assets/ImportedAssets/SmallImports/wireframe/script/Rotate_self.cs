using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate_self : MonoBehaviour
{
    float x = 0;

    float y = 0;
    float z = 0;

    // Start is called before the first frame update
    void Start()
    {
        this.x = UnityEditor.TransformUtils.GetInspectorRotation(this.transform).x;
        this.y = UnityEditor.TransformUtils.GetInspectorRotation(this.transform).y;
        this.z = UnityEditor.TransformUtils.GetInspectorRotation(this.transform).z;
    }

    float num = 0;
    // Update is called once per frame
    void Update()
    {
        num += Time.deltaTime * 20;
        if (num > 360)
            num = 0;
        this.transform.rotation = Quaternion.Euler(num, UnityEditor.TransformUtils.GetInspectorRotation(this.transform).y, UnityEditor.TransformUtils.GetInspectorRotation(this.transform).z);
    }
}
