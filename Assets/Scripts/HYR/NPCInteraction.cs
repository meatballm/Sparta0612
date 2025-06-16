using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCInteraction : MonoBehaviour
{
    [SerializeField] private GameObject outline;
    [SerializeField] private GameObject talkIcon;

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
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true;
            outline.SetActive(true);
            talkIcon.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;
            outline.SetActive(false);
            talkIcon.SetActive(false);
        }
    }
}