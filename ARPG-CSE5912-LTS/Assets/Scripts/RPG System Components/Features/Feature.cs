using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Feature: MonoBehaviour
{
    protected GameObject Target { get; private set; }

    public void Activate(GameObject target)
    {
        if (Target == null)
        {
            Target = target;
            OnActivate();
        }
    }

    public void Deactivate()
    {
        if (Target != null)
        {
            OnDeactivate();
            Target = null;
        }
    }

    protected abstract void OnActivate();
    protected virtual void OnDeactivate() { }
}
