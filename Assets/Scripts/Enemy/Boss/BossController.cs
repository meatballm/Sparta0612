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
    public float moveSpeed = 2f;
    public int damage = 10;
    private float maxHp = 500;
    public float currentHp { get; private set;}

    [Header("공격패턴 프리팹")]
    public GameObject explosionPrefab;
    public GameObject laserWarningPrefab;
    public GameObject laserBeamPrefab;

    private IBossPattern currentPattern;
    public Animator animator;

    void Start()
    {
        currentHp = maxHp;
        Debug.Log($"보스 체력 : {currentHp}");
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

        if (ratio >= 0.8f)
            currentPattern = new PatternChase();
        else if (ratio >= 0.7f)
            currentPattern = new PatternExplosion();
        else if (ratio >= 0.5f)
            currentPattern = new PatternLaser();

        // else if (ratio >= 0.2f)
        //     currentPattern = new PatternShield();
        else if (ratio <= 0f)
        {
            currentPattern = new PatternDie();
            GameObject.Find("Item_fish").transform.position = new Vector3(-17, -40, 0);
            return;
        }
        //     // currentPattern = new PatternSummon(); // 나중에 구현
    }
}

