using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : NPCManager
{
    [SerializeField] Player player;
    GameObject child;
    public float smooth;
    public float yVelocity;

    private void Start()
    {
        child = transform.GetChild(0).gameObject;
        smooth = 0.3f;
        yVelocity = 0.0f;
    }

    protected override bool Interactable()
    {
        if (player.NPCTarget == this && player.InInteractNPCRange())
        {
            return true;
        }
        return false;
    }

    public override void Interact()
    {
        if (Interactable())
        {
           StartCoroutine(BeginInteraction());
        }
    }

    IEnumerator BeginInteraction()
    {
        Quaternion rotate = Quaternion.LookRotation(player.transform.position - child.transform.position);

        while (Interactable())
        {
            Debug.Log("Interacting!");
            child.transform.rotation = Quaternion.RotateTowards(child.transform.rotation, rotate, 50f * Time.deltaTime);
            child.transform.eulerAngles = new Vector3(0, child.transform.eulerAngles.y, 0);
            yield return null;
        }
        player.NPCTarget = null;
    }
}
