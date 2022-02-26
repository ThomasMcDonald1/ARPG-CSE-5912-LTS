using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointBlankAbilityArea : BaseAbilityArea
{
    Player player;

    private void Awake()
    {
        player = GetComponentInParent<Player>();
    }

    public override void DisplayAOEArea()
    {
        player.gameplayStateController.aoeReticleCylinder.transform.localScale = new Vector3(aoeRadius * 2, 1, aoeRadius * 2);
        player.gameplayStateController.aoeReticleCylinder.transform.position = player.transform.position;
        player.gameplayStateController.aoeReticleCylinder.SetActive(true);
        abilityAreaNeedsShown = true;
    }

    public override List<Character> PerformAOECheckToGetColliders(AbilityCast abilityCast)
    {
        Collider[] hitColliders = Physics.OverlapSphere(abilityCast.caster.transform.position, aoeRadius);
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
        abilityCast.charactersAffected = characters;
        return characters;
    }

    public override Type GetAbilityArea()
    {
        return typeof(PointBlankAbilityArea);
    }
}
