using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening.Core.Easing;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NPCInteraction : MonoBehaviour
{
    [SerializeField] private GameObject outline;
    [SerializeField] private GameObject talkIcon;
    [SerializeField] private GameObject talkUI;
    [SerializeField] private SubInventory inventory;

    private bool isPlayerNearby = false;
    private bool isTalking = false;

    private void Awake()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
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
        var lines = new List<string> {
        "냐오옹~ 냐아..",
        "꼬리를 바닥에 치고 있다. 배가 고파보인다.",
        "먹을만한걸 구해다주자."
    };

        // 특정 아이템이 있을 때만 선택지O
        if (inventory.HasItem("Fish"))
        {
            lines = new List<string> {
            "냐오옹~ 냐아..",
            "꼬리를 바닥에 치고 있다. 배가 고파보인다."
        };

            UIManager.Instance.Game.Dialog.StartDialog(npcName, lines, true, "생선을 준다.", () =>
            {
                // 엔딩씬 전환
                UnityEngine.SceneManagement.SceneManager.LoadScene("EndingScene");
            });
        }
        else
        {
            // 아이템 없으면 선택지 X
            UIManager.Instance.Game.Dialog.StartDialog(npcName, lines, false, "", () =>
            {
                isTalking = false;
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
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        talkUI = Resources.FindObjectsOfTypeAll<GameObject>().FirstOrDefault(go => go.name == "TalkUI");
        inventory = FindObjectOfType<SubInventory>(true);
    }
}