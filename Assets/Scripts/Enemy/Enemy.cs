using UnityEngine;

public class Enmey : MonoBehaviour
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
    public void MoveTowards(float speed)
    {
        Vector3 dir = (player.position - transform.position).normalized;
        transform.position += dir * speed * Time.deltaTime;
    }

    public void Attack()
    {

    }
}