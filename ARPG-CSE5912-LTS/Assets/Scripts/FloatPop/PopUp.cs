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

    public void Setup(string text, bool isCrit, Color color)
    {
        if (isCrit)
            popUpText.fontSize = 300;
        else
            popUpText.fontSize = 200;

        popUpText.text = text;
        popUpText.color = color;

        textColor = popUpText.color;
        disappearTimer = 1f;
        disappearSpeed = 2f;
        float x = popUpText.rectTransform.position.x;
        float y = popUpText.rectTransform.position.y;
        float z = popUpText.rectTransform.position.z;
        if (color == PopTesting.fireColor)
        {
            x -= 1f;
            y += Random.Range(-0.2f, 0.2f);
            popUpText.fontSize = 150;
        }       
        else if (color == PopTesting.iceColor)
        {
            x -= 0.5f;
            y += Random.Range(-0.2f, 0.2f);
            popUpText.fontSize = 150;
        }
        else if (color == PopTesting.lightningColor)
        {
            x += 0.5f;
            y += Random.Range(-0.2f, 0.2f);
            popUpText.fontSize = 150;
        }
        else if (color == PopTesting.poisonColor)
        {
            x += 1f;
            y += Random.Range(-0.2f, 0.2f);
            popUpText.fontSize = 150;
        }
        else if (color == PopTesting.abilityDamageColor)
        {
            y += 1f;
        }
        popUpText.rectTransform.position = new Vector3(x, y, z);
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
