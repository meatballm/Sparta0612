using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class BossAttackZone : MonoBehaviour
{
    public float delay = 3f; 
    public float damage = 30f;
    public GameObject explosionEffectPrefab;
    private int times; // 반복 횟수

    private void Start()
    {
        Invoke(nameof(Explode), delay);
    }

    private void Explode()
    {
        // 이펙트 생성
        if (explosionEffectPrefab)
        {
            Instantiate(explosionEffectPrefab, transform.position, Quaternion.identity);
        }

        // 플레이어가 범위 안에 있으면 데미지
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, 1.5f);
        foreach (var hit in hits)
        {
            if (hit.CompareTag("Player"))
            {
                // 플레이어에 데미지 주기
                hit.GetComponent<PlayerController>().stats.ReduceHp(damage);
            }
        }
        Destroy(gameObject); // 장판 삭제
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 1.5f);
    }
}
