using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class LeapAbilityMovement : BaseAbilityMovement
{
    private const float LEAP_HEIGHT = 3f;
    private const int BEZ_NUM_POINTS = 25;

    public override void QueueSpecialMovement(AbilityCast abilityCast, List<Character> targets)
    {
        NavMeshAgent agent = abilityCast.caster.GetComponent<NavMeshAgent>();
        agent.isStopped = true;
        StartCoroutine(LeapToLocation(agent, abilityCast, targets));
    }

    private IEnumerator LeapToLocation(NavMeshAgent agent, AbilityCast abilityCast, List<Character> targets)
    {
        Debug.Log("Leaping to location selected");
        abilityCast.caster.transform.LookAt(abilityCast.hit.point);
        Vector3 start = abilityCast.caster.transform.position;
        Vector3 end = abilityCast.hit.point;
        float dist = Vector3.Distance(start, end);
        Vector3 mid = BezierCurve.GetMiddleControlPointUpwardArcingQuadratic(start, end, abilityCast.caster.transform.right, LEAP_HEIGHT);
        List<Vector3> bezierCurvePoints = BezierCurve.Quadratic(BEZ_NUM_POINTS, start, mid, end);

        int currentPoint = 0;
        while (currentPoint < bezierCurvePoints.Count - 1)
        {
            abilityCast.caster.transform.position = Vector3.MoveTowards(bezierCurvePoints[currentPoint], bezierCurvePoints[currentPoint + 1], dist / BEZ_NUM_POINTS);
            abilityCast.caster.transform.rotation = Quaternion.LookRotation(abilityCast.caster.transform.forward, abilityCast.caster.transform.up);
            currentPoint++;
            yield return null;
        }
        agent.isStopped = false;
        agent.destination = end;
        CompleteSpecialMovement(abilityCast, targets);
    }
}
