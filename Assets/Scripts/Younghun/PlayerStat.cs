using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System;

[System.Serializable]
public class PlayerStat
{
    public event Action OnDeath;

    [Header("Dodge")]
    private bool canDodge;               // 회피 가능 여부
    private float cooldownDodge;         // 회피 쿨타임
    public float CooldownDodge
    {
        get => cooldownDodge;
        set => cooldownDodge = Mathf.Clamp(value, 1f, 5f); // 범위 제한
    }
    public float dodgeSpeed;             // 회피 속도
    public float dodgeTime;              // 회피 시간
    public float dodgeInvincibleTime;    // 회피 무적 시간
    public bool isInvincible;            // 회피 무적 적용 여부
    public Vector2 dodgeDirection;       // 회피 방향

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
