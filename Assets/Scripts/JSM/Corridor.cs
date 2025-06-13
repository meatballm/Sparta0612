using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Collider2D), typeof(SpriteRenderer))]
public class CorridorTrigger2D : MonoBehaviour
{
    [Header("플레이어 태그")]
    [Tooltip("충돌 대상을 구분할 태그")]
    public string playerTag = "Player";

    [Header("충돌 시 활성화할 방벽")]
    public GameObject barrier;

    [Header("방벽 올라갈지 아닌지 여부")]
    public bool barrierUp = true;

    [Header("페이드 아웃 시간 (초)")]
    public float fadeDuration = 2f;

    bool triggered = false;
    SpriteRenderer sr;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        GetComponent<Collider2D>().isTrigger = true;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (triggered) return;
        if (!other.CompareTag(playerTag)) return;
        triggered = true;

        if (barrier != null && barrierUp)
            barrier.SetActive(true);

        StartCoroutine(FadeOut());
    }

    IEnumerator FadeOut()
    {
        Color col = sr.color;
        float startA = col.a;
        float t = 0f;

        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            col.a = Mathf.Lerp(startA, 0f, t / fadeDuration);
            sr.color = col;
            yield return null;
        }

        col.a = 0f;
        sr.color = col;
    }
}
