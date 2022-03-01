using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Threading.Tasks;

public class LoadingStateController : StateMachine
{
    public static LoadingStateController Instance;
    public GameObject loadingSceneCanvasObj;
    public Slider progressBar;
    public bool loadScene = true;
    public string sceneToLoad;
    public Text percentLoaded;
    public Texture[] images;
    public RawImage backgroundImage;
    public float height = 250.0f;
    public float width = 0.0f;

    private float progressValue;

    [HideInInspector] public Canvas loadingSceneCanvas;
    [HideInInspector] public bool clickSpace = true;
    void Update()
    {
        // if (loadScene == true)
        // {
        //     loadScene = false;
        //     StartCoroutine(loadImage());
        //     StartCoroutine(LoadYourAsyncScene());
        // }
    }
    public async void LoadScene(string sceneName)
    {
        loadingSceneCanvasObj.SetActive(true);
        SceneManager.UnloadSceneAsync(SceneManager.GetSceneAt(1));
        AsyncOperation scene = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        scene.allowSceneActivation = false;
        InputController.Instance.enabled = false;

        await Task.Delay(10000);

        scene.allowSceneActivation = true;
        loadingSceneCanvasObj.SetActive(false);
    }
    public IEnumerator LoadYourAsyncScene(string sceneName)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);

        asyncLoad.allowSceneActivation = false;
        bool checkTime = true;

        while (!asyncLoad.isDone)
        {
            progressValue = Mathf.Clamp01(asyncLoad.progress / 0.9f);
            progressBar.value = progressValue;
            Debug.Log(asyncLoad.progress);
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
                    clickSpace = false;
                    asyncLoad.allowSceneActivation = true;
                }

            }
            yield return null;
        }
        Debug.Log("level is loaded");
    }

    IEnumerator loadImage()
    {
        int i = 0;
        while (clickSpace)
        {
            backgroundImage.texture = images[i];
            width = (height / (float)images[i].height) * (float)images[i].width;
            var rectTrans = backgroundImage.GetComponent<RectTransform>();

            rectTrans.sizeDelta = new Vector2(width, height);
            i = (i + 1) % images.Length;
            yield return new WaitForSeconds(1);
        }
        yield return null;
    }

    private void Awake()
    {
        DontDestroyOnLoad(this);
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        SceneManager.LoadSceneAsync("SceneTeleport1", LoadSceneMode.Additive);
        loadingSceneCanvas = loadingSceneCanvasObj.GetComponent<Canvas>();
        loadingSceneCanvasObj.SetActive(false);
        ChangeState<LoadingState>();

        // backgroundImage.texture = images[1];


    }
}
