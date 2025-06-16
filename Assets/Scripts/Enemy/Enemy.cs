using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform player;
    public SpriteRenderer spriteRenderer;
    public Transform target;
    protected float enemyHP;
    protected float chaseRange;
    protected float attackRange;
    protected float speed;
    protected float damage;
    protected float defense;
    

    public StateMachine_enemy stateMachine {get; private set;}

    private void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        stateMachine = new StateMachine_enemy();
        stateMachine.ChangeState(new IdleState_enemy(this));
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
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
    public virtual void MoveTowards()
    {
    }

    public virtual void Attack()
    {
    }
    
    public void AttackDamag()
    {
        //player.hp - damage;
        //데미지를 입었을 때
    }
}