using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private DialogueUI dialogueUI;

    public DialogueUI DialogueUI => dialogueUI;

    public IInteractable Interactable {get; set; }

    public Controls controls;
    private bool interactHeld;

    private void Awake()
    {
        controls = new Controls();
    }

    void Update()
    {
        //if (dialogueUI.IsOpen) return;
        //if (Input.GetMouseButton(0))
        //{
        //   MoveToCursor();
        //}
        //if (Input.GetKeyDown(KeyCode.E))
        //{
        //    if (Interactable != null)
        //    {
        //        Interactable.Interact(player: this);
        //    }
        //}
    }

    private void OnEnable()
    {
        controls.Gameplay.Enable();
        controls.Gameplay.Interact.performed += OnInteractPressed;
        controls.Gameplay.Interact.canceled += OnInteractCanceled;
    }

    private void OnDisable()
    {
        controls.Gameplay.Interact.performed -= OnInteractPressed;
        controls.Gameplay.Interact.canceled -= OnInteractCanceled;
    }

    void OnInteractPressed(InputAction.CallbackContext context)
    {
        interactHeld = true;
        StartCoroutine(InteractHeldCoroutine());

        //TODO: If cursor is over the ground, then move
        //TODO: If cursor on NPC, interact with NPC
        //TODO: If cursor on enemy, attack
        //TODO: etc
    }

    void OnInteractCanceled(InputAction.CallbackContext context)
    {
        if (interactHeld)
        {
            interactHeld = false;
        }
    }

    IEnumerator InteractHeldCoroutine()
    {
        while (interactHeld)
        {
            MoveToCursor();

            yield return null; 
        } 
    }

    private void MoveToCursor()
    {
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        RaycastHit rcHit;
        if (Physics.Raycast(ray, out rcHit))
        {
            GetComponent<NavMeshAgent>().destination = rcHit.point;
        }
    }
}

