using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState_enemy : IEnemyState
{
    private Enemy enemy;
    [SerializeField] private float attackCooldown = 1.5f;
    private float timer = 0f;

    public AttackState_enemy(Enemy enemy)
    {
        this.enemy = enemy;
    }

    public void Enter()
    {
        Debug.Log("Attack 상태 진입");
        timer = 0f;
    }

    public void Update()
    {
        timer += Time.deltaTime;

        if(!enemy.IsPlayerInAttackRange())
        {
            enemy.stateMachine.ChangeState(new ChaseState_enemy(enemy));
        }

        if(timer >= attackCooldown)
        {
            enemy.Attack();
            timer = 0f;
        }
    }

    public void Exit()
    {
        Debug.Log("Attack 상태 종료");
    }
}
