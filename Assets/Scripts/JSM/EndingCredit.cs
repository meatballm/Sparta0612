using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndingCredit : MonoBehaviour
{
    public CanvasGroup creditCanvasGroup;    // 크레딧 전체 UI 그룹
    public RectTransform creditTransform;    // 크레딧 텍스트 또는 그룹
    public float targetAlpha = 0.7f;          // 최종 투명도
    public float fadeDuration = 1f;           // 페이드인 시간

    [Header("스크롤 설정")]
    public float scrollSpeed = 30f;           // 기본 스크롤 속도 (1배)
    public float scrollSpeedMultiplier = 3f;  // 최대 속도 배수 (3배)
    public float scrollAcceleration = 3f;     // Lerp 가속도
    public float stopYPosition = 1000f;       // Y좌표 도달 시 멈춤

    [Header("다음 신")]
    public string nextSceneName = "IntroScene";

    private float currentMultiplier = 1f;
    private float fadeTimer = 0f;
    private bool isFading = true;
    private bool scrollEnded = false;

    void Start()
    {
        creditCanvasGroup.alpha = 0;
    }

    void Update()
    {
        if (isFading)
        {
            fadeTimer += Time.deltaTime;
            float alpha = Mathf.Lerp(0, targetAlpha, fadeTimer / fadeDuration);
            creditCanvasGroup.alpha = alpha;

            if (fadeTimer >= fadeDuration)
            {
                creditCanvasGroup.alpha = targetAlpha;
                isFading = false;
            }
        }

        if (!scrollEnded)
        {
            float targetMultiplier = Input.GetMouseButton(0) ? scrollSpeedMultiplier : 1f;
            currentMultiplier = Mathf.Lerp(currentMultiplier, targetMultiplier, Time.deltaTime * scrollAcceleration);

            float moveY = scrollSpeed * currentMultiplier * Time.deltaTime;
            creditTransform.anchoredPosition += new Vector2(0, moveY);

            if (creditTransform.anchoredPosition.y >= stopYPosition)
            {
                scrollEnded = true;
            }
        }

        else if (Input.GetMouseButtonDown(0))
        {
            SceneManager.LoadScene(nextSceneName);
        }
    }
}
