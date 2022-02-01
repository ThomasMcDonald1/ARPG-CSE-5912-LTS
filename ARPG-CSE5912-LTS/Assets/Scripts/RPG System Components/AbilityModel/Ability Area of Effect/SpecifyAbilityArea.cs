using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SpecifyAbilityArea : BaseAbilityArea
{
    public int aoeRadius;
    Player player;
    int groundLayerMask = 1 << 6;

    private void Awake()
    {
        player = GetComponentInParent<Player>();
    }

    private void FixedUpdate()
    {
        if (abilityAreaNeedsShown)
        {
            //Get the point upon which to center the indicator
            Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());

            if (Physics.Raycast(ray, out RaycastHit hit, groundLayerMask))
            {
                player.gameplayStateController.aoeReticleCylinder.transform.position = hit.point;
            }
        }
    }

    public override void PerformAOE(RaycastHit hit)
    {
        Debug.Log("AOE ability centered on: " + hit.point);
        //TODO: Make overlap sphere the same size as the targeting reticle
        Collider[] hitColliders = Physics.OverlapSphere(hit.point, aoeRadius);

        foreach (Collider hitCollider in hitColliders)
        {
            Debug.Log("Collider hit: " + hitCollider.name);
            Character character = hitCollider.gameObject.GetComponent<Character>();
            if (character != null)
            {
                //Do ability effects on character

            }
        }
    }

    public override void DisplayAOEArea()
    {
        player.gameplayStateController.aoeReticleCylinder.transform.localScale = new Vector3(aoeRadius * 2, 1, aoeRadius * 2);
        player.gameplayStateController.aoeReticleCylinder.SetActive(true);
        abilityAreaNeedsShown = true;
    }
}
