using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class NPCManager : MonoBehaviour
{
    protected abstract bool Interactable();

    public virtual void Interact() {  }

}
