using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 보스 패턴 인터페이스
public interface IBossPattern
{
    void Execute(BossController boss);
}

public class BossController : MonoBehaviour
{
    public Transform player;
    public GameObject explosionPrefab;
    public float moveSpeed = 2f;
    public int damage = 10;
    private float maxHp = 200;
    public float currentHp { get; private set;}
    private IBossPattern currentPattern;
    Animator animator;

    void Start()
    {
        currentHp = maxHp;
        UpdatePattern();

        player = GameObject.FindWithTag("Player").transform;
        animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        UpdatePattern(); // 체력 기준으로 패턴 갱신
        currentPattern?.Execute(this);
    }

    public void TakeDamage(float dmg)
    {
        currentHp -= dmg;
        Debug.Log($"보스체력{currentHp}");
        if (currentHp < 0) currentHp = 0;
    }

    private void UpdatePattern()
    {
        float ratio = currentHp / maxHp;

        if (ratio >= 0.7f)
            currentPattern = new PatternLaser();
        else if (ratio >= 0.4f)
            currentPattern = new PatternExplosion();
        // else
        //     Debug.Log("보스 곧 죽음");
        //     // currentPattern = new PatternSummon(); // 나중에 구현
    }
}

