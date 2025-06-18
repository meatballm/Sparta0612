using System.Collections;
using System.Collections.Generic;
using DG.Tweening.Core.Easing;
using UnityEngine;

public class NPCInteraction : MonoBehaviour
{
    [SerializeField] private GameObject outline;
    [SerializeField] private GameObject talkIcon;
    [SerializeField] private GameObject talkUI;
    [SerializeField] private SubInventory inventory;

    private bool isPlayerNearby = false;
    private bool isTalking = false;

    private void Start()
    {
        outline.SetActive(false);
        talkIcon.SetActive(false);
    }

    private void Update()
    {
        if (isPlayerNearby && !isTalking && Input.GetKeyDown(KeyCode.F))
        {
            StartConversation();
        }
    }

    private void StartConversation()
    {
        if (isTalking) return;
        isTalking = true;

        Debug.Log("대화중");

        string npcName = "고양이";
        var lines = new List<string> { "야옹~ 냐아옹~" };

        if (inventory.HasItem("Fish"))
        {
            lines.Add("꼬리를 바닥에 치고 있다.\n배가 고파보인다.");
            UIManager.Instance.Game.Dialog.StartDialog(npcName, lines, true, "생선을 준다.", () =>
            {
                isTalking = false; // 선택지 클릭 후 대화 종료
            });
        }
        else
        {
            UIManager.Instance.Game.Dialog.StartDialog(npcName, lines, false, "", () =>
            {
                isTalking = false; // 대사 끝난 후 대화 재시작 가능
            });
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true;
            outline.SetActive(true);
            talkIcon.SetActive(true);
            talkUI.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;
            outline.SetActive(false);
            talkIcon.SetActive(false);
            talkUI.SetActive(false);
        }
    }
}