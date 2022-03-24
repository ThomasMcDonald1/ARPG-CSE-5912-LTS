using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayCameraZoom : MonoBehaviour
{
    float minClamp = 1.0f;
    float maxClamp = 10.0f;

    public void Start()
    {
        GetComponent<Camera>().orthographicSize = 8f;
    }

    public void ZoomIn()
    {
        if (GetComponent<Camera>().orthographicSize > minClamp)
        {
            GetComponent<Camera>().orthographicSize -= 0.25f;
        }
    }

    public void ZoomOut()
    {
        if (GetComponent<Camera>().orthographicSize < maxClamp)
        {
            GetComponent<Camera>().orthographicSize += 0.25f;
        }
    }
}
