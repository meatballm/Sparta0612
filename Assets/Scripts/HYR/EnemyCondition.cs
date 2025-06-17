using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyCondition : MonoBehaviour
{
    [SerializeField] private GameObject hpBarPrefab;
    [SerializeField] private float offsetY = -1.0f; // 몬스터로부터 체력바 띄울 거리

    private GameObject hpBarInstance;
    private Image hpFillImage;
    private Canvas hpBarCanvas;

    private void Start()
    {
        // C하위 프리팹 인스턴스 생성
        hpBarInstance = Instantiate(
            hpBarPrefab,
            Vector3.zero,
            Quaternion.identity,
            GameObject.Find("UI").transform
        );

        // fillAmount로 체력바 이미지 연결
        hpFillImage = hpBarInstance.transform.Find("Fill_H").GetComponent<Image>();

        hpBarInstance.SetActive(false);

        hpBarCanvas = hpBarInstance.GetComponentInParent<Canvas>();
    }

    // 체력바 갱신
    public void UpdateHealthBar(int currentHP, int maxHP)
    {
        if (hpFillImage != null)
        {
            float ratio = (float)currentHP / maxHP;
            hpFillImage.fillAmount = Mathf.Clamp01(ratio);
        }
    }

    // 체력바 제거
    public void HideAndDestroyBar()
    {
        if (hpBarInstance != null)
            Destroy(hpBarInstance);
    }

    private void LateUpdate()
    {
        // 항상 몬스터 아래쪽에 체력바가 따라가도록 이동
        if (hpBarInstance != null && hpBarCanvas != null)
        {
            Vector3 worldPos = transform.position + new Vector3(0, offsetY, 0);
            Vector3 screenPos = Camera.main.WorldToScreenPoint(worldPos);
            hpBarInstance.GetComponent<RectTransform>().position = screenPos;
        }
    }

    public void ShowHealthBar()
    {
        if (hpBarInstance != null && !hpBarInstance.activeSelf)
            hpBarInstance.SetActive(true);
    }

    public bool IsBarActive()
    {
        return hpBarInstance != null && hpBarInstance.activeSelf;
    }
}
