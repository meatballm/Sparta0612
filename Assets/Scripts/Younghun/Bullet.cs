using System.Collections.Generic;
using System.Transactions;
using UnityEngine;

public enum TargetTag
{
    Player,
    Enemy,
    Obstacle
}
// 탄환 쏘는 주체도 만들까 생각

// 탄환의 움직임, 충돌 처리 및 생존 시간 관리를 담당
public class Bullet : MonoBehaviour
{
    private string targetTag;
    Enemy enemy;

    private uint damage;
    private float speed;
    private float range;
    private ushort pierceCount;
    private Vector2 direction;
    private float traveled;

    private float traveledDistance = 0f;

    // RangeAttack에서 발사 시 넘겨주는 초기값 설정
    public void Initialize(
    uint damage,
    float speed,
    float range,
    ushort pierceCount,
    Vector2 direction,
    string targetTag)
    {
        this.damage = damage;
        this.speed = speed;
        this.range = range;
        this.pierceCount = pierceCount;
        this.direction = direction.normalized;
        this.targetTag = targetTag;
        this.traveled = 0f;
    }

    private void Update()
    {
        float distance = speed * Time.deltaTime;
        transform.Translate(direction * distance, Space.World);
        traveledDistance += distance;
        traveled += distance;
        // 사거리 초과 시 파괴
        if (traveled >= range) Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        enemy = other.GetComponentInParent<Enemy>();
        BossController boss = other.GetComponent<BossController>();

        if(other.CompareTag("Enemy"))
        {
            enemy.TakeDamage(damage);
            Destroy(gameObject);
        }
        else if (other.CompareTag("Boss"))
        {
            boss.TakeDamage(damage);
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
        // if (other.CompareTag(targetTag.ToString()))
        // {

        //     if (targetTag == TargetTag.Enemy)
        //     {
        //         enemy.TakeDamage(20);
        //         Destroy(gameObject);
        //     }

        //     else if (targetTag == TargetTag.Obstacle)
        //     {
        //         Destroy(gameObject);
        //     }
        // }
    // }
}