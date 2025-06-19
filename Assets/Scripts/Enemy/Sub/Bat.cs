using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat : Enemy
{   
    private Vector3 moveDir;

    void Awake()
    {
        maxHP = 50f;
        enemyHP = maxHP;
        chaseRange = 10f;
        attackRange = 1f;
        speed = 2f;
        damage = 10f;
        defense = 10f;
    }
    public override void Attack()
    {
        moveDir = (player.position - transform.position).normalized;

        if ( 0 < moveDir.x )
            spriteRenderer.flipX = true;
        else
            spriteRenderer.flipX = false;
    }

    public override void MoveTowards()
    {
        moveDir = (player.position - transform.position).normalized;
        transform.position += moveDir * speed * Time.deltaTime;

        if ( 0 < moveDir.x )
            spriteRenderer.flipX = true;
        else
            spriteRenderer.flipX = false;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerStat.ReduceHp(damage);
        }
        else if (collision.CompareTag("Wall"))
        {
            moveDir *= -1f;
        }
    }
}
