using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(Collider2D))]
public class Pit : MonoBehaviour
{
    [Header("플레이어 태그")]
    public string playerTag = "Player";

    [Header("이펙트 프리팹")]
    public GameObject effectPrefab;
    public float effectLifeTime = 1f;

    [Header("피해량")]
    public int damageAmount = 10;

    [Header("안전 타일맵")]
    [Tooltip("안전 지점이 될 타일이 깔린 Tilemap")]
    public Tilemap safeTilemap;

    private List<Vector3> _safeWorldPositions;

    void Awake()
    {
        SafePositions();
    }

    void SafePositions()
    {
        _safeWorldPositions = new List<Vector3>();
        if (safeTilemap == null) return;

        var bounds = safeTilemap.cellBounds;
        foreach (var pos in bounds.allPositionsWithin)
        {
            if (safeTilemap.HasTile(pos))
            {
                Vector3 wp = safeTilemap.GetCellCenterWorld(pos);
                _safeWorldPositions.Add(wp);
            }
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (!other.CompareTag(playerTag)) return;

        //플레이어 점프 상태인지 검사 필요

        var pitBounds = GetComponent<Collider2D>().bounds;
        var playerBounds = other.bounds;
        if (pitBounds.Contains(playerBounds.min) && pitBounds.Contains(playerBounds.max))
        {
            Vector3 fallPoint = other.transform.position;
            StartCoroutine(HandlePit(other.gameObject, fallPoint));
        }
    }

    private IEnumerator HandlePit(GameObject playerGO, Vector3 fallPoint)
    {
        if (effectPrefab != null)
        {
            var fx = Instantiate(effectPrefab, playerGO.transform.position, Quaternion.identity);
            Destroy(fx, effectLifeTime);
        }

        //플레이어의 모델링만 없어지게 수정 필요
        playerGO.SetActive(false);

        yield return new WaitForSeconds(effectLifeTime);

        if (_safeWorldPositions != null && _safeWorldPositions.Count > 0)
        {
            Vector3 best = _safeWorldPositions[0];
            float bestDist = Vector3.SqrMagnitude(fallPoint - best);
            for (int i = 1; i < _safeWorldPositions.Count; i++)
            {
                float d = Vector3.SqrMagnitude(fallPoint - _safeWorldPositions[i]);
                if (d < bestDist)
                {
                    bestDist = d;
                    best = _safeWorldPositions[i];
                }
            }
            playerGO.transform.position = best;
        }

        //여기에 데미지 주는 코드 추가 필요

        //플레이어의 모델링만 없어지게 수정 필요
        playerGO.SetActive(true);
    }
}
