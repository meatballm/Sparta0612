using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


public class Lizard : Enemy // 원거리 적 - Lizard
{
    [SerializeField] private GameObject fireballPrefab; // 파이어볼 프리팹
    [SerializeField] private Transform firePoint; // 파이어볼이 나오는 위치(입)
    Animator animator;

    void Awake() // Lizard 기본셋팅
    {
        maxHP = 100f;
        enemyHP = maxHP;
        chaseRange = 10f;
        attackRange = 6f;
        speed = 1f;
        damage = 10f;
        defense = 10f;
        animator = GetComponentInChildren<Animator>();   
    }

    public override void MoveTowards()
    {
        // 공격할 때 offset 거리만큼 떨어져서 함.
        Vector3 offset = Vector3.right * 3; 
        Vector3 dir = (player.position - transform.position + offset).normalized;
        transform.position += dir * speed * Time.deltaTime;

        // 움직이는 방향에 따라 sprite 반전.
        if ( 0 < dir.x )
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false;
        }
    }


    public override void Attack()
    {
        animator.SetTrigger("Attack");

        // 플레이어 방향 계산
        Vector2 direction = (player.position - firePoint.position).normalized;
        if ( 0 < direction.x )
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false;
        }

        // 파이어볼 생성
        GameObject fireball = Instantiate(fireballPrefab, firePoint.position, Quaternion.identity);
        fireball.GetComponent<Lizard_FireBall>().SetDirection(direction);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerStat.ReduceHp(damage);
        }
    }
}
