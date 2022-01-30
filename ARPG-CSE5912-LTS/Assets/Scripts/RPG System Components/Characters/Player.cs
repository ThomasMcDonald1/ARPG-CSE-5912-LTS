using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour
{
    [SerializeField] private DialogueUI dialogueUI;

    public DialogueUI DialogueUI => dialogueUI;

    public IInteractable Interactable { get; set; }
    private Vector3 playerVelocity;
    private bool isMoving;
    private bool soundPlaying = false;

    public List<Ability> abilitiesKnown;
    [SerializeField] Ability basicAttack;
    [SerializeField] Ability fireballTest;
    public HashSet<Vector3> unlockedWaypoints;
    void Awake()
    {
        abilitiesKnown = new List<Ability>();
        abilitiesKnown.Add(basicAttack);
        abilitiesKnown.Add(fireballTest);
        unlockedWaypoints = new HashSet<Vector3>();
    }

    void Update()
    {
        //if (dialogueUI.IsOpen) return;
        playerVelocity = GetComponent<NavMeshAgent>().velocity;
        if(playerVelocity.magnitude > 0)
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }
        if (isMoving && !soundPlaying)
        {
            FindObjectOfType<AudioManager>().Play("Footsteps");
            soundPlaying = true;
        }
        else if (!isMoving)
        {
            FindObjectOfType<AudioManager>().Stop("Footsteps");
            soundPlaying = false;
        }
    }
}
