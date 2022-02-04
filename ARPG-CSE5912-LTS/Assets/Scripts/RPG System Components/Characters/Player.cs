using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] private DialogueUI dialogueUI;

    [SerializeField] private InventoryUI uiInventory;
    private Inventory inventory;
    public DialogueUI DialogueUI => dialogueUI;

    public IInteractable Interactable { get; set; }
    private Vector3 playerVelocity;
    private bool isMoving;
    private bool soundPlaying = false;

    public List<Ability> abilitiesKnown;
    [SerializeField] Ability basicAttack;
    [SerializeField] Ability fireballTest;

    //[SerializeField] Camera mainCamera;
    private Camera mainCamera;
    void Awake()
    {
        inventory = new Inventory();
       
        abilitiesKnown = new List<Ability>();
        abilitiesKnown.Add(basicAttack);
        abilitiesKnown.Add(fireballTest);
        uiInventory.SetInventory(inventory);
        mainCamera = Camera.main;
        ItemWorld.SpawnItemWorld(new Vector3(-4.83f,1.13f,14.05f), new InventoryItems { itemType= InventoryItems.ItemType.Coin, amount =1});

    }
  
    private void detectObj()
    {

        Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
        RaycastHit hit;
        if (Mouse.current.leftButton.wasReleasedThisFrame)
        {
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider != null)
                {
                    Debug.Log("hit: " + hit.transform.gameObject.tag);
                    if(hit.transform.gameObject.tag == "InventoryItem")
                    {
                        InventoryItems item = hit.transform.gameObject.GetComponent<ItemWorld>().getItem();
                        inventory.AddItem(item);
                        hit.transform.gameObject.GetComponent<ItemWorld>().DestroySelf();

                    }
                    
                }
            }
        }
        //Debug.Log("In detect obj func");
        
    }
    void Update()
    {
        //if (dialogueUI.IsOpen) return;
        detectObj();
        //onTriggerEnter();
        //Debug.Log("In detect obj func");
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
