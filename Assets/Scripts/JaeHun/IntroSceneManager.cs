using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class IntroSceneManager : MonoBehaviour
{
    public GameObject titleObject;
    public CanvasGroup buttonGroup;

    public Button startButton;
    public Button exitButton;
    public Button settingsButton;

    void Start()
    {
        // 타이틀 애니메이션 시작
        StartCoroutine(PlayIntroAnimation());

        // 버튼 이벤트 등록
        startButton.onClick.AddListener(OnClickStart);
        exitButton.onClick.AddListener(OnClickExit);
        settingsButton.onClick.AddListener(OnClickSettings);
    }

    IEnumerator PlayIntroAnimation()
    {
        // 타이틀 크기 0에서 점점 키우기
        float duration = 1f;
        float elapsed = 0f;
        Vector3 initialScale = Vector3.zero;
        Vector3 targetScale = Vector3.one;

        titleObject.transform.localScale = initialScale;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / duration);
            titleObject.transform.localScale = Vector3.Lerp(initialScale, targetScale, t);
            yield return null;
        }

        titleObject.transform.localScale = targetScale;

        // 버튼 페이드 인
        StartCoroutine(FadeInButtons());
    }

    IEnumerator FadeInButtons()
    {
        float duration = 1f;
        float elapsed = 0f;
        buttonGroup.alpha = 0f;
        buttonGroup.gameObject.SetActive(true);

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / duration);
            buttonGroup.alpha = t;
            yield return null;
        }

        buttonGroup.alpha = 1f;
    }

    void OnClickStart()
    {
        SceneManager.LoadScene("MainScene"); // 실제 메인 씬 이름으로 변경
    }

    void OnClickExit()
    {
        Application.Quit();
    }

    void OnClickSettings()
    {
        Debug.Log("설정 버튼 클릭됨");
        // 옵션 창 열기 구현
    }
}
