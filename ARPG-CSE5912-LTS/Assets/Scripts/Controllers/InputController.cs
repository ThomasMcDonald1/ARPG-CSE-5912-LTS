using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InputController : MonoBehaviour
{
    private static InputController instance;
    public static InputController Instance { get { return instance; } }

    [SerializeField] GameObject gameplayUICanvas;
    GraphicRaycaster uiRaycaster;

    //Events associated with input being pressed
    public static event EventHandler<InfoEventArgs<RaycastHit>> ClickEvent;
    public static event EventHandler<InfoEventArgs<RaycastHit>> ClickCanceledEvent;
    public static event EventHandler<InfoEventArgs<int>> SecondaryClickPressedEvent;
    public static event EventHandler<InfoEventArgs<int>> CancelPressedEvent;
    public static event EventHandler<InfoEventArgs<int>> CharacterMenuPressedEvent;
    public static event EventHandler<InfoEventArgs<int>> StationaryButtonPressedEvent;
    public static event EventHandler<InfoEventArgs<bool>> StationaryButtonCanceledEvent;
    public static event EventHandler<InfoEventArgs<int>> Potion1PressedEvent;
    public static event EventHandler<InfoEventArgs<int>> Potion2PressedEvent;
    public static event EventHandler<InfoEventArgs<int>> Potion3PressedEvent;
    public static event EventHandler<InfoEventArgs<int>> Potion4PressedEvent;
    public static event EventHandler<InfoEventArgs<int>> ActionBar1PressedEvent;
    public static event EventHandler<InfoEventArgs<int>> ActionBar2PressedEvent;
    public static event EventHandler<InfoEventArgs<int>> ActionBar3PressedEvent;
    public static event EventHandler<InfoEventArgs<int>> ActionBar4PressedEvent;
    public static event EventHandler<InfoEventArgs<int>> ActionBar5PressedEvent;
    public static event EventHandler<InfoEventArgs<int>> ActionBar6PressedEvent;
    public static event EventHandler<InfoEventArgs<int>> ActionBar7PressedEvent;
    public static event EventHandler<InfoEventArgs<int>> ActionBar8PressedEvent;
    public static event EventHandler<InfoEventArgs<int>> ActionBar9PressedEvent;
    public static event EventHandler<InfoEventArgs<int>> ActionBar10PressedEvent;
    public static event EventHandler<InfoEventArgs<int>> ActionBar11PressedEvent;
    public static event EventHandler<InfoEventArgs<int>> ActionBar12PressedEvent;
    public static event EventHandler<InfoEventArgs<List<RaycastResult>>> UIElementLeftClickedEvent;
    public static event EventHandler<InfoEventArgs<List<RaycastResult>>> UIElementRightClickedEvent;
    public static event EventHandler<InfoEventArgs<int>> OpenPassiveTreeEvent;

    // Test mouse wheel zooming
    public static event EventHandler<InfoEventArgs<float>> DetectMouseScrollWheelEvent;

    Controls controls;
    private bool clickHeld;
    private bool stationaryHeld;

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

        uiRaycaster = gameplayUICanvas.GetComponent<GraphicRaycaster>();
    }

    private void FindCanvas()
    {
        if (gameplayUICanvas == null)
        {
            gameplayUICanvas = GameObject.Find("GameplayUICanvas");
            if (gameplayUICanvas != null)
            {
                uiRaycaster = gameplayUICanvas.GetComponent<GraphicRaycaster>();
            }
            if (gameplayUICanvas == null)
            {
                gameplayUICanvas = GameObject.Find("MainMenuCanvas");
                if (gameplayUICanvas != null)
                {
                    uiRaycaster = gameplayUICanvas.GetComponent<GraphicRaycaster>();
                }
            }
        }
    }

    private void Update()
    {
        FindCanvas();
        if (EventSystem.current.IsPointerOverGameObject() && Mouse.current.rightButton.wasReleasedThisFrame)
        {
            List<RaycastResult> results = GetUIElementsClicked();
            UIElementRightClickedEvent?.Invoke(this, new InfoEventArgs<List<RaycastResult>>(results));
        }
    }

    private void OnEnable()
    {
        controls.Gameplay.Enable();
        controls.Gameplay.Interact.performed += OnClickPressed;
        controls.Gameplay.Interact.canceled += OnClickCanceled;
        controls.Gameplay.SecondaryClick.performed += OnSecondaryClickPressed;
        controls.Gameplay.CharacterMenu.performed += OnCharacterMenuPressed;
        controls.Gameplay.Stationary.performed += OnStationaryPressed;
        controls.Gameplay.Stationary.canceled += OnStationaryCanceled;
        controls.Gameplay.Cancel.performed += OnCancelPressed;
        controls.Gameplay.Potion1.performed += OnPotion1Pressed;
        controls.Gameplay.Potion2.performed += OnPotion2Pressed;
        controls.Gameplay.Potion3.performed += OnPotion3Pressed;
        controls.Gameplay.Potion4.performed += OnPotion4Pressed;
        controls.Gameplay.ActionBar1.performed += OnActionBar1Pressed;
        controls.Gameplay.ActionBar2.performed += OnActionBar2Pressed;
        controls.Gameplay.ActionBar3.performed += OnActionBar3Pressed;
        controls.Gameplay.ActionBar4.performed += OnActionBar4Pressed;
        controls.Gameplay.ActionBar5.performed += OnActionBar5Pressed;
        controls.Gameplay.ActionBar6.performed += OnActionBar6Pressed;
        controls.Gameplay.ActionBar7.performed += OnActionBar7Pressed;
        controls.Gameplay.ActionBar8.performed += OnActionBar8Pressed;
        controls.Gameplay.ActionBar9.performed += OnActionBar9Pressed;
        controls.Gameplay.ActionBar10.performed += OnActionBar10Pressed;
        controls.Gameplay.ActionBar11.performed += OnActionBar11Pressed;
        controls.Gameplay.ActionBar12.performed += OnActionBar12Pressed;
        controls.Gameplay.OpenPassiveTree.performed += OnOpenPassiveTree;

        // Testing new zoom
        controls.Gameplay.CameraZoom.performed += OnZoomScrolled;

    }

    private void OnClickPressed(InputAction.CallbackContext context)
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            List<RaycastResult> results = GetUIElementsClicked();
            UIElementLeftClickedEvent?.Invoke(this, new InfoEventArgs<List<RaycastResult>>(results));
        }
        else
        {
            clickHeld = true;
            StartCoroutine(ClickHeldCoroutine());
        }
    }

    private void OnClickCanceled(InputAction.CallbackContext context)
    {
        if (clickHeld)
        {
            clickHeld = false;
            ClickCanceledEvent?.Invoke(this, new InfoEventArgs<RaycastHit>());
        }
    }

    private void OnSecondaryClickPressed(InputAction.CallbackContext context)
    {
        SecondaryClickPressedEvent?.Invoke(this, new InfoEventArgs<int>(25));
    }

    private void OnCharacterMenuPressed(InputAction.CallbackContext context)
    {
        CharacterMenuPressedEvent?.Invoke(this, new InfoEventArgs<int>(26));
    }

    private void OnStationaryPressed(InputAction.CallbackContext context)
    {
        stationaryHeld = true;
        StartCoroutine(StationaryHeldCoroutine());
    }

    private void OnStationaryCanceled(InputAction.CallbackContext context)
    {
        if (stationaryHeld)
        {
            stationaryHeld = false;
            StationaryButtonCanceledEvent?.Invoke(this, new InfoEventArgs<bool>());
        }
    }

    private void OnCancelPressed(InputAction.CallbackContext context)
    {
        CancelPressedEvent?.Invoke(this, new InfoEventArgs<int>(0));
    }

    private void OnPotion1Pressed(InputAction.CallbackContext context)
    {
        Potion1PressedEvent?.Invoke(this, new InfoEventArgs<int>(21));
    }

    private void OnPotion2Pressed(InputAction.CallbackContext context)
    {
        Potion2PressedEvent?.Invoke(this, new InfoEventArgs<int>(22));
    }

    private void OnPotion3Pressed(InputAction.CallbackContext context)
    {
        Potion3PressedEvent?.Invoke(this, new InfoEventArgs<int>(23));
    }

    private void OnPotion4Pressed(InputAction.CallbackContext context)
    {
        Potion4PressedEvent?.Invoke(this, new InfoEventArgs<int>(24));
    }

    private void OnActionBar1Pressed(InputAction.CallbackContext context)
    {
        ActionBar1PressedEvent?.Invoke(this, new InfoEventArgs<int>(1));
    }

    private void OnActionBar2Pressed(InputAction.CallbackContext context)
    {
        ActionBar2PressedEvent?.Invoke(this, new InfoEventArgs<int>(2));
    }
    private void OnActionBar3Pressed(InputAction.CallbackContext context)
    {
        ActionBar3PressedEvent?.Invoke(this, new InfoEventArgs<int>(3));
    }

    private void OnActionBar4Pressed(InputAction.CallbackContext context)
    {
        ActionBar4PressedEvent?.Invoke(this, new InfoEventArgs<int>(4));
    }

    private void OnActionBar5Pressed(InputAction.CallbackContext context)
    {
        ActionBar5PressedEvent?.Invoke(this, new InfoEventArgs<int>(5));
    }

    private void OnActionBar6Pressed(InputAction.CallbackContext context)
    {
        ActionBar6PressedEvent?.Invoke(this, new InfoEventArgs<int>(6));
    }

    private void OnActionBar7Pressed(InputAction.CallbackContext context)
    {
        ActionBar7PressedEvent?.Invoke(this, new InfoEventArgs<int>(7));
    }

    private void OnActionBar8Pressed(InputAction.CallbackContext context)
    {
        ActionBar8PressedEvent?.Invoke(this, new InfoEventArgs<int>(8));
    }

    private void OnActionBar9Pressed(InputAction.CallbackContext context)
    {
        ActionBar9PressedEvent?.Invoke(this, new InfoEventArgs<int>(9));
    }

    private void OnActionBar10Pressed(InputAction.CallbackContext context)
    {
        ActionBar10PressedEvent?.Invoke(this, new InfoEventArgs<int>(10));
    }
    private void OnActionBar11Pressed(InputAction.CallbackContext context)
    {
        ActionBar11PressedEvent?.Invoke(this, new InfoEventArgs<int>(11));
    }

    private void OnActionBar12Pressed(InputAction.CallbackContext context)
    {
        ActionBar12PressedEvent?.Invoke(this, new InfoEventArgs<int>(12));
    }

    private List<RaycastResult> GetUIElementsClicked()
    {
        PointerEventData eventData = new PointerEventData(EventSystem.current)
        {
            position = Mouse.current.position.ReadValue()
        };
        List<RaycastResult> results = new List<RaycastResult>();
        if (uiRaycaster == null)
        {
            FindCanvas();
        }
        uiRaycaster.Raycast(eventData, results);

        return results;
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

    //TODO: Write what happens when you hold the stationary button (most likely Shift)
    IEnumerator StationaryHeldCoroutine()
    {
        yield return null;
    }
    private void OnOpenPassiveTree(InputAction.CallbackContext context)
    {
        OpenPassiveTreeEvent?.Invoke(this, new InfoEventArgs<int>(27));
    }

    // Testing camera zoom
    private void OnZoomScrolled(InputAction.CallbackContext context)
    {
        DetectMouseScrollWheelEvent?.Invoke(this, new InfoEventArgs<float>(context.ReadValue<float>()));
    }
}
