using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState_enemy : IEnemyState
{
    private Enmey enemy;
    private float speed = 3.5f;

    public ChaseState_enemy(Enmey enemy)
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

        enemy.MoveTowards(enemy.player.position, speed);
    }

    public void Exit()
    {
        Debug.Log("Chase 상태 종료");
    }
}
