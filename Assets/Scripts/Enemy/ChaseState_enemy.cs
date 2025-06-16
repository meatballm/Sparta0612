using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState_enemy : IEnemyState
{
    private Enemy enemy;

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
            enemy.stateMachine.ChangeState(new PatrolState_enemy(enemy));
        }
        if (enemy.IsPlayerInAttackRange())
        {
            enemy.stateMachine.ChangeState(new AttackState_enemy(enemy));
        }
        enemy.MoveTowards();
    }

    public void Exit()
    {
        Debug.Log("Chase 상태 종료");
    }
}
