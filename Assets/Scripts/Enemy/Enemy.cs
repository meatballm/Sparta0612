using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform player;
    public float chaseRange = 5f;
    public float attackRange = 1.5f;

    public StateMachine_enemy stateMachine {get; private set;}

    private void Start()
    {
        stateMachine = new StateMachine_enemy();
        stateMachine.ChangeState(new IdleState_enemy(this));
    }

    private void Update()
    {
        stateMachine.Update();
    }

    public bool IsPlayerInRange()
    {
        return Vector3.Distance(transform.position, player.position) < chaseRange;
    }

    public bool IsPlayerInAttackRange()
    {
        return Vector3.Distance(transform.position, player.position) < attackRange;
    }
    public void MoveTowards(Vector3 target ,float speed)
    {
        Vector3 dir = (target - transform.position).normalized;
        transform.position += dir * speed * Time.deltaTime;
    }

    public void Attack()
    {
        Debug.Log("플레이어를 공격함");
        //플레이어 공격
    }
}