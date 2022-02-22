using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamagePopUp : MonoBehaviour
{
    private TextMeshProUGUI popUpText;

    private void Awake()
    {
        popUpText = transform.GetComponent<TextMeshProUGUI>();
    }

    public void Setup(int damageAmount)
    {
        popUpText.text = damageAmount.ToString();
    }
}
