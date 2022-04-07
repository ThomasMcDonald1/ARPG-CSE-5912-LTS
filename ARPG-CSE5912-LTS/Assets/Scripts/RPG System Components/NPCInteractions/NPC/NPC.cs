using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using Ink.Runtime;
using System;

public abstract class NPC : MonoBehaviour
{


    [SerializeField] protected Player player;

    public float smooth;
    public float yVelocity;

    protected bool hasNewInfo;

    private void Start()
    {
        // Player.InteractNPC += Interact;
        hasNewInfo = false;

        smooth = 0.3f;
        yVelocity = 0.0f;
    }

    protected bool Interactable()
    {
        if (player.NPCTarget == this && player.InInteractNPCRange())
        {
            return true;
        }
        return false;
    }

    protected abstract void Interact(object sender, EventArgs e);

    public abstract IEnumerator LookAtPlayer();

    public string GetName()
    {
        return this.tag;
    }

    public abstract TextAsset GetCurrentDialogue();
    //public abstract void NextStory();
}
