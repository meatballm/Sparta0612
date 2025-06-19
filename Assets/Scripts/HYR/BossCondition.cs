using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BossCondition : MonoBehaviour
{
    public Image fillImage;

    private float maxHp = 1f;

    public void SetMaxHp(float value)
    {
        maxHp = value;
        UpdateHp(maxHp);
    }

    public void UpdateHp(float currentHp)
    {
        if (fillImage != null)
            fillImage.fillAmount = Mathf.Clamp01(currentHp / maxHp);
    }

    public void Show(bool isShow)
    {
        gameObject.SetActive(isShow);
    }
}
