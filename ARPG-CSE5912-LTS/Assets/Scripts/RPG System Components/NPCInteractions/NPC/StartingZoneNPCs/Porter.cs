using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Porter : NPC
{
    [SerializeField] Lorekeeper lorekeeper;

    [Header("Ink JSON")]
    public List<TextAsset> DialogueJSON;
    public int currentStory;
    GameObject child;

    private void Start()
    {
        Player.InteractNPC += Interact;
        child = transform.GetChild(0).gameObject;
        currentStory = 0;
    }

    public override TextAsset GetCurrentDialogue()
    {
        return DialogueJSON[currentStory];
    }

    private void OnEnable()
    {
        InteractionManager.EndOfStoryEvent += NextStory;


    }
    private void OnDisable()
    {
        InteractionManager.EndOfStoryEvent -= NextStory;
    }

    private void NextStory(object sender, EventArgs e)
    {
        if (player.NPCTarget != this) return;
        switch (currentStory)
        {
            case 0:
                if (lorekeeper.CurrentStory == 0) { currentStory = 1; }
                else
                {
                    currentStory = 2;
                    InteractionManager.GetInstance().EnablePorterButton();
                }
                break;
            case 1:
                break;
            case 2:
                break;
            default:
                break;
        }
    }
    public override IEnumerator LookAtPlayer()
    {
        float time = 0.0f;
        float speed = 1.0f;
        Quaternion rotate = Quaternion.LookRotation(player.transform.position - child.transform.position);

        while (time < 1.0f)
        {
            child.transform.rotation = Quaternion.RotateTowards(child.transform.rotation, rotate, 50f * Time.deltaTime);
            child.transform.eulerAngles = new Vector3(0, child.transform.eulerAngles.y, 0);
            time += Time.deltaTime * speed;
            yield return null;
        }
    }

    protected override void Interact(object sender, EventArgs e)
    {
        if (Interactable())
        {
            if (currentStory >= 2)
            {
                Debug.Log(currentStory);
                InteractionManager.GetInstance().EnablePorterButton();
            }
            InteractionManager.GetInstance().DisableTradeButton();

            if (currentStory == 0)
            {
                InteractionManager.GetInstance().BeginDialogue();
            }
            else
            {
                InteractionManager.GetInstance().EnterOptionsMenu();
            }
            StartCoroutine(LookAtPlayer());
        }
    }
}
