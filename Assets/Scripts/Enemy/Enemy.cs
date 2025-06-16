using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform player;
    public SpriteRenderer spriteRenderer;
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

    // 플레이어 감지 범위
    public bool IsPlayerInRange()
    {
        return Vector3.Distance(transform.position, player.position) < chaseRange;
    }

    // 플레이어 공격 범위
    public bool IsPlayerInAttackRange()
    {
        return Vector3.Distance(transform.position, player.position) < attackRange;
    }

    // 움직이는 방향 지정
    public virtual void MoveTowards()
    {
    }

    // 공격
    public virtual void Attack()
    {
    }
    
    // 공격시 데미지
    public void AttackDamag()
    {
        //player.hp - damage;
        //데미지를 입었을 때
    }

    // 피격
    public void TakeDamage(float amount)
    {
        enemyHP -= amount;
        
        if(enemyHP <= 0)
        {
            Die();
        }
    }

    // 죽음
    protected void Die()
    {
        Destroy(gameObject, 2f);
    }
}