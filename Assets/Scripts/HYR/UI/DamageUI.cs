using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DamageUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI damageText;
    [SerializeField] private float riseDistance = 30f;  // UI 띄울 거리
    [SerializeField] private float duration = 0.8f;

    private CanvasGroup canvasGroup;
    private RectTransform rect;

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    /// <summary>
    /// 데미지 값
    /// </summary>
    public void Initialize(int damage, Vector2 screenPosition)
    {
        damageText.text = damage.ToString();
        rect.position = screenPosition;
        Debug.Log("데미지 텍스트 생성 위치 (screenPos): " + screenPosition);
        StartCoroutine(PopupRoutine());
    }

    private IEnumerator PopupRoutine()
    {
        float elapsed = 0f;
        Vector2 start = rect.position;
        while (elapsed < duration)
        {
            // 위로 살짝씩 이동
            rect.position = start + Vector2.up * (riseDistance * (elapsed / duration));
            // 페이드 아웃 효과
            canvasGroup.alpha = 1f - (elapsed / duration);

            elapsed += Time.deltaTime;
            yield return null;
        }
        Destroy(gameObject);
    }
}
