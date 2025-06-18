using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

// 보스 패턴 모음

// 추격 =================================================================================================================================
public class PatternChase : IBossPattern
{
    public void Execute(BossController boss)
    {
        boss.transform.position = Vector3.MoveTowards(boss.transform.position, boss.player.position, boss.moveSpeed * Time.deltaTime);
    }
}

// 폭발 =================================================================================================================================
public class PatternExplosion : IBossPattern
{
    float spawnInterval = 1f; // 장판 생성 주기
    private float elapsed = 0f; // 생성 주기 맞추는 타이머
    public void Execute(BossController boss)
    {
        elapsed += Time.deltaTime;

        if(elapsed >= spawnInterval)
        {
            elapsed = 0f;
            Vector3 spawnPos = boss.player.position;
            boss.StartCoroutine(ExplosionRoutine(boss, spawnPos));
        }
    }

    private IEnumerator ExplosionRoutine(BossController boss, Vector3 position)
    {
        GameObject aoe = GameObject.Instantiate(boss.explosionPrefab, position, Quaternion.identity); // 폭발 프리팹 생성
        
        yield return new WaitForSeconds(1.5f); // 1.5초 후 폭발처리

        Collider2D[] hits = Physics2D.OverlapCircleAll(position, 1.5f); // 폭발 범위
        foreach (var hit in hits)
        {
            if (hit.CompareTag("Player"))
                hit.GetComponent<PlayerController>().stats.ReduceHp(boss.damage);
        }
        GameObject.Destroy(aoe);
    }
}

// 레이저 =================================================================================================================================
public class PatternLaser : IBossPattern
{
    private float interval = 2f; // 광선 생성 주기
    private float elapsed = 0f; // 생성 주기 맞추는 타이머
    private int laserCount = 10; // 한 번에 떨어질 광선 개수

    public void Execute(BossController boss)
    {
        elapsed += Time.deltaTime;

        if (elapsed >= interval)
        {
            elapsed = 0f;
            
            for (int i = 0; i < laserCount; i++)
            {
                Vector3 targetPos = boss.player.position;
                targetPos.x = Random.Range(targetPos.x - 15, targetPos.x +15); // x 값은 랜덤
                targetPos.y = targetPos.y + 6; // 플레이어보다 위쪽에서 떨어지도록 조정

                boss.StartCoroutine(LaserRoutine(boss, targetPos));
            }
        }

    }

    private IEnumerator LaserRoutine(BossController boss, Vector3 spawnPosition)
    {
        // 1. 경고 표시
        GameObject warning = GameObject.Instantiate(boss.laserWarningPrefab, spawnPosition, Quaternion.identity); // Warning 생성
        yield return new WaitForSeconds(0.5f); // 0.5초 뒤
        GameObject.Destroy(warning); // 파괴

        // 2. 광선 발사
        GameObject beam = GameObject.Instantiate(boss.laserBeamPrefab, spawnPosition, Quaternion.identity); // 광선(Beam) 생성
        yield return new WaitForSeconds(0.1f); // 0.1초 뒤

        
        BoxCollider2D box = beam.GetComponent<BoxCollider2D>();

        Collider2D[] hits = Physics2D.OverlapBoxAll(box.bounds.center, box.bounds.size, 0f); // 충돌 체크
        foreach (var hit in hits)
        {
            if (hit.CompareTag("Player"))
            {
                hit.GetComponent<PlayerController>().stats.ReduceHp(boss.damage);
            }
        }

        yield return new WaitForSeconds(0.5f);
        GameObject.Destroy(beam);
    }
}

// 죽음 =================================================================================================================================
public class PatternDie : IBossPattern
{
    private float delay = 1f;
    public void Execute(BossController boss)
    {
        boss.animator.SetBool("Destroy", true);
        boss.StartCoroutine(Die(boss, delay));
    }

    private IEnumerator Die(BossController boss, float delay)
{
    yield return new WaitForSeconds(delay);
    boss.gameObject.SetActive(false);
}
}
