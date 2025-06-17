using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum SceneType
{
    IntroScene,
    StartScene,
    Stage1Scene,
    Stage2Scene,
    TownMapScene,
    WorldMapScene,
    EnemyScene,
    UIScene
}

public class GameSceneManager : MonoBehaviour
{
    public static GameSceneManager Instance;

    [Header("Optional Loading UI")]
    public GameObject loadingUI;                    // 로딩 UI (로딩 패널 등)
    public UnityEngine.UI.Slider loadingBar;        // 로딩 바 (Slider UI)

    private SceneType currentScene;                 // 현재 씬 저장용
    private bool isLoading = false;                 // 중복 로딩 방지

    private void Awake()
    {
        // 싱글톤 패턴 적용
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);              // 씬 바뀌어도 유지
    }

    
    public void LoadScene(SceneType scene)
    {
        if (!isLoading)
            StartCoroutine(LoadSceneRoutine(scene));
    }

 
    private IEnumerator LoadSceneRoutine(SceneType scene)
    {
        isLoading = true;
        currentScene = scene;

        // 로딩 UI 표시
        if (loadingUI != null) loadingUI.SetActive(true);

        // 씬 비동기로 로드 시작
        AsyncOperation async = SceneManager.LoadSceneAsync(scene.ToString());
        async.allowSceneActivation = false;

        // 90%까지 로딩 (0.9 전까진 async.progress가 계속 증가함)
        while (async.progress < 0.9f)
        {
            if (loadingBar != null)
                loadingBar.value = async.progress;
            yield return null;
        }

        // 마지막 바 100% 채우기
        if (loadingBar != null)
            loadingBar.value = 1f;

        // 약간의 시간 딜레이 후 씬 실제 전환
        yield return new WaitForSeconds(0.5f);
        async.allowSceneActivation = true;

        // 전환 완료까지 대기
        while (!async.isDone)
            yield return null;

        // 로딩 UI 끄기
        if (loadingUI != null) loadingUI.SetActive(false);
        isLoading = false;
    }

    // 현재 씬 타입 반환
    public SceneType GetCurrentScene()
    {
        return currentScene;
    }

    // 현재 씬 다시 로드 (재시작)
    public void ReloadCurrentScene()
    {
        LoadScene(currentScene);
    }

    // 빌드 인덱스 기준 다음 씬 로드
    public void LoadNextSceneInBuildOrder()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;

        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            // 다음 씬 이름 가져오기
            string nextSceneName = System.IO.Path.GetFileNameWithoutExtension(
                SceneUtility.GetScenePathByBuildIndex(nextSceneIndex));

            // 문자열 → enum으로 변환해서 로드 (이름이 정확히 일치해야 함(대문자 소문자))
            LoadScene((SceneType)System.Enum.Parse(typeof(SceneType), nextSceneName));
        }
    }
}
