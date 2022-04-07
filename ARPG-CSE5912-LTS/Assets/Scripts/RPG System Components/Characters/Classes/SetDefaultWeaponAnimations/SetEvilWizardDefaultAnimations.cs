using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetEvilWizardDefaultAnimations : MonoBehaviour
{
    void Start()
    {
        GetComponent<SetAnimationType>().ChangeToMage();
    }
}
