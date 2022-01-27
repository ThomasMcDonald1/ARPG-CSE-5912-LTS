using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseAbilityArea : MonoBehaviour
{
    public abstract List<Character> GetCharactersInAOE(RaycastHit hit);
}
