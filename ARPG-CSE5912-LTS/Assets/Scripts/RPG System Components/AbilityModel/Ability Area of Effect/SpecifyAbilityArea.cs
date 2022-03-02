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

    public override List<Character> PerformAOECheckToGetColliders(AbilityCast abilityCast)
    {
        //Debug.Log("AOE ability centered on: " + hit.point);
        //TODO: Make overlap sphere the same size as the targeting reticle
        Collider[] hitColliders = Physics.OverlapSphere(abilityCast.hit.point, aoeRadius);
        List<Character> characters = new List<Character>();

        foreach (Collider hitCollider in hitColliders)
        {
            Character character = hitCollider.gameObject.GetComponent<Character>();
            if (character != null)
            {
                //Debug.Log("Collider hit: " + hitCollider.name);
                characters.Add(character);
            }
        }
        return characters;
    }

    public override void DisplayAOEArea()
    {
        player.gameplayStateController.aoeReticleCylinder.transform.localScale = new Vector3(aoeRadius * 2, 1, aoeRadius * 2);
        player.gameplayStateController.aoeReticleCylinder.SetActive(true);
        abilityAreaNeedsShown = true;
    }
}
