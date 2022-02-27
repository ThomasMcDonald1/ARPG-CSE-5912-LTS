using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralStore : NPC
{

    [Header("Ink JSON")]
    [SerializeField] public Shop shop;
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
        UI_shop.instance.shop = shop;
        while (Interactable())
        {
            child.transform.rotation = Quaternion.RotateTowards(child.transform.rotation, rotate, 50f * Time.deltaTime);
            child.transform.eulerAngles = new Vector3(0, child.transform.eulerAngles.y, 0);
            SetMenu();
            yield return null;
        }
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
