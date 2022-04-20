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

    public void Setup(int Amount, bool isCrit, string popType, bool isBasic)
    {
        
        switch (popType)
        {
            case "damage":
                popUpText.text = Amount.ToString();
                if (isBasic)
                {
                    popUpText.color = new Color(1f, 1f, 1f, 1f); //white
                }
                else
                {
                    popUpText.color = new Color(0.9f, 0.2f, 0f, 1f); //red
                }
                popUpText.fontSize = 200;
                if (isCrit)
                {
                    if (isBasic)
                    {
                        popUpText.color = new Color(1f, 1f, 1f, 1f); //white
                    }
                    else
                    {
                        popUpText.color = new Color(1f, 0.6f, 0f, 1f); //orange
                    }
                    popUpText.fontSize = 300;
                }
                break;
            case "healing":
                popUpText.text = Amount.ToString();
                popUpText.color = new Color(0.1f, 0.8f, 0.4f, 1f); //green
                popUpText.fontSize = 200;
                if (isCrit)
                {
                    popUpText.fontSize = 300;
                }
                break;
            case "missing":
                popUpText.text = "MISS";
                popUpText.color = new Color(1f, 0.9f, 0f, 1f); //yellow
                popUpText.fontSize = 300;
                break;
            default:
                break;
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
