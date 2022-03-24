using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class LeapAbilityEffect : BaseAbilityEffect
{
    private const float LEAP_HEIGHT = 3f;
    private const int BEZ_NUM_POINTS = 25;
    public static event EventHandler<InfoEventArgs<(AbilityCast, Character)>> LeapDelayedDamageReadyEvent;

    protected override int OnApply(Character target, AbilityCast abilityCast)
    {
        NavMeshAgent agent = abilityCast.caster.GetComponent<NavMeshAgent>();
        agent.enabled = false;
        StartCoroutine(LeapToLocation(agent, abilityCast, target));
        return 0;
    }

    private IEnumerator LeapToLocation(NavMeshAgent agent, AbilityCast abilityCast, Character target)
    {
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
        agent.enabled = true;
        LeapDelayedDamageReadyEvent?.Invoke(this, new InfoEventArgs<(AbilityCast, Character)>((abilityCast, target)));
    }
}
