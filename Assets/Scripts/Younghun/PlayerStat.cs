using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System;

[System.Serializable]
public class PlayerStat
{
    public event Action OnDeath;

    private bool canDodge;
    
    private float cooldownDodge;
    public float CooldownDodge
    {
        get => cooldownDodge;
        set => cooldownDodge = Mathf.Clamp(value, 1f, 5f); // 범위 제한
    }

    public int maxHp = 100;
    public int curHp;

    private ConditionUI _conditionUI;

    public void Start()
    {
        curHp = maxHp;
        _conditionUI = UIManager.Instance.Game.Condition;
        _conditionUI.SetHP(1f); // UI 초기화
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

    public void Death()
    {
        OnDeath?.Invoke();
        Debug.Log("게임오버");
    }

}
