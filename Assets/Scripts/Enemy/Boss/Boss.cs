using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Boss : Enemy
{
    [SerializeField] private GameObject attackZonePrefab;

    private float attackColldown = 1f;
    private float timer = 0f;
    private float currentHP;

    private Animator animator;

    void Start()
    {
        enemyHP = 500f;
        chaseRange = 20f;
        attackRange = 15f;
        speed = 0f;
        damage = 20f;
        defense = 30f;     
        animator = GetComponentInChildren<Animator>();

        // SpawnAttackZone();
    }

    void Update()
    {
        // 행동 조건 체크
    }

    void Die()
    {
        animator.SetTrigger("Die");
        // 이후 연출 및 게임 종료 처리
    }

    private IEnumerator SpawnAttackZone()
    {
        int count = 0;
        while(count < 10)
        {
            Vector3 targetPos = player.position;
            Instantiate(attackZonePrefab, targetPos, Quaternion.identity);
            count++;
            yield return new WaitForSeconds(0.5f);
        }
    }

}
