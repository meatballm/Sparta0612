using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;


public class DialogUI : MonoBehaviour
{
    [SerializeField] private RectTransform dialogPanel;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI dialogText;
    [SerializeField] private Button choiceButton;
    [SerializeField] private GameObject bg;

    [SerializeField] private float typingSpeed = 0.05f;

    private Queue<string> sentences = new Queue<string>();
    private System.Action onDialogComplete;
    private bool isTyping = false;
    private bool isShowing = false;

    private void Start()
    {
        dialogPanel.gameObject.SetActive(false);
        nameText.gameObject.SetActive(false);
        dialogText.gameObject.SetActive(false);
        choiceButton.gameObject.SetActive(false);
        bg.gameObject.SetActive(false);
    }

    public void StartDialog(string npcName, List<string> lines, bool showChoice = false, string choiceText = "", System.Action onComplete = null)
    {
        dialogPanel.gameObject.SetActive(true);
        nameText.gameObject.SetActive(true);
        dialogText.gameObject.SetActive(true);
        bg.gameObject.SetActive(true);

        nameText.text = npcName;
        sentences.Clear();
        foreach (string line in lines)
            sentences.Enqueue(line);

        ShowPanel();
        ShowNextSentence();

        onDialogComplete = onComplete;

        // 조건 만족 시 선택지 보여주기
        choiceButton.gameObject.SetActive(showChoice);
        if (showChoice)
        {
            choiceButton.GetComponentInChildren<TextMeshProUGUI>().text = choiceText;
            choiceButton.onClick.RemoveAllListeners();
            choiceButton.onClick.AddListener(() =>
            {
                HidePanel();
                onDialogComplete?.Invoke();
            });
        }
    }

    public void ShowNextSentence()
    {
        if (isTyping) return;
        if (sentences.Count == 0)
        {
            HidePanel();
            return;
        }

        string nextLine = sentences.Dequeue();
        StartCoroutine(TypeSentence(nextLine));
    }

    IEnumerator TypeSentence(string sentence)
    {
        isTyping = true;
        dialogText.text = "";

        foreach (char c in sentence)
        {
            dialogText.text += c;
            yield return new WaitForSeconds(typingSpeed);
        }

        isTyping = false;
    }

    private void ShowPanel()
    {
        if (isShowing) return;
        isShowing = true;
        dialogPanel.DOScale(1f, 0.3f).SetEase(Ease.OutBack);
    }

    private void HidePanel() // 대화 종료
    {
        isShowing = false;
        dialogPanel.DOScale(0f, 0.2f).SetEase(Ease.InBack).OnComplete(() =>
        {
            dialogPanel.gameObject.SetActive(false);
            nameText.gameObject.SetActive(false);
            dialogText.gameObject.SetActive(false);
            choiceButton.gameObject.SetActive(false);
            bg.gameObject.SetActive(false);

            onDialogComplete?.Invoke();
        });
    }

    private void Update()
    {
        if (!isShowing) return;

        if (!isTyping && (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)))
        {
            ShowNextSentence();
        }
    }
}
