using System.Transactions;
using UnityEngine;

// 탄환의 움직임, 충돌 처리 및 생존 시간 관리를 담당
public class Bullet : MonoBehaviour
{
    Enemy enemy;

    private uint damage;            // 총알 대미지
    private float speed;            // 총알 속도
    private float range;            // 총알 사거리
    private ushort pierceCount;     // 관통 가능 수
    private Vector2 direction;      // 총알 방향
    private string targetTag;       // 총알의 타겟

    private float traveledDistance = 0f;

    // RangeAttack에서 발사 시 넘겨주는 초기값 설정
    public void Initialize(uint damage, float speed, float range, ushort pierceCount, Vector2 direction, string targetTag)
    {
        this.damage = damage;
        this.speed = speed;
        this.range = range;
        this.pierceCount = pierceCount;
        this.direction = direction.normalized;
        this.targetTag = targetTag;
    }

    private void Update()
    {
        float distance = speed * Time.deltaTime;
        transform.Translate(direction * distance, Space.World);
        traveledDistance += distance;

        // 사거리 초과 시, 관통 가능수 0일 시 파괴
        if (traveledDistance >= range || pierceCount == 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        enemy = other.GetComponentInParent<Enemy>();

        if(other.CompareTag("Enemy"))
        {
            enemy.TakeDamage(damage);
            pierceCount--;
        }

        else if (other.CompareTag("Wall"))
        {
            Destroy(gameObject);
            Debug.Log("벽(트리거)에 닿아 파괴");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            Destroy(gameObject);
            Debug.Log("벽(콜리전)에 닿아 파괴");
        }
    }
}