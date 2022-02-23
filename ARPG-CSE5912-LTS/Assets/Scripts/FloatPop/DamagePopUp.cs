using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamagePopUp : MonoBehaviour
{
    private TextMeshProUGUI popUpText;
    private float disappearTimer;
    private float disappearSpeed;
    private Color textColor;

    private void Awake()
    {
        popUpText = transform.GetComponent<TextMeshProUGUI>();
        
    }

    public void Setup(int damageAmount)
    {
        popUpText.text = damageAmount.ToString();
        textColor = popUpText.color;
        disappearTimer = 1f;
        disappearSpeed = 2f;
    }

    private void Update()
    {
        disappearTimer -= Time.deltaTime;
        if (disappearTimer < 0)
        {
            textColor.a -= disappearSpeed * Time.deltaTime;
            popUpText.color = textColor;

            if (textColor.a <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
