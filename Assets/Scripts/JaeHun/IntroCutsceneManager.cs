using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class IntroCutsceneManager : MonoBehaviour
{
    public Image cutsceneImage;
    public TextMeshProUGUI storyText;
    public Sprite[] cutsceneSprites;
    [TextArea(3, 10)]
    public string[] cutsceneTexts;

    public float typingSpeed = 0.05f;
    public CanvasGroup fadePanel;
    public float fadeDuration = 1f;
    public float imageDisplayTime = 2f;

    private bool isTyping = false;
    private bool skipTyping = false;
    private bool nextCutRequested = false;

    private int currentIndex = 0;

    private void Start()
    {
        // 전체 화면으로 이미지 고정
        cutsceneImage.rectTransform.anchorMin = Vector2.zero;
        cutsceneImage.rectTransform.anchorMax = Vector2.one;
        cutsceneImage.rectTransform.offsetMin = Vector2.zero;
        cutsceneImage.rectTransform.offsetMax = Vector2.zero;

        StartCoroutine(PlayCutscene());
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (isTyping)
                skipTyping = true;
            else
                nextCutRequested = true;
        }
    }

    IEnumerator PlayCutscene()
    {
        yield return StartCoroutine(FadeIn());

        for (int i = 0; i < cutsceneSprites.Length; i++)
        {
            currentIndex = i;
            cutsceneImage.sprite = cutsceneSprites[i];

            if (i < cutsceneTexts.Length && !string.IsNullOrEmpty(cutsceneTexts[i]))
            {
                yield return StartCoroutine(TypeText(cutsceneTexts[i]));
            }
            else
            {
                storyText.text = "";
                yield return StartCoroutine(WaitForInputOrTime(imageDisplayTime));
            }
        }

        yield return StartCoroutine(FadeOut());
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
                storyText.text = text;
                break;
            }

            storyText.text += c;
            yield return new WaitForSeconds(typingSpeed);
        }

        isTyping = false;
        skipTyping = false;

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
