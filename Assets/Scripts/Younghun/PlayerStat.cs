using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System;

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

    private ConditionUI _conditionUI;

    public void Start()
    {
        curHp = maxHp;
        curStamina = maxStamina;
        _conditionUI = UIManager.Instance.Game.Condition;
        _conditionUI.SetHP(1f); // UI 초기화
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
        Debug.Log($"플레이어 체력: {curHp}/{maxHp}");

        // UI 갱신
        float ratio = curHp / (float)maxHp;
        _conditionUI.SetHP(ratio);
        UIManager.Instance.Game.Condition.SetHP(ratio);
    }

    public void ReduceHp(float amount)
    {
        curHp = (int)Mathf.Max(curHp - amount, 0);
        Debug.Log($"플레이어 체력: {curHp}/{maxHp}");

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
        Debug.Log($"플레이어 스테미나: {curStamina}/{maxStamina}");
    }

    public void Death()
    {
        OnDeath?.Invoke();
        Debug.Log("게임오버");
        UIScript.Instance.Gameover();
    }
}
