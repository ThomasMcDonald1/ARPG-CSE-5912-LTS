using UnityEngine;


//The most fundamental level of defining a character. Encompasses Player character, NPCs, and enemies.
public class Character : MonoBehaviour
{
    public Stats statScript;

    public float smooth;
    public float yVelocity;
    public virtual Transform AttackTarget { get; set; }

    protected virtual void Start()
    {
        smooth = 0.3f;
        yVelocity = 0.0f;
        AttackTarget = null;
        statScript = GetComponent<Stats>();
    }
    protected virtual void Update()
    {
    }
    //Put any code here that should be shared functionality across every type of character

}