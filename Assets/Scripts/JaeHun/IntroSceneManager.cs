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
        // Ÿ��Ʋ �ִϸ��̼� ����
        StartCoroutine(PlayIntroAnimation());

        // ��ư �̺�Ʈ ���
        startButton.onClick.AddListener(OnClickStart);
        exitButton.onClick.AddListener(OnClickExit);
        settingsButton.onClick.AddListener(OnClickSettings);
    }

    IEnumerator PlayIntroAnimation()
    {
        // Ÿ��Ʋ ũ�� 0���� ���� Ű���
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

        // ��ư ���̵� ��
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
        SceneManager.LoadScene("MainScene"); // ���� ���� �� �̸����� ����
    }

    void OnClickExit()
    {
        Application.Quit();
    }

    void OnClickSettings()
    {
        Debug.Log("���� ��ư Ŭ����");
        // �ɼ� â ���� ����
    }
}
