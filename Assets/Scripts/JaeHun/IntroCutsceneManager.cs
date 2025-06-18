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
        AudioManager.Instance.PlayBGM(4);
    }

    void Update()
    {
        // 마우스 클릭하면 스킵하거나 다음 컷으로 넘기기
        if (Input.GetMouseButtonDown(0))
        {
            if (isTyping)
                skipTyping = true;          //스킵을 요청 했을 때
            else
                nextCutRequested = true;      //마우스를 클릭 했을 때 
        }
    }

    IEnumerator PlayCutscene()    //코루틴을 만들기 위한 리턴 타입이고  StartCoroutine(PlayCutscene())에서 호출해서 작동
    {
        // 처음에 검은 화면에서 천천히 밝아짐
        //yield return StartCoroutine(FadeIn());  //코르틴 함수 실행 후(페이드 인 함수가 완전히 끝날 때 까지 기다린다)
                                                //캔버스의 알파값을 1 >> 0으로 서서히 감소시킨다.

        for (int i = 0; i < cutsceneSprites.Length; i++)  //반복문의 시작이고 컷신스프라이트의 갯수만큼 실행한다 여기서
                                                          //i값은 0부터 해서 1씩 증가 시켜서 다음 컷 이미지로 넘김
        {
            currentIndex = i;            //현재 인덱스 값을 업데이트 한다.
            cutsceneImage.sprite = cutsceneSprites[i];   // 컷신 이미지 스프라이를 i값을 받는다.(이미지 설정)

            if (i < cutsceneTexts.Length && !string.IsNullOrEmpty(cutsceneTexts[i])) //컷씬 이미지에서 나오는 텍스트의 존재 유무를 확인하고, 존재 한다면
                                                                                     //.IsNullOrEmpty 은 텍스트가 null or 빈 문자열 인지 확인
                                                                                     // 텍스트를 타이핑으로 출력할건지 결정하는 조건문
            {

                yield return StartCoroutine(TypeText(cutsceneTexts[i])); // cutsceneTexts[i] 는 현재 컷씬텍스트의 문자열로서 타입 텍스트
                                                                         //함수로 전달하여 타자 효과로 출력한다.
            }
            else
            {
                // 텍스트가 없으면 잠깐 기다리기
                storyText.text = "";
                yield return StartCoroutine(WaitForInputOrTime(imageDisplayTime));   //  imageDisplayTime = 2f , 이미지 디스플레이는 2초이기 때문에 
                                                                                     // 2초가 지나고 다음 이미지로 넘어 가거나 사용자가 마우스 클릭을 했다면
                                                                                     //즉시 넘어가는 구조
            }
        }

        // 끝나면 다시 천천히 어두워짐
        //yield return StartCoroutine(FadeOut());

        // 인트로 끝나고 StartScene으로 넘어감
        SceneManager.LoadScene("StartScene");
    }

    IEnumerator TypeText(string text)  //타이핑 애니메이션을 구현하기 위한 코루틴, 이유는 시간의 흐름의 따라 텍스트를 출력하기 위해서
    {
        storyText.text = "";
        isTyping = true;               //텍스트 타이핑 중인 상태
        skipTyping = false;            //스킵을 했는가 안 했는가 여부

        foreach (char c in text)          //텍스트의 글자들을 하나씩 꺼냄
        {
            if (skipTyping)               //스킵 요청이 들어왔을 때
            {

                storyText.text = text;   //남은 텍스트를 한번에 출력함
                break;                   //텍스트 타이핑을 반복 종료
            }

            storyText.text += c;                          //foreach 문에서 문자열을 c값으로 받아서 덧붙여서 출력한다.
            yield return new WaitForSeconds(typingSpeed);  //타이핑이 끝날 때 까지 대기한다.
        }

        isTyping = false;   //타이핑이 완료
        skipTyping = false; // 스킵 상태 초기화

        // 잠깐 기다리거나 클릭할 때까지 대기
        yield return StartCoroutine(WaitForInputOrTime(1.5f));     // 다음 컷씬이 이동하기 전 1.5초 동안 대기 타이밍을 만들어 준다.
    }

    IEnumerator WaitForInputOrTime(float waitTime)              // 다음 컷씬으로 넘어가기 전에 밑에 조건문 2개중에 하나가 충족이 되었을 때
                                                                // 까지 기다린다.
    {
        float timer = 0f;    //다음 컷씬으로 넘어가기 전에 시간을 쟤는 변수
        nextCutRequested = false;  //사용자가 마우스를 클릭 했는지에 대한 여부

        while (timer < waitTime && !nextCutRequested)   //컷씬 이미지,  텍스트가 일정시간 출력 되거나 사용자가 스킵을 해서 넘기기를 요청했을 때까지 기다리는 반복문
        {
            timer += Time.deltaTime;  //경과 시간 계산
            yield return null;    // 프레임마다 검사
        }

        nextCutRequested = false;     //사용자가 마우스 클릭을 해서 스킵을 했을 때 반복문 종료
    }


IEnumerator FadeIn() //인트로 컷씬이 시작하기 전에 화면을 부드럽게 밝게 해주는 코루틴
{
    fadePanel.alpha = 1f;  //페이드 패널의 투명도가 1로 설정
    while (fadePanel.alpha > 0)  // 페이드 패널의 투명도가 0보다 클 때 반복
    {
        fadePanel.alpha -= Time.deltaTime / fadeDuration;   //프레임으로 페이트 패널의 투명도를 줄이고 fadeDuration 으로 속도 조절
            yield return null;   // 다음 프레임에서 다시 계산
    }
    fadePanel.alpha = 0f;    //페이드 패널의 투명도를 0으로 만들고 종료
}

IEnumerator FadeOut()   //인트로 컷씬이 끝날 때 밝기를 점점 어둡게 만들어서 자연스럽게 다음 이미지로 넘어가게 해주는 코루틴
{
    fadePanel.alpha = 0f;  //페이드 패널의 투명도를 0으로 설정
    while (fadePanel.alpha < 1)  //페이드 패널의 투명도가 1보다 작을 때 반복
    {
        fadePanel.alpha += Time.deltaTime / fadeDuration;   //위와 마찬가지로 프레임으로 페이드 패널의 투명도를 높이고 fadeDuration 으로 속도 조절
            yield return null;  //다음 프레임에서 다시 계산
    }
    fadePanel.alpha = 1f;   //페이드 패널의 투명도가 1로 설정되면서 종료
}
}
