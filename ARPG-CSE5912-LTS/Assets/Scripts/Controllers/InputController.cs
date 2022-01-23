using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class InputController: MonoBehaviour
{
    private static InputController instance;
    public static InputController Instance { get { return instance; } }


    public static event EventHandler<InfoEventArgs<RaycastHit>> ClickEvent;
    public static event EventHandler<InfoEventArgs<bool>> ClickCanceledEvent;
    public static event EventHandler<InfoEventArgs<int>> CancelPressedEvent;
    Controls controls;
    private bool clickHeld;

    private void Awake()
    {
        DontDestroyOnLoad(this);
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
        controls = new Controls();
    }

    private void OnEnable()
    {
        controls.Gameplay.Enable();
        controls.Gameplay.Interact.performed += OnClickPressed;
        controls.Gameplay.Interact.canceled += OnClickCanceled;
        controls.Gameplay.Cancel.performed += OnCancelPressed;
    }

    private void OnClickPressed(InputAction.CallbackContext context)
    {
        //Check if cursor is over a UI gameobject
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        clickHeld = true;
        StartCoroutine(ClickHeldCoroutine());
    }

    private void OnClickCanceled(InputAction.CallbackContext context)
    {
        if (clickHeld)
        {
            clickHeld = false;
            ClickCanceledEvent?.Invoke(this, new InfoEventArgs<bool>(clickHeld));
        }
    }

    private void OnCancelPressed(InputAction.CallbackContext context)
    {
        CancelPressedEvent?.Invoke(this, new InfoEventArgs<int>(0));
    }

    IEnumerator ClickHeldCoroutine()
    {
        while (clickHeld)
        {
            //Otherwise, cast ray to gameplay environment
            Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
            RaycastHit rcHit;

            if (Physics.Raycast(ray, out rcHit))
            {
                ClickEvent?.Invoke(this, new InfoEventArgs<RaycastHit>(rcHit));
            }

            yield return null;
        }
    }
}
