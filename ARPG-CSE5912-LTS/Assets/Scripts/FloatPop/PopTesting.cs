using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopTesting : MonoBehaviour
{
    [SerializeField] private Transform DamagePop;
    private void Start()
    {
        Transform damagePopUpTransform = Instantiate(DamagePop, Vector3.zero, Quaternion.identity);
        damagePopUpTransform.SetParent(transform, false);
        DamagePopUp damagePopUp = damagePopUpTransform.GetComponent<DamagePopUp>();
        damagePopUp.Setup(300);
    }

}
