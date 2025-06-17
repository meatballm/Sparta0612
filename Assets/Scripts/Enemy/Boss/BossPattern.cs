using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 보스 패턴 모음

// 추격
public class PatternChase : IBossPattern
{
    public void Execute(BossController boss)
    {
        boss.transform.position = Vector3.MoveTowards(boss.transform.position, boss.player.position, boss.moveSpeed * Time.deltaTime);
    }
}

// 폭발
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

// public class PatternLaser : IBossPattern
// {
//     private float interval = 2f;
//     private float elapsed = 0f;

//     public void Execute(BossController boss)
//     {
//         elapsed += Time.deltaTime;

//         if (elapsed >= interval)
//         {
//             elapsed = 0f;

//             Vector3 targetPos = boss.player.position;
//             targetPos.y = 6f; // 위쪽에서 떨어지도록 조정

//             boss.StartCoroutine(LaserRoutine(boss, targetPos));
//         }

//     }
// }
