using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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

    // 공격패턴
    private IBossPattern patternChase;
    private IBossPattern patternExplosion;
    private IBossPattern patternLaser;
    private IBossPattern patternDie;

    private List<IBossPattern> currentPattern = new List<IBossPattern>();
    public Animator animator;
    PlayerStat playerStat;

    void Start()
    {
        currentHp = maxHp;
        Debug.Log($"보스 체력 : {currentHp}");
        
        player = GameObject.FindWithTag("Player").transform;
        animator = GetComponentInChildren<Animator>();
        playerStat =  GameObject.FindWithTag("Player").GetComponent<PlayerController>().stats;

        patternChase = new PatternChase();
        patternExplosion = new PatternExplosion();
        patternLaser = new PatternLaser();
        patternDie = new PatternDie();

        UpdatePattern();
    }

    void Update()
    {
        UpdatePattern(); // 체력 기준으로 패턴 갱신

        Debug.Log("CurrentPattern Count: " + currentPattern.Count);
        foreach ( var pattern in currentPattern)
        {
            pattern.Execute(this);
        }
    }

    public void TakeDamage(float dmg)
    {
        currentHp -= dmg;
        Debug.Log($"보스체력{currentHp}");
        if (currentHp < 0) currentHp = 0;
    }

    private void UpdatePattern()
    {
        currentPattern.Clear();

        float ratio = currentHp / maxHp;

        if (ratio >= 0.8f)
            currentPattern.Add(patternChase);
        else if (ratio >= 0.7f)
            currentPattern.Add(patternExplosion);
        else if (ratio >= 0.5f)
            currentPattern.Add(patternLaser);
        // else if (ratio >= 0.2f) // 체력 20% 남았을 때 폭주모드. 모든 공격패턴 일어남.
        // {
        //     currentPattern.Add(patternChase);
        //     currentPattern.Add(patternExplosion);
        //     currentPattern.Add(patternLaser);
        // } 

        else if (ratio <= 0f)
        {
            currentPattern.Add(patternDie);
            GameObject.Find("Item_fish").transform.position = new Vector3(-17, -40, 0);
            return;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerStat.ReduceHp(damage);
        }

    }
}

