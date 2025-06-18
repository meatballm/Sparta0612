using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance {  get; private set; }

    // 각 UI에 연결
    [SerializeField] private StartUI startUI;
    [SerializeField] private GameUI gameUI;
    [SerializeField] private PauseUI pauseUI;

    // 외부 접근용 프로퍼티
    public GameUI Game => gameUI;
    public PauseUI Pause => pauseUI;
    public StartUI Start => startUI;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
            Destroy(gameObject);
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        startUI = FindObjectOfType<StartUI>(true);
        gameUI = FindObjectOfType<GameUI>(true);
        pauseUI = FindObjectOfType<PauseUI>(true);
    }
}
