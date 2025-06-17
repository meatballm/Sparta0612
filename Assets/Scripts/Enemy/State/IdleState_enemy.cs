using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class IdleState_enemy : IEnemyState
{
    private Enemy enemy;
    private float idleTime = 2f;
    private float timer = 0f;

    public IdleState_enemy(Enemy enemy)
    {
        this.enemy = enemy;
    }

    public void Enter()
    {
        timer = 0f;
        // Debug.Log("Idle 상태 진입");
    }
    
    public void Update()
    {
        timer += Time.deltaTime;
        if(enemy.IsPlayerInRange())
        {
            enemy.stateMachine.ChangeState(new ChaseState_enemy(enemy));
        }
        else if (timer >= idleTime)
        {
            enemy.stateMachine.ChangeState(new PatrolState_enemy(enemy));
        }
    }

    public void Exit()
    {
        // Debug.Log("Idle 상태 종료");
    }

}
