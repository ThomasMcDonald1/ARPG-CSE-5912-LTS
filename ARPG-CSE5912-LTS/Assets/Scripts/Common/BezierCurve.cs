using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BezierCurve
{
    /// <summary>
    /// Create a List of Vector3 that represent the points along a quadratic Bezier curve.
    /// </summary>
    /// <param name="numPoints">The desired number of points to represent the completed Bezier curve.</param>
    /// <param name="start">Starting control point of the Bezier curve.</param>
    /// <param name="middle">Middle control point of the Bezier curve.</param>
    /// <param name="end">Ending control point of the Bezier curve.</param>
    /// <returns></returns>
    public static List<Vector3> Quadratic(int numPoints, Vector3 start, Vector3 middle, Vector3 end)
    {
        List<Vector3> finalPath = new List<Vector3>();
        finalPath.Add(start);
        for (int i = 1; i < numPoints + 1; i++)
        {
            float t = i / (float)numPoints;
            finalPath.Add(CalculateBezierPoint(start, middle, end, t));
        }
        finalPath.Add(end);
        return finalPath;
    }

    //Calculate the next point on the curve
    private static Vector3 CalculateBezierPoint(Vector3 p0, Vector3 p1, Vector3 p2, float time)
    {
        float u = 1 - time;
        float tSquared = time * time;
        float uSquared = u * u;
        Vector3 pointAlongCurve = uSquared * p0;
        pointAlongCurve += 2 * u * time * p1;
        pointAlongCurve += tSquared * p2;
        return pointAlongCurve;
    }

    /// <summary>
    /// Find a middle control point exactly halfway between the start and end points for a quadratic Bezier curve.
    /// </summary>
    /// <param name="start">Starting control point of the Bezier curve.</param>
    /// <param name="end">Ending control point of the Bezier curve.</param>
    /// <param name="objRightVector">The transform.right vector of the object to be moved along the bezier curve, if the object's transform.forward faces the end point.</param>
    /// <param name="height">The desired height of the middle control point.</param>
    /// <returns></returns>
    public static Vector3 GetMiddleControlPointUpwardArcingQuadratic(Vector3 start, Vector3 end, Vector3 objRightVector, float height)
    {
        Vector3 dir = (end - start).normalized;
        Vector3 midPoint = (end + start) / 2;
        Vector3 normalVectorDir = Vector3.Cross(dir, objRightVector).normalized;
        float normalVectorDist = height;
        Vector3 normalVector = normalVectorDist * normalVectorDir;
        if (normalVector.y < 0)
            normalVector = -normalVector;
        Vector3 normalEndPoint = midPoint + normalVector;

        return normalEndPoint;
    }
}
