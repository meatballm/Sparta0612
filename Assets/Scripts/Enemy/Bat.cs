using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat : Enemy
{
    float speed = 3f;
    public override void Attack()
    {
        Vector2 direction = (player.position - target.position).normalized;
        if ( 0 < direction.x )
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false;
        }
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
}
