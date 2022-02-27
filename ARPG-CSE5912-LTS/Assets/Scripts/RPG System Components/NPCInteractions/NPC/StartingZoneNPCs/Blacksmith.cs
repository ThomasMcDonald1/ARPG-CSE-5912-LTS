using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blacksmith : NPC
{
    [Header("Ink JSON")]
    [SerializeField] public Shop shop;
    [SerializeField] public UI_shop shopUI;
    private List<TextAsset> DialogueJSON;
    private int currentStory;
    GameObject child;

    private void Start()
    {
        child = transform.GetChild(0).gameObject;
        currentStory = 0;
    }

    public override TextAsset GetCurrentDialogue()
    {
        return DialogueJSON[currentStory];
    }

    protected override IEnumerator BeginInteraction()
    {
        Quaternion rotate = Quaternion.LookRotation(player.transform.position - child.transform.position);
        shopUI.initializeShop(shop);

        while (Interactable())
        {
            child.transform.rotation = Quaternion.RotateTowards(child.transform.rotation, rotate, 50f * Time.deltaTime);
            child.transform.eulerAngles = new Vector3(0, child.transform.eulerAngles.y, 0);
            SetMenu();
            yield return null;
        }
        shopUI.resetShop();

        InteractionManager.GetInstance().StopInteraction();
        InteractionManager.GetInstance().DisableInteractionView();
    }

    public override void NextStory()
    {
        if (currentStory < DialogueJSON.Count - 1)
        {
            currentStory++;
        }
    }
}
