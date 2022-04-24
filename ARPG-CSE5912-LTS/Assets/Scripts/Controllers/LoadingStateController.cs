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
    // public Slider progressBar;
    public Image progressBar;
    public bool loadScene = true;
    public string sceneToLoad;
    public Text percentLoaded;
    public Texture[] images;
    public RawImage backgroundImage;
    public float height = 250.0f;
    public float width = 0.0f;
    [HideInInspector] public AsyncOperation scene;
    private float progressValue;
    private DefaultStatReader defaultStatReader;

    [HideInInspector] public Canvas loadingSceneCanvas;
    [HideInInspector] public bool clickSpace = true;
    private AudioManager audioManager;
    void Update()
    {
        // if (loadScene == true)
        // {
        //     loadScene = false;
        //     StartCoroutine(loadImage());
        //     StartCoroutine(LoadYourAsyncScene());
        // }
    }
    public void InitalizeGameScene()
    {
        loadingSceneCanvasObj.SetActive(true);

        SceneManager.UnloadSceneAsync(SceneManager.GetSceneAt(1));
        AudioListener tempAudioListener = gameObject.AddComponent<AudioListener>();
        scene = SceneManager.LoadSceneAsync("GameScene", LoadSceneMode.Additive);
        scene.allowSceneActivation = false;
        InputController.Instance.enabled = false;

        StartCoroutine(GetSceneLoadProgress());

        Destroy(tempAudioListener);
        InputController.Instance.enabled = true;
        scene.allowSceneActivation = true;
    }
    public void LoadScene(string sceneName)
    {
        loadingSceneCanvasObj.SetActive(true);

        audioManager.Stop(SceneMusic(SceneManager.GetActiveScene().name));
        Debug.Log(SceneMusic(SceneManager.GetActiveScene().name) + " asduinn");
        SceneManager.UnloadSceneAsync(SceneManager.GetSceneAt(1));
        AudioListener tempAudioListener = gameObject.AddComponent<AudioListener>();
        scene = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        scene.allowSceneActivation = false;
        InputController.Instance.enabled = false;

        StartCoroutine(GetSceneLoadProgress());

        Destroy(tempAudioListener);
        InputController.Instance.enabled = true;
        scene.allowSceneActivation = true;
        audioManager.Play(SceneMusic(sceneName));
    }
    public IEnumerator GetSceneLoadProgress()
    {
        while(!scene.isDone)
        {
            progressBar.fillAmount = scene.progress;
            yield return null;
        }
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
            // progressBar.value = progressValue;
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
        audioManager = FindObjectOfType<AudioManager>();
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

        defaultStatReader = GetComponent<DefaultStatReader>();
        SceneManager.LoadSceneAsync("MainMenu", LoadSceneMode.Additive);
        loadingSceneCanvas = loadingSceneCanvasObj.GetComponent<Canvas>();
        loadingSceneCanvasObj.SetActive(false);
        ChangeState<LoadingState>();

        // backgroundImage.texture = images[1];
    }
    public void InitializePlayerStats()
    {
        DefaultStatReader.Instance.InitializeStats(DefaultStatReader.CharacterIndex.Player);
    }
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "GameScene")
        {
            if (GameObject.Find("Player") != null)
            {
                Debug.Log("Initalizing Player Stats");
                InitializePlayerStats();
            }
        }
    }
    private string SceneMusic(string sceneName)
    {
        switch (sceneName)
        {
            case "Dungeon1":
                return "Dungeon1BGM";
            case "Dungeon2":
                return "Dungeon2BGM";
            case "Dungeon3":
                return "Dungeon3BGM";
            default:
                return "Theme";
        }
    }
}
