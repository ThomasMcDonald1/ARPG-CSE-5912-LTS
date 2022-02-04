using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeGlobe : MonoBehaviour
{

    [SerializeField] Slider globeSlider;
    public float refillSpeed;
    public bool canRefill;

    // Start is called before the first frame update
    void Start()
    {
        canRefill = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (canRefill)
        {
            globeSlider.value = globeSlider.value < 1 ? globeSlider.value + (refillSpeed * Time.deltaTime) : globeSlider.value;
            if (globeSlider.value >= 1) { canRefill = false; }
        }
    }

    void LowerAmount()
    {
        globeSlider.value -= 0.1f;
    }

    void AllowRefill()
    {
        canRefill = true;
    }
}
