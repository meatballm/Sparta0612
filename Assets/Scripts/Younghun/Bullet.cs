using UnityEngine;

// 탄환의 움직임, 충돌 처리 및 생존 시간 관리를 담당
public class Bullet : MonoBehaviour
{
    private uint damage;
    private float speed;
    private float range;
    private ushort pierceCount;
    private string targetTag;
    private Vector2 direction;

    private float traveledDistance = 0f;

    // Weapon에서 발사 시 넘겨주는 초기값 설정
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

        // 사거리 초과 시 파괴
        if (traveledDistance >= range)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag(targetTag)) return;

        // 타겟에게 데미지 주기 (이 예시에서는 Debug로 대체)
        Debug.Log($"Hit {other.name} for {damage} damage.");

        if (pierceCount > 0)
        {
            pierceCount--;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
