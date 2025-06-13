using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState_enemy : IEnemyState
{
    private Enemy enemy;
    private float speed = 3.5f;

    public ChaseState_enemy(Enemy enemy)
    {
        this.enemy = enemy;
    }

    public void Enter()
    {
        Debug.Log("Chase 상태 진입");
    }

    public void Update()
    {
        if (!enemy.IsPlayerInRange())
        {
            enemy.stateMachine.ChangeState(new IdleState_enemy(enemy));
            return;
        }
        else if (enemy.IsPlayerInAttackRange())
        {
            enemy.stateMachine.ChangeState(new AttackState_enemy(enemy));
        }
        enemy.MoveTowards(enemy.player.position, speed);
    }

    public void Exit()
    {
        Debug.Log("Chase 상태 종료");
    }
}
