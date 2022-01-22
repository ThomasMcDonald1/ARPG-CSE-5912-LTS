using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingStateController : StateMachine
{
    public GameObject loadingSceneCanvasObj;

    [HideInInspector] public Canvas loadingSceneCanvas;

    private void Awake()
    {
        loadingSceneCanvas = loadingSceneCanvasObj.GetComponent<Canvas>();
        loadingSceneCanvas.enabled = true;
        ChangeState<LoadingState>();
    }
}