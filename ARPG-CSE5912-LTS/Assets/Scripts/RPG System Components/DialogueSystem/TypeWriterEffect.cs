using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TypeWriterEffect : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]private float writingSpeed = 5f;
    public Coroutine Run(string textToType, TMP_Text textLabel)
    {
        return StartCoroutine(routine:TypeText(textToType,textLabel));
    }
    private IEnumerator TypeText(string textToType, TMP_Text textLabel)
    {
        textLabel.text = string.Empty;
        //delete yield return?
        //yield return new WaitForSeconds(1);
        float t = 0;
        int charIndex = 0;
        while ( charIndex < textToType.Length)
        {
            t += Time.deltaTime * writingSpeed;
            charIndex = Mathf.FloorToInt(t);
            charIndex = Mathf.Clamp(value: charIndex, min: 0, max: textToType.Length);
            textLabel.text = textToType.Substring(startIndex: 0, length: charIndex);
            yield return null;
        }
        textLabel.text = textToType;
    }
}
