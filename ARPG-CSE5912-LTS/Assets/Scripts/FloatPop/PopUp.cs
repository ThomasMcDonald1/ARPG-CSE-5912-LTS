using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PopUp : MonoBehaviour
{
    private TextMeshProUGUI popUpText;
    private float disappearTimer;
    private float disappearSpeed;
    private Color textColor;

    private void Awake()
    {
        popUpText = transform.GetComponent<TextMeshProUGUI>();
        
    }

    public void Setup(int Amount, bool isCrit, bool isDamage)
    {
        popUpText.text = Amount.ToString();
        if (isDamage) //damage
        {
            popUpText.color = new Color(0.9f, 0.2f, 0f, 1f); //red
            popUpText.fontSize = 200;
            if (isCrit)
            {
                popUpText.color = new Color(1f, 0.6f, 0f, 1f); //orange
                popUpText.fontSize = 300;
            }
        }
        else //heal
        {
            popUpText.color = new Color(0.1f, 0.8f, 0.4f, 1f); //green
            popUpText.fontSize = 200;
        }
        textColor = popUpText.color;
        disappearTimer = 1f;
        disappearSpeed = 2f;
    }

    private void Update()
    {
        disappearTimer -= Time.deltaTime;
        transform.position += new Vector3(0f, 2f, 0f) * Time.deltaTime;
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
