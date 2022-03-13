using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lorekeeper : NPC
{
    [Header("Ink JSON")]
    [SerializeField]
    private List<TextAsset> DialogueJSON;
    private int currentStory;
    GameObject child;
    QuestGiver questGiver;
    private void OnEnable()
    {
        QuestGiver.QuestCompleteEvent += OnQuestComplete;
    }
    private void OnDisable()
    {
        QuestGiver.QuestCompleteEvent -= OnQuestComplete;
    }
    private void OnQuestComplete(object sender, InfoEventArgs<(int, int)> e)
    {
        NextStory();
    }
    private void Start()
    {
        child = transform.GetChild(0).gameObject;
        currentStory = 0;
        questGiver = this.GetComponent<QuestGiver>(); 
    }

    public override TextAsset GetCurrentDialogue()
    {
        return DialogueJSON[currentStory];
    }

    protected override IEnumerator BeginInteraction()
    {
        questGiver.AddQuestToLogIfNew();//add quest to quest log if havent already added
        Quaternion rotate = Quaternion.LookRotation(player.transform.position - child.transform.position);

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
