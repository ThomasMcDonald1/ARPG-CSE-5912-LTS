using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingStateController : StateMachine
{
    public GameObject loadingSceneCanvasObj;
    public Slider progressBar;
    public bool loadScene = true;
    public string sceneToLoad;
    public Text percentLoaded;
    public Texture[] images;
    public RawImage backgroundImage;

    private float progressValue;

    [HideInInspector] public Canvas loadingSceneCanvas;
    

    void Update()
    {
        

        if (loadScene == true)
        {
            loadScene = false;
            StartCoroutine(loadImage());
            StartCoroutine(LoadYourAsyncScene());
        }
    }

    IEnumerator LoadYourAsyncScene()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(0);

        asyncLoad.allowSceneActivation = false;
        bool checkTime = true;

        while (!asyncLoad.isDone)
        {
            progressValue = Mathf.Clamp01(asyncLoad.progress / 0.9f);
            progressBar.value = progressValue;
            //Debug.Log(asyncLoad.progress);
            percentLoaded.text = Mathf.Round(progressValue * 100) + "%";

            if (asyncLoad.progress == 0.9f)
            {
                if (checkTime == true) //to make sure there is at least 1 second to wait.
                {
                    checkTime = false;
                    yield return new WaitForSeconds(1);
                }
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    asyncLoad.allowSceneActivation = true;
                }
                
            }
            yield return null;
        }
        //Debug.Log("level is loaded");
    }

    IEnumerator loadImage()
    {
        for (int i = 0; i < images.Length; i++)
        {
            backgroundImage.texture = images[i];
            yield return new WaitForSeconds(1);
        }
        yield return null;
    }

    private void Awake()
    {
        loadingSceneCanvas = loadingSceneCanvasObj.GetComponent<Canvas>();
        loadingSceneCanvas.enabled = true;
        ChangeState<LoadingState>();

        backgroundImage.texture = images[1];
        
        
    }
}