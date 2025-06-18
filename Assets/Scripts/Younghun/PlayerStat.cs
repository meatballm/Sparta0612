using UnityEngine;
using System;
using Unity.Mathematics;

[System.Serializable]
public class PlayerStat
{
    public event Action OnDeath;

    public int maxHp = 100;                 // 최대 체력
    public int curHp;                       // 현재 체력
    public float maxStamina = 100;          // 최대 스테미나
    public float curStamina;                // 현재 스테미나
    public int consumeStamina = 40;         // 스테미나 사용량
    public float reproductionStamina = 2;   // 스테미나 회복력

    public float staminaDecreaseRate = 15f; // 스테미나 감소
    public float staminaRecoverRate = 20f;  // 스테미나 회복

    private ConditionUI _conditionUI;

    public void Start()
    {
        curHp = maxHp;
        curStamina = maxStamina;
        _conditionUI = UIManager.Instance.Game.Condition;
        _conditionUI.SetHP(1f);
        _conditionUI.SetStamina(1f);
    }

    public void Updatd()
    {
        if (curStamina < maxStamina)
        {
            RecoverStamina(reproductionStamina * Time.deltaTime);
            
            if (curStamina > maxStamina)
            {
                curStamina = maxStamina;
            }
        } 
    }

    public void HealHp(int amount)
    {
        curHp = (int)Mathf.Max(curHp + amount, maxHp);

        // UI 갱신
        float ratio = curHp / (float)maxHp;
        _conditionUI.SetHP(ratio);
        UIManager.Instance.Game.Condition.SetHP(ratio);
    }

    public void ReduceHp(float amount)
    {
        curHp = (int)Mathf.Max(curHp - amount, 0);

        AudioManager.Instance.PlaySFX(UnityEngine.Random.Range(9, 11));

        // UI 갱신
        float ratio = curHp / (float)maxHp;
        _conditionUI.SetHP(ratio);
        UIManager.Instance.Game.Condition.SetHP(ratio);


        if (curHp <= 0)
        {
            Death();
        }
    }

    public void RecoverStamina(float amount)
    {
        curStamina += amount;
    }

    public void ReduceStamina(float amount)
    {
        curStamina = (int)Mathf.Max(curStamina - amount, 0);
    }

    public void UpdateStamina(float moveInputMagnitude, float deltaTime)
    {
        if (moveInputMagnitude > 0.1f)
        {
            curStamina = Mathf.Max(curStamina - (staminaDecreaseRate * deltaTime), 0f);
        }
        else
        {
            curStamina = Mathf.Min(curStamina + (staminaRecoverRate * deltaTime), maxStamina);
        }

        // UI 연동
        float ratio = curStamina / maxStamina;
        _conditionUI.SetStamina(ratio);
    }

    public void Death()
    {
        OnDeath?.Invoke();
        UIScript.Instance.Gameover();
        Debug.Log("게임오버");
    }
}
