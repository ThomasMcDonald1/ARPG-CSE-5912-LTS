using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability : MonoBehaviour
{
    public string description;
    public Sprite icon;
    public GameObject spellcastVFXObj;

    ////Add events here for checking if an ability can be performed, if an ability fails, if an ability was performed, if an ability crits, etc

    //public bool CanPerform()
    //{
    //    //TODO: Check if an ability can be performed here
    //    return true;
    //}

    //// check if a specific effect of an ability targets a specific type of character
    //public bool IsTarget(Character character)
    //{
    //    Transform thisAbility = transform;

    //    for (int i = 0; i < thisAbility.childCount; i++)
    //    {
    //        AbilityEffectTarget targeter = thisAbility.GetChild(i).GetComponent<AbilityEffectTarget>();
    //        //this only passes in the character, but the class that reads it will have a specific override method for IsTarget that will
    //        //find the type of target that is being targeted and determine if the ability targets it
    //        if (targeter.IsTarget(character))
    //        {
    //            return true;
    //        }
    //    }
    //    return false;
    //}

    //// Perform an ability (AI may also be able to use this function)
    //public void Perform(List<Character> characters)
    //{
    //    if (!CanPerform())
    //    {
    //        //maybe post an event that the ability can't be performed
    //        return;
    //    }

    //    for (int i = 0; i < characters.Count; i++)
    //    {
    //        Perform(characters[i]);
    //    }
    //    // maybe post an event that the ability was performed
    //}

    //void Perform(Character character)
    //{
    //    for (int i = 0; i < transform.childCount; i++)
    //    {
    //        Transform child = transform.GetChild(i);
    //        BaseAbilityEffect effect = child.GetComponent<BaseAbilityEffect>();
    //        effect.Apply(character);
    //    }
    //}
}
