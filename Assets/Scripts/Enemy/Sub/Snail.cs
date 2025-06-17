using UnityEngine;

// 가까이 가면 껍질에 숨어버리는 달팽이 몬스터
public class Snail : Enemy 
{
    Animator animator;

    void Awake()
    {
        maxHP = 100f;
        enemyHP = maxHP;
        chaseRange = 5f;
        attackRange = 3f;
        speed = 0.2f;
        damage = 0f;
        defense = 10f;
        animator = GetComponentInChildren<Animator>();
    }

    // 공격x 껍질에 숨음. 방어력 높아짐.
    public override void Attack()
    {
        HideShell();
        Invoke("OutOfShell", 2f);
    }


    // 플레이어와 반대방향으로 이동
    public override void MoveTowards()
    {
        Vector3 dir = (player.position - transform.position).normalized;
        transform.position += -dir * speed * Time.deltaTime;

        // 움직이는 방향에 따라 sprite 반전.
        if ( 0 < -dir.x )
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false;
        }

    }
    
    // 껍질에 숨음
    private void HideShell()
    {
        animator.SetBool("Hide", true);
        defense = 30f;
    }

    // 2초 뒤 껍질에서 나옴
    private void OutOfShell()
    {
        animator.SetBool("Hide", false);
        defense = 10f;
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerStat.ReduceHp(damage);
        }
    }
}
