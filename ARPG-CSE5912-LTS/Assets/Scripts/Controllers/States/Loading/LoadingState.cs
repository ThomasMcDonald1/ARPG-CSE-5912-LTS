using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingState : State
{
    protected LoadingStateController loadingStateController;

    protected virtual void Awake()
    {
        loadingStateController = GetComponent<LoadingStateController>();
    }
}

