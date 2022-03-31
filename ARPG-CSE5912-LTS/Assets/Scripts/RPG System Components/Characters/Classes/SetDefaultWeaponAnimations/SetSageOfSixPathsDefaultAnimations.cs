using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetSageOfSixPathsDefaultAnimations : MonoBehaviour
{
    void Start()
    {
        GetComponent<SetAnimationType>().ChangeToSummon();
    }
}
