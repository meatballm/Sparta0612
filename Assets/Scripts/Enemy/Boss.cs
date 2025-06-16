using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public float maxHP = 100f;
    private float currentHP;

    private Animator animator;

    void Awake()
    {
        currentHP = maxHP;
        animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        // 행동 조건 체크
    }

    public void TakeDamage(float amount)
    {
        currentHP -= amount;
        if (currentHP <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        animator.SetTrigger("Die");
        // 이후 연출 및 게임 종료 처리
    }

}
