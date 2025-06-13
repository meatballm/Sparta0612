using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState_enemy : IEnemyState
{
    private Enemy enemy;
    private Vector3 targetPos;
    private float speed = 2f;

    public PatrolState_enemy(Enemy enemy)
    {
        this.enemy = enemy;
        SetRandomTarget();
    }

    void SetRandomTarget()
    {
        float range = 3f;
        targetPos = enemy.transform.position + new Vector3(Random.Range(-range, range), Random.Range(-range, range), 0f);
    }

    public void Enter()
    {
        Debug.Log("Patrol 상태 진입");
    }

    public void Update()
    {
        enemy.MoveTowards(targetPos, speed);

        if(Vector3.Distance(enemy.transform.position, targetPos) < 0.1f)
        {
            enemy.stateMachine.ChangeState(new IdleState_enemy(enemy));
        }


        if(enemy.IsPlayerInRange())
        {
            enemy.stateMachine.ChangeState(new ChaseState_enemy(enemy));
        }

        if(enemy.IsPlayerInAttackRange())
        {
            enemy.stateMachine.ChangeState(new AttackState_enemy(enemy));
        }
    }

    public void Exit()
    {
        Debug.Log("Patrol 상태 종료");
    }

}
