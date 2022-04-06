using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using TMPro;

public class TipManager : MonoBehaviour
{
    public TextMeshProUGUI tipText;

    public RectTransform tipWindow;

    public static TipManager instance;

    void Awake()
    {
        instance = this;
    }
   

    // Start is called before the first frame update
    void Start()
    {
        HideTip();
    }

   public  void ShowTip(string tip)
    {

        tipText.text = tip;
        tipWindow.sizeDelta = new Vector2(tipText.preferredWidth > 200 ? 200 : tipText.preferredWidth, tipText.preferredHeight);
        tipWindow.gameObject.SetActive(true);
        tipWindow.transform.position =   new Vector2(Input.mousePosition.x + tipWindow.sizeDelta.x / 2, Input.mousePosition.y + tipWindow.sizeDelta.x / 2);
      
    }
    public void HideTip()
    {
        tipText.text = default;
        gameObject.SetActive(false);
    }
}
