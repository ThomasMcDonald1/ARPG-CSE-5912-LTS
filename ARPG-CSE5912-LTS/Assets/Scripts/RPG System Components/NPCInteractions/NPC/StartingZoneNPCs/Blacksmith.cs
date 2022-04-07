using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blacksmith : NPC
{
    [Header("Ink JSON")]

    // Hank TODO

    [SerializeField] public Shop shop;
    [SerializeField] public UI_shop shopUI;
    [SerializeField] public UI_Sale saleUI;


    public List<TextAsset> DialogueJSON;
    private int currentStory;
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
                currentStory++;
                break;
            case 1:
                break;
            case 2:
                break;
            default:
                break;
        }
    }

    protected override void Interact(object sender, EventArgs e)
    {
        if (Interactable())
        {
            InteractionManager.GetInstance().EnableTradeButton();
            InteractionManager.GetInstance().DisablePorterButton();
            if (currentStory == 0)
            {
                InteractionManager.GetInstance().BeginDialogue();
            }
            else
            {
                InteractionManager.GetInstance().EnterOptionsMenu();
            }

            shopUI.initializeShop(shop);
            saleUI.shop = shop;
            StartCoroutine(LookAtPlayer());
        }
    }
}
