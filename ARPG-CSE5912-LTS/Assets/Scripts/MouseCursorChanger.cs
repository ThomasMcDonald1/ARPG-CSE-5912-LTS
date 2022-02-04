using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseCursorChanger : MonoBehaviour
{
    [SerializeField] Texture2D defaultCursor;
    [SerializeField] Texture2D selectionCursor;

    private void Awake()
    {
        ChangeCursorToDefaultGraphic();
    }

    public void ChangeCursorToSelectionGraphic()
    {
        Cursor.SetCursor(selectionCursor, Vector2.zero, CursorMode.ForceSoftware);
    }

    public void ChangeCursorToDefaultGraphic()
    {
        Cursor.SetCursor(defaultCursor, Vector2.zero, CursorMode.ForceSoftware);
    }
}
