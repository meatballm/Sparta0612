using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConditionUI : MonoBehaviour
{
    [SerializeField] private Image hpFill;
    [SerializeField] private Image staminaFill;
    [SerializeField] private GameObject reloadCooldownIcon;
    [SerializeField] private Image reloadProgress;

    public void SetHP(float ratio)
    {
        hpFill.fillAmount = Mathf.Clamp01(ratio);
    }

    public void SetStamina(float ratio)
    {
        staminaFill.fillAmount = Mathf.Clamp01(ratio);
    }

    public void ShowReloadCooldown(float duration)
    {
        reloadCooldownIcon.SetActive(true);
        StartCoroutine(UpdateReloadBar(duration));
    }

    private IEnumerator UpdateReloadBar(float duration)
    {
        float time = 0f;
        while (time < duration)
        {
            time += Time.deltaTime;
            reloadProgress.fillAmount = time / duration;
            yield return null;
        }

        reloadProgress.fillAmount = 1f;
        yield return new WaitForSeconds(0.3f);
        reloadCooldownIcon.SetActive(false);
    }
}
