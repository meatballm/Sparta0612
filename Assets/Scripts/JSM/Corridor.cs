using System.Collections;
using System.Threading;
using UnityEngine;

[RequireComponent(typeof(Collider2D), typeof(SpriteRenderer))]
public class CorridorTrigger2D : MonoBehaviour
{
    [Header("플레이어 태그")]
    [Tooltip("충돌 대상을 구분할 태그")]
    public string playerTag = "Player";

    [Header("충돌 시 활성화할 방벽")]
    public GameObject barrier;

    [Header("방벽 올라갈지 아닌지 여부")]
    public bool barrierUp = true;

    [Header("페이드 아웃 시간 (초)")]
    public float fadeDuration = 2f;

    [System.Serializable]
    public class WaveData
    {
        public int[] monsterIndices; // 사용할 몬스터 종류 (BattleManager 프리팹 인덱스)
        public int count;
    }

    [Header("웨이브 데이터")]
    public WaveData[] waves;

    private int currentWave = 0;
    private int aliveCount = 0;
    bool triggered = false;
    SpriteRenderer sr;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        GetComponent<Collider2D>().isTrigger = true;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (triggered) return;
        if (!other.CompareTag(playerTag)) return;
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
            yield break;
        }

        WaveData wave = waves[currentWave];
        aliveCount = wave.count;

        var bounds = GetComponent<Collider2D>().bounds;

        for (int i = 0; i < wave.count; i++)
        {
            int index = wave.monsterIndices[Random.Range(0, wave.monsterIndices.Length)];

            float x = Random.Range(bounds.min.x, bounds.max.x);
            float y = Random.Range(bounds.min.y, bounds.max.y);
            Vector3 spawnPos = new Vector3(x, y, 0f);

            // 기존 직접 생성 → 제거
            // GameObject m = BattleManager.Instance.SpawnMonster(index, spawnPos);
            // var enemy = m.GetComponent<Enemy>();
            // if (enemy != null) enemy.Init(this);

            // ✅ 새로 추가: 1초 딜레이 후 소환
            StartCoroutine(DelayedSpawn(index, spawnPos));
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
        col.a = 0.3f; // 투명도 설정
        previewSR.color = col;

        // 1초 기다리기
        yield return new WaitForSeconds(1f);

        Destroy(preview); // 실루엣 삭제

        // 진짜 몬스터 소환
        GameObject monster = BattleManager.Instance.SpawnMonster(monsterIndex, spawnPos);
        var enemy = monster.GetComponent<Enemy>();
        //if (enemy != null) enemy.Init(this);
    }


    public void OnMonsterKilled()
    {
        aliveCount--;
        if (aliveCount <= 0)
        {
            currentWave++;
            StartCoroutine(SpawnWave());
        }
    }
}
