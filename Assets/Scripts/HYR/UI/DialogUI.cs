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

    [SerializeField] private float typingSpeed = 0.05f;

    private Queue<string> sentences = new Queue<string>();
    private bool isTyping = false;
    private bool isShowing = false;

    private void Start()
    {
        dialogPanel.localScale = Vector3.zero;
        choiceButton.gameObject.SetActive(false);
    }

    public void StartDialog(string npcName, List<string> lines, bool showChoice = false, string choiceText = "")
    {
        nameText.text = npcName;
        sentences.Clear();
        foreach (string line in lines)
            sentences.Enqueue(line);

        ShowPanel();
        ShowNextSentence();

        // 조건 만족 시 선택지 보여주기
        choiceButton.gameObject.SetActive(showChoice);
        if (showChoice)
        {
            choiceButton.GetComponentInChildren<TextMeshProUGUI>().text = choiceText;
            choiceButton.onClick.RemoveAllListeners();
            choiceButton.onClick.AddListener(() =>
            {
                Debug.Log("선택지 클릭");
                HidePanel();
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

    private void HidePanel()
    {
        isShowing = false;
        dialogPanel.DOScale(0f, 0.2f).SetEase(Ease.InBack);
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
