using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class IntroCutsceneManager : MonoBehaviour
{
    public Image cutsceneImage;                     // 컷씬 그림 넣는 곳
    public TextMeshProUGUI storyText;               // 텍스트 타이핑 효과용 텍스트

    public Sprite[] cutsceneSprites;                // 컷씬에 나올 이미지들
    [TextArea(3, 10)]
    public string[] cutsceneTexts;                  // 각 이미지에 나올 텍스트들

    public float typingSpeed = 0.05f;               // 글자 하나 나올 때 딜레이
    public float imageDisplayTime = 2f;             // 자동으로 넘길 경우 시간

    public CanvasGroup fadePanel;                   // 페이드 효과용 UI (검은 배경)
    public float fadeDuration = 1f;                 // 페이드 시간

    private bool isTyping = false;                  // 지금 글자 타이핑 중인지
    private bool skipTyping = false;                // 마우스 클릭으로 스킵할 때
    private bool nextCutRequested = false;          // 다음 컷으로 넘기기 요청

    private int currentIndex = 0;                   // 현재 몇 번째 컷인지

    void Start()
    {
        // 이미지가 화면 전체에 꽉 차게 설정
        cutsceneImage.rectTransform.anchorMin = Vector2.zero;
        cutsceneImage.rectTransform.anchorMax = Vector2.one;
        cutsceneImage.rectTransform.offsetMin = Vector2.zero;
        cutsceneImage.rectTransform.offsetMax = Vector2.zero;

        // 컷씬 재생 시작
        StartCoroutine(PlayCutscene());
    }

    void Update()
    {
        // 마우스 클릭하면 스킵하거나 다음 컷으로 넘기기
        if (Input.GetMouseButtonDown(0))
        {
            if (isTyping)
                skipTyping = true;
            else
                nextCutRequested = true;
        }
    }

    IEnumerator  PlayCutscene()    //코루틴을 만들기 위한 리턴 타입이고  StartCoroutine(PlayCutscene())에서 호출해서 작동
    {
        // 처음에 검은 화면에서 천천히 밝아짐
        yield return StartCoroutine(FadeIn());  //코르틴 함수 실행 후(페이드 인 함수가 완전히 끝날 때 까지 기다린다)
                                                //캔버스의 알파값을 1 >> 0으로 서서히 감소시킨다.

        for (int i = 0; i < cutsceneSprites.Length; i++)  //반복문의 시작이고 컷신스프라이트의 갯수만큼 실행한다 여기서
                                                          //i값은 0부터 해서 1씩 증가 시켜서 다음 컷 이미지로 넘김
        {
            currentIndex = i;            //현재 인덱스 값을 업데이트 한다.
            cutsceneImage.sprite = cutsceneSprites[i];   // 컷신 이미지 스프라이를 i값을 받는다.(이미지 설정)

            if (i < cutsceneTexts.Length && !string.IsNullOrEmpty(cutsceneTexts[i])) //만약에 텍스트가 존재 한다면
            {
                
                yield return StartCoroutine(TypeText(cutsceneTexts[i])); // 
            }
            else
            {
                // 텍스트가 없으면 잠깐 기다리기
                storyText.text = "";
                yield return StartCoroutine(WaitForInputOrTime(imageDisplayTime));
            }
        }

        // 끝나면 다시 천천히 어두워짐
        yield return StartCoroutine(FadeOut());

        // 인트로 끝나고 StartScene으로 넘어감
        SceneManager.LoadScene("StartScene");
    }

    IEnumerator TypeText(string text)
    {
        storyText.text = "";
        isTyping = true;
        skipTyping = false;

        foreach (char c in text)
        {
            if (skipTyping)
            {
                // 클릭하면 한 번에 다 보여줌
                storyText.text = text;
                break;
            }

            storyText.text += c;
            yield return new WaitForSeconds(typingSpeed);
        }

        isTyping = false;
        skipTyping = false;

        // 잠깐 기다리거나 클릭할 때까지 대기
        yield return StartCoroutine(WaitForInputOrTime(1.5f));
    }

    IEnumerator WaitForInputOrTime(float waitTime)
    {
        float timer = 0f;
        nextCutRequested = false;

        while (timer < waitTime && !nextCutRequested)
        {
            timer += Time.deltaTime;
            yield return null;
        }

        nextCutRequested = false;
    }

    IEnumerator FadeIn()
    {
        fadePanel.alpha = 1f;
        while (fadePanel.alpha > 0)
        {
            fadePanel.alpha -= Time.deltaTime / fadeDuration;
            yield return null;
        }
        fadePanel.alpha = 0f;
    }

    IEnumerator FadeOut()
    {
        fadePanel.alpha = 0f;
        while (fadePanel.alpha < 1)
        {
            fadePanel.alpha += Time.deltaTime / fadeDuration;
            yield return null;
        }
        fadePanel.alpha = 1f;
    }
}
