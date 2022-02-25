using ARPG.Combat;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAbiityEffectTarget : AbilityEffectTarget
{
    public override bool IsTarget(Character character)
    {
        if (character == null)
            return false;
        return character.GetComponent<Enemy>() != null; 
    }
}
