using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform player;
    public SpriteRenderer spriteRenderer;
    [SerializeField] protected float enemyHP;
    protected float chaseRange;
    protected float attackRange;
    protected float speed;
    protected float damage;
    protected float defense;
    protected BoxCollider2D boxCollider;
    protected PlayerStat playerStat;
    public StateMachine_enemy stateMachine {get; private set;}

    private void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        playerStat = GameObject.FindWithTag("Player").GetComponent<PlayerController>().stats;
        stateMachine = new StateMachine_enemy();
        stateMachine.ChangeState(new IdleState_enemy(this));
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        boxCollider = GetComponentInChildren<BoxCollider2D>();
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
            StartCoroutine(Die());
        }
    }

    // 죽음
    protected IEnumerator Die()
    {
        float duration = 1.2f; // Enemy가 사라지는데 걸리는 시간
        float time = 0f;

        Color originalColor = spriteRenderer.color;
        boxCollider.enabled = false;

        while (time < duration)
        {   
            float alpha = Mathf.Lerp(1f, 0f, time / duration); // 점점 투명해지게
            spriteRenderer.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);

            time += Time.deltaTime;
            yield return null;
        }

        // 마지막에 완전히 투명하게
        spriteRenderer.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0f);
        Destroy(gameObject);
    }

}