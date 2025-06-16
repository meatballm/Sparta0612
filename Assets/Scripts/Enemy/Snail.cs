using UnityEngine;

public class Snail : Enemy
{
    Animator animator;
    void Awake()
    {
        chaseRange = 5f;
        attackRange = 3f;
        speed = 0.2f;
        defense = 10f;
        animator = GetComponentInChildren<Animator>();
    }

    public override void Attack()
    {
        animator.SetBool("Hide", true);
        defense = 30f;
        Invoke("OutofShell", 2);
    }

    public override void MoveTowards()
    {
        Vector3 dir = (player.position - transform.position).normalized;
        transform.position += dir * speed * Time.deltaTime;

        // 움직이는 방향에 따라 sprite 반전.
        if ( 0 < dir.x )
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false;
        }

    }

    private void OutOfShell()
    {
        animator.SetBool("Hide", false);
        
    }
}
