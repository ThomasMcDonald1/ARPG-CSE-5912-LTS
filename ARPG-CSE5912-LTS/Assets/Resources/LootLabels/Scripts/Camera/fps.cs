using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Shows an fps meter in the supplied text object
/// </summary>
public class fps : MonoBehaviour {

    float deltaTime = 0.0f;
    float msec;
    float fpsTotal;
    string text;
    string format = "{0:0.0} ms ({1:0.} fps)";

    float timeSinceLastCalled;
    float delay = 1f;

    public Text textGO;

    void Update() {

        deltaTime += (Time.deltaTime - deltaTime) * 0.1f;

        timeSinceLastCalled += Time.deltaTime;
        if (timeSinceLastCalled > delay) {
            ShowFPS();
            timeSinceLastCalled = 0f;
        }
    }

    void ShowFPS() {
        
        msec = deltaTime * 1000.0f;
        fpsTotal = 1.0f / deltaTime;
        text = string.Format(format, msec, fpsTotal);
        textGO.text = text;
    }
    
}
