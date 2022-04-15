using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointInteraction : NPC
{
    private void OnEnable()
    {
        //InteractionManager.EndOfStoryEvent += NextStory;
        Player.InteractNPC += Interact;
    }
    private void OnDisable()
    {
        //InteractionManager.EndOfStoryEvent -= NextStory;
        Player.InteractNPC -= Interact;
    }

    private void NextStory(object sender, EventArgs e)
    {
        //if (player.NPCTarget != this) return;
    }
    public override IEnumerator LookAtPlayer()
    {
        float time = 0.0f;

        while (time < 1.0f)
        {
            time += Time.deltaTime;
            yield return null;
        }
    }

    protected override void Interact(object sender, EventArgs e)
    {
        InteractionManager.GetInstance().EnterWaypointMenu();
    }

    public override TextAsset GetCurrentDialogue()
    {
        throw new NotImplementedException();
    }
}
