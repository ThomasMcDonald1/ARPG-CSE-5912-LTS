using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolPath : MonoBehaviour
{
    const float vertexRadius = 0.3f;
    private void OnDrawGizmos()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Gizmos.color = new Color(200, 100, 0);
            Gizmos.DrawSphere(GetVertex(i), vertexRadius);
            Gizmos.color = Color.white;
            Gizmos.DrawLine(GetVertex(i), GetVertex(GetNextIndex(i)));
        }
    }

    public int GetNextIndex(int i)
    {
        if (i+1 == transform.childCount) { return 0; }
        return i + 1;
    }

    public Vector3 GetVertex(int i)
    {
        return transform.GetChild(i).position;
    }
}

