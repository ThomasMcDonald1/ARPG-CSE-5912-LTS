using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapCameraZoom : MonoBehaviour
{
    float minClamp = 10.0f;
    float maxClamp = 40.0f;

    public void Start()
    {
        GetComponent<Camera>().orthographicSize = 25f;
    }

    public void ZoomIn()
    {
        if (GetComponent<Camera>().orthographicSize > minClamp)
        {
            GetComponent<Camera>().orthographicSize -= 5f;
        }
    }

    public void ZoomOut()
    {
        if (GetComponent<Camera>().orthographicSize < maxClamp)
        {
            GetComponent<Camera>().orthographicSize += 5f;
        }
    }
}
