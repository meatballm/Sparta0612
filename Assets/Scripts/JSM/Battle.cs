using System.Collections;
using System.Threading;
using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(Collider2D), typeof(SpriteRenderer))]
public class Battle : MonoBehaviour
{
    [Header("충돌 시 활성화할 방벽")]
    public GameObject barrier;

    [Header("방벽 올라갈지 아닌지 여부")]
    public bool barrierUp = true;

    [Header("페이드 아웃 시간 (초)")]
    public float fadeDuration = 2f;

    [System.Serializable]
    public class WaveEntry
    {
        public int monsterIndex;  // BattleManager에 등록된 몬스터 프리팹 인덱스
        public int count;         // 해당 몬스터 개수
    }

    [System.Serializable]
    public class WaveData
    {
        public WaveEntry[] entries;  // 여러 종류 몬스터를 하나의 웨이브에 담음
    }
    [Header("충돌 타일맵")]
    public Tilemap colliderTilemap;

    [Header("웨이브 데이터")]
    public WaveData[] waves;

    private int currentWave = 0;
    private int aliveCount = 0;
    private bool triggered = false;
    private SpriteRenderer sr;

    [Header("스폰 범위 (선택)")]
    [Tooltip("비워두면 트리거 콜라이더가 기준이 됨")]
    public Collider2D[] customSpawnAreas;
    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        GetComponent<Collider2D>().isTrigger = true;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (triggered) return;
        if (!other.CompareTag("Player")) return;
        triggered = true;

        if (barrier != null && barrierUp)
            barrier.SetActive(true);

        StartCoroutine(SpawnWave());
        StartCoroutine(FadeOut());
    }

    IEnumerator FadeOut()
    {
        Color col = sr.color;
        float startA = col.a;
        float t = 0f;

        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            col.a = Mathf.Lerp(startA, 0f, t / fadeDuration);
            sr.color = col;
            yield return null;
        }

        col.a = 0f;
        sr.color = col;
    }
    private IEnumerator SpawnWave()
    {
        if (currentWave >= waves.Length)
        {
            Debug.Log("전투 종료");
            barrier.SetActive(false);

            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                BattleManager.Instance.SpawnReward(player.transform.position);
            }


            yield break;
        }

        WaveData wave = waves[currentWave];
        aliveCount = 0;
        foreach (var entry in wave.entries)
        {
            aliveCount += entry.count;
        }

        foreach (var entry in wave.entries)
        {
            for (int i = 0; i < entry.count; i++)
            {
                Bounds bounds;
                if (customSpawnAreas != null && customSpawnAreas.Length > 0)
                {
                    var selected = customSpawnAreas[Random.Range(0, customSpawnAreas.Length)];
                    bounds = selected.bounds;
                }
                else
                {
                    bounds = GetComponent<Collider2D>().bounds;
                }

                Vector3 spawnPos = GetValidSpawnPosition(bounds);
                StartCoroutine(DelayedSpawn(entry.monsterIndex, spawnPos));
            }
        }

        Debug.Log($"Wave {currentWave + 1} 준비됨");
    }

    private IEnumerator DelayedSpawn(int monsterIndex, Vector3 spawnPos)
    {
        // 몬스터 프리팹에서 SpriteRenderer 가져오기
        SpriteRenderer realSR = BattleManager.Instance.monsterPrefabs[monsterIndex].GetComponentInChildren<SpriteRenderer>();
        if (realSR == null)
        {
            Debug.LogError("몬스터 프리팹에 SpriteRenderer 없음");
            yield break;
        }

        // 실루엣용 GameObject 생성
        GameObject preview = new GameObject("MonsterPreview");
        preview.transform.position = spawnPos;

        SpriteRenderer previewSR = preview.AddComponent<SpriteRenderer>();
        previewSR.sprite = realSR.sprite;
        previewSR.sortingLayerID = realSR.sortingLayerID;
        previewSR.sortingOrder = realSR.sortingOrder;

        Color col = previewSR.color;
        col.a = 0.3f;
        previewSR.color = col;

        yield return new WaitForSeconds(1f);

        Destroy(preview);

        GameObject monster = BattleManager.Instance.SpawnMonster(monsterIndex, spawnPos);
        var enemy = monster.GetComponent<Enemy>();
        if (enemy != null) enemy.Init(this);
    }


    public void OnMonsterKilled()
    {
        aliveCount--;
        Debug.Log(aliveCount);
        if (aliveCount <= 0)
        {
            currentWave++;
            StartCoroutine(SpawnWave());
        }
    }
    private Vector3 GetValidSpawnPosition(Bounds bounds)
    {
        const int maxAttempts = 10;
        for (int i = 0; i < maxAttempts; i++)
        {
            float x = Random.Range(bounds.min.x, bounds.max.x);
            float y = Random.Range(bounds.min.y, bounds.max.y);
            Vector3 spawnPos = new Vector3(x, y, 0f);

            Vector3Int cellPos = colliderTilemap.WorldToCell(spawnPos);
            if (!colliderTilemap.HasTile(cellPos))
            {
                return spawnPos;
            }
        }

        Debug.LogWarning("유효한 스폰 위치를 찾지 못했습니다. 마지막 시도 위치를 반환합니다.");
        return bounds.center;
    }

}
