using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseAbilityRange : MonoBehaviour
{
    //The number representing the maximum distance from the caster that an ability can reach
    public float range = 5;

    //directionOriented should be true when the range is a pattern like a cone or a line.
    //When it's true, player will hold the mouse cursor in a direction away from the player character
    //to change the character's facing direction while the ability is readied so the targeted area changes.
    //When it's false, while the ability is readied, the player may move the cursor to select anywhere within
    //range without the character changing its facing. If the cursor is moved outside the ability range, it 
    //should not be able to be used upon a click, and the cursor should change color to red to indicate
    //that the cursor has been moved outside the ability's maximum range.
    public virtual bool directionOriented { get { return false; } } //not yet implemented functionality 

    //The Character that is casting the ability
    protected Character caster { get { return GetComponentInParent<Character>(); } }

    //All concrete classes will have to implement this to grab the characters that are specifically within the 
    //ability's range (could be useful for AI to decide what to attack or heal/buff/etc)
    public abstract List<Character> GetCharactersInRange();
    public abstract Type GetAbilityRange();
}
